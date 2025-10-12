![](assets/banner.png)

# 🚀 The Modern Testing Framework for .NET

**TUnit** is a next-generation testing framework for C# that outpaces traditional frameworks with **source-generated tests**, **parallel execution by default**, and **Native AOT support**. Built on the modern Microsoft.Testing.Platform, TUnit delivers faster test runs, better developer experience, and unmatched flexibility.

<div align="center">

[![thomhurst%2FTUnit | Trendshift](https://trendshift.io/api/badge/repositories/11781)](https://trendshift.io/repositories/11781)


[![Codacy Badge](https://api.codacy.com/project/badge/Grade/a8231644d844435eb9fd15110ea771d8)](https://app.codacy.com/gh/thomhurst/TUnit?utm_source=github.com&utm_medium=referral&utm_content=thomhurst/TUnit&utm_campaign=Badge_Grade)![GitHub Repo stars](https://img.shields.io/github/stars/thomhurst/TUnit) ![GitHub Issues or Pull Requests](https://img.shields.io/github/issues-closed-raw/thomhurst/TUnit)
 [![GitHub Sponsors](https://img.shields.io/github/sponsors/thomhurst)](https://github.com/sponsors/thomhurst) [![nuget](https://img.shields.io/nuget/v/TUnit.svg)](https://www.nuget.org/packages/TUnit/) [![NuGet Downloads](https://img.shields.io/nuget/dt/TUnit)](https://www.nuget.org/packages/TUnit/) ![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/thomhurst/TUnit/dotnet.yml) ![GitHub last commit (branch)](https://img.shields.io/github/last-commit/thomhurst/TUnit/main) ![License](https://img.shields.io/github/license/thomhurst/TUnit)

</div>

## ⚡ Why Choose TUnit?

| Feature | Traditional Frameworks | **TUnit** |
|---------|----------------------|-----------|
| Test Discovery | ❌ Runtime reflection | ✅ **Compile-time generation** |
| Execution Speed | ❌ Sequential by default | ✅ **Parallel by default** |
| Modern .NET | ⚠️ Limited AOT support | ✅ **Full Native AOT & trimming** |
| Test Dependencies | ❌ Not supported | ✅ **`[DependsOn]` chains** |
| Resource Management | ❌ Manual lifecycle | ✅ **Intelligent cleanup** |

⚡ **Parallel by Default** - Tests run concurrently with intelligent dependency management

🎯 **Compile-Time Discovery** - Know your test structure before runtime

🔧 **Modern .NET Ready** - Native AOT, trimming, and latest .NET features

🎭 **Extensible** - Customize data sources, attributes, and test behavior

---

<div align="center">

## 📚 **[Complete Documentation & Learning Center](https://tunit.dev)**

**🚀 New to TUnit?** Start with our **[Getting Started Guide](https://tunit.dev/docs/getting-started/installation)**

**🔄 Migrating?** See our **[Migration Guides](https://tunit.dev/docs/migration/xunit)**

**🎯 Advanced Features?** Explore **[Data-Driven Testing](https://tunit.dev/docs/test-authoring/arguments)**, **[Test Dependencies](https://tunit.dev/docs/test-authoring/depends-on)**, and **[Parallelism Control](https://tunit.dev/docs/parallelism/not-in-parallel)**

</div>

---

## 🏁 Quick Start

### Using the Project Template (Recommended)
```bash
dotnet new install TUnit.Templates
dotnet new TUnit -n "MyTestProject"
```

### Manual Installation
```bash
dotnet add package TUnit --prerelease
```

📖 **[📚 Complete Documentation & Guides](https://tunit.dev)** - Everything you need to master TUnit

## ✨ Key Features

<table>
<tr>
<td width="50%">

**🚀 Performance & Modern Platform**
- 🔥 Source-generated tests (no reflection)
- ⚡ Parallel execution by default
- 🚀 Native AOT & trimming support
- 📈 Optimized for performance

</td>
<td width="50%">

**🎯 Advanced Test Control**
- 🔗 Test dependencies with `[DependsOn]`
- 🎛️ Parallel limits & custom scheduling
- 🛡️ Built-in analyzers & compile-time checks
- 🎭 Custom attributes & extensible conditions

</td>
</tr>
<tr>
<td>

**📊 Rich Data & Assertions**
- 📋 Multiple data sources (`[Arguments]`, `[Matrix]`, `[ClassData]`)
- ✅ Fluent async assertions
- 🔄 Smart retry logic & conditional execution
- 📝 Rich test metadata & context

</td>
<td>

**🔧 Developer Experience**
- 💉 Full dependency injection support
- 🪝 Comprehensive lifecycle hooks
- 🎯 IDE integration (VS, Rider, VS Code)
- 📚 Extensive documentation & examples

</td>
</tr>
</table>

## 📝 Simple Test Example

```csharp
[Test]
public async Task User_Creation_Should_Set_Timestamp()
{
    // Arrange
    var userService = new UserService();

    // Act
    var user = await userService.CreateUserAsync("john.doe@example.com");

    // Assert - TUnit's fluent assertions
    await Assert.That(user.CreatedAt)
        .IsEqualTo(DateTime.Now)
        .Within(TimeSpan.FromMinutes(1));

    await Assert.That(user.Email)
        .IsEqualTo("john.doe@example.com");
}
```

## 🎯 Data-Driven Testing

```csharp
[Test]
[Arguments("user1@test.com", "ValidPassword123")]
[Arguments("user2@test.com", "AnotherPassword456")]
[Arguments("admin@test.com", "AdminPass789")]
public async Task User_Login_Should_Succeed(string email, string password)
{
    var result = await authService.LoginAsync(email, password);
    await Assert.That(result.IsSuccess).IsTrue();
}

// Matrix testing - tests all combinations
[Test]
[MatrixDataSource]
public async Task Database_Operations_Work(
    [Matrix("Create", "Update", "Delete")] string operation,
    [Matrix("User", "Product", "Order")] string entity)
{
    await Assert.That(await ExecuteOperation(operation, entity))
        .IsTrue();
}
```

## 🔗 Advanced Test Orchestration

```csharp
[Before(Class)]
public static async Task SetupDatabase(ClassHookContext context)
{
    await DatabaseHelper.InitializeAsync();
}

[Test, DisplayName("Register a new account")]
[MethodDataSource(nameof(GetTestUsers))]
public async Task Register_User(string username, string password)
{
    // Test implementation
}

[Test, DependsOn(nameof(Register_User))]
[Retry(3)] // Retry on failure
public async Task Login_With_Registered_User(string username, string password)
{
    // This test runs after Register_User completes
}

[Test]
[ParallelLimit<LoadTestParallelLimit>] // Custom parallel control
[Repeat(100)] // Run 100 times
public async Task Load_Test_Homepage()
{
    // Performance testing
}

// Custom attributes
[Test, WindowsOnly, RetryOnHttpError(5)]
public async Task Windows_Specific_Feature()
{
    // Platform-specific test with custom retry logic
}

public class LoadTestParallelLimit : IParallelLimit
{
    public int Limit => 10; // Limit to 10 concurrent executions
}
```

## 🔧 Smart Test Control

```csharp
// Custom conditional execution
public class WindowsOnlyAttribute : SkipAttribute
{
    public WindowsOnlyAttribute() : base("Windows only test") { }

    public override Task<bool> ShouldSkip(TestContext testContext)
        => Task.FromResult(!OperatingSystem.IsWindows());
}

// Custom retry logic
public class RetryOnHttpErrorAttribute : RetryAttribute
{
    public RetryOnHttpErrorAttribute(int times) : base(times) { }

    public override Task<bool> ShouldRetry(TestInformation testInformation,
        Exception exception, int currentRetryCount)
        => Task.FromResult(exception is HttpRequestException { StatusCode: HttpStatusCode.ServiceUnavailable });
}
```

## 🎯 Perfect For Every Testing Scenario

<table>
<tr>
<td width="33%">

### 🧪 **Unit Testing**
```csharp
[Test]
[Arguments(1, 2, 3)]
[Arguments(5, 10, 15)]
public async Task Calculate_Sum(int a, int b, int expected)
{
    await Assert.That(Calculator.Add(a, b))
        .IsEqualTo(expected);
}
```
**Fast, isolated, and reliable**

</td>
<td width="33%">

### 🔗 **Integration Testing**
```csharp
[Test, DependsOn(nameof(CreateUser))]
public async Task Login_After_Registration()
{
    // Runs after CreateUser completes
    var result = await authService.Login(user);
    await Assert.That(result.IsSuccess).IsTrue();
}
```
**Stateful workflows made simple**

</td>
<td width="33%">

### ⚡ **Load Testing**
```csharp
[Test]
[ParallelLimit<LoadTestLimit>]
[Repeat(1000)]
public async Task API_Handles_Concurrent_Requests()
{
    await Assert.That(await httpClient.GetAsync("/api/health"))
        .HasStatusCode(HttpStatusCode.OK);
}
```
**Built-in performance testing**

</td>
</tr>
</table>

## 🚀 What Makes TUnit Different?

### **Compile-Time Intelligence**
Tests are discovered at build time, not runtime - enabling faster discovery, better IDE integration, and precise resource lifecycle management.

### **Parallel-First Architecture**
Built for concurrency from day one with `[DependsOn]` for test chains, `[ParallelLimit]` for resource control, and intelligent scheduling.

### **Extensible by Design**
The `DataSourceGenerator<T>` pattern and custom attribute system let you extend TUnit's capabilities without modifying core framework code.

## 🏆 Community & Ecosystem

<div align="center">

**🌟 Join thousands of developers modernizing their testing**

[![Downloads](https://img.shields.io/nuget/dt/TUnit?label=Downloads&color=blue)](https://www.nuget.org/packages/TUnit/)
[![Contributors](https://img.shields.io/github/contributors/thomhurst/TUnit?label=Contributors)](https://github.com/thomhurst/TUnit/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/thomhurst/TUnit?label=Discussions)](https://github.com/thomhurst/TUnit/discussions)

</div>

### 🤝 **Active Community**
- 📚 **[Official Documentation](https://tunit.dev)** - Comprehensive guides, tutorials, and API reference
- 💬 **[GitHub Discussions](https://github.com/thomhurst/TUnit/discussions)** - Get help and share ideas
- 🐛 **[Issue Tracking](https://github.com/thomhurst/TUnit/issues)** - Report bugs and request features
- 📢 **[Release Notes](https://github.com/thomhurst/TUnit/releases)** - Stay updated with latest improvements

## 🛠️ IDE Support

TUnit works seamlessly across all major .NET development environments:

### Visual Studio (2022 17.13+)
✅ **Fully supported** - No additional configuration needed for latest versions

⚙️ **Earlier versions**: Enable "Use testing platform server mode" in Tools > Manage Preview Features

### JetBrains Rider
✅ **Fully supported**

⚙️ **Setup**: Enable "Testing Platform support" in Settings > Build, Execution, Deployment > Unit Testing > VSTest

### Visual Studio Code
✅ **Fully supported**

⚙️ **Setup**: Install [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) and enable "Use Testing Platform Protocol"

### Command Line
✅ **Full CLI support** - Works with `dotnet test`, `dotnet run`, and direct executable execution

## 📦 Package Options

| Package | Use Case |
|---------|----------|
| **`TUnit`** | ⭐ **Start here** - Complete testing framework (includes Core + Engine + Assertions) |
| **`TUnit.Core`** | 📚 Test libraries and shared components (no execution engine) |
| **`TUnit.Engine`** | 🚀 Test execution engine and adapter (for test projects) |
| **`TUnit.Assertions`** | ✅ Standalone assertions (works with any test framework) |
| **`TUnit.Playwright`** | 🎭 Playwright integration with automatic lifecycle management |

## 🎯 Migration from Other Frameworks

**Coming from NUnit or xUnit?** TUnit maintains familiar syntax while adding modern capabilities:

```csharp
// Enhanced with TUnit's advanced features
[Test]
[Arguments("value1")]
[Arguments("value2")]
[Retry(3)]
[ParallelLimit<CustomLimit>]
public async Task Modern_TUnit_Test(string value) { }
```

📖 **Need help migrating?** Check our detailed **[Migration Guides](https://tunit.dev/docs/migration/xunit)** with step-by-step instructions for xUnit, NUnit, and MSTest.


## 💡 Current Status

The API is mostly stable, but may have some changes based on feedback or issues before v1.0 release.

---

<div align="center">

## 🚀 Ready to Experience the Future of .NET Testing?

### ⚡ **Start in 30 Seconds**

```bash
# Create a new test project with examples
dotnet new install TUnit.Templates && dotnet new TUnit -n "MyAwesomeTests"

# Or add to existing project
dotnet add package TUnit --prerelease
```

### 🎯 **Why Wait? Join the Movement**

<table>
<tr>
<td align="center" width="25%">

### 📈 **Performance**
**Optimized execution**
**Parallel by default**
**Zero reflection overhead**

</td>
<td align="center" width="25%">

### 🔮 **Future-Ready**
**Native AOT support**
**Latest .NET features**
**Source generation**

</td>
<td align="center" width="25%">

### 🛠️ **Developer Experience**
**Compile-time checks**
**Rich IDE integration**
**Intelligent debugging**

</td>
<td align="center" width="25%">

### 🎭 **Flexibility**
**Test dependencies**
**Custom attributes**
**Extensible architecture**

</td>
</tr>
</table>

---

**📖 Learn More**: [tunit.dev](https://tunit.dev) | **💬 Get Help**: [GitHub Discussions](https://github.com/thomhurst/TUnit/discussions) | **⭐ Show Support**: [Star on GitHub](https://github.com/thomhurst/TUnit)

*TUnit is actively developed and production-ready. Join our growing community of developers who've made the switch!*

</div>

## Performance Benchmark

### Scenario: Building the test project

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method       | Version | Mean       | Error     | StdDev    | Median     |
|------------- |-------- |-----------:|----------:|----------:|-----------:|
| Build_TUnit  | 0.64.0  | 1,549.9 ms | 102.49 ms | 294.06 ms | 1,449.6 ms |
| Build_NUnit  | 4.4.0   | 1,302.1 ms |  77.46 ms | 224.73 ms | 1,234.1 ms |
| Build_xUnit  | 2.9.3   | 1,013.9 ms |  32.94 ms |  93.44 ms | 1,011.6 ms |
| Build_MSTest | 3.11.0  | 1,194.3 ms |  87.01 ms | 249.66 ms | 1,131.4 ms |
| Build_xUnit3 | 3.1.0   |   964.6 ms |  31.44 ms |  92.72 ms |   971.0 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 3.02GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.64.0  | 1.738 s | 0.0317 s | 0.0296 s | 1.738 s |
| Build_NUnit  | 4.4.0   | 1.516 s | 0.0302 s | 0.0282 s | 1.510 s |
| Build_xUnit  | 2.9.3   | 1.508 s | 0.0203 s | 0.0180 s | 1.509 s |
| Build_MSTest | 3.11.0  | 1.524 s | 0.0103 s | 0.0096 s | 1.527 s |
| Build_xUnit3 | 3.1.0   | 1.502 s | 0.0192 s | 0.0180 s | 1.496 s |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.64.0  | 2.085 s | 0.0411 s | 0.0867 s | 2.081 s |
| Build_NUnit  | 4.4.0   | 1.957 s | 0.0238 s | 0.0211 s | 1.956 s |
| Build_xUnit  | 2.9.3   | 1.914 s | 0.0268 s | 0.0250 s | 1.912 s |
| Build_MSTest | 3.11.0  | 1.994 s | 0.0399 s | 0.0632 s | 1.967 s |
| Build_xUnit3 | 3.1.0   | 1.923 s | 0.0380 s | 0.0337 s | 1.921 s |


### Scenario: Tests focused on assertion performance and validation

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 348.30 ms |  8.989 ms | 25.50 ms | 346.22 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  | 641.16 ms | 24.246 ms | 69.57 ms | 624.15 ms |
| xUnit3    | 3.1.0   | 311.84 ms |  7.676 ms | 21.78 ms | 308.52 ms |
| TUnit_AOT | 0.64.0  |  40.07 ms |  5.081 ms | 14.66 ms |  31.53 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   505.67 ms |  5.411 ms |  5.061 ms |   503.89 ms |
| NUnit     | 4.4.0   |   936.65 ms | 14.037 ms | 13.130 ms |   936.19 ms |
| xUnit     | 2.9.3   | 1,045.36 ms | 15.542 ms | 14.538 ms | 1,042.06 ms |
| MSTest    | 3.11.0  |   891.02 ms | 16.780 ms | 18.651 ms |   890.58 ms |
| xUnit3    | 3.1.0   |   476.86 ms |  4.208 ms |  3.514 ms |   477.44 ms |
| TUnit_AOT | 0.64.0  |    26.59 ms |  0.205 ms |  0.182 ms |    26.55 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   541.66 ms |  7.101 ms |  6.642 ms |   539.24 ms |
| NUnit     | 4.4.0   | 1,028.80 ms | 19.951 ms | 24.502 ms | 1,023.28 ms |
| xUnit     | 2.9.3   | 1,085.87 ms | 20.615 ms | 20.246 ms | 1,084.91 ms |
| MSTest    | 3.11.0  |   943.15 ms | 18.019 ms | 18.504 ms |   936.29 ms |
| xUnit3    | 3.1.0   |   505.85 ms |  3.918 ms |  3.474 ms |   504.76 ms |
| TUnit_AOT | 0.64.0  |    66.81 ms |  1.605 ms |  4.733 ms |    66.36 ms |


### Scenario: Tests running asynchronous operations and async/await patterns

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error    | StdDev    | Median     |
|---------- |-------- |-----------:|---------:|----------:|-----------:|
| TUnit     | 0.64.0  |   561.7 ms | 34.19 ms | 100.26 ms |   547.3 ms |
| NUnit     | 4.4.0   | 1,233.4 ms | 71.12 ms | 208.57 ms | 1,233.5 ms |
| xUnit     | 2.9.3   | 1,045.4 ms | 53.27 ms | 156.24 ms | 1,002.5 ms |
| MSTest    | 3.11.0  |         NA |       NA |        NA |         NA |
| xUnit3    | 3.1.0   |   457.5 ms | 13.02 ms |  38.38 ms |   455.4 ms |
| TUnit_AOT | 0.64.0  |   115.9 ms | 12.52 ms |  36.93 ms |   110.2 ms |

Benchmarks with issues:
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 451.11 ms |  3.407 ms |  3.020 ms | 450.59 ms |
| NUnit     | 4.4.0   | 918.27 ms | 17.903 ms | 18.385 ms | 920.56 ms |
| xUnit     | 2.9.3   | 988.62 ms | 15.587 ms | 14.580 ms | 981.11 ms |
| MSTest    | 3.11.0  | 847.72 ms | 15.887 ms | 16.999 ms | 849.84 ms |
| xUnit3    | 3.1.0   | 464.10 ms |  3.340 ms |  3.124 ms | 465.11 ms |
| TUnit_AOT | 0.64.0  |  26.92 ms |  0.525 ms |  0.583 ms |  26.63 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   495.47 ms |  6.326 ms |  5.917 ms |   493.83 ms |
| NUnit     | 4.4.0   |   998.34 ms | 17.470 ms | 16.341 ms |   995.81 ms |
| xUnit     | 2.9.3   | 1,104.06 ms | 21.995 ms | 31.545 ms | 1,099.74 ms |
| MSTest    | 3.11.0  |   946.59 ms | 18.395 ms | 19.682 ms |   943.42 ms |
| xUnit3    | 3.1.0   |   532.62 ms |  9.679 ms | 14.187 ms |   534.99 ms |
| TUnit_AOT | 0.64.0  |    88.65 ms |  2.442 ms |  7.162 ms |    89.30 ms |


### Scenario: Simple tests with basic operations and assertions

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 357.86 ms | 17.207 ms | 50.19 ms | 349.29 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  | 674.95 ms | 23.182 ms | 67.99 ms | 656.57 ms |
| xUnit3    | 3.1.0   | 400.95 ms | 19.299 ms | 56.60 ms | 393.69 ms |
| TUnit_AOT | 0.64.0  |  64.77 ms |  9.084 ms | 26.64 ms |  60.18 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 468.92 ms |  3.184 ms |  2.823 ms | 468.69 ms |
| NUnit     | 4.4.0   | 926.69 ms | 15.704 ms | 14.690 ms | 928.28 ms |
| xUnit     | 2.9.3   | 991.21 ms | 18.483 ms | 18.153 ms | 984.60 ms |
| MSTest    | 3.11.0  | 854.86 ms | 16.736 ms | 16.437 ms | 854.53 ms |
| xUnit3    | 3.1.0   | 466.29 ms |  4.974 ms |  4.653 ms | 466.15 ms |
| TUnit_AOT | 0.64.0  |  25.21 ms |  0.151 ms |  0.142 ms |  25.26 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   512.19 ms |  2.809 ms |  2.193 ms |   512.59 ms |
| NUnit     | 4.4.0   | 1,001.75 ms | 17.380 ms | 16.257 ms | 1,007.14 ms |
| xUnit     | 2.9.3   | 1,064.54 ms | 16.663 ms | 15.586 ms | 1,059.26 ms |
| MSTest    | 3.11.0  |   934.02 ms | 18.197 ms | 18.687 ms |   928.08 ms |
| xUnit3    | 3.1.0   |   505.67 ms |  4.535 ms |  4.020 ms |   504.61 ms |
| TUnit_AOT | 0.64.0  |    62.81 ms |  1.581 ms |  4.636 ms |    61.97 ms |


### Scenario: Parameterized tests with multiple test cases using data attributes

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 461.85 ms | 35.667 ms | 105.17 ms | 451.16 ms |
| NUnit     | 4.4.0   |        NA |        NA |        NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |        NA |        NA |
| xUnit3    | 3.1.0   | 311.40 ms |  7.571 ms |  20.98 ms | 309.62 ms |
| TUnit_AOT | 0.64.0  |  46.42 ms |  6.168 ms |  18.19 ms |  39.16 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.73GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 464.29 ms |  2.595 ms |  2.427 ms | 464.58 ms |
| NUnit     | 4.4.0   | 910.18 ms | 17.692 ms | 16.549 ms | 911.03 ms |
| xUnit     | 2.9.3   | 982.67 ms | 17.253 ms | 16.139 ms | 976.62 ms |
| MSTest    | 3.11.0  | 838.46 ms | 16.684 ms | 17.133 ms | 839.73 ms |
| xUnit3    | 3.1.0   | 471.17 ms |  2.448 ms |  2.290 ms | 470.08 ms |
| TUnit_AOT | 0.64.0  |  26.44 ms |  0.166 ms |  0.130 ms |  26.41 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   523.37 ms |  3.941 ms |  3.493 ms |   522.12 ms |
| NUnit     | 4.4.0   | 1,018.81 ms | 20.166 ms | 28.922 ms | 1,017.30 ms |
| xUnit     | 2.9.3   | 1,093.82 ms | 19.824 ms | 34.196 ms | 1,091.60 ms |
| MSTest    | 3.11.0  |   942.59 ms | 18.643 ms | 24.242 ms |   944.45 ms |
| xUnit3    | 3.1.0   |   522.16 ms |  3.446 ms |  3.055 ms |   522.24 ms |
| TUnit_AOT | 0.64.0  |    66.74 ms |  1.476 ms |  4.306 ms |    66.33 ms |


### Scenario: Tests utilizing class fixtures and shared test context

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 303.39 ms |  5.987 ms |  7.784 ms | 305.57 ms |
| NUnit     | 4.4.0   |        NA |        NA |        NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |        NA |        NA |
| xUnit3    | 3.1.0   | 329.09 ms | 10.247 ms | 29.891 ms | 326.63 ms |
| TUnit_AOT | 0.64.0  |  73.36 ms |  9.266 ms | 26.882 ms |  71.59 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 3.20GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   462.65 ms |  3.931 ms |  3.677 ms |   462.48 ms |
| NUnit     | 4.4.0   |   935.80 ms | 17.864 ms | 16.710 ms |   933.83 ms |
| xUnit     | 2.9.3   | 1,009.43 ms | 19.699 ms | 18.427 ms | 1,003.31 ms |
| MSTest    | 3.11.0  |   868.17 ms | 16.929 ms | 19.496 ms |   865.67 ms |
| xUnit3    | 3.1.0   |   457.70 ms |  3.990 ms |  3.732 ms |   457.42 ms |
| TUnit_AOT | 0.64.0  |    36.25 ms |  1.209 ms |  3.564 ms |    36.98 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   499.26 ms |  5.618 ms |  5.255 ms |   498.45 ms |
| NUnit     | 4.4.0   | 1,007.72 ms | 18.198 ms | 17.023 ms | 1,007.26 ms |
| xUnit     | 2.9.3   | 1,091.13 ms | 14.981 ms | 14.013 ms | 1,086.12 ms |
| MSTest    | 3.11.0  |   953.07 ms | 14.984 ms | 14.016 ms |   950.98 ms |
| xUnit3    | 3.1.0   |   501.54 ms |  2.725 ms |  2.549 ms |   501.74 ms |
| TUnit_AOT | 0.64.0  |    81.50 ms |  2.321 ms |  6.843 ms |    81.92 ms |


### Scenario: Tests executing in parallel to test framework parallelization

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 294.42 ms |  5.996 ms | 17.009 ms | 288.97 ms |
| NUnit     | 4.4.0   | 582.52 ms | 10.987 ms | 21.687 ms | 579.69 ms |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |        NA |        NA |
| xUnit3    | 3.1.0   | 276.00 ms |  6.030 ms | 17.780 ms | 277.42 ms |
| TUnit_AOT | 0.64.0  |  30.47 ms |  0.459 ms |  0.383 ms |  30.35 ms |

Benchmarks with issues:
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 447.69 ms |  2.991 ms |  2.798 ms | 448.13 ms |
| NUnit     | 4.4.0   | 922.86 ms | 17.445 ms | 16.318 ms | 920.82 ms |
| xUnit     | 2.9.3   | 989.79 ms | 19.236 ms | 17.993 ms | 982.27 ms |
| MSTest    | 3.11.0  | 854.02 ms | 16.710 ms | 18.573 ms | 856.08 ms |
| xUnit3    | 3.1.0   | 451.28 ms |  2.566 ms |  2.400 ms | 450.20 ms |
| TUnit_AOT | 0.64.0  |  25.14 ms |  0.182 ms |  0.152 ms |  25.12 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   495.97 ms |  3.374 ms |  2.991 ms |   495.75 ms |
| NUnit     | 4.4.0   | 1,018.15 ms | 19.118 ms | 17.883 ms | 1,019.36 ms |
| xUnit     | 2.9.3   | 1,083.47 ms | 19.165 ms | 17.927 ms | 1,085.79 ms |
| MSTest    | 3.11.0  |   939.53 ms | 18.440 ms | 18.937 ms |   933.07 ms |
| xUnit3    | 3.1.0   |   501.09 ms |  4.562 ms |  4.267 ms |   499.41 ms |
| TUnit_AOT | 0.64.0  |    62.42 ms |  1.553 ms |  4.553 ms |    61.49 ms |


### Scenario: A test that takes 50ms to execute, repeated 100 times

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   456.78 ms | 22.762 ms |  65.31 ms |   457.27 ms |
| NUnit     | 4.4.0   |          NA |        NA |        NA |          NA |
| xUnit     | 2.9.3   | 1,209.91 ms | 97.861 ms | 285.46 ms | 1,137.61 ms |
| MSTest    | 3.11.0  |          NA |        NA |        NA |          NA |
| xUnit3    | 3.1.0   |   482.62 ms | 22.088 ms |  64.08 ms |   474.10 ms |
| TUnit_AOT | 0.64.0  |    79.12 ms |  8.261 ms |  23.83 ms |    78.07 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   481.46 ms |  3.750 ms |  3.508 ms |   481.20 ms |
| NUnit     | 4.4.0   |   913.32 ms | 17.137 ms | 16.030 ms |   916.32 ms |
| xUnit     | 2.9.3   | 1,062.65 ms | 16.353 ms | 15.297 ms | 1,060.61 ms |
| MSTest    | 3.11.0  |   838.86 ms | 16.366 ms | 17.512 ms |   836.03 ms |
| xUnit3    | 3.1.0   |   483.24 ms |  2.999 ms |  2.805 ms |   483.82 ms |
| TUnit_AOT | 0.64.0  |    39.12 ms |  0.315 ms |  0.246 ms |    39.02 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   541.37 ms |  7.153 ms |  6.691 ms |   541.78 ms |
| NUnit     | 4.4.0   | 1,017.39 ms | 20.057 ms | 18.761 ms | 1,016.48 ms |
| xUnit     | 2.9.3   | 1,158.69 ms | 17.081 ms | 15.978 ms | 1,167.27 ms |
| MSTest    | 3.11.0  |   940.83 ms | 18.745 ms | 17.534 ms |   942.89 ms |
| xUnit3    | 3.1.0   |   539.70 ms |  2.738 ms |  2.427 ms |   539.17 ms |
| TUnit_AOT | 0.64.0  |    77.13 ms |  1.507 ms |  2.476 ms |    76.58 ms |


### Scenario: Tests with setup and teardown lifecycle methods

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 354.66 ms | 11.170 ms | 31.87 ms | 350.74 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 345.96 ms | 14.435 ms | 40.95 ms | 335.27 ms |
| TUnit_AOT | 0.64.0  |  50.43 ms |  6.559 ms | 19.13 ms |  46.36 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.64GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 452.85 ms |  3.295 ms |  2.751 ms | 452.96 ms |
| NUnit     | 4.4.0   | 908.49 ms | 17.637 ms | 19.604 ms | 899.92 ms |
| xUnit     | 2.9.3   | 983.14 ms | 19.301 ms | 18.956 ms | 979.70 ms |
| MSTest    | 3.11.0  | 844.04 ms | 16.419 ms | 18.250 ms | 840.45 ms |
| xUnit3    | 3.1.0   | 446.65 ms |  1.962 ms |  1.531 ms | 446.49 ms |
| TUnit_AOT | 0.64.0  |  25.07 ms |  0.179 ms |  0.150 ms |  25.02 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.4, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   499.95 ms |  5.819 ms |  5.159 ms |   500.48 ms |
| NUnit     | 4.4.0   |   975.97 ms | 15.072 ms | 14.099 ms |   978.96 ms |
| xUnit     | 2.9.3   | 1,097.76 ms | 22.364 ms | 65.940 ms | 1,070.35 ms |
| MSTest    | 3.11.0  | 1,003.70 ms | 23.579 ms | 69.524 ms | 1,000.04 ms |
| xUnit3    | 3.1.0   |   500.47 ms |  9.964 ms | 11.474 ms |   501.11 ms |
| TUnit_AOT | 0.64.0  |    63.46 ms |  2.203 ms |  6.141 ms |    62.56 ms |



