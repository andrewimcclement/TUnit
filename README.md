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
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.64.0  | 2.050 s | 0.1568 s | 0.4622 s | 1.972 s |
| Build_NUnit  | 4.4.0   | 1.462 s | 0.0911 s | 0.2685 s | 1.398 s |
| Build_xUnit  | 2.9.3   | 1.206 s | 0.0942 s | 0.2749 s | 1.141 s |
| Build_MSTest | 3.11.0  | 1.495 s | 0.1309 s | 0.3860 s | 1.507 s |
| Build_xUnit3 | 3.1.0   | 1.211 s | 0.0623 s | 0.1818 s | 1.193 s |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.78GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.64.0  | 1.749 s | 0.0343 s | 0.0523 s | 1.748 s |
| Build_NUnit  | 4.4.0   | 1.541 s | 0.0168 s | 0.0149 s | 1.543 s |
| Build_xUnit  | 2.9.3   | 1.549 s | 0.0232 s | 0.0217 s | 1.544 s |
| Build_MSTest | 3.11.0  | 1.571 s | 0.0161 s | 0.0134 s | 1.570 s |
| Build_xUnit3 | 3.1.0   | 1.541 s | 0.0184 s | 0.0163 s | 1.543 s |



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
| Build_TUnit  | 0.64.0  | 1.835 s | 0.0351 s | 0.0328 s | 1.839 s |
| Build_NUnit  | 4.4.0   | 1.682 s | 0.0157 s | 0.0139 s | 1.680 s |
| Build_xUnit  | 2.9.3   | 1.676 s | 0.0153 s | 0.0119 s | 1.681 s |
| Build_MSTest | 3.11.0  | 1.738 s | 0.0247 s | 0.0231 s | 1.736 s |
| Build_xUnit3 | 3.1.0   | 1.651 s | 0.0165 s | 0.0138 s | 1.647 s |


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
| TUnit     | 0.64.0  | 467.97 ms | 27.802 ms | 80.22 ms | 468.71 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 435.24 ms | 18.515 ms | 53.72 ms | 424.11 ms |
| TUnit_AOT | 0.64.0  |  68.35 ms |  9.257 ms | 26.41 ms |  61.42 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
Intel Xeon Platinum 8370C CPU 2.80GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v4
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v4

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 475.76 ms |  6.048 ms |  5.657 ms | 476.66 ms |
| NUnit     | 4.4.0   | 887.57 ms | 17.036 ms | 16.731 ms | 883.70 ms |
| xUnit     | 2.9.3   | 965.09 ms | 18.015 ms | 16.851 ms | 960.13 ms |
| MSTest    | 3.11.0  | 829.65 ms | 16.024 ms | 17.810 ms | 825.35 ms |
| xUnit3    | 3.1.0   | 443.78 ms |  3.677 ms |  3.439 ms | 443.86 ms |
| TUnit_AOT | 0.64.0  |  42.20 ms |  1.818 ms |  5.360 ms |  42.69 ms |



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
| TUnit     | 0.64.0  |   561.28 ms | 11.155 ms | 10.434 ms |   556.85 ms |
| NUnit     | 4.4.0   | 1,116.13 ms | 21.814 ms | 30.581 ms | 1,107.78 ms |
| xUnit     | 2.9.3   | 1,209.94 ms | 23.645 ms | 33.147 ms | 1,216.67 ms |
| MSTest    | 3.11.0  | 1,094.28 ms | 21.620 ms | 34.291 ms | 1,086.59 ms |
| xUnit3    | 3.1.0   |   528.02 ms | 10.524 ms | 22.653 ms |   516.54 ms |
| TUnit_AOT | 0.64.0  |    69.37 ms |  1.983 ms |  5.395 ms |    68.53 ms |


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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   555.54 ms | 29.102 ms |  84.89 ms |   541.89 ms |
| NUnit     | 4.4.0   |          NA |        NA |        NA |          NA |
| xUnit     | 2.9.3   | 1,332.79 ms | 78.830 ms | 231.19 ms | 1,315.31 ms |
| MSTest    | 3.11.0  |          NA |        NA |        NA |          NA |
| xUnit3    | 3.1.0   |   619.24 ms | 44.725 ms | 131.87 ms |   615.36 ms |
| TUnit_AOT | 0.64.0  |    98.06 ms |  8.338 ms |  24.58 ms |    98.14 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.67GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 445.31 ms |  1.946 ms |  1.820 ms | 444.90 ms |
| NUnit     | 4.4.0   | 905.94 ms | 15.731 ms | 14.715 ms | 906.96 ms |
| xUnit     | 2.9.3   | 985.55 ms | 15.581 ms | 14.574 ms | 982.65 ms |
| MSTest    | 3.11.0  | 850.82 ms | 16.906 ms | 18.791 ms | 856.40 ms |
| xUnit3    | 3.1.0   | 461.40 ms |  4.755 ms |  4.448 ms | 460.12 ms |
| TUnit_AOT | 0.64.0  |  27.70 ms |  0.552 ms |  0.859 ms |  27.56 ms |



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
| TUnit     | 0.64.0  |   520.86 ms |  9.981 ms | 13.324 ms |   520.40 ms |
| NUnit     | 4.4.0   | 1,088.65 ms | 21.657 ms | 19.198 ms | 1,086.96 ms |
| xUnit     | 2.9.3   | 1,208.98 ms | 24.142 ms | 36.134 ms | 1,203.17 ms |
| MSTest    | 3.11.0  | 1,034.71 ms | 20.389 ms | 44.755 ms | 1,023.38 ms |
| xUnit3    | 3.1.0   |   557.80 ms | 11.031 ms | 12.703 ms |   556.10 ms |
| TUnit_AOT | 0.64.0  |    92.81 ms |  3.177 ms |  9.317 ms |    92.39 ms |


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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   389.16 ms | 21.176 ms |  62.44 ms |   381.29 ms |
| NUnit     | 4.4.0   | 1,113.11 ms | 71.418 ms | 206.06 ms | 1,098.92 ms |
| xUnit     | 2.9.3   |          NA |        NA |        NA |          NA |
| MSTest    | 3.11.0  |   812.45 ms | 41.624 ms | 120.76 ms |   803.79 ms |
| xUnit3    | 3.1.0   |   398.60 ms | 17.030 ms |  49.95 ms |   400.49 ms |
| TUnit_AOT | 0.64.0  |    53.31 ms |  8.240 ms |  23.77 ms |    39.05 ms |

Benchmarks with issues:
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
| TUnit     | 0.64.0  |   482.32 ms |  6.435 ms |  6.019 ms |   481.84 ms |
| NUnit     | 4.4.0   |   941.02 ms | 17.781 ms | 19.764 ms |   934.02 ms |
| xUnit     | 2.9.3   | 1,012.78 ms | 17.352 ms | 16.231 ms | 1,010.20 ms |
| MSTest    | 3.11.0  |   877.24 ms | 17.047 ms | 20.294 ms |   870.53 ms |
| xUnit3    | 3.1.0   |   469.56 ms |  3.291 ms |  2.917 ms |   469.33 ms |
| TUnit_AOT | 0.64.0  |    26.20 ms |  0.213 ms |  0.189 ms |    26.15 ms |



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
| TUnit     | 0.64.0  |   505.79 ms |  2.424 ms |  1.893 ms |   506.09 ms |
| NUnit     | 4.4.0   |   996.16 ms | 18.262 ms | 17.082 ms |   993.28 ms |
| xUnit     | 2.9.3   | 1,070.32 ms | 16.552 ms | 15.482 ms | 1,076.18 ms |
| MSTest    | 3.11.0  |   937.44 ms | 18.270 ms | 18.762 ms |   943.89 ms |
| xUnit3    | 3.1.0   |   500.96 ms |  2.570 ms |  2.006 ms |   500.80 ms |
| TUnit_AOT | 0.64.0  |    68.80 ms |  2.505 ms |  7.147 ms |    68.29 ms |


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
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 383.37 ms | 20.919 ms | 60.02 ms | 371.88 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 503.05 ms | 32.503 ms | 94.30 ms | 482.18 ms |
| TUnit_AOT | 0.64.0  |  56.21 ms |  6.313 ms | 18.01 ms |  52.02 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   475.54 ms |  5.408 ms |  5.059 ms |   476.55 ms |
| NUnit     | 4.4.0   |   926.55 ms | 18.336 ms | 17.152 ms |   921.35 ms |
| xUnit     | 2.9.3   | 1,011.24 ms | 18.823 ms | 18.487 ms | 1,011.08 ms |
| MSTest    | 3.11.0  |   850.60 ms | 15.913 ms | 17.027 ms |   849.07 ms |
| xUnit3    | 3.1.0   |   476.14 ms |  2.351 ms |  2.199 ms |   476.62 ms |
| TUnit_AOT | 0.64.0  |    27.02 ms |  0.191 ms |  0.178 ms |    26.99 ms |



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
| TUnit     | 0.64.0  |   525.32 ms |  4.241 ms |  3.967 ms |   526.45 ms |
| NUnit     | 4.4.0   | 1,026.38 ms | 20.047 ms | 19.689 ms | 1,024.98 ms |
| xUnit     | 2.9.3   | 1,101.98 ms | 19.994 ms | 18.702 ms | 1,102.42 ms |
| MSTest    | 3.11.0  |   976.50 ms | 19.473 ms | 31.994 ms |   972.38 ms |
| xUnit3    | 3.1.0   |   533.76 ms |  9.794 ms |  8.682 ms |   529.57 ms |
| TUnit_AOT | 0.64.0  |    65.75 ms |  1.435 ms |  4.230 ms |    65.42 ms |


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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   462.51 ms | 27.940 ms |  82.38 ms |   449.98 ms |
| NUnit     | 4.4.0   |          NA |        NA |        NA |          NA |
| xUnit     | 2.9.3   | 1,130.10 ms | 73.695 ms | 216.13 ms | 1,093.43 ms |
| MSTest    | 3.11.0  |          NA |        NA |        NA |          NA |
| xUnit3    | 3.1.0   |   505.32 ms | 43.960 ms | 128.93 ms |   483.70 ms |
| TUnit_AOT | 0.64.0  |    70.38 ms |  8.460 ms |  24.94 ms |    68.78 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.78GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   473.00 ms |  8.179 ms |  7.251 ms |   472.18 ms |
| NUnit     | 4.4.0   |   960.32 ms | 18.494 ms | 25.926 ms |   963.89 ms |
| xUnit     | 2.9.3   | 1,043.98 ms | 20.852 ms | 21.414 ms | 1,039.27 ms |
| MSTest    | 3.11.0  |   878.68 ms | 13.618 ms | 12.738 ms |   873.28 ms |
| xUnit3    | 3.1.0   |   466.48 ms |  7.764 ms |  7.625 ms |   464.14 ms |
| TUnit_AOT | 0.64.0  |    30.32 ms |  0.705 ms |  2.079 ms |    30.40 ms |



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
| TUnit     | 0.64.0  |   495.22 ms |  5.450 ms |  5.098 ms |   493.69 ms |
| NUnit     | 4.4.0   |   982.82 ms | 17.655 ms | 15.651 ms |   975.98 ms |
| xUnit     | 2.9.3   | 1,049.83 ms | 17.780 ms | 16.632 ms | 1,053.17 ms |
| MSTest    | 3.11.0  |   917.17 ms | 18.027 ms | 16.863 ms |   910.23 ms |
| xUnit3    | 3.1.0   |   486.96 ms |  3.367 ms |  3.150 ms |   486.12 ms |
| TUnit_AOT | 0.64.0  |    78.54 ms |  1.554 ms |  3.693 ms |    78.84 ms |


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
| Method    | Version | Mean      | Error    | StdDev    | Median    |
|---------- |-------- |----------:|---------:|----------:|----------:|
| TUnit     | 0.64.0  | 493.77 ms | 28.90 ms |  82.00 ms | 483.47 ms |
| NUnit     | 4.4.0   |        NA |       NA |        NA |        NA |
| xUnit     | 2.9.3   | 898.01 ms | 36.28 ms | 105.27 ms | 904.86 ms |
| MSTest    | 3.11.0  |        NA |       NA |        NA |        NA |
| xUnit3    | 3.1.0   | 431.65 ms | 29.50 ms |  86.52 ms | 428.98 ms |
| TUnit_AOT | 0.64.0  |  72.72 ms | 10.90 ms |  31.97 ms |  67.02 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.66GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 454.18 ms |  5.652 ms |  5.287 ms | 452.89 ms |
| NUnit     | 4.4.0   | 926.65 ms | 14.572 ms | 12.918 ms | 925.83 ms |
| xUnit     | 2.9.3   | 999.51 ms | 17.765 ms | 16.617 ms | 991.16 ms |
| MSTest    | 3.11.0  | 853.98 ms | 15.681 ms | 14.668 ms | 852.62 ms |
| xUnit3    | 3.1.0   | 453.91 ms |  2.940 ms |  2.455 ms | 454.37 ms |
| TUnit_AOT | 0.64.0  |  25.74 ms |  0.208 ms |  0.173 ms |  25.70 ms |



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
| TUnit     | 0.64.0  |   496.03 ms |  3.052 ms |  2.548 ms |   496.08 ms |
| NUnit     | 4.4.0   | 1,005.66 ms | 18.834 ms | 17.617 ms | 1,001.39 ms |
| xUnit     | 2.9.3   | 1,069.60 ms | 21.104 ms | 19.740 ms | 1,075.52 ms |
| MSTest    | 3.11.0  |   952.17 ms | 18.114 ms | 17.791 ms |   955.05 ms |
| xUnit3    | 3.1.0   |   501.65 ms |  5.055 ms |  4.729 ms |   500.78 ms |
| TUnit_AOT | 0.64.0  |    62.42 ms |  1.442 ms |  4.230 ms |    62.89 ms |


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
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 460.58 ms | 19.575 ms | 56.16 ms | 458.37 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 484.33 ms | 18.949 ms | 55.57 ms | 478.64 ms |
| TUnit_AOT | 0.64.0  |  72.61 ms |  8.053 ms | 23.36 ms |  64.27 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   493.14 ms |  5.387 ms |  5.039 ms |   493.66 ms |
| NUnit     | 4.4.0   |   929.31 ms | 17.083 ms | 15.979 ms |   933.18 ms |
| xUnit     | 2.9.3   | 1,083.16 ms | 20.084 ms | 18.786 ms | 1,080.84 ms |
| MSTest    | 3.11.0  |   863.62 ms | 17.271 ms | 18.480 ms |   867.09 ms |
| xUnit3    | 3.1.0   |   494.16 ms |  6.365 ms |  5.954 ms |   493.83 ms |
| TUnit_AOT | 0.64.0  |    40.20 ms |  0.202 ms |  0.179 ms |    40.22 ms |



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
| TUnit     | 0.64.0  |   551.59 ms |  5.735 ms |  5.365 ms |   550.91 ms |
| NUnit     | 4.4.0   | 1,029.03 ms | 20.071 ms | 18.775 ms | 1,029.73 ms |
| xUnit     | 2.9.3   | 1,187.65 ms | 20.672 ms | 41.759 ms | 1,180.51 ms |
| MSTest    | 3.11.0  |   959.98 ms | 19.050 ms | 26.705 ms |   953.88 ms |
| xUnit3    | 3.1.0   |   556.93 ms |  8.132 ms |  7.606 ms |   556.95 ms |
| TUnit_AOT | 0.64.0  |    76.04 ms |  1.453 ms |  1.492 ms |    75.79 ms |


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
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 453.86 ms | 22.014 ms |  61.73 ms | 446.35 ms |
| NUnit     | 4.4.0   |        NA |        NA |        NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  | 796.60 ms | 41.161 ms | 118.76 ms | 786.95 ms |
| xUnit3    | 3.1.0   | 538.83 ms | 30.836 ms |  89.95 ms | 537.33 ms |
| TUnit_AOT | 0.64.0  |  60.63 ms |  6.614 ms |  19.19 ms |  55.84 ms |

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
| TUnit     | 0.64.0  | 454.95 ms |  2.384 ms |  1.990 ms | 454.78 ms |
| NUnit     | 4.4.0   | 920.54 ms | 18.111 ms | 20.857 ms | 923.48 ms |
| xUnit     | 2.9.3   | 988.64 ms | 17.241 ms | 15.284 ms | 983.35 ms |
| MSTest    | 3.11.0  | 851.30 ms | 16.878 ms | 16.576 ms | 857.78 ms |
| xUnit3    | 3.1.0   | 451.18 ms |  3.319 ms |  2.942 ms | 451.03 ms |
| TUnit_AOT | 0.64.0  |  25.35 ms |  0.139 ms |  0.123 ms |  25.31 ms |



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
| TUnit     | 0.64.0  |   505.35 ms |  3.201 ms |  2.994 ms |   506.02 ms |
| NUnit     | 4.4.0   | 1,028.97 ms | 19.655 ms | 25.557 ms | 1,028.60 ms |
| xUnit     | 2.9.3   | 1,092.61 ms | 11.816 ms |  9.867 ms | 1,092.15 ms |
| MSTest    | 3.11.0  |   982.37 ms | 19.609 ms | 49.555 ms |   967.75 ms |
| xUnit3    | 3.1.0   |   504.73 ms |  4.017 ms |  3.561 ms |   504.79 ms |
| TUnit_AOT | 0.64.0  |    64.84 ms |  1.708 ms |  4.981 ms |    64.36 ms |



