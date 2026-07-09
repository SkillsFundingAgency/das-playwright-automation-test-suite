namespace SFA.DAS.SeleniumToPlaywrightMigration;

/// <summary>
/// Generates Playwright C# code for migrated projects
/// </summary>
public class CodeGenerator
{
    public string GenerateCsprojContent(ProjectMetadata project)
    {
        var projectType = project.ProjectType == "UITests" ? "UI" : "API";
        
        return $$"""
<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<TargetFramework>net10.0</TargetFramework>
		<RootNamespace>SFA.DAS.{{project.Name}}</RootNamespace>
		<ErrorOnDuplicatePublishOutputFiles>false</ErrorOnDuplicatePublishOutputFiles>
		<IsPackable>false</IsPackable>
	</PropertyGroup>

	<ItemGroup>
		<Content Include="..\Sample.pdf" Link="Sample.pdf">
			<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
		</Content>
	</ItemGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.6.0" />
		<PackageReference Include="NUnit3TestAdapter" Version="6.1.0">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>
		<PackageReference Include="Reqnroll.Assist.Dynamic" Version="3.3.4" />
		<PackageReference Include="Reqnroll.Tools.MsBuild.Generation" Version="3.3.4" />
		<PackageReference Include="Reqnroll.NUnit" Version="3.3.4" />
	</ItemGroup>

	<ItemGroup>
		<ProjectReference Include="..\SFA.DAS.Framework\SFA.DAS.Framework.csproj" />
	</ItemGroup>

</Project>
""";
    }

    public string GenerateReqnrollJsonContent()
    {
        return """
{
  "language": "en",
  "generateJson": false,
  "generateNUnitTestSource": true,
  "generateMarkdownDocumentation": false
}
""";
    }

    public string GenerateUsingsContent()
    {
        return """
global using Microsoft.Playwright;
global using Microsoft.Playwright.NUnit;
global using NUnit.Framework;
global using Reqnroll;
global using SFA.DAS.Framework;
global using System.Text.RegularExpressions;
""";
    }

    public string GenerateHooksClassContent(string projectNamespace)
    {
        return $$"""
namespace {{projectNamespace}}.Project.Hooks;

[Binding]
public class PlaywrightHooks
{
    private readonly IBrowser? _browser;
    private readonly IBrowserContext? _context;
    private readonly IPage? _page;
    private readonly Driver? _driver;

    public PlaywrightHooks(ObjectContext objectContext)
    {
        ObjectContext = objectContext;
    }

    public ObjectContext ObjectContext { get; }

    [BeforeScenario(Order = 1)]
    public async Task BeforeScenario()
    {
        var pw = await Playwright.CreateAsync();
        
        // Configure browser based on environment settings
        _browser = await pw.Chromium.LaunchAsync(new() { Headless = true });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
        
        // Store in ScenarioContext for access in step definitions
        ObjectContext.ScenarioContext["driver"] = new Driver(_browser, _context, _page, ObjectContext);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_page != null)
            await _page.CloseAsync();
        if (_context != null)
            await _context.CloseAsync();
        if (_browser != null)
            await _browser.CloseAsync();
    }

    [AfterStep]
    public async Task AfterStep()
    {
        // Take screenshots on failure for debugging
        if (TestContext.CurrentContext.Result.Outcome?.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            if (_page != null)
            {
                await _page.ScreenshotAsync(new() 
                { 
                    Path = $"failure_{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png" 
                });
            }
        }
    }
}
""";
    }

    public string GenerateObjectContextExtensionContent(string projectNamespace)
    {
        return $$"""
namespace {{projectNamespace}}.Project;

public static class ObjectContextExtension
{
    public static Driver GetDriver(this ObjectContext context)
    {
        if (context.ScenarioContext.TryGetValue("driver", out var driver) && driver is Driver d)
            return d;
            
        throw new InvalidOperationException("Driver not initialized. Ensure Playwright hooks ran before scenario.");
    }

    public static IPage GetPage(this ObjectContext context) => context.GetDriver().Page;
}
""";
    }

    public string GenerateStepDefinitionTemplate(string projectNamespace, string featureName)
    {
        var className = $"{featureName.Replace(" ", "")}Steps";
        
        return $$"""
namespace {{projectNamespace}}.Project.StepDefinitions;

[Binding]
public class {{className}}
{
    private readonly ObjectContext _objectContext;
    private readonly Driver _driver;

    public {{className}}(ObjectContext objectContext)
    {
        _objectContext = objectContext;
        _driver = objectContext.GetDriver();
    }

    // Example: Given/When/Then steps
    // Replace with actual step definitions from your feature files

    [Given(@"I navigate to the application")]
    public async Task GivenINavigateToApplication()
    {
        // var baseUrl = _objectContext.FrameworkConfig.BaseUrl;
        // await _driver.Page.GotoAsync(baseUrl);
    }

    [When(@"I perform an action")]
    public async Task WhenIPerformAction()
    {
        // Add action here
    }

    [Then(@"I verify the result")]
    public async Task ThenIVerifyResult()
    {
        // Add assertion here
    }
}
""";
    }

    public string GenerateMigrationReadme(ProjectMetadata project, List<string> migrationSteps)
    {
        var stepsText = string.Join("\n", migrationSteps.Select((s, i) => $"{i + 1}. {s}"));
        
        return $$"""
# {{project.Name}} - Selenium to Playwright Migration

## Overview
This project has been migrated from Selenium WebDriver to Microsoft Playwright with Reqnroll (BDD).

## Key Changes

### Framework & Dependencies
- **Removed**: Selenium WebDriver and related dependencies
- **Added**: Microsoft.Playwright NUnit adapter, Reqnroll 3.3.4+
- **Updated**: .csproj references to use `SFA.DAS.Framework` (Playwright base framework)

### Structure
- **Feature Files**: `Project/Features/` - Gherkin BDD scenarios
- **Step Definitions**: `Project/StepDefinitions/` - Step implementations
- **Page Objects**: `Project/Pages/` - Page abstraction layer (if applicable)
- **Hooks**: `Project/Hooks/PlaywrightHooks.cs` - Setup/teardown and test lifecycle
- **Helpers**: `Project/Helpers/` - Utility functions

### Migration Steps
{{stepsText}}

### Configuration Files
- **reqnroll.json**: BDD test generation configuration
- **appsettings.Environment.json**: Environment and secret overrides
- **.csproj**: Project dependencies and framework references

### Parallel Test Execution
This framework supports feature-level parallelization:
- Default: All available cores (configurable via `LevelOfParallelism`)
- Current setting: 5 parallel test workers

### Running Tests Locally
```powershell
# Build the solution
dotnet build src/SFA.DAS.TestAutomation.sln --configuration release

# Run tests for this project
dotnet test {{project.Name}}.csproj --configuration release --logger "console;verbosity=detailed"
```

### Key Differences from Selenium

| Aspect | Selenium | Playwright |
|--------|----------|-----------|
| Browser Control | WebDriver Protocol | CDP/DevTools Protocol |
| Waits | Implicit/Explicit waits | Auto-waiting on actions |
| Locators | By class (XPath, CSS) | Locator API with multiple selectors |
| Screenshots | Manual handling | Built-in screenshot methods |
| Context Isolation | Single browser instance | Multiple contexts per browser |

### Troubleshooting

#### Issue: Tests timing out
- **Solution**: Playwright has built-in auto-waiting; remove explicit waits
- Check `Driver.cs` for timeout configurations

#### Issue: Locator not found
- **Solution**: Use Playwright's debug tools (`page.Pause()`) or inspector
- Verify selectors work in browser console first

#### Issue: Screenshot/test data access
- **Solution**: Use helper methods from `SFA.DAS.Framework`
- See `ScreenShotHelper` and `UsersSqlDataHelper` patterns

## Next Steps
1. Run the test suite locally to identify failing tests
2. Update step definitions with actual Playwright actions
3. Migrate page objects and assertions
4. Add parallel execution tags if needed (`@sequential` for sequential tests)
5. Integrate with CI/CD pipeline

## Resources
- [Playwright Documentation](https://playwright.dev/dotnet/)
- [Reqnroll Documentation](https://reqnroll.net/)
- [Framework Base Patterns](../SFA.DAS.Framework/README.md)
""";
    }
}
