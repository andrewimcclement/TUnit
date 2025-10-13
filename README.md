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
| Method       | Version | Mean       | Error    | StdDev    | Median     |
|------------- |-------- |-----------:|---------:|----------:|-----------:|
| Build_TUnit  | 0.64.0  | 1,018.5 ms | 33.06 ms |  96.95 ms | 1,025.5 ms |
| Build_NUnit  | 4.4.0   |   984.8 ms | 35.23 ms | 103.88 ms |   954.1 ms |
| Build_xUnit  | 2.9.3   |   921.1 ms | 25.25 ms |  73.67 ms |   904.3 ms |
| Build_MSTest | 3.11.0  | 1,005.8 ms | 29.63 ms |  87.37 ms |   987.3 ms |
| Build_xUnit3 | 3.1.0   |   929.8 ms | 29.21 ms |  85.68 ms |   919.3 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.45GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.64.0  | 1.673 s | 0.0318 s | 0.0297 s | 1.664 s |
| Build_NUnit  | 4.4.0   | 1.508 s | 0.0155 s | 0.0145 s | 1.510 s |
| Build_xUnit  | 2.9.3   | 1.522 s | 0.0196 s | 0.0174 s | 1.523 s |
| Build_MSTest | 3.11.0  | 1.549 s | 0.0138 s | 0.0129 s | 1.544 s |
| Build_xUnit3 | 3.1.0   | 1.516 s | 0.0176 s | 0.0156 s | 1.518 s |



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
| Build_TUnit  | 0.64.0  | 2.185 s | 0.0432 s | 0.1091 s | 2.178 s |
| Build_NUnit  | 4.4.0   | 1.998 s | 0.0377 s | 0.0434 s | 2.008 s |
| Build_xUnit  | 2.9.3   | 2.018 s | 0.0393 s | 0.0497 s | 2.037 s |
| Build_MSTest | 3.11.0  | 2.078 s | 0.0410 s | 0.0488 s | 2.071 s |
| Build_xUnit3 | 3.1.0   | 2.011 s | 0.0398 s | 0.0557 s | 2.002 s |


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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   317.67 ms |  9.584 ms |  27.34 ms |   313.73 ms |
| NUnit     | 4.4.0   |   916.13 ms | 93.762 ms | 272.02 ms |   851.33 ms |
| xUnit     | 2.9.3   | 1,186.99 ms | 51.584 ms | 152.10 ms | 1,181.42 ms |
| MSTest    | 3.11.0  |   964.94 ms | 32.388 ms |  91.35 ms |   968.95 ms |
| xUnit3    | 3.1.0   |   520.93 ms | 18.356 ms |  53.55 ms |   512.24 ms |
| TUnit_AOT | 0.64.0  |    60.13 ms |  8.712 ms |  24.86 ms |    55.92 ms |



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
| TUnit     | 0.64.0  |   521.68 ms |  4.556 ms |  4.262 ms |   520.72 ms |
| NUnit     | 4.4.0   |   974.32 ms | 14.715 ms | 13.764 ms |   980.78 ms |
| xUnit     | 2.9.3   | 1,064.52 ms | 17.904 ms | 16.748 ms | 1,060.21 ms |
| MSTest    | 3.11.0  |   899.95 ms | 17.004 ms | 17.462 ms |   904.00 ms |
| xUnit3    | 3.1.0   |   486.09 ms |  3.682 ms |  3.444 ms |   486.55 ms |
| TUnit_AOT | 0.64.0  |    28.57 ms |  0.545 ms |  0.583 ms |    28.52 ms |



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
| TUnit     | 0.64.0  |   566.44 ms |  6.936 ms |  6.148 ms |   567.36 ms |
| NUnit     | 4.4.0   | 1,101.83 ms | 21.729 ms | 35.089 ms | 1,105.34 ms |
| xUnit     | 2.9.3   | 1,180.51 ms | 23.540 ms | 32.222 ms | 1,177.27 ms |
| MSTest    | 3.11.0  | 1,019.05 ms | 26.253 ms | 77.407 ms | 1,025.45 ms |
| xUnit3    | 3.1.0   |   504.12 ms | 10.038 ms |  9.390 ms |   500.31 ms |
| TUnit_AOT | 0.64.0  |    65.14 ms |  1.504 ms |  4.118 ms |    65.44 ms |


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
| Method    | Version | Mean       | Error    | StdDev    | Median   |
|---------- |-------- |-----------:|---------:|----------:|---------:|
| TUnit     | 0.64.0  |   517.8 ms | 29.59 ms |  87.26 ms | 524.0 ms |
| NUnit     | 4.4.0   |         NA |       NA |        NA |       NA |
| xUnit     | 2.9.3   | 1,025.6 ms | 71.26 ms | 208.99 ms | 977.5 ms |
| MSTest    | 3.11.0  |         NA |       NA |        NA |       NA |
| xUnit3    | 3.1.0   |   439.7 ms | 28.94 ms |  84.41 ms | 421.6 ms |
| TUnit_AOT | 0.64.0  |   101.6 ms |  8.64 ms |  25.05 ms | 100.5 ms |

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
| TUnit     | 0.64.0  |   455.26 ms |  3.750 ms |  3.325 ms |   455.03 ms |
| NUnit     | 4.4.0   |   926.03 ms | 14.797 ms | 13.842 ms |   923.73 ms |
| xUnit     | 2.9.3   | 1,007.71 ms | 18.370 ms | 17.183 ms | 1,005.53 ms |
| MSTest    | 3.11.0  |   863.06 ms | 16.439 ms | 16.882 ms |   864.74 ms |
| xUnit3    | 3.1.0   |   470.07 ms |  5.843 ms |  5.465 ms |   468.22 ms |
| TUnit_AOT | 0.64.0  |    28.02 ms |  0.552 ms |  0.937 ms |    27.95 ms |



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
| TUnit     | 0.64.0  |   498.00 ms |  8.564 ms |  7.591 ms |   497.60 ms |
| NUnit     | 4.4.0   | 1,010.77 ms | 19.675 ms | 18.404 ms | 1,008.37 ms |
| xUnit     | 2.9.3   | 1,092.53 ms | 21.439 ms | 25.521 ms | 1,091.76 ms |
| MSTest    | 3.11.0  |   965.08 ms | 18.452 ms | 23.335 ms |   965.74 ms |
| xUnit3    | 3.1.0   |   530.91 ms |  8.651 ms |  8.092 ms |   533.02 ms |
| TUnit_AOT | 0.64.0  |    89.30 ms |  2.380 ms |  7.016 ms |    89.61 ms |


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
| TUnit     | 0.64.0  | 485.61 ms | 22.892 ms | 67.50 ms | 484.52 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 491.80 ms | 14.885 ms | 42.71 ms | 495.07 ms |
| TUnit_AOT | 0.64.0  |  71.00 ms |  9.107 ms | 26.56 ms |  64.25 ms |

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
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 466.00 ms |  2.948 ms |  2.613 ms | 466.04 ms |
| NUnit     | 4.4.0   | 924.94 ms | 16.975 ms | 15.048 ms | 925.33 ms |
| xUnit     | 2.9.3   | 992.87 ms | 18.274 ms | 17.094 ms | 985.79 ms |
| MSTest    | 3.11.0  | 845.81 ms | 16.599 ms | 17.046 ms | 840.55 ms |
| xUnit3    | 3.1.0   | 455.99 ms |  1.607 ms |  1.255 ms | 456.38 ms |
| TUnit_AOT | 0.64.0  |  24.99 ms |  0.218 ms |  0.194 ms |  24.94 ms |



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
| TUnit     | 0.64.0  |   533.05 ms |  9.775 ms |  8.162 ms |   529.78 ms |
| NUnit     | 4.4.0   | 1,002.38 ms | 19.572 ms | 20.099 ms |   995.69 ms |
| xUnit     | 2.9.3   | 1,073.88 ms | 21.203 ms | 19.833 ms | 1,071.68 ms |
| MSTest    | 3.11.0  |   941.19 ms | 18.520 ms | 19.018 ms |   942.73 ms |
| xUnit3    | 3.1.0   |   514.12 ms | 10.243 ms | 15.642 ms |   507.02 ms |
| TUnit_AOT | 0.64.0  |    63.48 ms |  1.595 ms |  4.627 ms |    62.38 ms |


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
| TUnit     | 0.64.0  | 323.28 ms | 12.967 ms | 36.36 ms | 321.62 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 373.29 ms | 27.466 ms | 80.98 ms | 346.85 ms |
| TUnit_AOT | 0.64.0  |  51.61 ms |  5.309 ms | 15.57 ms |  43.23 ms |

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
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 463.89 ms |  2.187 ms |  1.938 ms | 464.13 ms |
| NUnit     | 4.4.0   | 908.14 ms | 18.061 ms | 16.895 ms | 906.88 ms |
| xUnit     | 2.9.3   | 989.54 ms | 18.778 ms | 17.565 ms | 980.85 ms |
| MSTest    | 3.11.0  | 835.27 ms | 14.269 ms | 13.347 ms | 839.94 ms |
| xUnit3    | 3.1.0   | 473.07 ms |  4.244 ms |  3.762 ms | 472.46 ms |
| TUnit_AOT | 0.64.0  |  26.57 ms |  0.204 ms |  0.171 ms |  26.56 ms |



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
| TUnit     | 0.64.0  |   564.22 ms | 11.114 ms | 17.304 ms |   565.41 ms |
| NUnit     | 4.4.0   | 1,001.88 ms | 13.992 ms | 18.193 ms | 1,000.75 ms |
| xUnit     | 2.9.3   | 1,086.11 ms | 17.105 ms | 14.283 ms | 1,087.33 ms |
| MSTest    | 3.11.0  |   935.03 ms | 18.257 ms | 18.749 ms |   938.73 ms |
| xUnit3    | 3.1.0   |   522.61 ms |  4.975 ms |  4.654 ms |   522.34 ms |
| TUnit_AOT | 0.64.0  |    65.04 ms |  1.666 ms |  4.912 ms |    63.91 ms |


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
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 349.53 ms | 12.932 ms | 36.89 ms | 338.82 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   | 800.29 ms | 34.189 ms | 99.19 ms | 791.88 ms |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 362.48 ms | 14.969 ms | 44.14 ms | 359.61 ms |
| TUnit_AOT | 0.64.0  |  74.40 ms |  9.405 ms | 27.58 ms |  72.32 ms |

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
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 454.89 ms |  3.442 ms |  3.220 ms | 454.89 ms |
| NUnit     | 4.4.0   | 910.45 ms | 17.380 ms | 18.596 ms | 910.64 ms |
| xUnit     | 2.9.3   | 982.85 ms | 19.350 ms | 18.100 ms | 981.93 ms |
| MSTest    | 3.11.0  | 846.75 ms | 16.749 ms | 17.200 ms | 851.80 ms |
| xUnit3    | 3.1.0   | 449.80 ms |  2.341 ms |  1.955 ms | 449.99 ms |
| TUnit_AOT | 0.64.0  |  43.43 ms |  1.086 ms |  3.203 ms |  44.21 ms |



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
| TUnit     | 0.64.0  |   503.76 ms |  9.902 ms | 14.514 ms |   497.05 ms |
| NUnit     | 4.4.0   |   999.87 ms | 15.350 ms | 14.358 ms |   995.61 ms |
| xUnit     | 2.9.3   | 1,085.02 ms | 21.606 ms | 25.720 ms | 1,087.96 ms |
| MSTest    | 3.11.0  |   981.73 ms | 18.779 ms | 34.339 ms |   971.76 ms |
| xUnit3    | 3.1.0   |   502.71 ms |  9.274 ms | 12.059 ms |   498.19 ms |
| TUnit_AOT | 0.64.0  |    82.83 ms |  2.386 ms |  7.035 ms |    83.61 ms |


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
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit     | 0.64.0  | 289.75 ms | 13.238 ms | 38.41 ms | 274.21 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   | 769.88 ms | 26.363 ms | 76.48 ms | 775.18 ms |
| MSTest    | 3.11.0  | 683.07 ms | 19.715 ms | 57.51 ms | 690.50 ms |
| xUnit3    | 3.1.0   | 360.15 ms | 10.901 ms | 31.10 ms | 359.96 ms |
| TUnit_AOT | 0.64.0  |  50.31 ms |  5.409 ms | 15.52 ms |  43.02 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.79GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   455.42 ms |  3.883 ms |  3.632 ms |   456.10 ms |
| NUnit     | 4.4.0   |   930.38 ms | 12.236 ms | 11.446 ms |   935.74 ms |
| xUnit     | 2.9.3   | 1,004.54 ms | 19.523 ms | 18.262 ms | 1,003.99 ms |
| MSTest    | 3.11.0  |   851.83 ms | 15.503 ms | 14.502 ms |   845.51 ms |
| xUnit3    | 3.1.0   |   458.94 ms |  2.914 ms |  2.726 ms |   458.02 ms |
| TUnit_AOT | 0.64.0  |    25.32 ms |  0.179 ms |  0.159 ms |    25.37 ms |



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
| TUnit     | 0.64.0  |   523.85 ms |  8.653 ms |  7.671 ms |   523.24 ms |
| NUnit     | 4.4.0   | 1,095.73 ms | 21.046 ms | 23.393 ms | 1,101.18 ms |
| xUnit     | 2.9.3   | 1,196.65 ms | 23.486 ms | 38.589 ms | 1,202.95 ms |
| MSTest    | 3.11.0  | 1,004.53 ms | 19.839 ms | 37.746 ms | 1,003.16 ms |
| xUnit3    | 3.1.0   |   530.95 ms | 10.028 ms |  9.380 ms |   529.82 ms |
| TUnit_AOT | 0.64.0  |    67.83 ms |  1.523 ms |  4.490 ms |    67.52 ms |


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
| TUnit     | 0.64.0  |   469.32 ms | 31.922 ms |  94.12 ms |   464.98 ms |
| NUnit     | 4.4.0   |          NA |        NA |        NA |          NA |
| xUnit     | 2.9.3   | 1,118.87 ms | 93.858 ms | 276.74 ms | 1,048.25 ms |
| MSTest    | 3.11.0  |          NA |        NA |        NA |          NA |
| xUnit3    | 3.1.0   |   412.91 ms | 21.373 ms |  59.93 ms |   409.45 ms |
| TUnit_AOT | 0.64.0  |    50.79 ms |  5.795 ms |  16.44 ms |    44.28 ms |

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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   490.17 ms |  6.299 ms |  5.584 ms |   489.46 ms |
| NUnit     | 4.4.0   |   938.48 ms | 18.436 ms | 20.491 ms |   939.16 ms |
| xUnit     | 2.9.3   | 1,083.32 ms | 21.255 ms | 23.625 ms | 1,082.24 ms |
| MSTest    | 3.11.0  |   869.85 ms | 17.126 ms | 22.268 ms |   865.18 ms |
| xUnit3    | 3.1.0   |   497.46 ms |  6.307 ms |  5.591 ms |   498.25 ms |
| TUnit_AOT | 0.64.0  |    41.11 ms |  0.528 ms |  0.494 ms |    41.20 ms |



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
| TUnit     | 0.64.0  |   535.66 ms |  8.656 ms |  8.097 ms |   535.84 ms |
| NUnit     | 4.4.0   |   994.99 ms | 17.772 ms | 16.624 ms |   993.20 ms |
| xUnit     | 2.9.3   | 1,143.02 ms | 12.919 ms | 12.084 ms | 1,139.83 ms |
| MSTest    | 3.11.0  |   924.05 ms | 18.146 ms | 20.169 ms |   916.45 ms |
| xUnit3    | 3.1.0   |   535.56 ms |  4.872 ms |  4.319 ms |   536.16 ms |
| TUnit_AOT | 0.64.0  |    74.51 ms |  0.857 ms |  0.760 ms |    74.81 ms |


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
| TUnit     | 0.64.0  | 278.33 ms |  5.435 ms |  9.081 ms | 281.49 ms |
| NUnit     | 4.4.0   | 570.53 ms | 11.392 ms | 23.779 ms | 567.84 ms |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  | 539.08 ms | 10.697 ms | 19.290 ms | 541.07 ms |
| xUnit3    | 3.1.0   | 285.79 ms |  5.695 ms |  5.327 ms | 283.36 ms |
| TUnit_AOT | 0.64.0  |  40.87 ms |  5.794 ms | 16.717 ms |  28.83 ms |

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
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 454.83 ms |  4.408 ms |  4.123 ms | 454.55 ms |
| NUnit     | 4.4.0   | 906.64 ms | 17.134 ms | 16.027 ms | 909.68 ms |
| xUnit     | 2.9.3   | 975.40 ms | 15.486 ms | 14.486 ms | 970.00 ms |
| MSTest    | 3.11.0  | 839.46 ms | 16.284 ms | 17.424 ms | 837.99 ms |
| xUnit3    | 3.1.0   | 445.83 ms |  2.013 ms |  1.784 ms | 446.27 ms |
| TUnit_AOT | 0.64.0  |  24.79 ms |  0.214 ms |  0.190 ms |  24.75 ms |



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
| TUnit     | 0.64.0  |   524.55 ms |  4.851 ms |  4.538 ms |   523.98 ms |
| NUnit     | 4.4.0   | 1,082.34 ms | 21.421 ms | 57.914 ms | 1,072.74 ms |
| xUnit     | 2.9.3   | 1,156.69 ms | 22.938 ms | 61.227 ms | 1,155.34 ms |
| MSTest    | 3.11.0  | 1,020.45 ms | 19.703 ms | 24.918 ms | 1,025.77 ms |
| xUnit3    | 3.1.0   |   534.30 ms | 10.569 ms | 10.854 ms |   530.82 ms |
| TUnit_AOT | 0.64.0  |    69.10 ms |  1.739 ms |  5.101 ms |    68.95 ms |



