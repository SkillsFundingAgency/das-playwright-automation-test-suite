# Selenium to Playwright Migration Agent

This project is a **Microsoft Foundry AI Agent** for automating the migration of Selenium test projects to Playwright C# with Reqnroll (BDD).

## Agent Purpose

The agent analyzes existing Selenium test projects and generates comprehensive migration plans and code templates to facilitate conversion to the new Playwright + Reqnroll architecture.

## Model Configuration

- **Primary Model**: Qwen (Code analysis, pattern recognition across C# codebases)
- **Secondary Model**: GPT Mini (Code generation, creating migration recommendations)

## Key Capabilities

1. **Project Scanning** — Identifies all Selenium UITests/APITests projects in the workspace
2. **Pattern Analysis** — Maps Selenium patterns to Playwright equivalents
3. **Complexity Assessment** — Evaluates migration effort and risks per project
4. **Code Generation** — Produces migration templates (.csproj, hooks, step definitions)
5. **Migration Planning** — Creates phased migration schedules
6. **Best Practices Guidance** — Recommends patterns from SFA.DAS.Framework

## Usage

```bash
cd src/SFA.DAS.SeleniumToPlaywrightMigration
dotnet run
```

The agent will:
1. Scan the entire workspace for test projects
2. Analyze each project for migration complexity
3. Generate detailed migration plans
4. Create code templates for Playwright + Reqnroll
5. Provide recommendations for phased execution

## Workflow

**Input**: Selenium-based C# test projects  
**Processing**: Analysis → Assessment → Plan Generation → Code Template Creation  
**Output**: Migration guides, code templates, step-by-step instructions

## Integration with Foundry

This agent is designed to work within the Microsoft Foundry ecosystem and can be:
- Extended with additional AI capabilities
- Deployed as a hosted agent for team collaboration
- Integrated with CI/CD pipelines for automated migration support
- Evaluated and optimized using Foundry's eval framework

## Related Projects

- **SFA.DAS.Framework** - Base Playwright framework and utilities
- **SFA.DAS.Apar.UITests** - Reference implementation for migrated projects
- **SFA.DAS.Campaigns.UITests** - Template configuration source (reqnroll.json)

## Migration Roadmap

Phase 1: Infrastructure & Frameworks
Phase 2: UITests Projects (Small → Large)
Phase 3: APITests Projects
Phase 4: Integration & CI/CD Setup
Phase 5: Validation & Documentation

---

**Note**: This agent assists with migration planning and code generation. Actual test execution and validation should be performed in the migrated project's test environment.

For Foundry-specific operations, run: `azd ai agent --help`
