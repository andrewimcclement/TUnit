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
| Build_TUnit  | 0.64.0  | 1.355 s | 0.1192 s | 0.3439 s | 1.221 s |
| Build_NUnit  | 4.4.0   | 1.680 s | 0.1871 s | 0.5515 s | 1.566 s |
| Build_xUnit  | 2.9.3   | 1.438 s | 0.0979 s | 0.2887 s | 1.378 s |
| Build_MSTest | 3.11.0  | 1.475 s | 0.1266 s | 0.3733 s | 1.409 s |
| Build_xUnit3 | 3.1.0   | 1.585 s | 0.1103 s | 0.3253 s | 1.567 s |



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
| Build_TUnit  | 0.64.0  | 1.677 s | 0.0325 s | 0.0466 s | 1.672 s |
| Build_NUnit  | 4.4.0   | 1.526 s | 0.0198 s | 0.0176 s | 1.527 s |
| Build_xUnit  | 2.9.3   | 1.577 s | 0.0307 s | 0.0378 s | 1.577 s |
| Build_MSTest | 3.11.0  | 1.642 s | 0.0195 s | 0.0183 s | 1.645 s |
| Build_xUnit3 | 3.1.0   | 1.541 s | 0.0227 s | 0.0212 s | 1.539 s |



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
| Build_TUnit  | 0.64.0  | 1.938 s | 0.0373 s | 0.0922 s | 1.949 s |
| Build_NUnit  | 4.4.0   | 1.746 s | 0.0341 s | 0.0478 s | 1.735 s |
| Build_xUnit  | 2.9.3   | 1.779 s | 0.0343 s | 0.0445 s | 1.771 s |
| Build_MSTest | 3.11.0  | 1.919 s | 0.0372 s | 0.0522 s | 1.921 s |
| Build_xUnit3 | 3.1.0   | 1.718 s | 0.0337 s | 0.0316 s | 1.717 s |


### Scenario: Tests focused on assertion performance and validation

#### macos-latest

```

BenchmarkDotNet v0.15.4, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host] : .NET 9.0.9 (9.0.9, 9.0.925.41916), Arm64 RyuJIT armv8.0-a

Runtime=.NET 9.0  

```
| Method | Version | Mean | Error | StdDev | Median |
|------- |-------- |-----:|------:|-------:|-------:|
| TUnit  | 0.64.0  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)



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
| TUnit     | 0.64.0  |   504.49 ms |  8.332 ms |  7.794 ms |   503.23 ms |
| NUnit     | 4.4.0   |   937.72 ms | 16.411 ms | 15.351 ms |   929.82 ms |
| xUnit     | 2.9.3   | 1,033.83 ms | 19.166 ms | 17.928 ms | 1,036.03 ms |
| MSTest    | 3.11.0  |   875.66 ms | 17.397 ms | 17.865 ms |   879.57 ms |
| xUnit3    | 3.1.0   |   474.53 ms |  4.060 ms |  3.798 ms |   475.18 ms |
| TUnit_AOT | 0.64.0  |    26.75 ms |  0.268 ms |  0.237 ms |    26.72 ms |



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
| TUnit     | 0.64.0  |   561.74 ms |  6.252 ms |  5.848 ms |   562.02 ms |
| NUnit     | 4.4.0   | 1,057.71 ms | 17.750 ms | 16.603 ms | 1,055.27 ms |
| xUnit     | 2.9.3   | 1,130.21 ms | 15.965 ms | 14.934 ms | 1,131.82 ms |
| MSTest    | 3.11.0  |   987.97 ms | 19.349 ms | 18.099 ms |   981.37 ms |
| xUnit3    | 3.1.0   |   524.85 ms |  2.541 ms |  2.377 ms |   524.86 ms |
| TUnit_AOT | 0.64.0  |    65.77 ms |  1.540 ms |  4.516 ms |    64.45 ms |


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
| TUnit     | 0.64.0  | 324.1 ms | 15.92 ms | 46.93 ms | 328.0 ms |
| NUnit     | 4.4.0   |       NA |       NA |       NA |       NA |
| xUnit     | 2.9.3   |       NA |       NA |       NA |       NA |
| MSTest    | 3.11.0  |       NA |       NA |       NA |       NA |
| xUnit3    | 3.1.0   | 555.7 ms | 20.17 ms | 58.83 ms | 548.4 ms |
| TUnit_AOT | 0.64.0  | 125.1 ms | 13.62 ms | 39.94 ms | 124.0 ms |

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
| TUnit     | 0.64.0  |   457.06 ms |  4.545 ms |  4.251 ms |   457.41 ms |
| NUnit     | 4.4.0   |   928.40 ms | 17.630 ms | 16.491 ms |   931.40 ms |
| xUnit     | 2.9.3   | 1,015.89 ms | 17.533 ms | 16.400 ms | 1,015.70 ms |
| MSTest    | 3.11.0  |   862.21 ms | 16.612 ms | 17.060 ms |   866.20 ms |
| xUnit3    | 3.1.0   |   470.58 ms |  5.280 ms |  4.939 ms |   470.99 ms |
| TUnit_AOT | 0.64.0  |    27.53 ms |  0.550 ms |  0.540 ms |    27.41 ms |



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
| TUnit     | 0.64.0  |   530.14 ms | 10.382 ms | 10.661 ms |   532.03 ms |
| NUnit     | 4.4.0   | 1,065.76 ms | 21.308 ms | 47.217 ms | 1,056.67 ms |
| xUnit     | 2.9.3   | 1,212.30 ms | 24.063 ms | 69.426 ms | 1,218.97 ms |
| MSTest    | 3.11.0  | 1,038.62 ms | 20.052 ms | 28.759 ms | 1,037.74 ms |
| xUnit3    | 3.1.0   |   560.70 ms | 11.085 ms | 10.369 ms |   560.79 ms |
| TUnit_AOT | 0.64.0  |    94.01 ms |  3.122 ms |  9.207 ms |    94.05 ms |


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
| Method    | Version | Mean      | Error    | StdDev    | Median    |
|---------- |-------- |----------:|---------:|----------:|----------:|
| TUnit     | 0.64.0  | 365.67 ms | 15.32 ms |  43.97 ms | 367.18 ms |
| NUnit     | 4.4.0   | 668.84 ms | 25.15 ms |  72.98 ms | 656.43 ms |
| xUnit     | 2.9.3   | 834.62 ms | 54.43 ms | 154.41 ms | 814.54 ms |
| MSTest    | 3.11.0  | 801.69 ms | 31.75 ms |  91.59 ms | 788.52 ms |
| xUnit3    | 3.1.0   | 505.37 ms | 17.36 ms |  50.65 ms | 503.25 ms |
| TUnit_AOT | 0.64.0  |  87.68 ms | 15.37 ms |  45.09 ms |  80.99 ms |



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
| TUnit     | 0.64.0  | 466.53 ms |  3.778 ms |  3.349 ms | 465.63 ms |
| NUnit     | 4.4.0   | 917.15 ms | 16.405 ms | 15.345 ms | 910.62 ms |
| xUnit     | 2.9.3   | 987.94 ms | 18.204 ms | 17.028 ms | 982.48 ms |
| MSTest    | 3.11.0  | 847.34 ms | 15.313 ms | 14.324 ms | 843.86 ms |
| xUnit3    | 3.1.0   | 458.32 ms |  3.088 ms |  2.889 ms | 457.18 ms |
| TUnit_AOT | 0.64.0  |  25.55 ms |  0.106 ms |  0.099 ms |  25.53 ms |



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
| TUnit     | 0.64.0  |   516.35 ms |  3.904 ms |  3.652 ms |   516.04 ms |
| NUnit     | 4.4.0   | 1,003.69 ms | 17.289 ms | 16.172 ms | 1,000.60 ms |
| xUnit     | 2.9.3   | 1,076.58 ms | 18.032 ms | 16.867 ms | 1,076.36 ms |
| MSTest    | 3.11.0  |   941.04 ms | 16.731 ms | 15.650 ms |   938.68 ms |
| xUnit3    | 3.1.0   |   507.03 ms |  3.139 ms |  2.936 ms |   507.20 ms |
| TUnit_AOT | 0.64.0  |    62.82 ms |  1.649 ms |  4.863 ms |    62.44 ms |


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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   435.41 ms |  25.00 ms |  70.92 ms |   420.39 ms |
| NUnit     | 4.4.0   | 1,145.27 ms | 119.71 ms | 347.29 ms | 1,020.59 ms |
| xUnit     | 2.9.3   |          NA |        NA |        NA |          NA |
| MSTest    | 3.11.0  | 1,101.83 ms |  55.61 ms | 160.44 ms | 1,094.11 ms |
| xUnit3    | 3.1.0   |   579.61 ms |  26.74 ms |  76.73 ms |   581.75 ms |
| TUnit_AOT | 0.64.0  |    96.96 ms |  14.23 ms |  41.73 ms |    88.64 ms |

Benchmarks with issues:
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 3.12GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 460.55 ms |  4.661 ms |  4.360 ms | 460.87 ms |
| NUnit     | 4.4.0   | 904.78 ms | 16.929 ms | 15.836 ms | 904.59 ms |
| xUnit     | 2.9.3   | 975.91 ms | 17.787 ms | 16.638 ms | 975.14 ms |
| MSTest    | 3.11.0  | 833.91 ms | 16.030 ms | 15.744 ms | 835.69 ms |
| xUnit3    | 3.1.0   | 469.70 ms |  2.997 ms |  2.657 ms | 469.48 ms |
| TUnit_AOT | 0.64.0  |  26.09 ms |  0.114 ms |  0.101 ms |  26.10 ms |



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
| TUnit     | 0.64.0  |   532.92 ms | 10.528 ms | 15.099 ms |   524.94 ms |
| NUnit     | 4.4.0   | 1,035.54 ms | 14.883 ms | 13.193 ms | 1,038.63 ms |
| xUnit     | 2.9.3   | 1,155.68 ms | 23.360 ms | 68.876 ms | 1,145.95 ms |
| MSTest    | 3.11.0  |   987.38 ms | 19.642 ms | 52.088 ms |   971.09 ms |
| xUnit3    | 3.1.0   |   526.82 ms |  5.856 ms |  4.890 ms |   525.74 ms |
| TUnit_AOT | 0.64.0  |    71.10 ms |  1.646 ms |  4.852 ms |    71.49 ms |


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
| Method    | Version | Mean       | Error    | StdDev    | Median      |
|---------- |-------- |-----------:|---------:|----------:|------------:|
| TUnit     | 0.64.0  |   594.5 ms | 24.35 ms |  69.86 ms |   587.04 ms |
| NUnit     | 4.4.0   | 1,246.5 ms | 62.45 ms | 183.15 ms | 1,244.63 ms |
| xUnit     | 2.9.3   | 1,094.2 ms | 50.35 ms | 144.48 ms | 1,074.51 ms |
| MSTest    | 3.11.0  | 1,085.4 ms | 54.50 ms | 159.85 ms | 1,072.91 ms |
| xUnit3    | 3.1.0   |   501.1 ms | 25.64 ms |  74.81 ms |   509.08 ms |
| TUnit_AOT | 0.64.0  |   104.4 ms | 14.83 ms |  43.71 ms |    95.40 ms |



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
| TUnit     | 0.64.0  |   462.46 ms |  3.885 ms |  3.634 ms |   463.05 ms |
| NUnit     | 4.4.0   |   975.56 ms | 18.239 ms | 17.913 ms |   974.23 ms |
| xUnit     | 2.9.3   | 1,060.21 ms | 14.445 ms | 13.512 ms | 1,060.68 ms |
| MSTest    | 3.11.0  |   908.51 ms | 17.429 ms | 16.303 ms |   908.65 ms |
| xUnit3    | 3.1.0   |   478.19 ms |  3.964 ms |  3.514 ms |   478.02 ms |
| TUnit_AOT | 0.64.0  |    30.53 ms |  0.608 ms |  0.811 ms |    30.43 ms |



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
| TUnit     | 0.64.0  |   505.72 ms |  3.721 ms |  3.299 ms |   505.94 ms |
| NUnit     | 4.4.0   | 1,010.02 ms | 18.284 ms | 17.103 ms | 1,004.36 ms |
| xUnit     | 2.9.3   | 1,080.26 ms | 19.896 ms | 18.610 ms | 1,076.10 ms |
| MSTest    | 3.11.0  |   941.25 ms | 16.297 ms | 15.244 ms |   940.64 ms |
| xUnit3    | 3.1.0   |   498.76 ms |  4.516 ms |  4.224 ms |   498.78 ms |
| TUnit_AOT | 0.64.0  |    83.81 ms |  1.868 ms |  5.506 ms |    84.38 ms |


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
| Method    | Version | Mean      | Error    | StdDev   | Median    |
|---------- |-------- |----------:|---------:|---------:|----------:|
| TUnit     | 0.64.0  | 343.35 ms | 20.97 ms | 60.51 ms | 329.15 ms |
| NUnit     | 4.4.0   |        NA |       NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |       NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |       NA |       NA |        NA |
| xUnit3    | 3.1.0   | 385.62 ms | 26.66 ms | 77.76 ms | 374.43 ms |
| TUnit_AOT | 0.64.0  |  65.91 ms | 11.45 ms | 33.03 ms |  58.71 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.92GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 442.33 ms |  3.173 ms |  2.968 ms | 442.67 ms |
| NUnit     | 4.4.0   | 916.82 ms | 16.966 ms | 15.870 ms | 914.27 ms |
| xUnit     | 2.9.3   | 989.86 ms | 18.651 ms | 17.446 ms | 984.72 ms |
| MSTest    | 3.11.0  | 837.82 ms | 15.979 ms | 17.097 ms | 834.64 ms |
| xUnit3    | 3.1.0   | 447.62 ms |  1.610 ms |  1.428 ms | 447.49 ms |
| TUnit_AOT | 0.64.0  |  25.75 ms |  0.455 ms |  0.425 ms |  25.62 ms |



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
| TUnit     | 0.64.0  |   494.46 ms |  3.476 ms |  3.081 ms |   494.57 ms |
| NUnit     | 4.4.0   | 1,028.62 ms | 17.791 ms | 32.977 ms | 1,023.60 ms |
| xUnit     | 2.9.3   | 1,151.52 ms | 22.676 ms | 49.775 ms | 1,157.39 ms |
| MSTest    | 3.11.0  | 1,012.26 ms | 19.486 ms | 26.673 ms | 1,014.24 ms |
| xUnit3    | 3.1.0   |   528.36 ms |  9.332 ms |  8.729 ms |   528.73 ms |
| TUnit_AOT | 0.64.0  |    67.79 ms |  1.630 ms |  4.780 ms |    68.00 ms |


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
| Method    | Version | Mean        | Error      | StdDev    | Median      |
|---------- |-------- |------------:|-----------:|----------:|------------:|
| TUnit     | 0.64.0  |   456.64 ms |  22.658 ms |  66.09 ms |   445.98 ms |
| NUnit     | 4.4.0   |   676.46 ms |  27.955 ms |  79.30 ms |   652.18 ms |
| xUnit     | 2.9.3   | 1,142.35 ms | 102.843 ms | 303.24 ms | 1,040.45 ms |
| MSTest    | 3.11.0  |   955.98 ms |  97.099 ms | 286.30 ms |   864.60 ms |
| xUnit3    | 3.1.0   |   437.18 ms |  19.884 ms |  58.63 ms |   448.31 ms |
| TUnit_AOT | 0.64.0  |    48.61 ms |   5.851 ms |  16.79 ms |    40.26 ms |



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
| TUnit     | 0.64.0  |   500.52 ms |  6.073 ms |  5.680 ms |   502.93 ms |
| NUnit     | 4.4.0   |   947.82 ms | 18.893 ms | 26.486 ms |   944.77 ms |
| xUnit     | 2.9.3   | 1,112.72 ms | 19.600 ms | 18.334 ms | 1,109.60 ms |
| MSTest    | 3.11.0  |   884.80 ms | 16.588 ms | 16.291 ms |   887.57 ms |
| xUnit3    | 3.1.0   |   491.27 ms |  3.882 ms |  3.441 ms |   491.44 ms |
| TUnit_AOT | 0.64.0  |    40.27 ms |  0.273 ms |  0.256 ms |    40.22 ms |



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
| TUnit     | 0.64.0  |   533.13 ms |  4.787 ms |  4.477 ms |   533.35 ms |
| NUnit     | 4.4.0   | 1,082.29 ms | 26.263 ms | 77.438 ms | 1,075.72 ms |
| xUnit     | 2.9.3   | 1,254.33 ms | 24.662 ms | 36.149 ms | 1,256.80 ms |
| MSTest    | 3.11.0  | 1,008.88 ms | 17.167 ms | 16.058 ms | 1,010.24 ms |
| xUnit3    | 3.1.0   |   579.39 ms | 11.115 ms | 10.397 ms |   581.24 ms |
| TUnit_AOT | 0.64.0  |    84.61 ms |  1.657 ms |  1.842 ms |    84.19 ms |


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
| TUnit     | 0.64.0  | 506.48 ms | 30.770 ms | 90.24 ms | 500.81 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 395.00 ms | 18.733 ms | 54.35 ms | 396.82 ms |
| TUnit_AOT | 0.64.0  |  61.62 ms |  8.138 ms | 23.48 ms |  55.26 ms |

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
| TUnit     | 0.64.0  | 453.62 ms |  5.360 ms |  5.013 ms | 454.07 ms |
| NUnit     | 4.4.0   | 916.42 ms | 17.265 ms | 16.150 ms | 918.79 ms |
| xUnit     | 2.9.3   | 982.33 ms | 17.671 ms | 16.530 ms | 978.64 ms |
| MSTest    | 3.11.0  | 841.93 ms | 16.726 ms | 15.646 ms | 845.84 ms |
| xUnit3    | 3.1.0   | 449.62 ms |  4.281 ms |  4.005 ms | 450.31 ms |
| TUnit_AOT | 0.64.0  |  24.96 ms |  0.162 ms |  0.144 ms |  24.96 ms |



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
| TUnit     | 0.64.0  |   493.26 ms |  1.864 ms |  1.652 ms |   493.21 ms |
| NUnit     | 4.4.0   | 1,029.36 ms | 20.487 ms | 47.073 ms | 1,030.74 ms |
| xUnit     | 2.9.3   | 1,112.88 ms | 22.082 ms | 54.167 ms | 1,107.65 ms |
| MSTest    | 3.11.0  |   992.36 ms | 19.568 ms | 41.276 ms |   994.24 ms |
| xUnit3    | 3.1.0   |   523.53 ms |  9.864 ms | 10.964 ms |   523.13 ms |
| TUnit_AOT | 0.64.0  |    67.67 ms |  1.794 ms |  5.235 ms |    67.12 ms |



