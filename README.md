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
| Build_TUnit  | 0.64.0  | 1.111 s | 0.0516 s | 0.1463 s | 1.083 s |
| Build_NUnit  | 4.4.0   | 1.311 s | 0.1335 s | 0.3852 s | 1.227 s |
| Build_xUnit  | 2.9.3   | 1.611 s | 0.1288 s | 0.3717 s | 1.548 s |
| Build_MSTest | 3.11.0  | 1.326 s | 0.0701 s | 0.2067 s | 1.327 s |
| Build_xUnit3 | 3.1.0   | 1.429 s | 0.0863 s | 0.2505 s | 1.395 s |



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
| Build_TUnit  | 0.64.0  | 1.648 s | 0.0279 s | 0.0261 s | 1.641 s |
| Build_NUnit  | 4.4.0   | 1.485 s | 0.0190 s | 0.0169 s | 1.485 s |
| Build_xUnit  | 2.9.3   | 1.506 s | 0.0099 s | 0.0082 s | 1.504 s |
| Build_MSTest | 3.11.0  | 1.523 s | 0.0181 s | 0.0169 s | 1.521 s |
| Build_xUnit3 | 3.1.0   | 1.490 s | 0.0203 s | 0.0190 s | 1.498 s |



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
| Build_TUnit  | 0.64.0  | 1.858 s | 0.0360 s | 0.0353 s | 1.859 s |
| Build_NUnit  | 4.4.0   | 1.686 s | 0.0224 s | 0.0198 s | 1.683 s |
| Build_xUnit  | 2.9.3   | 1.691 s | 0.0323 s | 0.0331 s | 1.703 s |
| Build_MSTest | 3.11.0  | 1.722 s | 0.0109 s | 0.0097 s | 1.725 s |
| Build_xUnit3 | 3.1.0   | 1.678 s | 0.0319 s | 0.0380 s | 1.670 s |


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
| Method    | Version | Mean      | Error    | StdDev    | Median    |
|---------- |-------- |----------:|---------:|----------:|----------:|
| TUnit     | 0.64.0  | 325.18 ms | 6.348 ms |  7.557 ms | 326.66 ms |
| NUnit     | 4.4.0   |        NA |       NA |        NA |        NA |
| xUnit     | 2.9.3   |        NA |       NA |        NA |        NA |
| MSTest    | 3.11.0  |        NA |       NA |        NA |        NA |
| xUnit3    | 3.1.0   | 303.80 ms | 4.770 ms |  4.229 ms | 303.40 ms |
| TUnit_AOT | 0.64.0  |  46.25 ms | 4.023 ms | 11.673 ms |  44.01 ms |

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
| TUnit     | 0.64.0  |   497.49 ms |  5.640 ms |  5.000 ms |   497.99 ms |
| NUnit     | 4.4.0   |   922.87 ms | 11.926 ms | 11.156 ms |   921.91 ms |
| xUnit     | 2.9.3   | 1,005.27 ms | 16.491 ms | 15.426 ms | 1,003.43 ms |
| MSTest    | 3.11.0  |   865.08 ms | 16.999 ms | 21.498 ms |   866.98 ms |
| xUnit3    | 3.1.0   |   463.75 ms |  4.643 ms |  4.343 ms |   462.67 ms |
| TUnit_AOT | 0.64.0  |    26.25 ms |  0.429 ms |  0.459 ms |    26.16 ms |



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
| TUnit     | 0.64.0  |   534.99 ms |  3.918 ms |  3.474 ms |   534.59 ms |
| NUnit     | 4.4.0   |   995.67 ms | 16.021 ms | 14.986 ms | 1,000.87 ms |
| xUnit     | 2.9.3   | 1,068.76 ms | 20.819 ms | 19.475 ms | 1,076.02 ms |
| MSTest    | 3.11.0  |   926.87 ms | 18.109 ms | 16.939 ms |   926.37 ms |
| xUnit3    | 3.1.0   |   501.72 ms |  3.023 ms |  2.680 ms |   500.98 ms |
| TUnit_AOT | 0.64.0  |    63.17 ms |  1.451 ms |  4.234 ms |    63.04 ms |


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
| Method    | Version | Mean     | Error    | StdDev   | Median   |
|---------- |-------- |---------:|---------:|---------:|---------:|
| TUnit     | 0.64.0  | 485.9 ms | 25.94 ms | 75.66 ms | 488.7 ms |
| NUnit     | 4.4.0   |       NA |       NA |       NA |       NA |
| xUnit     | 2.9.3   |       NA |       NA |       NA |       NA |
| MSTest    | 3.11.0  |       NA |       NA |       NA |       NA |
| xUnit3    | 3.1.0   | 490.0 ms | 25.06 ms | 73.90 ms | 481.7 ms |
| TUnit_AOT | 0.64.0  | 134.5 ms | 15.66 ms | 46.18 ms | 131.6 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.82GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   454.80 ms |  2.698 ms |  2.253 ms |   454.02 ms |
| NUnit     | 4.4.0   |   927.09 ms | 18.122 ms | 22.256 ms |   919.64 ms |
| xUnit     | 2.9.3   | 1,003.81 ms | 13.252 ms | 12.396 ms | 1,000.44 ms |
| MSTest    | 3.11.0  |   858.55 ms | 16.180 ms | 15.135 ms |   858.24 ms |
| xUnit3    | 3.1.0   |   466.88 ms |  2.914 ms |  2.726 ms |   466.55 ms |
| TUnit_AOT | 0.64.0  |    27.80 ms |  0.544 ms |  0.558 ms |    27.54 ms |



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
| TUnit     | 0.64.0  |   495.06 ms |  5.583 ms |  4.949 ms |   494.85 ms |
| NUnit     | 4.4.0   | 1,002.53 ms | 19.674 ms | 19.323 ms | 1,002.87 ms |
| xUnit     | 2.9.3   | 1,084.78 ms | 20.385 ms | 19.068 ms | 1,082.33 ms |
| MSTest    | 3.11.0  |   945.00 ms | 18.215 ms | 17.890 ms |   946.46 ms |
| xUnit3    | 3.1.0   |   515.64 ms | 10.065 ms |  9.415 ms |   512.69 ms |
| TUnit_AOT | 0.64.0  |    89.24 ms |  2.685 ms |  7.917 ms |    89.53 ms |


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
| Method    | Version | Mean      | Error    | StdDev   | Median    |
|---------- |-------- |----------:|---------:|---------:|----------:|
| TUnit     | 0.64.0  | 325.41 ms | 12.10 ms | 35.49 ms | 321.56 ms |
| NUnit     | 4.4.0   |        NA |       NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |       NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |       NA |       NA |        NA |
| xUnit3    | 3.1.0   | 403.62 ms | 27.44 ms | 77.84 ms | 394.05 ms |
| TUnit_AOT | 0.64.0  |  83.11 ms | 14.27 ms | 39.56 ms |  76.19 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
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
| TUnit     | 0.64.0  | 466.59 ms |  4.026 ms |  3.766 ms | 467.04 ms |
| NUnit     | 4.4.0   | 916.48 ms | 15.601 ms | 13.830 ms | 917.10 ms |
| xUnit     | 2.9.3   | 987.47 ms | 18.559 ms | 17.360 ms | 981.29 ms |
| MSTest    | 3.11.0  | 844.80 ms | 15.748 ms | 14.731 ms | 847.11 ms |
| xUnit3    | 3.1.0   | 460.04 ms |  5.065 ms |  4.490 ms | 459.75 ms |
| TUnit_AOT | 0.64.0  |  25.31 ms |  0.287 ms |  0.269 ms |  25.26 ms |



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
| TUnit     | 0.64.0  |   526.58 ms |  4.112 ms |  3.645 ms |   526.68 ms |
| NUnit     | 4.4.0   | 1,035.52 ms | 19.778 ms | 19.425 ms | 1,034.79 ms |
| xUnit     | 2.9.3   | 1,098.64 ms | 19.084 ms | 17.851 ms | 1,098.55 ms |
| MSTest    | 3.11.0  |   981.83 ms | 19.178 ms | 34.582 ms |   973.45 ms |
| xUnit3    | 3.1.0   |   518.52 ms |  2.506 ms |  2.221 ms |   518.80 ms |
| TUnit_AOT | 0.64.0  |    66.13 ms |  1.458 ms |  4.275 ms |    65.12 ms |


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
| Method    | Version | Mean        | Error      | StdDev    | Median      |
|---------- |-------- |------------:|-----------:|----------:|------------:|
| TUnit     | 0.64.0  |   547.54 ms |  29.131 ms |  84.98 ms |   548.00 ms |
| NUnit     | 4.4.0   | 1,221.67 ms | 122.542 ms | 359.39 ms | 1,159.19 ms |
| xUnit     | 2.9.3   |          NA |         NA |        NA |          NA |
| MSTest    | 3.11.0  | 1,002.08 ms |  91.167 ms | 268.81 ms |   951.01 ms |
| xUnit3    | 3.1.0   |   557.32 ms |  40.536 ms | 118.89 ms |   535.79 ms |
| TUnit_AOT | 0.64.0  |    62.89 ms |   6.689 ms |  19.62 ms |    59.00 ms |

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
| TUnit     | 0.64.0  |   474.87 ms |  4.744 ms |  4.437 ms |   474.18 ms |
| NUnit     | 4.4.0   |   949.88 ms | 18.695 ms | 20.780 ms |   946.77 ms |
| xUnit     | 2.9.3   | 1,030.00 ms | 19.502 ms | 20.027 ms | 1,026.65 ms |
| MSTest    | 3.11.0  |   876.39 ms | 12.284 ms | 11.491 ms |   876.59 ms |
| xUnit3    | 3.1.0   |   487.37 ms |  4.094 ms |  3.830 ms |   487.98 ms |
| TUnit_AOT | 0.64.0  |    28.92 ms |  0.328 ms |  0.291 ms |    28.94 ms |



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
| TUnit     | 0.64.0  |   520.55 ms |  9.248 ms |  8.650 ms |   516.75 ms |
| NUnit     | 4.4.0   | 1,009.05 ms | 19.512 ms | 19.164 ms | 1,003.38 ms |
| xUnit     | 2.9.3   | 1,086.47 ms | 20.879 ms | 19.530 ms | 1,090.20 ms |
| MSTest    | 3.11.0  |   941.26 ms | 18.727 ms | 18.392 ms |   942.27 ms |
| xUnit3    | 3.1.0   |   552.22 ms | 10.126 ms |  9.472 ms |   550.57 ms |
| TUnit_AOT | 0.64.0  |    66.98 ms |  1.703 ms |  4.968 ms |    65.88 ms |


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
| TUnit     | 0.64.0  | 497.76 ms | 25.547 ms | 72.47 ms | 491.80 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 410.11 ms | 16.292 ms | 47.52 ms | 409.80 ms |
| TUnit_AOT | 0.64.0  |  65.94 ms |  9.217 ms | 27.03 ms |  60.60 ms |

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
| TUnit     | 0.64.0  | 452.84 ms |  3.126 ms |  2.924 ms | 453.00 ms |
| NUnit     | 4.4.0   | 912.29 ms | 16.031 ms | 14.995 ms | 917.53 ms |
| xUnit     | 2.9.3   | 985.55 ms | 18.582 ms | 17.381 ms | 988.44 ms |
| MSTest    | 3.11.0  | 840.18 ms | 15.644 ms | 14.633 ms | 848.22 ms |
| xUnit3    | 3.1.0   | 450.35 ms |  3.537 ms |  3.135 ms | 450.26 ms |
| TUnit_AOT | 0.64.0  |  43.51 ms |  1.031 ms |  3.041 ms |  43.82 ms |



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
| TUnit     | 0.64.0  |   507.72 ms |  3.853 ms |  3.416 ms |   506.59 ms |
| NUnit     | 4.4.0   | 1,028.31 ms | 20.373 ms | 23.461 ms | 1,037.15 ms |
| xUnit     | 2.9.3   | 1,076.48 ms | 17.822 ms | 16.671 ms | 1,080.10 ms |
| MSTest    | 3.11.0  |   947.12 ms | 18.808 ms | 23.098 ms |   944.90 ms |
| xUnit3    | 3.1.0   |   504.30 ms |  6.598 ms |  6.172 ms |   500.83 ms |
| TUnit_AOT | 0.64.0  |    90.61 ms |  1.808 ms |  3.773 ms |    91.53 ms |


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
| TUnit     | 0.64.0  | 304.74 ms |  6.084 ms | 16.03 ms | 300.24 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 317.11 ms | 14.434 ms | 41.88 ms | 302.42 ms |
| TUnit_AOT | 0.64.0  |  38.80 ms |  4.317 ms | 12.32 ms |  32.84 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.65GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   445.72 ms |  3.321 ms |  3.107 ms |   445.64 ms |
| NUnit     | 4.4.0   |   922.38 ms | 18.002 ms | 16.839 ms |   925.10 ms |
| xUnit     | 2.9.3   | 1,014.66 ms | 18.639 ms | 17.435 ms | 1,012.34 ms |
| MSTest    | 3.11.0  |   861.98 ms | 16.247 ms | 15.197 ms |   856.06 ms |
| xUnit3    | 3.1.0   |   453.16 ms |  1.932 ms |  1.807 ms |   452.53 ms |
| TUnit_AOT | 0.64.0  |    25.23 ms |  0.122 ms |  0.108 ms |    25.26 ms |



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
| TUnit     | 0.64.0  |   504.59 ms |  7.032 ms |  6.234 ms |   504.05 ms |
| NUnit     | 4.4.0   | 1,021.67 ms | 14.099 ms | 13.188 ms | 1,018.41 ms |
| xUnit     | 2.9.3   | 1,089.87 ms | 18.578 ms | 17.378 ms | 1,095.75 ms |
| MSTest    | 3.11.0  |   953.17 ms | 15.782 ms | 26.799 ms |   948.62 ms |
| xUnit3    | 3.1.0   |   512.92 ms | 10.032 ms | 20.265 ms |   501.88 ms |
| TUnit_AOT | 0.64.0  |    64.69 ms |  2.061 ms |  6.077 ms |    64.07 ms |


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
| Method    | Version | Mean      | Error    | StdDev   | Median    |
|---------- |-------- |----------:|---------:|---------:|----------:|
| TUnit     | 0.64.0  | 318.21 ms | 9.045 ms | 26.67 ms | 314.50 ms |
| NUnit     | 4.4.0   |        NA |       NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |       NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |       NA |       NA |        NA |
| xUnit3    | 3.1.0   | 316.49 ms | 8.569 ms | 25.27 ms | 313.59 ms |
| TUnit_AOT | 0.64.0  |  70.41 ms | 6.708 ms | 19.78 ms |  64.50 ms |

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
| TUnit     | 0.64.0  |   494.46 ms |  7.050 ms |  6.595 ms |   492.45 ms |
| NUnit     | 4.4.0   |   952.94 ms | 10.708 ms |  8.941 ms |   952.17 ms |
| xUnit     | 2.9.3   | 1,108.51 ms | 21.119 ms | 22.597 ms | 1,104.20 ms |
| MSTest    | 3.11.0  |   872.12 ms | 15.669 ms | 14.656 ms |   871.06 ms |
| xUnit3    | 3.1.0   |   500.81 ms |  5.631 ms |  5.268 ms |   500.56 ms |
| TUnit_AOT | 0.64.0  |    41.46 ms |  0.566 ms |  0.530 ms |    41.30 ms |



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
| TUnit     | 0.64.0  |   523.41 ms |  3.404 ms |  3.018 ms |   523.39 ms |
| NUnit     | 4.4.0   |   984.01 ms | 18.452 ms | 17.260 ms |   981.06 ms |
| xUnit     | 2.9.3   | 1,120.35 ms | 15.598 ms | 14.590 ms | 1,118.41 ms |
| MSTest    | 3.11.0  |   916.65 ms | 17.326 ms | 19.257 ms |   919.17 ms |
| xUnit3    | 3.1.0   |   525.71 ms |  3.668 ms |  3.431 ms |   525.48 ms |
| TUnit_AOT | 0.64.0  |    73.12 ms |  0.988 ms |  0.825 ms |    73.38 ms |


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
| TUnit     | 0.64.0  | 296.49 ms |  7.956 ms | 22.18 ms | 294.82 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 360.79 ms | 25.567 ms | 75.38 ms | 349.03 ms |
| TUnit_AOT | 0.64.0  |  61.95 ms | 10.145 ms | 29.91 ms |  55.53 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.94GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 457.98 ms |  2.643 ms |  2.343 ms | 458.65 ms |
| NUnit     | 4.4.0   | 923.40 ms | 16.928 ms | 15.834 ms | 919.55 ms |
| xUnit     | 2.9.3   | 996.33 ms | 13.467 ms | 12.597 ms | 988.73 ms |
| MSTest    | 3.11.0  | 859.77 ms | 16.197 ms | 15.908 ms | 858.04 ms |
| xUnit3    | 3.1.0   | 456.68 ms |  4.540 ms |  4.247 ms | 455.66 ms |
| TUnit_AOT | 0.64.0  |  26.03 ms |  0.307 ms |  0.287 ms |  25.98 ms |



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
| TUnit     | 0.64.0  |   503.69 ms |  5.503 ms |  5.148 ms |   502.11 ms |
| NUnit     | 4.4.0   | 1,000.51 ms | 15.596 ms | 14.589 ms |   997.44 ms |
| xUnit     | 2.9.3   | 1,069.27 ms | 20.997 ms | 20.622 ms | 1,068.40 ms |
| MSTest    | 3.11.0  |   934.86 ms | 18.200 ms | 18.690 ms |   935.38 ms |
| xUnit3    | 3.1.0   |   500.79 ms |  7.562 ms |  7.073 ms |   500.75 ms |
| TUnit_AOT | 0.64.0  |    63.39 ms |  1.748 ms |  5.155 ms |    62.55 ms |



