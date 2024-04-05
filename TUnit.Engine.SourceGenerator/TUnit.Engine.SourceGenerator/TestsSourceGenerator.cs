using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using TUnit.Engine.SourceGenerator.Models;

namespace TUnit.Engine.SourceGenerator;

/// <summary>
/// A sample source generator that creates C# classes based on the text file (in this case, Domain Driven Design ubiquitous language registry).
/// When using a simple text file as a baseline, we can create a non-incremental source generator.
/// </summary>
[Generator]
public class TestsSourceGenerator : IIncrementalGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this generator.
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var testMethods = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), 
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null)
            .Collect();
            
        context.RegisterSourceOutput(testMethods, Execute);
    }
    
    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is MethodDeclarationSyntax { AttributeLists.Count: > 0 } methodDeclarationSyntax;
    }

    static Method? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        if (context.Node is not MethodDeclarationSyntax methodDeclarationSyntax)
        {
            return null;
        }
        
        var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node);
        
        if (symbol is not IMethodSymbol methodSymbol)
        {
            return null;
        }

        var attributes = methodSymbol.GetAttributes();

        if (!attributes.Any(x =>
                x.AttributeClass?.BaseType?.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix)
                == WellKnownFullyQualifiedClassNames.BaseTestAttribute))
        {
            return null;
        }

        return new Method(methodDeclarationSyntax, methodSymbol);
    }
    
    private static void Execute(SourceProductionContext context, ImmutableArray<Method?> methods)
    {
        foreach (var method in methods.OfType<Method>())
        {
            var classSource = ProcessTests(method);
                
            if (string.IsNullOrEmpty(classSource))
            {
                continue;
            }

            var className = $"{method.MethodSymbol.Name}_{Guid.NewGuid():N}";
            context.AddSource($"{className}.g.cs", SourceText.From(WrapInClass(className, classSource), Encoding.UTF8));
        }
    }

    private static string WrapInClass(string className, string methodCode)
    {
        return $$"""
               // <auto-generated/>
               using System.Linq;
               using System.Runtime.CompilerServices;

               namespace TUnit.Engine;

               file class {{className}}
               {
                   [ModuleInitializer]
                   public static void Initialise()
                   {
                        {{methodCode}}
                   }
               } 
               """;
    }

    private static string ProcessTests(Method method)
    {
        var methodSymbol = method.MethodSymbol;
        
        if (methodSymbol.ContainingType.IsAbstract)
        {
            return string.Empty;
        }

        var sourceBuilder = new StringBuilder();
        
        AttributeData[] attributes =
        [
            ..methodSymbol.GetAttributes(),
            ..methodSymbol.ContainingType.GetAttributes()
        ];
        
        var repeatCount = attributes
            .FirstOrDefault(x =>
                x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix) ==
                WellKnownFullyQualifiedClassNames.RepeatAttribute)
            ?.ConstructorArguments
            .FirstOrDefault()
            .Value as int? ?? 1;

        for (var i = 1; i <= repeatCount; i++)
        {
            foreach (var attributeData in attributes)
            {
                switch (attributeData.AttributeClass?.ToDisplayString(DisplayFormats
                            .FullyQualifiedNonGenericWithGlobalPrefix))
                {
                    case "global::TUnit.Core.TestAttribute":
                        foreach (var classInvocation in GenerateClassInvocations(methodSymbol.ContainingType))
                        {
                            sourceBuilder.AppendLine(
                                GenerateTestInvocationCode(methodSymbol, classInvocation, [], i)
                            );
                        }

                        break;
                    case "global::TUnit.Core.DataDrivenTestAttribute":
                        break;
                    case "global::TUnit.Core.DataSourceDrivenTestAttribute":
                        break;
                    case "global::TUnit.Core.CombinativeTestAttribute":
                        break;
                }
            }
        }

        return sourceBuilder.ToString();
    }

    private static string GenerateTestInvocationCode(
        IMethodSymbol methodSymbol, 
        ClassInvocationString classInvocationString,
        IEnumerable<string> methodArguments,
        int currentCount)
    {
        var testId = GetTestId(methodSymbol, classInvocationString, methodArguments, currentCount);

        var classType = methodSymbol.ContainingType;
        
        var fullyQualifiedClassType = classType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix);
        return $$"""
                    global::TUnit.Core.TestDictionary.AddTest("{{testId}}", () => 
                    {
                 {{classInvocationString.ClassInvocation}};
             
                        var methodInfo = global::TUnit.Core.Helpers.MethodHelpers.GetMethodInfo(classInstance.{{methodSymbol.Name}});
             
                        var testInformation = new global::TUnit.Core.TestInformation()
                        {
                            Categories = [{{string.Join(", ", GetCategories(methodSymbol))}}],
                            ClassInstance = classInstance,
                            ClassType = typeof({{fullyQualifiedClassType}}),
                            Timeout = {{GetTimeOut(methodSymbol)}},
                            TestClassArguments = classArgs,
                            TestMethodArguments = [{{string.Join(", ", methodArguments)}}],
                            TestClassParameterTypes = typeof({{fullyQualifiedClassType}}).GetConstructors().First().GetParameters().Select(x => x.ParameterType).ToArray(),
                            TestMethodParameterTypes = methodInfo.GetParameters().Select(x => x.ParameterType).ToArray(),
                            NotInParallelConstraintKeys = {{GetNotInParallelConstraintKeys(methodSymbol)}},
                            RepeatCount = {{GetRepeatCount(methodSymbol)}},
                            RetryCount = {{GetRetryCount(methodSymbol)}},
                            MethodInfo = methodInfo,
                            TestName = "{{methodSymbol.Name}}",
                            CustomProperties = new global::System.Collections.Generic.Dictionary<string, string>()
                        };
                        
                        var testContext = new global::TUnit.Core.TestContext(testInformation);
                        
                        return new global::TUnit.Core.UnInvokedTest
                        {
                            Id = "{{testId}}",
                            TestContext = testContext,
                            OneTimeSetUps = [{{OneTimeSetUpWriter.GenerateCode(classType)}}],
                            BeforeEachTestSetUps = [{{SetUpWriter.GenerateCode(classType)}}],
                            TestClass = classInstance,
                            TestBody = () => global::TUnit.Engine.RunHelpers.RunAsync(() => classInstance.{{GenerateTestMethodInvocation(methodSymbol)}}),
                            AfterEachTestCleanUps = [{{CleanUpWriter.GenerateCode(classType)}}],
                            OneTimeCleanUps = [{{OneTimeCleanUpWriter.GenerateCode(classType)}}],
                        };
                    });
                 """;
    }

    private static string GetNotInParallelConstraintKeys(IMethodSymbol methodSymbol)
    {
        var notInParallelAttributes = GetMethodAndClassAttributes(methodSymbol)
            .Where(x => x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                        == "global::TUnit.Core.NotInParallelAttribute")
            .ToList();

        if (!notInParallelAttributes.Any())
        {
            return "null";
        }
        
        var notInConstraintKeys = notInParallelAttributes
            .SelectMany(x => x.ConstructorArguments)
            .SelectMany(x => x.Value is null ? x.Values.Select(x => x.Value) : [x.Value])
            .Select(x => $"\"{x}\"");
        
        return $"[{string.Join(", ", notInConstraintKeys)}]";
    }

    private static int GetRepeatCount(IMethodSymbol methodSymbol)
    {
        return GetMethodAndClassAttributes(methodSymbol)
            .FirstOrDefault(x => x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                                 == "global::TUnit.Core.RepeatAttribute")
            ?.ConstructorArguments.First().Value as int? ?? 0;
    }

    private static int GetRetryCount(IMethodSymbol methodSymbol)
    {
        return GetMethodAndClassAttributes(methodSymbol)
            .FirstOrDefault(x => x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                                 == "global::TUnit.Core.RetryAttribute")
            ?.ConstructorArguments.First().Value as int? ?? 0;
    }

    private static string GetTimeOut(IMethodSymbol methodSymbol)
    {
        var timeoutAttribute = GetMethodAndClassAttributes(methodSymbol)
            .FirstOrDefault(x => x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                                 == "global::TUnit.Core.TimeoutAttribute");

        if (timeoutAttribute is null)
        {
            return "null";
        }

        var timeoutMillis = (int)timeoutAttribute.ConstructorArguments.First().Value!;
        
        return $"global::System.TimeSpan.FromMilliseconds({timeoutMillis})";
    }

    private static IEnumerable<string> GetCategories(IMethodSymbol methodSymbol)
    {
        return GetMethodAndClassAttributes(methodSymbol)
            .Where(x => x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                        == "global::TUnit.Core.TestCategoryAttribute")
            .Select(x => $"\"{x.ConstructorArguments.First().Value}\"");
    }

    private static IEnumerable<AttributeData> GetMethodAndClassAttributes(IMethodSymbol methodSymbol)
    {
        return [..methodSymbol.GetAttributes(), ..methodSymbol.ContainingType.GetAttributes()];
    }

    private static string GetTestId(IMethodSymbol methodSymbol, 
        ClassInvocationString classInvocationString,
        IEnumerable<string> methodArguments, 
        int count)
    {
        // Format must match TestDetails.GenerateUniqueId, but we can't share code
        // as we're inside a source generator
        var fullyQualifiedClassName =
            methodSymbol.ContainingType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithoutGlobalPrefix);

        var testName = methodSymbol.Name;
        
        var classParameters = methodSymbol.ContainingType.Constructors.First().Parameters;
        var classParameterTypes = GetTypes(classParameters);

        var methodParameterTypes = GetTypes(methodSymbol.Parameters);
        
        return $"{fullyQualifiedClassName}.{testName}.{classParameterTypes}.{classInvocationString.Arguments}.{methodParameterTypes}.{string.Join(",", methodArguments)}.{count}";
    }

    private IEnumerable<ArgumentString> GetMethodArguments(IMethodSymbol methodSymbol)
    {
        throw new System.NotImplementedException();
    }

    private static string GetTypes(ImmutableArray<IParameterSymbol> parameters)
    {
        if (parameters.IsDefaultOrEmpty)
        {
            return string.Empty;
        }

        var parameterTypesFullyQualified = parameters.Select(x => x.Type)
            .Select(x => x.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithoutGlobalPrefix));
        
        return string.Join(",", parameterTypesFullyQualified);
    }

    private static IEnumerable<ClassInvocationString> GenerateClassInvocations(INamedTypeSymbol namedTypeSymbol)
    {
        var className =
            namedTypeSymbol.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix);

        var args = GetClassArguments(namedTypeSymbol);
        
        foreach (var (arguments, argumentsCount) in args)
        {
            if (argumentsCount == ArgumentsCount.Zero)
            {
                yield return new ClassInvocationString($"""
                                                              object[] classArgs = [];
                                                              var classInstance = new {className}()
                                                        """, string.Empty);
            }
            if (argumentsCount == ArgumentsCount.One)
            {
                yield return new ClassInvocationString($"""
                                                        var arg = {arguments};
                                                        object[] classArgs = [arg];
                                                        var classInstance = new {className}(arg)
                                                        """, arguments);
            }
            if (argumentsCount == ArgumentsCount.Multiple)
            {
                yield return new ClassInvocationString($"""
                                                        object[] classArgs = [{arguments}];
                                                        var classInstance = new {className}({arguments})
                                                        """, arguments);
            }
        }
    }
    
    private static IEnumerable<ArgumentString> GetClassArguments(INamedTypeSymbol namedTypeSymbol)
    {
        var className =
            namedTypeSymbol.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix);
        
        if (namedTypeSymbol.InstanceConstructors.First().Parameters.IsDefaultOrEmpty)
        {
            yield return new ArgumentString(string.Empty, ArgumentsCount.Zero);
        }

        foreach (var dataSourceDrivenTestAttribute in namedTypeSymbol.GetAttributes().Where(x =>
                     x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                         is "global::TUnit.Core.MethodDataAttribute"))
        {
            var arg = dataSourceDrivenTestAttribute.ConstructorArguments.Length == 1
                ? $"{className}.{dataSourceDrivenTestAttribute.ConstructorArguments.First().Value}()"
                : $"{dataSourceDrivenTestAttribute.ConstructorArguments[0].Value}.{dataSourceDrivenTestAttribute.ConstructorArguments[1].Value}()";

            yield return new ArgumentString(arg, ArgumentsCount.One);
        }
        
        foreach (var classDataAttribute in namedTypeSymbol.GetAttributes().Where(x =>
                     x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                         is "global::TUnit.Core.ClassDataAttribute"))
        {
            yield return new ArgumentString($"new {classDataAttribute.ConstructorArguments.First().Value}()", ArgumentsCount.One);
        }
        
        foreach (var classDataAttribute in namedTypeSymbol.GetAttributes().Where(x =>
                     x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                         is "global::TUnit.Core.InjectAttribute"))
        {
            var genericType = classDataAttribute.AttributeClass!.TypeArguments.First();
            var fullyQualifiedGenericType =
                genericType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix);
            var sharedArgument = classDataAttribute.NamedArguments.First(x => x.Key == "Shared").Value;

            if (sharedArgument.Type?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                is "global::TUnit.Core.None")
            {
                yield return new ArgumentString(
                    $"new {genericType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix)}()",
                    ArgumentsCount.One);
            }
            
            if (sharedArgument.Type?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                is "global::TUnit.Core.Globally")
            {
                yield return new ArgumentString(
                    $"global::TUnit.Engine.TestDataContainer.InjectedSharedGlobally.GetOrAdd(typeof({fullyQualifiedGenericType}), x => new {fullyQualifiedGenericType}())",
                    ArgumentsCount.One);
            }
            
            if (sharedArgument.Type?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                is "global::TUnit.Core.ForClass")
            {
                yield return new ArgumentString(
                    $"global::TUnit.Engine.TestDataContainer.InjectedSharedPerClassType.GetOrAdd(new global::TUnit.Engine.Models.DictionaryTypeTypeKey(typeof({className}), typeof({fullyQualifiedGenericType})), x => new {fullyQualifiedGenericType}())",
                    ArgumentsCount.One
                );
            }
            
            if (sharedArgument.Type?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                is "global::TUnit.Core.ForKey")
            {
                var key = sharedArgument.Value?.GetType().GetProperty("Key")?.GetValue(sharedArgument.Value);
                yield return new ArgumentString(
                    $"global::TUnit.Engine.TestDataContainer.InjectedSharedPerKey.GetOrAdd(new global::TUnit.Engine.Models.DictionaryStringTypeKey(\"{key}\", typeof({fullyQualifiedGenericType})), x => new {fullyQualifiedGenericType}())",
                    ArgumentsCount.One);
            }
        }
    }

    private static string GenerateTestMethodInvocation(IMethodSymbol method, params string[] methodArguments)
    {
        var methodName = method.Name;

        var args = string.Join(", ", methodArguments);

        if (method.GetAttributes().Any(x =>
                x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                    is "global::TUnit.Core.TimeoutAttribute"))
        {
            // TODO : We don't want Engine cancellation token? We want a new linked one that'll cancel after the specified timeout in the attribute
            if(string.IsNullOrEmpty(args))
            {
                return $"{methodName}(EngineCancellationToken.Token)";
            }

            return $"{methodName}({args}, EngineCancellationToken.Token)";
        }
        
        return $"{methodName}({args})";
    }
}

public record Method
{
    public MethodDeclarationSyntax MethodDeclarationSyntax { get; }
    public IMethodSymbol MethodSymbol { get; }

    public Method(MethodDeclarationSyntax methodDeclarationSyntax, IMethodSymbol methodSymbol)
    {
        MethodDeclarationSyntax = methodDeclarationSyntax;
        MethodSymbol = methodSymbol;
    }
}