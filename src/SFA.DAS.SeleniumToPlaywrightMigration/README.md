# Selenium to Playwright Migration Agent

## Overview

This is a **C# .NET 10 AI-powered migration agent** that analyzes Selenium-based test projects and generates comprehensive migration plans for converting to **Microsoft Playwright with Reqnroll (BDD)**.

**Models**: Qwen (analysis) + GPT Mini (code generation)  
**Framework**: Microsoft Agents SDK, integrated with SFA.DAS.Framework

---

## Quick Start

### Prerequisites
- .NET 10 SDK or later
- Visual Studio Code or Visual Studio 2022+
- Access to the workspace (c:\repos\das-playwright-automation-test-suite)

### Running the Agent

```bash
# Navigate to the project directory
cd src/SFA.DAS.SeleniumToPlaywrightMigration

# Restore dependencies
dotnet restore

# Run the migration agent
dotnet run
```

### Expected Output

The agent will:
1. Scan all test projects in `src/` directory
2. Identify projects needing migration (UITests/APITests)
3. Analyze each project's structure, tests, and page objects
4. Generate migration plans with estimated effort
5. Display recommendations and transformation areas
6. Create code templates for migration

---

## Architecture

### Core Components

#### 1. **ProjectAnalyzer.cs**
- Scans workspace for test projects
- Extracts project metadata (tests, page objects, dependencies)
- Assesses complexity and identifies risks
- Calculates migration effort estimates

#### 2. **CodeGenerator.cs**
- Generates .csproj configurations for Playwright
- Creates reqnroll.json templates
- Produces hook implementations (BeforeScenario/AfterScenario)
- Generates step definition templates
- Outputs comprehensive migration README files

#### 3. **MigrationOrchestrator.cs**
- Orchestrates the complete workflow
- Generates migration plans for each project
- Creates comprehensive migration guides
- Provides recommendations for batch sizing and phasing

#### 4. **Program.cs** (Main Agent)
- Entry point for agent execution
- Coordinates analysis and reporting
- Displays results to user

---

## Key Features

### 1. Workspace Analysis
Automatically discovers and catalogs:
- All UITests and APITests projects
- Feature file locations
- Page object hierarchies
- Existing dependencies
- Test method counts

### 2. Complexity Assessment
Evaluates migration difficulty:
- **Low Complexity**: <50 tests, few page objects, minimal Selenium-specific code
- **Medium Complexity**: 50-150 tests, moderate page objects, some refactoring needed
- **High Complexity**: >150 tests, complex page hierarchies, extensive Selenium usage

### 3. Risk Identification
Flags potential migration challenges:
- Large test counts requiring phased migration
- Active Selenium dependencies
- Complex page object hierarchies
- Custom utilities and helpers

### 4. Code Generation
Produces production-ready templates:
- **Project Files**: .csproj with correct dependencies
- **Configuration**: reqnroll.json, appsettings.Environment.json
- **Hooks**: Playwright lifecycle hooks (setup/teardown)
- **Step Templates**: Method signatures for step definitions
- **Documentation**: Migration guides and patterns

---

## Migration Workflow

### Phase 1: Analysis (What You're Running)
```
Workspace Scan → Project Analysis → Complexity Assessment → Plan Generation
```

### Phase 2: Planning
- Review generated migration plans
- Select projects for phased migration
- Allocate resources per complexity level

### Phase 3: Execution
For each project:
1. Update .csproj (add Playwright/Reqnroll, remove Selenium)
2. Implement hooks and configuration
3. Convert step definitions
4. Migrate page objects
5. Update assertions and waits
6. Test locally

### Phase 4: Validation
- Run full test suite
- Verify parallel execution
- Update CI/CD pipelines
- Document patterns

### Phase 5: Optimization
- Enable continuous evaluation via Foundry
- Optimize prompt instructions
- Fine-tune agent for specific patterns
- Deploy as hosted agent for team use

---

## Integration with Foundry

### Deployment as Hosted Agent
```bash
azd ai agent init --manifest <path-to-manifest>
azd provision
azd deploy
```

### Prompt Optimization
Use the agent iteratively with Foundry's `prompt_optimize` tool to:
- Improve analysis accuracy
- Refine code generation quality
- Customize for your specific patterns

### Continuous Evaluation
Track migration progress with Foundry's evaluation framework:
- Measure code quality of generated templates
- Compare different migration strategies
- Optimize migration sequence

---

## Reference Implementation

The agent uses **SFA.DAS.Framework** patterns as the target architecture:

### Driver.cs Pattern
```csharp
public class Driver(IBrowser browser, IBrowserContext context, IPage page, ObjectContext objectContext)
{
    public IBrowser Browser { get; } = browser;
    public IBrowserContext BrowserContext { get; } = context;
    public IPage Page = page;
    // Helper methods for common actions
}
```

### Hook Implementation
```csharp
[Binding]
public class PlaywrightHooks
{
    [BeforeScenario]
    public async Task BeforeScenario() { /* Setup */ }
    
    [AfterScenario]
    public async Task AfterScenario() { /* Teardown */ }
}
```

### Step Definition Pattern
```csharp
[Binding]
public class StepDefinitions
{
    private readonly Driver _driver;
    
    [Given/When/Then(@"step text")]
    public async Task StepMethod() { /* Playwright actions */ }
}
```

---

## Customization & Extension

### Adding Analysis Rules
Edit `ProjectAnalyzer.cs` to:
- Detect custom patterns
- Identify project-specific dependencies
- Add custom complexity metrics

### Extending Code Generation
Edit `CodeGenerator.cs` to:
- Generate additional file types
- Add project-specific configurations
- Create domain-specific templates

### Integrating with AI Models
The agent is designed to leverage:
- **Qwen**: Deep code analysis, pattern recognition
- **GPT Mini**: Efficient code generation, quick recommendations

Update `MigrationRecommendations` to call specific models for specialized tasks.

---

## Common Migration Patterns

### Selenium WebDriver → Playwright
```csharp
// Selenium
var element = driver.FindElement(By.XPath("//button[@id='submit']"));
element.Click();
WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("result")));

// Playwright (with auto-waiting)
var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "submit" });
await submitButton.ClickAsync();
// Auto-waits for element to be actionable and visible
```

### Page Object Conversion
```csharp
// Selenium PO
public class LoginPage
{
    private IWebDriver _driver;
    private By UsernameField = By.Id("username");
    
    public void EnterUsername(string username) => _driver.FindElement(UsernameField).SendKeys(username);
}

// Playwright PO
public class LoginPage
{
    private IPage _page;
    
    public ILocator UsernameField => _page.GetByLabel("Username");
    
    public async Task EnterUsernameAsync(string username) => await UsernameField.FillAsync(username);
}
```

---

## Troubleshooting

### "Project not found" errors
- Ensure workspace path is correct
- Check that .csproj files exist in subdirectories

### Timeout or scanning delays
- Large workspaces may take several seconds to analyze
- Monitor console output for progress updates

### Model access issues
For Foundry deployment:
- Verify Qwen and GPT Mini are deployed in your project
- Check Azure authentication and permissions
- Review model deployment configurations

---

## Resources

- [Playwright Documentation](https://playwright.dev/dotnet/)
- [Reqnroll (SpecFlow v3)](https://reqnroll.net/)
- [Microsoft Agents SDK](https://github.com/microsoft/semantic-kernel)
- [SFA.DAS.Framework](../SFA.DAS.Framework/) - Base framework patterns

---

## Support

For issues or enhancements:
1. Check the AGENTS.md file for agent-specific guidance
2. Review the migration plans generated for detailed recommendations
3. Consult the SFA.DAS.Framework documentation for pattern reference

---

**Agent Version**: 1.0.0  
**Framework**: .NET 10, Playwright, Reqnroll  
**Models**: Qwen + GPT Mini  
**Status**: Production-ready for migration planning and code generation
