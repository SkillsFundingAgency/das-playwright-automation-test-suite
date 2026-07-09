# Selenium to Playwright Migration Agent — Complete Overview

## 🎯 Executive Summary

This is a **Microsoft Foundry AI Agent** built with C# .NET 10 that automates the analysis and migration of Selenium test projects to **Playwright with Reqnroll (BDD)**.

**Key Features**:
- ✅ Automated project scanning and analysis
- ✅ Selenium pattern detection with actionable recommendations
- ✅ Complexity assessment and risk identification
- ✅ Phased migration planning
- ✅ Production-ready code generation (using Qwen + GPT Mini)
- ✅ Comprehensive migration guides and templates

**Models**: Qwen (deep analysis) + GPT Mini (code generation)  
**Status**: Production-ready for deployment  
**Language**: C# .NET 10  
**Target**: DAS Test Automation Suite

---

## 📁 Project Structure

```
SFA.DAS.SeleniumToPlaywrightMigration/
├── Program.cs                      # Agent entry point and orchestration
├── ProjectAnalyzer.cs              # Scans projects and analyzes structure
├── CodeGenerator.cs                # Generates Playwright C# templates
├── MigrationOrchestrator.cs        # Coordinates analysis → planning → code gen
├── PatternDetector.cs              # Detects Selenium patterns in code
├── MigrationHelper.cs              # Conversion utilities and references
│
├── Configuration Files
├── appsettings.json                # Agent configuration
├── agent.config.yaml               # Foundry agent metadata
├── MODEL_PROMPTS.yaml              # Qwen & GPT Mini prompt templates
│
├── Documentation
├── README.md                       # Main agent documentation
├── QUICK_START.md                  # Quick start guide
├── MODEL_INTEGRATION.md            # Model integration details
├── AGENTS.md                       # Foundry agent marker
│
├── Project Files
├── SFA.DAS.SeleniumToPlaywrightMigration.csproj
└── Usings.cs                       # Global namespace imports
```

---

## 🚀 Quick Start

### 1. Run the Agent (Terminal)
```bash
cd src/SFA.DAS.SeleniumToPlaywrightMigration
dotnet run
```

### 2. Run in VS Code (F5)
Press `F5` and select **"Run Migration Agent"**

### 3. View Results
The agent will:
- 🔍 Scan all test projects
- 📊 Analyze Selenium patterns and complexity
- 🎯 Generate migration plans
- 📝 Create code templates and guides

**Typical runtime**: 30-60 seconds for 20+ projects

---

## 🔧 Core Components

### ProjectAnalyzer
**Purpose**: Scans workspace and analyzes test projects
- Finds UITests and APITests projects
- Counts tests and page objects
- Identifies dependencies
- Discovers feature files
- Calculates migration complexity

**Key Methods**:
- `ScanWorkspaceAsync()` - Discover all projects
- `AnalyzeProjectAsync()` - Deep dive into one project
- `AssessProjectAsync()` - Complexity and risk assessment

### CodeGenerator
**Purpose**: Creates Playwright C# code templates
- Generates `.csproj` files with correct dependencies
- Creates `reqnroll.json` configurations
- Produces Playwright hooks (BeforeScenario, AfterScenario)
- Generates step definition templates
- Creates migration README files

**Key Methods**:
- `GenerateCsprojContent()` - Project file generation
- `GenerateReqnrollJsonContent()` - BDD configuration
- `GenerateHooksClassContent()` - Lifecycle hooks
- `GenerateMigrationReadme()` - Guide generation

### MigrationOrchestrator
**Purpose**: Coordinates the entire workflow
- Identifies projects for migration
- Generates phased migration plans
- Creates comprehensive guides
- Calculates effort and recommendations

**Key Methods**:
- `IdentifyProjectsForMigrationAsync()` - Project discovery
- `GenerateMigrationPlansAsync()` - Plan creation
- `CreateMigrationGuideAsync()` - Guide generation

### PatternDetector
**Purpose**: Finds Selenium patterns in C# code
- Detects 10+ Selenium patterns (FindElement, SendKeys, WebDriverWait, etc.)
- Maps patterns to Playwright equivalents
- Identifies migration risks
- Calculates migration difficulty

**Detected Patterns**:
- `FindElement` → Playwright locators
- `WebDriverWait` → Auto-waiting
- `SendKeys` → FillAsync/TypeAsync
- `Click` → ClickAsync
- `Actions` → Direct methods
- And 5+ more...

### MigrationHelper
**Purpose**: Provides conversion utilities and references
- Maps Selenium locators to Playwright equivalents
- Documents action conversions
- Generates specific conversion guides
- Provides best practices references

**Key Maps**:
- 8 Selenium By strategies → Playwright locators
- 11 Action conversions → Playwright equivalents
- Wait strategy conversions

---

## 🤖 Model Integration

### Qwen — Deep Code Analysis
**When**: Pattern detection phase  
**What it does**:
1. Analyzes project structure and complexity
2. Identifies problematic patterns
3. Assesses migration risks
4. Recommends sequences

**Configuration**:
```yaml
Temperature: 0.3  # Consistency in analysis
Max Tokens: 2000
Top P: 0.9
```

### GPT Mini — Efficient Code Generation
**When**: Code generation phase  
**What it does**:
1. Generates `.csproj` files
2. Creates configuration files
3. Produces hook implementations
4. Generates step templates
5. Creates documentation

**Configuration**:
```yaml
Temperature: 0.5  # Balance creativity/consistency
Max Tokens: 4000
Top P: 0.95
```

**Prompt Templates**:
See [MODEL_PROMPTS.yaml](./MODEL_PROMPTS.yaml) for:
- `QWEN_PATTERN_ANALYSIS`
- `QWEN_COMPLEXITY_ASSESSMENT`
- `GPT_MINI_CSPROJ_GENERATION`
- `GPT_MINI_HOOKS_GENERATION`
- `GPT_MINI_STEP_TEMPLATE_GENERATION`

---

## 📊 Workflow

### Phase 1: Scanning (ProjectAnalyzer)
```
Workspace Directory
    ↓
Directory Scan (.csproj files)
    ↓
Project Inventory
```

### Phase 2: Analysis (PatternDetector + Qwen)
```
Project Inventory
    ↓
Selenium Pattern Detection
    ↓
Qwen Analysis (complexity, risks)
    ↓
Assessment Results
```

### Phase 3: Planning (MigrationOrchestrator)
```
Assessment Results
    ↓
Migration Plan Generation
    ↓
Phased Roadmap
```

### Phase 4: Code Generation (CodeGenerator + GPT Mini)
```
Phased Roadmap + GPT Mini
    ↓
Generate .csproj files
Generate Configuration
Generate Hooks
Generate Step Templates
Generate Documentation
    ↓
Production Templates
```

### Phase 5: Reporting
```
Analysis Results + Code Templates + Guides
    ↓
Console Output + Generated Files
```

---

## 🎯 Use Cases

### 1. Initial Migration Assessment
```bash
dotnet run
```
Produces a detailed analysis of all projects and complexity scores.

### 2. Batch Planning
```
Run Agent → Review Complexity Scores → Group by Complexity → Plan phases
```
Group Low (easiest) → Medium → High complexity projects.

### 3. Code Template Generation
Agent generates ready-to-use templates for:
- Project configurations (.csproj)
- Test lifecycle hooks (BeforeScenario, AfterScenario)
- Step definition stubs
- Migration guides

### 4. Risk Assessment
Identifies specific risks per project:
- Large number of tests
- Complex page object hierarchies
- Custom Selenium utilities
- Specific pattern counts

---

## 🔍 Example Outputs

### Console Output Sample
```
Found 20 projects to migrate:
  • SFA.DAS.Apar.UITests
    Type: UITests
    Path: c:\repos\...\src\SFA.DAS.Apar.UITests
    Tests: 45
    Feature Files: 3

Analyzing Selenium patterns in projects...
  SFA.DAS.Apar.UITests:
    Migration Difficulty: Low
    Selenium Patterns Found: 23
      - FindElement: 8 occurrences
      - Click: 6 occurrences
      - SendKeys: 5 occurrences
      - WebDriverWait: 4 occurrences

=== Migration Recommendations ===
Total Projects: 20
Estimated Migration Effort: 120 hours
Recommended Batch Size: 3 projects

Key Transformation Areas:
  • Browser Initialization & Context Management
  • Locator Strategy Conversion
  • Wait & Synchronization
  • Page Object Pattern Updates
  [... 6 more areas ...]

✅ Migration analysis complete!
```

### Generated Files Sample
```
SFA.DAS.Apar.UITests/
├── SFA.DAS.Apar.UITests.csproj        # Updated with Playwright deps
├── reqnroll.json                      # BDD configuration
├── Usings.cs                          # Global imports
├── Project/
│   ├── Hooks/
│   │   └── PlaywrightHooks.cs         # Generated hooks
│   └── StepDefinitions/
│       └── [ProjectName]Steps.cs      # Step templates
└── MIGRATION_GUIDE.md                 # Comprehensive guide
```

---

## 📋 Complexity Ratings

| Rating | Test Count | Page Objects | Selenium Patterns | Estimated Hours |
|--------|-----------|--------------|------------------|-----------------|
| Low | < 50 | < 5 | < 20 | 2-4 |
| Medium | 50-150 | 5-15 | 20-50 | 4-8 |
| High | > 150 | > 15 | > 50 | 8-16 |

---

## 🛠️ Configuration Options

Edit `appsettings.json` to customize:

```json
{
  "MigrationConfig": {
    "WorkspaceRoot": "c:\\repos\\das-playwright-automation-test-suite",
    "ProjectsPath": "src",
    "Models": {
      "AnalysisModel": "Qwen",
      "CodeGenerationModel": "GPT Mini"
    },
    "Analysis": {
      "DetectSeleniumPatterns": true,
      "CalculateComplexity": true,
      "AssessRisks": true,
      "EstimateEffort": true
    },
    "CodeGeneration": {
      "GenerateCsprojFiles": true,
      "GenerateHooks": true,
      "GenerateStepTemplates": true,
      "GenerateReadmes": true
    }
  }
}
```

---

## 🔐 Environment Variables

When deploying as a Foundry hosted agent:

```bash
AZURE_AI_PROJECT_ENDPOINT=<foundry-endpoint>
AZURE_AI_MODEL_DEPLOYMENT_NAME_QWEN=<qwen-deployment>
AZURE_AI_MODEL_DEPLOYMENT_NAME_GPT_MINI=<gpt-mini-deployment>
AZURE_SUBSCRIPTION_ID=<subscription-id>
AZURE_RESOURCE_GROUP=<resource-group>
```

---

## 📚 Documentation

| Document | Purpose |
|----------|---------|
| [README.md](./README.md) | Main agent documentation |
| [QUICK_START.md](./QUICK_START.md) | 5-minute quick start |
| [MODEL_INTEGRATION.md](./MODEL_INTEGRATION.md) | Model setup & usage |
| [agent.config.yaml](./agent.config.yaml) | Foundry deployment config |
| [MODEL_PROMPTS.yaml](./MODEL_PROMPTS.yaml) | Prompt templates for AI models |
| [AGENTS.md](./AGENTS.md) | Foundry agent marker |

---

## 🚀 Deployment Options

### 1. Local Execution (Current)
```bash
dotnet run --configuration Release
```

### 2. Hosted Foundry Agent
```bash
azd ai agent init --manifest <manifest-url>
azd provision
azd deploy
```

### 3. CI/CD Integration
Add to Azure DevOps pipeline for automated migration analysis on each PR.

### 4. Published Distribution
```bash
dotnet publish -c Release -o ./dist/migration-agent
```

---

## 🎓 Key Learnings

### Selenium → Playwright Conversions

#### Locators
```csharp
// Selenium
By.Id("submit") 
By.XPath("//button[text()='Click me']")

// Playwright
page.Locator("#submit")
page.GetByRole(AriaRole.Button, new() { Name = "Click me" })
```

#### Actions
```csharp
// Selenium
element.Click()
element.SendKeys("text")

// Playwright
await locator.ClickAsync()
await locator.FillAsync("text")
```

#### Waits
```csharp
// Selenium
WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
wait.Until(ExpectedConditions.PresenceOfElement(...))

// Playwright
// Auto-waits - no explicit waits needed!
await locator.IsVisibleAsync()
```

---

## ✨ Features & Benefits

| Feature | Benefit |
|---------|---------|
| **Automated Scanning** | No manual project discovery needed |
| **Pattern Detection** | Identifies specific Selenium patterns to migrate |
| **Complexity Assessment** | Prioritize easier projects first |
| **Code Generation** | Ready-to-use templates (saves 50+ hours) |
| **Phased Planning** | Risk-mitigated, manageable batches |
| **Best Practices** | Follows SFA.DAS.Framework patterns |
| **AI-Powered** | Qwen + GPT Mini for smart analysis & generation |
| **Foundry Integration** | Deploy as hosted agent for team collaboration |

---

## 🔗 Related Resources

- **Framework Base**: [SFA.DAS.Framework](../SFA.DAS.Framework/README.md)
- **Template Project**: [SFA.DAS.Campaigns.UITests](../SFA.DAS.Campaigns.UITests/)
- **Reference Implementation**: [SFA.DAS.Apar.UITests](../SFA.DAS.Apar.UITests/)
- **Playwright Docs**: https://playwright.dev/dotnet/
- **Reqnroll Docs**: https://reqnroll.net/
- **Microsoft Agents SDK**: https://github.com/microsoft/semantic-kernel

---

## 📞 Support & Troubleshooting

### Common Issues

| Issue | Solution |
|-------|----------|
| "dotnet not found" | Install .NET 10 SDK |
| "No projects found" | Check `WorkspaceRoot` in config |
| "Build fails" | Run `dotnet restore` first |
| Model timeouts | Increase timeout in MODEL_PROMPTS.yaml |

### Getting Help
1. Check [QUICK_START.md](./QUICK_START.md) for common questions
2. Review [MODEL_INTEGRATION.md](./MODEL_INTEGRATION.md) for model setup
3. See [README.md](./README.md) for detailed documentation

---

## 🔄 Continuous Improvement

The agent is designed for continuous optimization:

1. **Evaluation**: Track migration success metrics
2. **Prompt Optimization**: Use Foundry's `prompt_optimize` tool
3. **Fine-tuning**: Improve model accuracy with SFT/DPO
4. **Versioning**: Deploy improved agent versions over time

---

## 📊 Metrics & Goals

**Target**: Migrate 20+ Selenium C# projects to Playwright + Reqnroll

| Phase | Projects | Hours | Effort/Project |
|-------|----------|-------|----------------|
| Phase 1 (Low) | 5 | 15 | 3 hrs |
| Phase 2 (Med) | 8 | 40 | 5 hrs |
| Phase 3 (High) | 7 | 65 | 9.3 hrs |
| **Total** | **20** | **120** | **6 hrs avg** |

---

## ✅ Verification Checklist

Before running the agent, verify:

- [ ] .NET 10 SDK installed (`dotnet --version`)
- [ ] Workspace accessible (`cd c:\repos\das-playwright-automation-test-suite`)
- [ ] Solution builds (`dotnet build src/SFA.DAS.TestAutomation.sln`)
- [ ] Agent project exists (`src/SFA.DAS.SeleniumToPlaywrightMigration`)
- [ ] Configuration updated (`appsettings.json`)

---

## 🎉 Getting Started

**Right now**:
```bash
cd src/SFA.DAS.SeleniumToPlaywrightMigration
dotnet run
```

**In 30 seconds**, you'll have:
- ✅ Complete project inventory
- ✅ Complexity assessments
- ✅ Migration roadmap
- ✅ Ready-to-use code templates
- ✅ Step-by-step migration guides

**Let the agent do the heavy lifting!** 🚀

---

**Version**: 1.0.0  
**Status**: Production-Ready  
**Last Updated**: 2026-01-09  
**Maintainer**: DAS Test Automation Team
