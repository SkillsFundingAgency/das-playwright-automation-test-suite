# README for DAS Playwright Automation Test Suite

## Project Overview
This is a **BDD (Behavior-Driven Development) test automation suite** using SpecFlow 3.9 and Playwright for .NET. It tests multiple DAS (Digital Apprenticeship Service) applications. The suite is organized as a monorepo with 20+ test projects, each testing a specific service/application.

### Architecture Pattern
- **Base Framework**: `SFA.DAS.Framework` + `SFA.DAS.FrameworkHelpers` - shared test infrastructure
- **Project Structure**: Each test project (e.g., `SFA.DAS.Apar.UITests`, `SFA.DAS.EmployerPortal.UITests`) contains:
  - `Project/` - test code for that specific application
  - `specflow.json` - test language configuration (en-GB)
  - `appsettings.Environment.json` - environment/secret overrides (git-ignored)
  
### Build System
- **Solution**: `src/SFA.DAS.TestAutomation.sln`
- **Build**: `dotnet build src/**/*.csproj --configuration release`
- **Test Filter**: Unit tests use `TestCategory=Unittests`; UI tests run via NUnit
- **Parallel Execution**: Configured via NUnit runner; some scenarios tagged `@donotexecuteinparallel`

---

## Core Framework Concepts

### 1. **ScenarioContext + ObjectContext Pattern**
SpecFlow's `ScenarioContext` is the primary dependency injection container. Framework layers it with a custom `ObjectContext` (in `SFA.DAS.FrameworkHelpers`):
```csharp
// Get/Set values in scenario lifecycle
context.Set<Driver>(driver);
var driver = context.Get<Driver>();
// ScenarioContext is injectable via constructor
public class MyHooks(ScenarioContext context) { ... }
```
**Key Objects stored in context**: `Driver`, `ObjectContext`, `DbConfig`, `ConfigSection`, `RetryHelper`, data helpers

### 2. **Hook Execution Order** (Critical)
Framework uses `Order` parameter to control hook execution:
```csharp
[BeforeScenario(Order = 1)] // ConfigurationHooks - load configs
[BeforeScenario(Order = 2)] // API framework setup (if using APIs)
[BeforeScenario(Order = 3)] // ObjectContext + Driver initialization
[BeforeScenario(Order = 4)] // Project-specific setup (configuration, data helpers)
```
When adding hooks, **assign the correct Order** to avoid initialization race conditions.

### 3. **Base Classes - Mandatory Inheritance**
All test classes must inherit from framework base classes:
- **Hooks**: Inherit from `FrameworkBaseHooks` or project-specific `BaseHooks` (e.g., `AparBaseHooks`)
  - Provides `Navigate(url)` and `OpenNewTab()`
  - Project versions add domain-specific methods like `GoToUrl()`, data helper setup
- **Page Objects**: Inherit from `BasePage`
  - Constructor takes `ScenarioContext`
  - Must implement `public abstract Task VerifyPage()`
  - Access page via `protected IPage page` (Playwright)
- **Data Helpers**: Inherit from `BaseDataHelper` (for test data generation)
  - Auto-generates unique identifiers via `DateTimeToNanoSeconds`, `NextNumber`
  - For DB interactions: inherit `SqlDbBaseHelper` (has connection + query execution)

### 4. **Configuration System**
Configuration layering (priority order):
1. `appsettings.json` (committed defaults)
2. `appsettings.Environment.json` (environment overrides, git-ignored)
3. `appsettings.Project.json` (project-specific URLs/settings, committed)
4. `appsettings.AdminConfig.json`, `appsettings.TimeOutConfig.json`, etc. (feature-specific)

Access via:
```csharp
var configSection = context.Get<ConfigSection>();
var dbConfig = configSection.GetConfigSection<DbConfig>();
```

### 5. **Playwright + Driver Abstraction**
- `Driver` class wraps Playwright's `IBrowserContext` and `IPage`
- Access Playwright page: `context.Get<Driver>().Page`
- **Important**: `Driver.Page` can be replaced (e.g., new tab) - always fetch from context, not cached
- Page waits/retries are delegated to `RetryHelper` (exponential backoff, configurable timeouts)

---

## Project-Specific Patterns

### Adding a New Step Definition
1. Inherit from the project's `BaseHooks` (e.g., `AparBaseHooks`)
2. Use `[Given]`, `[When]`, `[Then]` attributes
3. Access test state via constructor-injected `ScenarioContext`:
```csharp
public class MySteps(ScenarioContext context) : AparBaseHooks(context)
{
    [When(@"I do something")]
    public async Task DoSomething()
    {
        var driver = context.Get<Driver>();
        await driver.Page.ClickAsync("[data-testid='button']");
    }
}
```

### Adding a Page Object
1. Inherit from `BasePage`
2. Implement `VerifyPage()` to assert page load condition
3. Use Playwright locators (prefer `GetByRole`, `GetByLabel`, data attributes):
```csharp
public class MyPage(ScenarioContext context) : BasePage(context)
{
    public async Task ClickSubmit() => await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
    
    public override async Task VerifyPage()
    {
        // This is retried - should throw if page not loaded
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Expected Title");
    }
}
```

### Adding Test Data Helpers
- **SQL-based**: Inherit `SqlDbBaseHelper`, use `ExecuteSqlCommand()` / `GetSqlQuery()`
- **Non-SQL**: Inherit `BaseDataHelper` for unique ID generation
- Store in context: `context.Set(new MyDataHelper(objectContext, dbConfig))`
- Example: `AparApplyDataHelpers` wraps test data creation for apply workflow

### Test Data Cleanup
Use `[AfterScenario]` tags in base hooks to clean up:
```csharp
[AfterScenario]
public async Task Cleanup() => await _sqlHelper.DeleteCreatedData();
```
This ensures test isolation and database consistency.

---

## Common Workflows

### Running Tests Locally
```powershell
# Full suite (all projects)
dotnet build src/SFA.DAS.TestAutomation.sln --configuration release
dotnet test src/**/*Tests.csproj --configuration release --logger "console;verbosity=detailed"

# Single project
dotnet test src/SFA.DAS.Apar.UITests/SFA.DAS.Apar.UITests.csproj --configuration release

# Specific scenario (by tag)
dotnet test src/SFA.DAS.Apar.UITests/SFA.DAS.Apar.UITests.csproj --configuration release -- --filter "TestCategory=Unittests" -t "@mytag"
```

### Environment Configuration
- **Local Development**: Add `appsettings.Environment.json` to override URLs/credentials
  - Format: same as `appsettings.json` but only your overrides
  - Example: change target URL from test to local/staging
- **CI/CD**: Azure Pipelines (see `azure-pipelines.yml`) manages secrets via variable groups
  - Builds, runs all tests, publishes artifacts to staging

### Debugging / Logging
- `ObjectContext.SetDebugInformation()` logs action descriptions
- **Test Traces**: Enabled via `TraceStartAndStopSteps` - screenshots + Playwright trace on failure
- **Retry Logic**: `RetryHelper` wraps waits with exponential backoff (configurable in `WaitConfigurationHelper`)

---

## Key Files & Patterns

| File/Folder | Purpose |
|---|---|
| `SFA.DAS.Framework/BasePage.cs` | Page object base class with Playwright utilities |
| `SFA.DAS.Framework/Hooks/FrameworkBaseHooks.cs` | Core hook methods (`Navigate`, `OpenNewTab`) |
| `SFA.DAS.Framework/Hooks/ConfigurationHooks.cs` | Configuration loading (Order = 1) |
| `SFA.DAS.FrameworkHelpers/ObjectContext.cs` | Dictionary-based test state container |
| `SFA.DAS.FrameworkHelpers/SqlDbBaseHelper.cs` | SQL execution (parameterized queries, connection mgmt) |
| `[Project]/Project/AparBaseHooks.cs` | Project-specific hook setup, data helpers initialization |
| `[Project]/Project/Hooks/` | Feature-specific hooks (e.g., `AparApplyHooks`, `AparAdminHooks`) |
| `[Project]/Project/Helpers/` | Data helpers, page object collections, project utilities |

---

## Anti-Patterns to Avoid

1. **Hardcoded Wait Times**: Use `RetryHelper` instead of `Thread.Sleep()`
2. **XPath Locators**: Prefer Playwright's `GetByRole()`, `GetByLabel()`, `GetByTestId()` - more stable
3. **Storing Driver as Field**: Always fetch from `context.Get<Driver>()` - page can be replaced
4. **Skipping VerifyPage()**: Page objects must assert load conditions - critical for flake prevention
5. **Synchronous SQL Calls in Async Context**: Always `await ExecuteSqlCommand()` 
6. **Hook Order Issues**: Remember Order matters - missing/wrong order causes "object not found" errors

---

## Extending the Framework

### Add a New Test Project
1. Create folder: `src/SFA.DAS.MyService.UITests/`
2. Create `SFA.DAS.MyService.UITests.csproj` (copy from existing project, update namespace)
3. Reference `SFA.DAS.Framework.csproj` and any service projects
4. Create `Project/MyServiceBaseHooks.cs` inheriting `FrameworkBaseHooks` with project-specific setup
5. Create `specflow.json` (see `SFA.DAS.Apar.UITests/specflow.json` template)
6. Add to `SFA.DAS.TestAutomation.sln` by building the project

### Add a New Hook Order
- If adding framework-level setup, use Order 0-3 range
- If adding project-level, use Order 4-10 range
- **Always document the Order reason** in a comment

---

## Testing Philosophy

This suite emphasizes:
- **BDD Clarity**: Feature files are living documentation
- **Isolation**: Each scenario cleans up its own data; can run in parallel
- **Resilience**: Retries + explicit waits prevent flakes; page verification before interaction
- **Scalability**: 20+ projects share one framework - consistency is critical

### How to use User secrets:
Navigate to "%APPDATA%/Microsoft" then Create Directory "UserSecrets" if you don't find it.
Create a <YourProjectName>_<EnvironmentName>_Secrets (for project specific config) and <EnvironmentName>_Secrets (for environment specific config) folder under "%APPDATA%/Microsoft/UserSecrets". You can get project name and environment name from the "appsettings.Environment.json" file under your respective project(s). ex: For Registration project, the "appsettings.Environment.json" file will look like
```json
{
  "local_EnvironmentName": "PP",
  "ProjectName": "Registration"
}
```
so you need to create a folder as Registration_PP_Secrets and PP_Secrets under "%APPDATA%/Microsoft/UserSecrets" folder

Create a file named "secrets.json" and replace only those values you want to keep it as secrets (you can copy the structure from "appsettings.Project.json" file under your respective project(s)).
Project secrets and framework secrets can be found in the Automation Test Data (https://skillsfundingagency.atlassian.net/wiki/spaces/NDL/pages/5088477187/Automation+Test+Secrets) confluence page

## To Execute tests in your desktop :
1. Whitelist your IP address ```(use FcS account for PP, use CDS account for AT, TEST, TEST2 and DEMO)```
2. To execute tests in your local, change the Browser value to "local" (will execute in chrome, ref to [Supported Browsers](https://github.com/SkillsFundingAgency/das-enterprise-automation-test-suite#supported-browsers) ) in ``secrets.json`` in your project specific secrets file (you can add the below section into it if it does not exist already).
3. To execute tests through Zap Proxy, change the Browser value to "zapProxyChrome"
```json
"TestExecutionConfig": {
    "Browser": "local"
  }
```

## Pipelines
Every weekday (morning between 12 to 5) most of the release pipeline is scheduled to execute against latest master which will give us an idea of the state of the appication.

### Variables
We use variable groups (library) to define and declare the variables, and the values will be shared among the piepline to avoid duplication. They are
```
 1. TEST Automation Suite Variables - will hold variables for TEST environment
 2. TEST2 Automation Suite Variables - will hold variables for TEST2 environment
 3. PreProd Automation Suite Variables - will hold variables for PP environment
 4. Release Automation Suite Variables - will hold variables at Release Level
 5. YAML Automation Suite Variables (Db) - will hold DB variables at Release Level
 6. Provider Release Automation Suite Variables - will hold Provider variables at Release Level
 7. Release Automation Suite Variables (AppService) - will hold AppService variables at Release Level
```

### Variable scope
If the variables are defined in more than one place then vsts will prioritize in following order

	1. Environment specfic pipeline private variable  
	2. Release specific pipeline private variable
	3. Environment specfic variable group
	4. Release specific variable group

## Framework:

### Supported Browsers: 
The framework can currently work on the following browsers
1. Chrome - use "chrome", "googlechrome" or "local" as values for the Browser in appsettings
2. Firefox - use "firefox" or "mozillafirefox" as values for the Browser in appsettings
3. Edge - use "edge" or "microsoftedge" as values for the Browser in appsettings
3. Chrome Headless - use "headlessbrowser" or "headless" as values for the Browser in appsettings

Note: Tests can be executed on different browsers versions using BrowserStack.

## Parallel Test Execution Limitations:

This framework supports Feature Level parallelization (tests under different feature file will run in parallel) not Scenario Level parallelization (tests under same feature file will not execute in parallel).

Note : referenced from https://github.com/techtalk/SpecFlow/issues/1599, https://github.com/techtalk/SpecFlow/issues/1535 and https://github.com/SpecFlowOSS/SpecFlow/issues/2225

By default up to all available cores on the machine may be used, we can use ```/Parallel``` argument or ```MaxCpuCount``` node to restrict the no of tests to be executed in parellel. 

By default we run 5 tests in parallel per docker instance, we can change it using ```__NumberOfTestWorkers__```

## Parallel Test Execution in your desktop:
If LevelOfParallelism is not specified, workers defaults to the number of processors on the machine, or 2, whichever is greater.
1. You can specify no of threads to use in the parameter : ``[assembly: LevelOfParallelism(2)]``
2. You can specify 0 to exeute tests in sequential order : ``[assembly: LevelOfParallelism(0)]``

By default we run 5 tests in parallel, you can change it under ```NUnitConfigurator.cs```
