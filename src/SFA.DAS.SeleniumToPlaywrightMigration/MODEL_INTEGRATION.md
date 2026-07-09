# Model Integration Guide for Selenium to Playwright Migration Agent

## Overview

This document describes how **Qwen** and **GPT Mini** models are integrated into the migration agent to provide intelligent code analysis and generation.

## Model Roles

### 1. Qwen — Deep Code Analysis & Pattern Recognition
**Purpose**: Analyze existing Selenium codebases for patterns, complexity, and migration requirements

**Capabilities**:
- Parse C# test code and identify Selenium patterns
- Map locator strategies (XPath, CSS, ID) to accessibility patterns
- Assess test complexity and migration risk
- Generate detailed pattern reports
- Recommend optimal migration sequences

**When Invoked**:
```csharp
// Pattern analysis phase
var analysisPrompt = MigrationRecommendations.GetQwenAnalysisPrompt(project);
// Qwen provides: Complexity assessment, risk analysis, pattern detection
```

**Example Tasks**:
1. **Project Analysis**
   - Input: C# test project structure
   - Output: Complexity rating, pattern inventory, risk assessment
   
2. **Locator Strategy Analysis**
   - Input: XPath/CSS locators in page objects
   - Output: Accessibility mapping, preferred Playwright equivalents
   
3. **Dependency Assessment**
   - Input: .csproj file
   - Output: Selenium dependencies to remove, framework alignment check

4. **Test Complexity Analysis**
   - Input: Feature files and step definitions
   - Output: Migration difficulty score, prerequisite transformations

### 2. GPT Mini — Efficient Code Generation & Templates
**Purpose**: Generate production-ready Playwright C# code and migration templates

**Capabilities**:
- Generate `.csproj` file content with correct dependencies
- Create `reqnroll.json` configurations
- Produce Playwright hook implementations
- Generate step definition templates from Selenium steps
- Create migration guides and best practices documentation

**When Invoked**:
```csharp
// Code generation phase
var codeGenPrompt = MigrationRecommendations.GetGptMiniCodeGenPrompt(project, className);
// GPT Mini provides: Production-ready code for hooks, steps, configuration
```

**Example Tasks**:
1. **Project File Generation**
   - Input: Project name, dependency requirements
   - Output: Complete .csproj with Playwright, Reqnroll, framework references
   
2. **Hook Implementation**
   - Input: Project namespace, existing hook patterns
   - Output: PlaywrightHooks.cs with BeforeScenario, AfterScenario, AfterStep
   
3. **Step Template Generation**
   - Input: Feature file name, existing Selenium step patterns
   - Output: Stub step definitions matching feature scenarios
   
4. **Documentation Generation**
   - Input: Project metadata, migration specifics
   - Output: Comprehensive migration README with conversion guides

## Integration Points

### Phase 1: Analysis (Qwen Primary)
```
Input Projects → ProjectAnalyzer.cs 
              → Qwen Analysis Prompt
              → Pattern Detection Results
              → Complexity Assessment
```

**Files Involved**:
- `ProjectAnalyzer.cs` - Prepares project metadata
- `PatternDetector.cs` - Provides pattern detection context
- `MigrationHelper.cs` - Supplies conversion reference data
- `MigrationRecommendations.GetQwenAnalysisPrompt()` - Generates analysis prompt

### Phase 2: Planning (Qwen + Logic)
```
Qwen Analysis Results → MigrationOrchestrator.cs
                     → GenerateMigrationPlans()
                     → Phase recommendations
                     → Risk mitigation strategies
```

### Phase 3: Code Generation (GPT Mini Primary)
```
Project Metadata → MigrationRecommendations.GetGptMiniCodeGenPrompt()
                → GPT Mini Generation
                → CodeGenerator.cs templates
                → Production .cs files
```

**Files Involved**:
- `CodeGenerator.cs` - Template structure and patterns
- `MigrationRecommendations.GetGptMiniCodeGenPrompt()` - Generates code gen prompt
- Generated files: `.csproj`, `reqnroll.json`, `Hooks/*.cs`, `StepDefinitions/*.cs`

### Phase 4: Documentation (GPT Mini)
```
Migration Plans → CodeGenerator.GenerateMigrationReadme()
               → GPT Mini Enhancement (optional)
               → Comprehensive README.md
```

## Model Prompting Strategy

### Qwen Analysis Prompt Template
```
Analyze this C# test project for Selenium to Playwright migration:

Project: {project.Name}
Type: {project.ProjectType}
Test Count: {project.TestCount}
Page Objects: {project.PageObjects.Count}
Feature Files: {project.FeatureFiles.Count}

Key Concerns:
1. What are the main technical challenges in migrating this project?
2. Which patterns from Selenium are most problematic to convert?
3. What testing strategies should change?
4. Are there potential data loss or behavioral change risks?

Provide detailed analysis focusing on code refactoring complexity.
```

**Expected Qwen Output**:
- Detailed pattern analysis
- Complexity justification (Low/Medium/High)
- Risk identification with mitigation strategies
- Recommended migration sequence
- Specific code transformation guidance

### GPT Mini Code Generation Prompt Template
```
Generate a C# {className} class for Playwright migration:

Context:
- Project: {project.Name}
- Framework: Playwright + Reqnroll (BDD)
- Base Framework: SFA.DAS.Framework
- Namespace: SFA.DAS.{project.Name}.Project.*

Requirements:
1. Follow Playwright async/await patterns
2. Use [Binding] and ObjectContext from Reqnroll
3. Integrate with SFA.DAS.Framework patterns
4. Include XML documentation

Output production-ready code ready to integrate.
```

**Expected GPT Mini Output**:
- Complete class implementations
- Proper error handling
- Documentation comments
- Best practices compliance
- NUnit/Reqnroll attribute usage

## Workflow Execution

### Running the Complete Migration Agent
```bash
cd src/SFA.DAS.SeleniumToPlaywrightMigration
dotnet run
```

**Execution Flow**:
1. **Initialization** → Load configuration, initialize analyzers
2. **Scanning** → ProjectAnalyzer finds projects to migrate
3. **Analysis** → Invoke Qwen for pattern and complexity analysis
4. **Planning** → MigrationOrchestrator creates plans using Qwen results
5. **Code Gen** → Invoke GPT Mini for code templates and configurations
6. **Reporting** → Display analysis, recommendations, and generated artifacts

### Output Structure
```
migration-output/
├── analysis/
│   ├── project-analysis.json
│   ├── pattern-report.md
│   └── complexity-assessment.md
├── plans/
│   ├── batch-1-plan.md
│   ├── batch-2-plan.md
│   └── migration-sequence.md
└── templates/
    ├── SFA.DAS.ProjectName/
    │   ├── SFA.DAS.ProjectName.csproj
    │   ├── reqnroll.json
    │   ├── Usings.cs
    │   └── Project/Hooks/PlaywrightHooks.cs
    └── [additional projects...]
```

## Model Configuration for Foundry Deployment

### Qwen Configuration
```yaml
Model: Qwen (or deployment name)
Version: Latest stable
Temperature: 0.3  # Lower for consistent analysis
MaxTokens: 2000
Purpose: Code analysis and pattern recognition
```

### GPT Mini Configuration
```yaml
Model: gpt-4o mini (or deployment name)
Version: Latest stable
Temperature: 0.5  # Balanced for code generation
MaxTokens: 4000
Purpose: Code generation and template creation
```

## Integration with Microsoft Agents SDK

The agent uses the Microsoft Agents SDK to:
1. Interface with Foundry-deployed models
2. Handle model invocation and response parsing
3. Manage prompts and context windows
4. Log and trace model interactions
5. Support evaluation and optimization

### Example Model Invocation Pattern (Future Enhancement)
```csharp
// When integrated with Microsoft Agents SDK
var qwenClient = new ModelClient("qwen-deployment");
var analysisResponse = await qwenClient.CompleteAsync(
    MigrationRecommendations.GetQwenAnalysisPrompt(project),
    new CompleteOptions { Temperature = 0.3, MaxTokens = 2000 }
);

var gptClient = new ModelClient("gpt-4o-mini-deployment");
var codeGenResponse = await gptClient.CompleteAsync(
    MigrationRecommendations.GetGptMiniCodeGenPrompt(project, className),
    new CompleteOptions { Temperature = 0.5, MaxTokens = 4000 }
);
```

## Evaluation & Optimization

### Evaluation Metrics
- **Analysis Accuracy**: Compare Qwen's complexity assessments to actual migration effort
- **Code Quality**: Evaluate GPT Mini-generated code for compilation and test compatibility
- **Documentation Quality**: Assess clarity and completeness of generated guides
- **Time Savings**: Measure reduction in manual migration work

### Continuous Improvement
1. Collect traces from model interactions
2. Evaluate output quality using Foundry's evaluation framework
3. Use prompt_optimizer to refine prompts
4. Fine-tune models if needed (SFT/DPO)
5. Deploy improved versions

## Troubleshooting Model Integration

### Issue: Qwen Analysis Shows High Complexity Consistently
- **Cause**: Prompts may be highlighting worst-case patterns
- **Solution**: Adjust prompt to focus on actionable, mitigatable risks

### Issue: GPT Mini Generates Outdated Syntax
- **Cause**: Model may not have latest framework information
- **Solution**: Include version-specific patterns in prompt template

### Issue: Long Model Response Times
- **Cause**: Large context windows or complex prompts
- **Solution**: Batch analyses, simplify prompts, increase MaxTokens limits

## Future Enhancements

1. **Multi-Agent Coordination**: Use Qwen for analysis, GPT Mini for code, and a coordinator agent for overall orchestration
2. **Reinforcement Learning**: Fine-tune models based on successful migrations
3. **Interactive Mode**: Real-time Q&A with agent during migration
4. **Custom Evaluators**: Domain-specific evaluation for Playwright patterns
5. **Automated Validation**: Test generated code against actual projects

---

**Last Updated**: 2026-01-09  
**Model Status**: Ready for Qwen + GPT Mini integration  
**Foundry Deployment**: Supported via Microsoft Agents SDK
