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
| Build_TUnit  | 0.64.0  | 1,036.3 ms | 34.98 ms | 103.13 ms | 1,025.1 ms |
| Build_NUnit  | 4.4.0   |   980.1 ms | 32.41 ms |  95.56 ms |   941.8 ms |
| Build_xUnit  | 2.9.3   |   918.8 ms | 24.34 ms |  71.76 ms |   905.0 ms |
| Build_MSTest | 3.11.0  | 1,000.2 ms | 34.70 ms | 102.30 ms |   970.1 ms |
| Build_xUnit3 | 3.1.0   |   934.6 ms | 24.10 ms |  71.06 ms |   923.4 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.60GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.64.0  | 1.722 s | 0.0330 s | 0.0353 s | 1.713 s |
| Build_NUnit  | 4.4.0   | 1.549 s | 0.0159 s | 0.0141 s | 1.550 s |
| Build_xUnit  | 2.9.3   | 1.570 s | 0.0210 s | 0.0197 s | 1.570 s |
| Build_MSTest | 3.11.0  | 1.582 s | 0.0131 s | 0.0117 s | 1.583 s |
| Build_xUnit3 | 3.1.0   | 1.552 s | 0.0200 s | 0.0188 s | 1.549 s |



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
| Build_TUnit  | 0.64.0  | 1.865 s | 0.0226 s | 0.0188 s | 1.868 s |
| Build_NUnit  | 4.4.0   | 1.658 s | 0.0206 s | 0.0193 s | 1.653 s |
| Build_xUnit  | 2.9.3   | 1.660 s | 0.0222 s | 0.0208 s | 1.653 s |
| Build_MSTest | 3.11.0  | 1.733 s | 0.0264 s | 0.0247 s | 1.731 s |
| Build_xUnit3 | 3.1.0   | 1.642 s | 0.0117 s | 0.0098 s | 1.645 s |


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
| TUnit     | 0.64.0  | 394.68 ms | 23.090 ms | 67.35 ms | 384.28 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  | 761.12 ms | 30.295 ms | 86.43 ms | 755.94 ms |
| xUnit3    | 3.1.0   | 400.49 ms | 22.521 ms | 66.40 ms | 405.95 ms |
| TUnit_AOT | 0.64.0  |  54.18 ms |  8.117 ms | 23.81 ms |  51.16 ms |

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
| TUnit     | 0.64.0  | 491.90 ms |  3.115 ms |  2.914 ms | 492.43 ms |
| NUnit     | 4.4.0   | 918.40 ms | 14.296 ms | 13.373 ms | 918.62 ms |
| xUnit     | 2.9.3   | 998.20 ms | 14.629 ms | 13.684 ms | 993.25 ms |
| MSTest    | 3.11.0  | 851.57 ms | 16.394 ms | 16.101 ms | 850.92 ms |
| xUnit3    | 3.1.0   | 461.36 ms |  4.611 ms |  4.313 ms | 460.13 ms |
| TUnit_AOT | 0.64.0  |  25.07 ms |  0.144 ms |  0.112 ms |  25.06 ms |



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
| TUnit     | 0.64.0  |   535.53 ms |  4.265 ms |  3.990 ms |   535.40 ms |
| NUnit     | 4.4.0   | 1,001.32 ms | 19.835 ms | 20.369 ms | 1,005.14 ms |
| xUnit     | 2.9.3   | 1,064.79 ms | 13.945 ms | 13.044 ms | 1,060.13 ms |
| MSTest    | 3.11.0  |   934.15 ms | 18.441 ms | 19.732 ms |   934.42 ms |
| xUnit3    | 3.1.0   |   501.27 ms |  2.896 ms |  2.567 ms |   501.13 ms |
| TUnit_AOT | 0.64.0  |    66.31 ms |  1.839 ms |  5.306 ms |    66.16 ms |


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
| TUnit     | 0.64.0  |   530.3 ms | 31.11 ms |  89.76 ms |   518.8 ms |
| NUnit     | 4.4.0   | 1,091.1 ms | 53.54 ms | 157.01 ms | 1,107.0 ms |
| xUnit     | 2.9.3   | 1,156.1 ms | 56.36 ms | 165.30 ms | 1,149.6 ms |
| MSTest    | 3.11.0  |         NA |       NA |        NA |         NA |
| xUnit3    | 3.1.0   |   469.3 ms | 18.31 ms |  51.95 ms |   457.1 ms |
| TUnit_AOT | 0.64.0  |   121.2 ms | 12.60 ms |  37.15 ms |   118.8 ms |

Benchmarks with issues:
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
Intel Xeon Platinum 8370C CPU 2.80GHz (Max: 2.79GHz), 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v4
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v4

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 418.31 ms |  4.215 ms |  3.943 ms | 417.92 ms |
| NUnit     | 4.4.0   | 857.57 ms | 16.512 ms | 16.217 ms | 858.99 ms |
| xUnit     | 2.9.3   | 931.01 ms | 16.042 ms | 15.006 ms | 931.46 ms |
| MSTest    | 3.11.0  | 799.48 ms | 15.609 ms | 15.330 ms | 799.52 ms |
| xUnit3    | 3.1.0   | 439.19 ms |  5.380 ms |  4.769 ms | 438.49 ms |
| TUnit_AOT | 0.64.0  |  29.29 ms |  0.697 ms |  2.032 ms |  29.40 ms |



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
| TUnit     | 0.64.0  |   507.51 ms |  9.355 ms |  9.188 ms |   507.37 ms |
| NUnit     | 4.4.0   | 1,037.28 ms | 20.453 ms | 19.132 ms | 1,041.23 ms |
| xUnit     | 2.9.3   | 1,126.95 ms | 21.263 ms | 22.751 ms | 1,127.89 ms |
| MSTest    | 3.11.0  |   980.16 ms | 17.147 ms | 16.039 ms |   980.28 ms |
| xUnit3    | 3.1.0   |   536.02 ms | 10.177 ms | 10.451 ms |   536.85 ms |
| TUnit_AOT | 0.64.0  |    96.79 ms |  2.702 ms |  7.968 ms |    97.72 ms |


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
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 507.79 ms | 28.863 ms |  85.10 ms | 502.20 ms |
| NUnit     | 4.4.0   |        NA |        NA |        NA |        NA |
| xUnit     | 2.9.3   | 929.68 ms | 26.726 ms |  77.11 ms | 927.51 ms |
| MSTest    | 3.11.0  | 790.93 ms | 52.553 ms | 154.95 ms | 751.20 ms |
| xUnit3    | 3.1.0   | 367.86 ms | 15.682 ms |  45.75 ms | 374.43 ms |
| TUnit_AOT | 0.64.0  |  44.41 ms |  4.597 ms |  13.19 ms |  42.02 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)



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
| TUnit     | 0.64.0  | 461.65 ms |  3.388 ms |  3.169 ms | 460.82 ms |
| NUnit     | 4.4.0   | 911.11 ms | 17.853 ms | 17.534 ms | 909.08 ms |
| xUnit     | 2.9.3   | 979.59 ms | 18.573 ms | 18.241 ms | 980.28 ms |
| MSTest    | 3.11.0  | 837.62 ms | 16.401 ms | 15.342 ms | 843.85 ms |
| xUnit3    | 3.1.0   | 453.19 ms |  1.940 ms |  1.720 ms | 452.97 ms |
| TUnit_AOT | 0.64.0  |  24.52 ms |  0.193 ms |  0.161 ms |  24.52 ms |



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
| TUnit     | 0.64.0  |   515.18 ms |  4.526 ms |  4.012 ms |   514.49 ms |
| NUnit     | 4.4.0   | 1,006.19 ms | 19.566 ms | 18.302 ms | 1,004.66 ms |
| xUnit     | 2.9.3   | 1,062.21 ms | 16.165 ms | 15.121 ms | 1,063.33 ms |
| MSTest    | 3.11.0  |   922.34 ms | 18.203 ms | 20.963 ms |   914.14 ms |
| xUnit3    | 3.1.0   |   498.97 ms |  3.230 ms |  3.021 ms |   499.31 ms |
| TUnit_AOT | 0.64.0  |    62.31 ms |  1.608 ms |  4.741 ms |    61.68 ms |


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
| TUnit     | 0.64.0  |   456.74 ms |  29.278 ms |  85.87 ms |   467.54 ms |
| NUnit     | 4.4.0   | 1,242.30 ms | 121.176 ms | 355.39 ms | 1,205.21 ms |
| xUnit     | 2.9.3   |          NA |         NA |        NA |          NA |
| MSTest    | 3.11.0  |          NA |         NA |        NA |          NA |
| xUnit3    | 3.1.0   |   382.14 ms |  19.014 ms |  55.46 ms |   366.23 ms |
| TUnit_AOT | 0.64.0  |    48.35 ms |   5.553 ms |  16.11 ms |    44.39 ms |

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
| TUnit     | 0.64.0  | 457.15 ms |  2.875 ms |  2.689 ms | 458.18 ms |
| NUnit     | 4.4.0   | 899.12 ms | 14.134 ms | 13.221 ms | 898.03 ms |
| xUnit     | 2.9.3   | 951.55 ms | 18.883 ms | 17.663 ms | 954.50 ms |
| MSTest    | 3.11.0  | 822.07 ms | 16.224 ms | 22.208 ms | 824.43 ms |
| xUnit3    | 3.1.0   | 463.66 ms |  5.602 ms |  4.678 ms | 464.66 ms |
| TUnit_AOT | 0.64.0  |  25.69 ms |  0.514 ms |  0.549 ms |  25.86 ms |



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
| TUnit     | 0.64.0  |   526.98 ms |  7.102 ms |  6.296 ms |   527.47 ms |
| NUnit     | 4.4.0   | 1,047.24 ms | 18.084 ms | 15.101 ms | 1,050.56 ms |
| xUnit     | 2.9.3   | 1,113.29 ms | 22.013 ms | 20.591 ms | 1,106.40 ms |
| MSTest    | 3.11.0  |   978.78 ms | 19.315 ms | 25.785 ms |   977.97 ms |
| xUnit3    | 3.1.0   |   532.49 ms |  5.651 ms |  6.281 ms |   530.75 ms |
| TUnit_AOT | 0.64.0  |    66.34 ms |  1.400 ms |  4.063 ms |    65.70 ms |


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
| Method    | Version | Mean      | Error    | StdDev   | Median    |
|---------- |-------- |----------:|---------:|---------:|----------:|
| TUnit     | 0.64.0  | 335.87 ms | 10.08 ms | 29.71 ms | 326.63 ms |
| NUnit     | 4.4.0   |        NA |       NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |       NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |       NA |       NA |        NA |
| xUnit3    | 3.1.0   | 353.91 ms | 17.69 ms | 51.34 ms | 345.50 ms |
| TUnit_AOT | 0.64.0  |  91.62 ms | 11.79 ms | 34.75 ms |  88.93 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.61GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 446.13 ms |  2.909 ms |  2.429 ms | 447.10 ms |
| NUnit     | 4.4.0   | 896.38 ms | 17.767 ms | 16.619 ms | 895.69 ms |
| xUnit     | 2.9.3   | 970.12 ms | 15.844 ms | 14.821 ms | 964.55 ms |
| MSTest    | 3.11.0  | 830.65 ms | 15.753 ms | 16.178 ms | 837.60 ms |
| xUnit3    | 3.1.0   | 442.43 ms |  2.412 ms |  2.256 ms | 442.25 ms |
| TUnit_AOT | 0.64.0  |  44.23 ms |  1.170 ms |  3.448 ms |  44.95 ms |



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
| TUnit     | 0.64.0  |   491.49 ms |  4.709 ms |  4.405 ms |   491.03 ms |
| NUnit     | 4.4.0   |   982.52 ms | 16.341 ms | 15.286 ms |   976.50 ms |
| xUnit     | 2.9.3   | 1,049.13 ms | 19.380 ms | 18.128 ms | 1,043.66 ms |
| MSTest    | 3.11.0  |   915.72 ms | 17.126 ms | 16.020 ms |   909.67 ms |
| xUnit3    | 3.1.0   |   487.24 ms |  3.181 ms |  2.820 ms |   486.52 ms |
| TUnit_AOT | 0.64.0  |    76.40 ms |  1.906 ms |  5.619 ms |    76.49 ms |


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
| TUnit     | 0.64.0  | 412.77 ms | 21.018 ms |  59.62 ms | 404.22 ms |
| NUnit     | 4.4.0   |        NA |        NA |        NA |        NA |
| xUnit     | 2.9.3   | 730.64 ms | 29.156 ms |  82.71 ms | 713.88 ms |
| MSTest    | 3.11.0  | 846.79 ms | 67.073 ms | 192.44 ms | 788.09 ms |
| xUnit3    | 3.1.0   | 530.82 ms | 45.307 ms | 132.88 ms | 540.88 ms |
| TUnit_AOT | 0.64.0  |  58.94 ms |  9.792 ms |  27.94 ms |  47.02 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)



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
| TUnit     | 0.64.0  | 442.44 ms |  3.290 ms |  3.078 ms | 441.03 ms |
| NUnit     | 4.4.0   | 906.07 ms | 16.813 ms | 15.727 ms | 900.40 ms |
| xUnit     | 2.9.3   | 979.16 ms | 19.215 ms | 19.732 ms | 971.06 ms |
| MSTest    | 3.11.0  | 833.29 ms | 16.132 ms | 15.090 ms | 832.05 ms |
| xUnit3    | 3.1.0   | 446.90 ms |  1.594 ms |  1.491 ms | 447.31 ms |
| TUnit_AOT | 0.64.0  |  24.44 ms |  0.166 ms |  0.147 ms |  24.40 ms |



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
| TUnit     | 0.64.0  |   499.03 ms |  9.690 ms |  9.064 ms |   496.67 ms |
| NUnit     | 4.4.0   | 1,012.14 ms | 17.472 ms | 16.343 ms | 1,008.84 ms |
| xUnit     | 2.9.3   | 1,084.27 ms | 21.379 ms | 20.997 ms | 1,087.32 ms |
| MSTest    | 3.11.0  |   933.83 ms | 14.685 ms | 13.736 ms |   928.06 ms |
| xUnit3    | 3.1.0   |   501.87 ms |  3.623 ms |  3.389 ms |   501.22 ms |
| TUnit_AOT | 0.64.0  |    60.26 ms |  1.333 ms |  3.909 ms |    59.69 ms |


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
| TUnit     | 0.64.0  | 326.03 ms | 12.710 ms | 37.28 ms | 315.82 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 316.27 ms |  9.720 ms | 28.35 ms | 315.26 ms |
| TUnit_AOT | 0.64.0  |  75.37 ms |  8.502 ms | 24.93 ms |  68.02 ms |

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
| TUnit     | 0.64.0  |   482.24 ms |  3.078 ms |  2.729 ms |   481.97 ms |
| NUnit     | 4.4.0   |   919.71 ms | 18.273 ms | 18.765 ms |   908.28 ms |
| xUnit     | 2.9.3   | 1,075.50 ms | 17.901 ms | 16.745 ms | 1,073.75 ms |
| MSTest    | 3.11.0  |   848.13 ms | 16.783 ms | 16.483 ms |   846.58 ms |
| xUnit3    | 3.1.0   |   489.94 ms |  5.386 ms |  4.774 ms |   490.43 ms |
| TUnit_AOT | 0.64.0  |    40.75 ms |  0.370 ms |  0.309 ms |    40.75 ms |



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
| TUnit     | 0.64.0  |   526.98 ms |  3.545 ms |  2.961 ms |   526.76 ms |
| NUnit     | 4.4.0   |   993.14 ms | 19.148 ms | 19.664 ms |   985.45 ms |
| xUnit     | 2.9.3   | 1,148.77 ms | 18.289 ms | 29.533 ms | 1,144.20 ms |
| MSTest    | 3.11.0  |   920.67 ms | 17.502 ms | 16.371 ms |   914.57 ms |
| xUnit3    | 3.1.0   |   528.65 ms |  2.168 ms |  1.810 ms |   528.39 ms |
| TUnit_AOT | 0.64.0  |    71.79 ms |  0.844 ms |  0.748 ms |    71.86 ms |


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
| TUnit     | 0.64.0  | 384.84 ms | 14.987 ms |  44.19 ms | 385.72 ms |
| NUnit     | 4.4.0   | 749.52 ms | 43.814 ms | 129.19 ms | 763.45 ms |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |        NA |        NA |
| xUnit3    | 3.1.0   | 294.37 ms |  5.459 ms |  13.59 ms | 293.91 ms |
| TUnit_AOT | 0.64.0  |  46.08 ms |  4.629 ms |  13.43 ms |  38.89 ms |

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
| TUnit     | 0.64.0  | 459.97 ms |  2.350 ms |  2.199 ms | 460.84 ms |
| NUnit     | 4.4.0   | 927.45 ms | 12.028 ms | 10.663 ms | 928.04 ms |
| xUnit     | 2.9.3   | 994.71 ms | 16.321 ms | 15.267 ms | 991.91 ms |
| MSTest    | 3.11.0  | 856.62 ms | 16.316 ms | 18.789 ms | 857.39 ms |
| xUnit3    | 3.1.0   | 456.79 ms |  2.741 ms |  2.564 ms | 457.85 ms |
| TUnit_AOT | 0.64.0  |  26.15 ms |  0.351 ms |  0.328 ms |  26.16 ms |



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
| TUnit     | 0.64.0  |   504.75 ms |  5.479 ms |  5.125 ms |   504.00 ms |
| NUnit     | 4.4.0   | 1,006.88 ms | 19.647 ms | 21.838 ms | 1,006.64 ms |
| xUnit     | 2.9.3   | 1,114.85 ms | 21.820 ms | 38.784 ms | 1,106.40 ms |
| MSTest    | 3.11.0  |   972.75 ms | 18.329 ms | 17.145 ms |   973.29 ms |
| xUnit3    | 3.1.0   |   516.93 ms |  8.127 ms |  7.602 ms |   516.24 ms |
| TUnit_AOT | 0.64.0  |    66.05 ms |  1.563 ms |  4.608 ms |    65.36 ms |



