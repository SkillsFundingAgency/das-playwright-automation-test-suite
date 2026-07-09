namespace SFA.DAS.SeleniumToPlaywrightMigration;

/// <summary>
/// Orchestrates the complete migration analysis and planning workflow
/// </summary>
public class MigrationOrchestrator
{
    private readonly ProjectAnalyzer _analyzer;
    private readonly CodeGenerator _generator;

    public MigrationOrchestrator(ProjectAnalyzer analyzer, CodeGenerator generator)
    {
        _analyzer = analyzer;
        _generator = generator;
    }

    public async Task<List<ProjectMetadata>> IdentifyProjectsForMigrationAsync(string workspaceRoot)
    {
        var projects = await _analyzer.ScanWorkspaceAsync(workspaceRoot);
        
        // Filter out already migrated projects and sort by complexity
        var projectsToMigrate = projects
            .Where(p => !IsAlreadyMigrated(p))
            .OrderBy(p => _analyzer.AssessProjectAsync(p).Result.EstimatedHours)
            .ToList();

        return projectsToMigrate;
    }

    public async Task<List<MigrationPlan>> GenerateMigrationPlansAsync(List<ProjectMetadata> projects)
    {
        var plans = new List<MigrationPlan>();

        foreach (var project in projects)
        {
            var assessment = await _analyzer.AssessProjectAsync(project);
            var plan = new MigrationPlan
            {
                ProjectName = project.Name,
                Steps = GenerateMigrationSteps(project, assessment),
                RequiredChanges = GenerateRequiredChanges(project),
                FileMappings = GenerateFileMappings(project)
            };

            plans.Add(plan);
        }

        return plans;
    }

    public async Task<MigrationGuide> CreateMigrationGuideAsync(List<MigrationPlan> plans)
    {
        var guide = new MigrationGuide
        {
            TotalProjects = plans.Count,
            EstimatedEffort = CalculateTotalEffort(plans),
            RecommendedBatchSize = CalculateRecommendedBatchSize(plans),
            TransformationAreas = GetTransformationAreas(),
            DetailedPlans = plans
        };

        return guide;
    }

    private bool IsAlreadyMigrated(ProjectMetadata project)
    {
        // Check if project already has Playwright markers
        var csprojFile = Directory.GetFiles(project.Path, "*.csproj").FirstOrDefault();
        if (csprojFile != null)
        {
            var content = File.ReadAllText(csprojFile);
            return content.Contains("Microsoft.Playwright") && content.Contains("Reqnroll");
        }

        return false;
    }

    private List<string> GenerateMigrationSteps(ProjectMetadata project, MigrationAssessment assessment)
    {
        var steps = new List<string>
        {
            "1. Analyze existing test structure and dependencies",
            "2. Update .csproj file with Playwright and Reqnroll references",
            "3. Remove Selenium WebDriver dependencies",
            "4. Create/update reqnroll.json configuration",
            "5. Implement Playwright hooks (BeforeScenario, AfterScenario)",
            "6. Migrate step definitions from Selenium to Playwright",
            "7. Convert page objects and locators",
            "8. Update assertions and wait strategies",
            "9. Configure parallel execution settings",
            "10. Add screenshot and logging utilities",
            "11. Test locally and fix failing scenarios",
            "12. Update CI/CD pipeline configuration",
            "13. Document migration changes and patterns",
            "14. Code review and QA testing"
        };

        // Add risk mitigation steps if needed
        if (assessment.Risks.Any())
        {
            steps.Add("15. Address identified risks:");
            foreach (var risk in assessment.Risks)
                steps.Add($"    - {risk}");
        }

        return steps;
    }

    private List<string> GenerateRequiredChanges(ProjectMetadata project)
    {
        var changes = new List<string>
        {
            "Update project file (.csproj) with new dependencies",
            "Create/update Usings.cs for global imports",
            "Create Playwright hooks for test lifecycle",
            "Update ObjectContext extensions for Driver access",
            "Convert PageObject classes to Playwright Locators",
            "Update step definitions with async/await patterns",
            "Replace wait strategies with Playwright auto-waiting",
            "Update assertion libraries (NUnit matchers)",
            "Add screenshot configuration and error handling",
            "Update configuration files (appsettings.Environment.json)",
            "Create reqnroll.json for BDD configuration"
        };

        if (project.PageObjects.Any())
            changes.Add($"Migrate {project.PageObjects.Count} page objects");

        if (project.FeatureFiles.Any())
            changes.Add($"Review {project.FeatureFiles.Count} feature files for Selenium-specific steps");

        return changes;
    }

    private Dictionary<string, string> GenerateFileMappings(ProjectMetadata project)
    {
        return new Dictionary<string, string>
        {
            { "Hooks/*", "Project/Hooks/" },
            { "Pages/*", "Project/Pages/" },
            { "StepDefinitions/*", "Project/StepDefinitions/" },
            { "Features/*", "Project/Features/" },
            { "Helpers/*", "Project/Helpers/" },
            { "appsettings.json", "appsettings.json" },
            { "appsettings.Environment.json", "appsettings.Environment.json" },
            { "reqnroll.json", "reqnroll.json" },
            { "Usings.cs", "Usings.cs" }
        };
    }

    private string CalculateTotalEffort(List<MigrationPlan> plans)
    {
        // This would integrate with assessment data
        var totalHours = 10; // Base infrastructure
        totalHours += plans.Count * 4; // ~4 hours per project average
        
        return $"{totalHours} hours";
    }

    private int CalculateRecommendedBatchSize(List<MigrationPlan> plans)
    {
        if (plans.Count <= 5) return plans.Count;
        if (plans.Count <= 20) return 3;
        return 2; // Large migrations should go slower
    }

    private List<string> GetTransformationAreas()
    {
        return new List<string>
        {
            "Browser Initialization & Context Management - Switch from WebDriver to Playwright context model",
            "Locator Strategy Conversion - Transform XPath/CSS selectors to Playwright's locator strategy",
            "Wait & Synchronization - Replace implicit/explicit waits with Playwright's auto-waiting",
            "Page Object Pattern - Update PO classes to use Playwright locators and async methods",
            "Step Definition Updates - Convert Selenium actions to Playwright's fluent API",
            "Assertion Framework - Update NUnit assertions (minimal changes needed)",
            "Test Lifecycle Hooks - Implement Playwright BeforeScenario/AfterScenario patterns",
            "Error Handling & Screenshots - Utilize Playwright's built-in debugging features",
            "Parallel Execution - Configure Reqnroll feature-level parallelization",
            "Configuration Management - Environment-specific settings via appsettings"
        };
    }
}

/// <summary>
/// Provides recommendations for migration strategy and execution
/// </summary>
public class MigrationRecommendations
{
    public static string GetQwenAnalysisPrompt(ProjectMetadata project)
    {
        return $$"""
Analyze this C# test project for Selenium to Playwright migration:

Project: {{project.Name}}
Type: {{project.ProjectType}}
Path: {{project.Path}}
Test Count: {{project.TestCount}}
Page Objects: {{project.PageObjects.Count}}
Feature Files: {{project.FeatureFiles.Count}}

Dependencies Found:
{{string.Join("\n", project.Dependencies.Take(10))}}

Key Concerns:
1. What are the main technical challenges in migrating this project?
2. Which patterns from Selenium are most problematic to convert?
3. What testing strategies should change?
4. Are there any potential data loss or behavioral change risks?

Provide detailed analysis focusing on code refactoring complexity and pattern transformations needed.
""";
    }

    public static string GetGptMiniCodeGenPrompt(ProjectMetadata project, string className)
    {
        return $$"""
Generate a C# {{className}} class for our Playwright migration:

Context:
- Project: {{project.Name}}
- Framework: Playwright + Reqnroll (BDD)
- Base Framework: SFA.DAS.Framework
- Language: C# .NET 10

Requirements:
1. Follow namespace convention: SFA.DAS.{{project.Name}}.Project.*
2. Use Playwright's async/await patterns
3. Integrate with Reqnroll [Binding] and ObjectContext
4. Use IPage and ILocator from Microsoft.Playwright
5. Follow the patterns from SFA.DAS.Framework

Generate production-ready code with:
- Proper error handling
- XML documentation comments
- NUnit test attributes
- Playwright best practices

Output should be ready to integrate into our test suite.
""";
    }
}
