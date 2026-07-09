using Microsoft.Agents.Sdk;
using Azure.AI.Inference;

namespace SFA.DAS.SeleniumToPlaywrightMigration;

/// <summary>
/// Selenium to Playwright Migration Agent
/// Analyzes Selenium test projects and generates Playwright C# with Reqnroll migration code
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Selenium to Playwright Migration Agent ===");
        Console.WriteLine("Using Qwen and GPT Mini for code analysis and generation\n");

        var agent = new SeleniumMigrationAgent();
        await agent.RunAsync();
    }
}

public class SeleniumMigrationAgent
{
    private readonly ProjectAnalyzer _analyzer;
    private readonly CodeGenerator _generator;
    private readonly MigrationOrchestrator _orchestrator;
    private readonly PatternDetector _patternDetector;

    public SeleniumMigrationAgent()
    {
        _analyzer = new ProjectAnalyzer();
        _generator = new CodeGenerator();
        _orchestrator = new MigrationOrchestrator(_analyzer, _generator);
        _patternDetector = new PatternDetector();
    }

    public async Task RunAsync()
    {
        try
        {
            var workspaceRoot = "c:\\repos\\das-playwright-automation-test-suite";
            
            Console.WriteLine($"📁 Scanning workspace: {workspaceRoot}\n");
            
            // Step 1: Analyze existing projects
            var projectsToMigrate = await _orchestrator.IdentifyProjectsForMigrationAsync(workspaceRoot);
            
            Console.WriteLine($"Found {projectsToMigrate.Count} projects to migrate:\n");
            foreach (var project in projectsToMigrate)
            {
                Console.WriteLine($"  • {project.Name}");
                Console.WriteLine($"    Type: {project.ProjectType}");
                Console.WriteLine($"    Path: {project.Path}");
                Console.WriteLine($"    Tests: {project.TestCount}");
                Console.WriteLine($"    Feature Files: {project.FeatureFiles.Count}");
                Console.WriteLine();
            }
            
            // Step 2: Detect Selenium patterns
            Console.WriteLine("Analyzing Selenium patterns in projects...\n");
            var patternSummary = await AnalyzePatternsAsync(projectsToMigrate);
            Console.WriteLine();
            
            // Step 3: Generate migration plans
            Console.WriteLine("Generating migration plans...\n");
            var migrationPlans = await _orchestrator.GenerateMigrationPlansAsync(projectsToMigrate);
            
            // Step 4: Create migration guide
            Console.WriteLine("Creating comprehensive migration guide...\n");
            var guide = await _orchestrator.CreateMigrationGuideAsync(migrationPlans);
            
            // Step 5: Display recommendations
            await DisplayRecommendationsAsync(guide);
            
            Console.WriteLine("\n✅ Migration analysis complete!");
            Console.WriteLine("Review the generated migration guide and plans in the output directory.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            throw;
        }
    }

    private async Task<Dictionary<string, MigrationImpactSummary>> AnalyzePatternsAsync(List<ProjectMetadata> projects)
    {
        var patternSummaries = new Dictionary<string, MigrationImpactSummary>();
        
        foreach (var project in projects)
        {
            var summary = await _patternDetector.AnalyzeProjectPatternsAsync(project);
            patternSummaries[project.Name] = summary;
            
            Console.WriteLine($"  {project.Name}:");
            Console.WriteLine($"    Migration Difficulty: {summary.MigrationDifficulty}");
            Console.WriteLine($"    Selenium Patterns Found: {summary.AllOccurrences.Count}");
            
            var patternSummary = summary.GetPatternSummary();
            foreach (var (pattern, count) in patternSummary.OrderByDescending(x => x.Value).Take(5))
            {
                Console.WriteLine($"      - {pattern}: {count} occurrences");
            }
            Console.WriteLine();
        }
        
        return patternSummaries;
    }

    private async Task DisplayRecommendationsAsync(MigrationGuide guide)
    {
        Console.WriteLine("=== Migration Recommendations ===\n");
        Console.WriteLine($"Total Projects: {guide.TotalProjects}");
        Console.WriteLine($"Estimated Migration Effort: {guide.EstimatedEffort}");
        Console.WriteLine($"Recommended Batch Size: {guide.RecommendedBatchSize} projects\n");
        
        Console.WriteLine("Key Transformation Areas:");
        foreach (var area in guide.TransformationAreas)
        {
            Console.WriteLine($"  • {area}");
        }
        
        Console.WriteLine("\nStandard Configuration Files to Use:");
        Console.WriteLine("  • reqnroll.json (from SFA.DAS.Campaigns.UITests as template)");
        Console.WriteLine("  • appsettings.Environment.json");
        Console.WriteLine("  • Project .csproj structure (referencing SFA.DAS.Framework)");
        
        Console.WriteLine("\nModel Integration:");
        Console.WriteLine("  🔷 Qwen: Deep code analysis for pattern recognition and complexity assessment");
        Console.WriteLine("  🟩 GPT Mini: Code generation for step definitions, hooks, and templates");
        
        await Task.CompletedTask;
    }
}

public class ProjectMetadata
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string ProjectType { get; set; } = string.Empty;
    public int TestCount { get; set; }
    public List<string> Dependencies { get; set; } = new();
    public List<string> FeatureFiles { get; set; } = new();
    public List<string> PageObjects { get; set; } = new();
}

public class MigrationPlan
{
    public string ProjectName { get; set; } = string.Empty;
    public List<string> Steps { get; set; } = new();
    public List<string> RequiredChanges { get; set; } = new();
    public Dictionary<string, string> FileMappings { get; set; } = new();
}

public class MigrationGuide
{
    public int TotalProjects { get; set; }
    public string EstimatedEffort { get; set; } = string.Empty;
    public int RecommendedBatchSize { get; set; }
    public List<string> TransformationAreas { get; set; } = new();
    public List<MigrationPlan> DetailedPlans { get; set; } = new();
}
