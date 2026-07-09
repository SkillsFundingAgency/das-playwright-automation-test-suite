# Migration Agent - Quick Start Guide

Get started with the Selenium to Playwright migration agent in 5 minutes!

## Prerequisites

- **.NET 10 SDK** - [Download here](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- **Visual Studio Code** (optional, but recommended)
- **Git** - to access the repository
- Access to the workspace: `c:\repos\das-playwright-automation-test-suite`

## Quick Start - Option 1: Using Terminal

### Step 1: Navigate to the Agent Directory
```bash
cd c:\repos\das-playwright-automation-test-suite\src\SFA.DAS.SeleniumToPlaywrightMigration
```

### Step 2: Restore Dependencies
```bash
dotnet restore
```

### Step 3: Build the Agent
```bash
dotnet build --configuration Debug
```

### Step 4: Run the Migration Analysis
```bash
dotnet run --configuration Debug
```

**Expected Output:**
```
=== Selenium to Playwright Migration Agent ===
Using Qwen and GPT Mini for code analysis and generation

📁 Scanning workspace: c:\repos\das-playwright-automation-test-suite

Found X projects to migrate:
  • SFA.DAS.Apar.UITests
    Type: UITests
    Path: ...
    Tests: Y
    Feature Files: Z
    
[More projects listed...]

Analyzing Selenium patterns in projects...
  SFA.DAS.Apar.UITests:
    Migration Difficulty: Low/Medium/High
    Selenium Patterns Found: X
      - FindElement: Y occurrences
      - WebDriverWait: Z occurrences
      ...

Generating migration plans...
Creating comprehensive migration guide...

=== Migration Recommendations ===
Total Projects: X
Estimated Migration Effort: X hours
Recommended Batch Size: X projects

✅ Migration analysis complete!
```

## Quick Start - Option 2: Using VS Code

### Step 1: Open Workspace
```bash
code c:\repos\das-playwright-automation-test-suite
```

### Step 2: Open the Agent Project
- In VS Code Explorer, navigate to: `src/SFA.DAS.SeleniumToPlaywrightMigration`

### Step 3: Run with F5 (Debug)
- Press `F5` or click **Run** → **Start Debugging**
- Select **"Run Migration Agent"** or **"Debug Migration Agent"**
- Watch the output in the integrated terminal

### Step 4: View Results
- Results will be printed to the terminal
- Generated code templates can be found in the project directory

## Understanding the Output

### Phase 1: Project Scanning
The agent discovers all test projects (UITests/APITests) in the `src/` directory.

```
Found 20 projects to migrate:
  • SFA.DAS.Apar.UITests (UITests, 45 tests, 3 feature files)
  • SFA.DAS.Approvals.UITests (UITests, 62 tests, 5 feature files)
  ...
```

### Phase 2: Pattern Detection
Analyzes each project for Selenium-specific patterns that need migration.

```
  SFA.DAS.Apar.UITests:
    Migration Difficulty: Low
    Selenium Patterns Found: 23
      - FindElement: 8 occurrences
      - Click: 6 occurrences
      - SendKeys: 5 occurrences
      - WebDriverWait: 4 occurrences
```

### Phase 3: Recommendations
Displays migration strategy, phasing, and transformation areas.

```
=== Migration Recommendations ===
Total Projects: 20
Estimated Migration Effort: 120 hours
Recommended Batch Size: 3 projects

Key Transformation Areas:
  • Browser Initialization & Context Management
  • Locator Strategy Conversion
  • Wait & Synchronization
  • Page Object Pattern Updates
  • Step Definition Updates
  ...
```

## Generated Artifacts

The agent creates analysis results showing:

1. **Project Inventory** — All projects to be migrated with metadata
2. **Pattern Analysis** — Selenium patterns found in each project
3. **Complexity Assessment** — Low/Medium/High migration difficulty ratings
4. **Migration Plans** — Phased migration roadmaps
5. **Code Templates** — Generated `.csproj`, hooks, configurations (ready to use)

## Configuration

### Modify Agent Behavior

Edit `appsettings.json`:
```json
{
  "MigrationConfig": {
    "WorkspaceRoot": "c:\\repos\\das-playwright-automation-test-suite",
    "Analysis": {
      "DetectSeleniumPatterns": true,
      "CalculateComplexity": true
    },
    "CodeGeneration": {
      "GenerateCsprojFiles": true,
      "GenerateHooks": true
    }
  }
}
```

### Enable Verbose Output

Set `VerboseOutput: true` in `appsettings.json` to see detailed analysis.

## Model Integration

### Using with Qwen & GPT Mini

When deployed with Azure AI Foundry:

1. **Qwen** analyzes project complexity and patterns
   - Runs during pattern detection phase
   - Provides risk assessment and recommendations
   - Configuration in `MODEL_PROMPTS.yaml`

2. **GPT Mini** generates code templates
   - Runs during code generation phase
   - Creates `.csproj`, hooks, step templates
   - Configuration in `MODEL_PROMPTS.yaml`

### Environment Variables (for Foundry)

When deploying as a Foundry hosted agent, set:
```bash
AZURE_AI_PROJECT_ENDPOINT=<your-foundry-endpoint>
AZURE_AI_MODEL_DEPLOYMENT_NAME_QWEN=<qwen-model>
AZURE_AI_MODEL_DEPLOYMENT_NAME_GPT_MINI=<gpt-mini-model>
```

## Common Tasks

### Run in Release Mode (Optimized)
```bash
dotnet run --configuration Release --no-build
```

### Clean Build
```bash
dotnet clean
dotnet build --configuration Debug
```

### Publish Agent
```bash
dotnet publish -c Release -o ./dist/migration-agent
```

### Watch Mode (Auto-rebuild on changes)
```bash
dotnet watch --project ./SFA.DAS.SeleniumToPlaywrightMigration.csproj run
```

## Next Steps

After running the migration agent:

1. **Review the Analysis** — Examine the pattern detection results
2. **Prioritize Projects** — Identify which projects to migrate first
3. **Start with Simple Projects** — Begin with Low complexity projects
4. **Use Generated Templates** — Apply generated code as a starting point
5. **Follow Migration Guide** — Reference generated README files for detailed steps

## Troubleshooting

### Issue: "dotnet command not found"
**Solution**: Ensure .NET 10 SDK is installed and added to PATH. Verify with `dotnet --version`.

### Issue: "Project not found"
**Solution**: Check that `WorkspaceRoot` in `appsettings.json` points to the correct directory.

### Issue: "Build fails with missing dependencies"
**Solution**: Run `dotnet restore` first, then retry build.

### Issue: Agent runs but shows no projects found
**Solution**: Verify the `ProjectsPath` setting and that test projects exist in that directory.

## Getting Help

- **Agent Documentation**: See [README.md](./README.md)
- **Model Configuration**: See [MODEL_INTEGRATION.md](./MODEL_INTEGRATION.md)
- **Prompts Configuration**: See [MODEL_PROMPTS.yaml](./MODEL_PROMPTS.yaml)
- **Agent Configuration**: See [agent.config.yaml](./agent.config.yaml)
- **Framework Reference**: See `src/SFA.DAS.Framework/README.md`

## Keyboard Shortcuts in VS Code

| Shortcut | Action |
|----------|--------|
| `F5` | Start Debugging |
| `Shift+F5` | Stop Debugging |
| `Ctrl+Shift+B` | Build Solution |
| `Ctrl+Shift+D` | Show Debug View |
| `Ctrl+K Ctrl+T` | Run Selected Task |
| `Ctrl+`` | Toggle Terminal |

## Quick Commands Reference

```bash
# Build the agent
dotnet build src/SFA.DAS.SeleniumToPlaywrightMigration

# Run the agent
dotnet run --project src/SFA.DAS.SeleniumToPlaywrightMigration

# Debug the agent (with breakpoints)
dotnet build && dotnet run --project src/SFA.DAS.SeleniumToPlaywrightMigration -- --debug

# Publish for distribution
dotnet publish -c Release -o ./dist/migration-agent

# Clean all build artifacts
dotnet clean
```

---

**Happy Migrating!** 🚀

For questions or issues, reach out to the DAS Test Automation Team.
