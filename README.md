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
| Build_TUnit  | 0.64.0  | 1.622 s | 0.1218 s | 0.3493 s | 1.545 s |
| Build_NUnit  | 4.4.0   | 1.445 s | 0.1038 s | 0.2979 s | 1.416 s |
| Build_xUnit  | 2.9.3   | 1.445 s | 0.1127 s | 0.3305 s | 1.402 s |
| Build_MSTest | 3.11.0  | 1.676 s | 0.1312 s | 0.3808 s | 1.625 s |
| Build_xUnit3 | 3.1.0   | 1.264 s | 0.0795 s | 0.2307 s | 1.223 s |



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
| Build_TUnit  | 0.64.0  | 1.672 s | 0.0295 s | 0.0442 s | 1.662 s |
| Build_NUnit  | 4.4.0   | 1.524 s | 0.0214 s | 0.0190 s | 1.520 s |
| Build_xUnit  | 2.9.3   | 1.528 s | 0.0110 s | 0.0103 s | 1.529 s |
| Build_MSTest | 3.11.0  | 1.549 s | 0.0158 s | 0.0148 s | 1.546 s |
| Build_xUnit3 | 3.1.0   | 1.521 s | 0.0239 s | 0.0223 s | 1.519 s |



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
| Build_TUnit  | 0.64.0  | 1.806 s | 0.0359 s | 0.0368 s | 1.809 s |
| Build_NUnit  | 4.4.0   | 1.644 s | 0.0175 s | 0.0155 s | 1.648 s |
| Build_xUnit  | 2.9.3   | 1.646 s | 0.0302 s | 0.0283 s | 1.647 s |
| Build_MSTest | 3.11.0  | 1.706 s | 0.0189 s | 0.0158 s | 1.706 s |
| Build_xUnit3 | 3.1.0   | 1.629 s | 0.0228 s | 0.0202 s | 1.628 s |


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
| Method    | Version | Mean        | Error    | StdDev    | Median      |
|---------- |-------- |------------:|---------:|----------:|------------:|
| TUnit     | 0.64.0  |   596.54 ms | 40.40 ms | 112.63 ms |   576.46 ms |
| NUnit     | 4.4.0   | 1,531.22 ms | 93.54 ms | 272.86 ms | 1,533.45 ms |
| xUnit     | 2.9.3   | 1,404.68 ms | 53.36 ms | 154.81 ms | 1,382.40 ms |
| MSTest    | 3.11.0  | 1,259.38 ms | 59.40 ms | 170.42 ms | 1,244.45 ms |
| xUnit3    | 3.1.0   |   694.52 ms | 25.79 ms |  74.42 ms |   689.96 ms |
| TUnit_AOT | 0.64.0  |    70.37 ms | 10.42 ms |  29.88 ms |    68.42 ms |



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
| TUnit     | 0.64.0  | 490.97 ms |  5.491 ms |  5.136 ms | 490.98 ms |
| NUnit     | 4.4.0   | 915.89 ms | 16.123 ms | 15.081 ms | 914.27 ms |
| xUnit     | 2.9.3   | 989.46 ms | 19.452 ms | 18.195 ms | 982.17 ms |
| MSTest    | 3.11.0  | 846.24 ms | 16.069 ms | 15.031 ms | 843.45 ms |
| xUnit3    | 3.1.0   | 458.33 ms |  3.903 ms |  3.460 ms | 459.20 ms |
| TUnit_AOT | 0.64.0  |  25.78 ms |  0.295 ms |  0.246 ms |  25.77 ms |



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
| TUnit     | 0.64.0  |   530.49 ms |  3.651 ms |  3.236 ms |   529.89 ms |
| NUnit     | 4.4.0   |   979.42 ms | 15.302 ms | 14.314 ms |   975.15 ms |
| xUnit     | 2.9.3   | 1,057.55 ms | 14.978 ms | 14.011 ms | 1,054.74 ms |
| MSTest    | 3.11.0  |   936.77 ms | 18.532 ms | 24.739 ms |   935.76 ms |
| xUnit3    | 3.1.0   |   501.54 ms |  2.864 ms |  2.539 ms |   500.74 ms |
| TUnit_AOT | 0.64.0  |    69.87 ms |  2.145 ms |  6.325 ms |    69.62 ms |


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
| Method    | Version | Mean     | Error    | StdDev    | Median   |
|---------- |-------- |---------:|---------:|----------:|---------:|
| TUnit     | 0.64.0  | 357.1 ms | 20.14 ms |  58.76 ms | 353.6 ms |
| NUnit     | 4.4.0   |       NA |       NA |        NA |       NA |
| xUnit     | 2.9.3   |       NA |       NA |        NA |       NA |
| MSTest    | 3.11.0  | 929.5 ms | 43.64 ms | 124.51 ms | 930.0 ms |
| xUnit3    | 3.1.0   | 509.8 ms | 26.73 ms |  75.83 ms | 514.5 ms |
| TUnit_AOT | 0.64.0  | 150.5 ms | 16.84 ms |  49.67 ms | 141.0 ms |

Benchmarks with issues:
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.80GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  | 449.73 ms |  2.929 ms |  2.739 ms | 449.53 ms |
| NUnit     | 4.4.0   | 910.13 ms | 13.254 ms | 12.398 ms | 912.44 ms |
| xUnit     | 2.9.3   | 995.09 ms | 16.952 ms | 15.857 ms | 994.82 ms |
| MSTest    | 3.11.0  | 848.97 ms | 16.328 ms | 16.768 ms | 854.63 ms |
| xUnit3    | 3.1.0   | 461.87 ms |  2.575 ms |  2.409 ms | 461.05 ms |
| TUnit_AOT | 0.64.0  |  27.14 ms |  0.529 ms |  0.495 ms |  27.31 ms |



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
| TUnit     | 0.64.0  |   494.03 ms |  7.632 ms |  7.139 ms |   492.50 ms |
| NUnit     | 4.4.0   |   987.07 ms | 18.914 ms | 20.237 ms |   984.90 ms |
| xUnit     | 2.9.3   | 1,065.76 ms | 21.093 ms | 20.716 ms | 1,068.91 ms |
| MSTest    | 3.11.0  |   927.74 ms | 17.140 ms | 16.032 ms |   931.66 ms |
| xUnit3    | 3.1.0   |   513.76 ms |  9.818 ms |  9.184 ms |   514.28 ms |
| TUnit_AOT | 0.64.0  |    86.88 ms |  2.512 ms |  7.406 ms |    86.63 ms |


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
| TUnit     | 0.64.0  | 314.79 ms |  6.226 ms | 12.29 ms | 314.18 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   | 643.62 ms | 12.849 ms | 37.28 ms | 631.13 ms |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 343.00 ms | 18.761 ms | 54.73 ms | 321.37 ms |
| TUnit_AOT | 0.64.0  |  43.16 ms |  5.450 ms | 15.90 ms |  40.21 ms |

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
| Method    | Version | Mean        | Error     | StdDev    | Median    |
|---------- |-------- |------------:|----------:|----------:|----------:|
| TUnit     | 0.64.0  |   472.70 ms |  5.817 ms |  5.441 ms | 473.66 ms |
| NUnit     | 4.4.0   |   937.18 ms | 18.599 ms | 18.267 ms | 940.18 ms |
| xUnit     | 2.9.3   | 1,001.81 ms | 18.662 ms | 17.456 ms | 999.18 ms |
| MSTest    | 3.11.0  |   861.04 ms | 16.839 ms | 18.716 ms | 864.74 ms |
| xUnit3    | 3.1.0   |   463.71 ms |  2.278 ms |  2.019 ms | 464.30 ms |
| TUnit_AOT | 0.64.0  |    25.97 ms |  0.316 ms |  0.296 ms |  25.86 ms |



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
| TUnit     | 0.64.0  |   535.12 ms |  9.232 ms |  8.184 ms |   533.61 ms |
| NUnit     | 4.4.0   | 1,035.67 ms | 15.835 ms | 14.812 ms | 1,036.75 ms |
| xUnit     | 2.9.3   | 1,109.97 ms | 20.371 ms | 27.884 ms | 1,109.52 ms |
| MSTest    | 3.11.0  |   967.67 ms | 19.118 ms | 22.759 ms |   970.94 ms |
| xUnit3    | 3.1.0   |   517.06 ms |  4.430 ms |  4.144 ms |   517.03 ms |
| TUnit_AOT | 0.64.0  |    69.40 ms |  1.885 ms |  5.529 ms |    69.58 ms |


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
| TUnit     | 0.64.0  |   682.63 ms |  37.484 ms | 108.75 ms |   673.59 ms |
| NUnit     | 4.4.0   | 1,502.04 ms | 105.660 ms | 308.21 ms | 1,488.41 ms |
| xUnit     | 2.9.3   | 1,151.81 ms |  44.944 ms | 129.67 ms | 1,150.33 ms |
| MSTest    | 3.11.0  |   985.73 ms |  38.128 ms | 109.40 ms |   968.11 ms |
| xUnit3    | 3.1.0   |   562.40 ms |  20.118 ms |  58.05 ms |   561.35 ms |
| TUnit_AOT | 0.64.0  |    66.29 ms |   9.275 ms |  27.35 ms |    64.66 ms |



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
| TUnit     | 0.64.0  | 466.01 ms |  3.833 ms |  3.585 ms | 465.81 ms |
| NUnit     | 4.4.0   | 914.54 ms | 15.283 ms | 14.296 ms | 917.33 ms |
| xUnit     | 2.9.3   | 995.75 ms | 19.685 ms | 20.215 ms | 988.89 ms |
| MSTest    | 3.11.0  | 848.32 ms | 16.794 ms | 17.969 ms | 847.53 ms |
| xUnit3    | 3.1.0   | 475.52 ms |  6.998 ms |  5.844 ms | 476.80 ms |
| TUnit_AOT | 0.64.0  |  27.30 ms |  0.448 ms |  0.420 ms |  27.24 ms |



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
| TUnit     | 0.64.0  |   517.20 ms |  3.994 ms |  3.736 ms |   515.96 ms |
| NUnit     | 4.4.0   | 1,018.76 ms | 20.141 ms | 25.471 ms | 1,009.14 ms |
| xUnit     | 2.9.3   | 1,088.57 ms | 21.438 ms | 24.688 ms | 1,087.48 ms |
| MSTest    | 3.11.0  |   948.34 ms | 15.658 ms | 14.646 ms |   942.29 ms |
| xUnit3    | 3.1.0   |   529.22 ms |  5.031 ms |  4.706 ms |   529.19 ms |
| TUnit_AOT | 0.64.0  |    68.96 ms |  1.685 ms |  4.808 ms |    68.62 ms |


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
| TUnit     | 0.64.0  | 482.86 ms | 25.653 ms | 74.01 ms | 488.79 ms |
| NUnit     | 4.4.0   |        NA |        NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |        NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |       NA |        NA |
| xUnit3    | 3.1.0   | 432.32 ms | 24.449 ms | 70.54 ms | 428.89 ms |
| TUnit_AOT | 0.64.0  |  78.17 ms |  9.917 ms | 29.24 ms |  73.67 ms |

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
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   466.95 ms |  5.395 ms |  5.046 ms |   466.08 ms |
| NUnit     | 4.4.0   |   942.73 ms | 16.397 ms | 15.338 ms |   939.63 ms |
| xUnit     | 2.9.3   | 1,025.17 ms | 18.617 ms | 17.415 ms | 1,027.90 ms |
| MSTest    | 3.11.0  |   866.60 ms | 15.158 ms | 13.438 ms |   865.65 ms |
| xUnit3    | 3.1.0   |   462.67 ms |  4.194 ms |  3.923 ms |   461.52 ms |
| TUnit_AOT | 0.64.0  |    31.08 ms |  0.621 ms |  1.593 ms |    31.08 ms |



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
| TUnit     | 0.64.0  |   502.11 ms |  4.299 ms |  4.021 ms |   501.83 ms |
| NUnit     | 4.4.0   | 1,014.08 ms | 18.101 ms | 16.931 ms | 1,014.17 ms |
| xUnit     | 2.9.3   | 1,085.00 ms | 18.574 ms | 17.374 ms | 1,083.85 ms |
| MSTest    | 3.11.0  |   959.23 ms | 17.029 ms | 21.537 ms |   960.62 ms |
| xUnit3    | 3.1.0   |   508.76 ms |  5.933 ms |  5.550 ms |   507.01 ms |
| TUnit_AOT | 0.64.0  |    87.97 ms |  2.184 ms |  6.438 ms |    87.78 ms |


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
| TUnit     | 0.64.0  | 470.62 ms | 34.689 ms | 100.64 ms | 456.75 ms |
| NUnit     | 4.4.0   | 739.82 ms | 23.792 ms |  69.40 ms | 726.57 ms |
| xUnit     | 2.9.3   |        NA |        NA |        NA |        NA |
| MSTest    | 3.11.0  |        NA |        NA |        NA |        NA |
| xUnit3    | 3.1.0   | 370.57 ms | 15.373 ms |  44.60 ms | 359.61 ms |
| TUnit_AOT | 0.64.0  |  50.92 ms |  6.147 ms |  17.64 ms |  47.75 ms |

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
| TUnit     | 0.64.0  | 444.46 ms |  2.292 ms |  2.144 ms | 444.96 ms |
| NUnit     | 4.4.0   | 908.38 ms | 16.966 ms | 15.870 ms | 903.83 ms |
| xUnit     | 2.9.3   | 982.59 ms | 17.988 ms | 16.826 ms | 975.17 ms |
| MSTest    | 3.11.0  | 838.78 ms | 16.200 ms | 16.637 ms | 839.75 ms |
| xUnit3    | 3.1.0   | 450.33 ms |  1.803 ms |  1.598 ms | 450.12 ms |
| TUnit_AOT | 0.64.0  |  24.77 ms |  0.124 ms |  0.104 ms |  24.74 ms |



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
| TUnit     | 0.64.0  |   496.14 ms |  6.537 ms |  5.795 ms |   494.02 ms |
| NUnit     | 4.4.0   | 1,009.58 ms | 16.086 ms | 15.047 ms | 1,001.73 ms |
| xUnit     | 2.9.3   | 1,087.00 ms | 18.783 ms | 20.097 ms | 1,085.60 ms |
| MSTest    | 3.11.0  |   942.13 ms | 18.226 ms | 18.716 ms |   933.65 ms |
| xUnit3    | 3.1.0   |   503.42 ms |  5.528 ms |  4.900 ms |   501.61 ms |
| TUnit_AOT | 0.64.0  |    62.67 ms |  1.694 ms |  4.993 ms |    61.80 ms |


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
| TUnit     | 0.64.0  |   567.00 ms |  37.53 ms | 110.06 ms |   565.29 ms |
| NUnit     | 4.4.0   | 1,312.29 ms | 139.08 ms | 410.08 ms | 1,302.05 ms |
| xUnit     | 2.9.3   |          NA |        NA |        NA |          NA |
| MSTest    | 3.11.0  |          NA |        NA |        NA |          NA |
| xUnit3    | 3.1.0   |   460.16 ms |  27.42 ms |  79.55 ms |   454.50 ms |
| TUnit_AOT | 0.64.0  |    93.82 ms |  14.34 ms |  42.27 ms |    85.28 ms |

Benchmarks with issues:
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.4, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763 2.62GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  Job-YNJDZW : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit     | 0.64.0  |   504.84 ms |  4.068 ms |  3.805 ms |   505.08 ms |
| NUnit     | 4.4.0   |   962.24 ms | 18.791 ms | 19.297 ms |   957.31 ms |
| xUnit     | 2.9.3   | 1,134.07 ms | 13.694 ms | 12.809 ms | 1,133.61 ms |
| MSTest    | 3.11.0  |   891.69 ms | 16.538 ms | 16.242 ms |   895.33 ms |
| xUnit3    | 3.1.0   |   507.15 ms |  3.425 ms |  3.036 ms |   507.83 ms |
| TUnit_AOT | 0.64.0  |    42.31 ms |  0.443 ms |  0.393 ms |    42.27 ms |



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
| TUnit     | 0.64.0  |   523.58 ms |  3.518 ms |  2.937 ms |   523.77 ms |
| NUnit     | 4.4.0   |   986.41 ms | 16.309 ms | 15.256 ms |   984.16 ms |
| xUnit     | 2.9.3   | 1,171.98 ms | 21.956 ms | 47.263 ms | 1,154.73 ms |
| MSTest    | 3.11.0  |   923.03 ms | 15.610 ms | 14.602 ms |   928.15 ms |
| xUnit3    | 3.1.0   |   533.59 ms |  3.815 ms |  3.382 ms |   533.43 ms |
| TUnit_AOT | 0.64.0  |    74.41 ms |  1.396 ms |  1.165 ms |    74.65 ms |


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
| Method    | Version | Mean      | Error    | StdDev   | Median    |
|---------- |-------- |----------:|---------:|---------:|----------:|
| TUnit     | 0.64.0  | 387.84 ms | 30.24 ms | 83.80 ms | 372.63 ms |
| NUnit     | 4.4.0   |        NA |       NA |       NA |        NA |
| xUnit     | 2.9.3   |        NA |       NA |       NA |        NA |
| MSTest    | 3.11.0  |        NA |       NA |       NA |        NA |
| xUnit3    | 3.1.0   | 409.93 ms | 23.57 ms | 68.75 ms | 409.91 ms |
| TUnit_AOT | 0.64.0  |  86.35 ms | 12.72 ms | 37.31 ms |  83.93 ms |

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
| TUnit     | 0.64.0  | 455.92 ms |  3.301 ms |  2.927 ms | 456.90 ms |
| NUnit     | 4.4.0   | 927.42 ms | 17.609 ms | 16.472 ms | 925.63 ms |
| xUnit     | 2.9.3   | 997.73 ms | 12.757 ms | 11.933 ms | 994.35 ms |
| MSTest    | 3.11.0  | 853.74 ms | 16.531 ms | 19.037 ms | 849.23 ms |
| xUnit3    | 3.1.0   | 452.15 ms |  3.173 ms |  2.813 ms | 451.11 ms |
| TUnit_AOT | 0.64.0  |  26.10 ms |  0.519 ms |  0.555 ms |  26.18 ms |



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
| TUnit     | 0.64.0  |   512.80 ms |  3.215 ms |  2.684 ms |   512.21 ms |
| NUnit     | 4.4.0   | 1,042.21 ms | 17.963 ms | 16.803 ms | 1,045.14 ms |
| xUnit     | 2.9.3   | 1,096.36 ms | 19.576 ms | 18.311 ms | 1,098.07 ms |
| MSTest    | 3.11.0  |   967.30 ms | 17.582 ms | 16.446 ms |   966.98 ms |
| xUnit3    | 3.1.0   |   509.49 ms |  2.869 ms |  2.543 ms |   509.34 ms |
| TUnit_AOT | 0.64.0  |    63.72 ms |  1.752 ms |  5.165 ms |    63.17 ms |



