using System.Xml.Linq;

namespace SFA.DAS.SeleniumToPlaywrightMigration;

/// <summary>
/// Analyzes C# test projects to identify Selenium patterns and migration requirements
/// </summary>
public class ProjectAnalyzer
{
    private const string UITestsPattern = "*UITests";
    private const string APITestsPattern = "*APITests";

    public async Task<List<ProjectMetadata>> ScanWorkspaceAsync(string rootPath)
    {
        var projects = new List<ProjectMetadata>();
        
        try
        {
            var srcPath = Path.Combine(rootPath, "src");
            if (!Directory.Exists(srcPath))
                return projects;

            // Find all test projects
            var projectDirs = Directory.GetDirectories(srcPath)
                .Where(d =>
                {
                    var name = new DirectoryInfo(d).Name;
                    return name.Contains("UITests") || name.Contains("APITests");
                })
                .ToList();

            foreach (var projectDir in projectDirs)
            {
                var metadata = await AnalyzeProjectAsync(projectDir);
                if (metadata != null)
                    projects.Add(metadata);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scanning workspace: {ex.Message}");
        }

        return projects;
    }

    public async Task<ProjectMetadata?> AnalyzeProjectAsync(string projectPath)
    {
        try
        {
            var projectName = new DirectoryInfo(projectPath).Name;
            var csprojFile = Directory.GetFiles(projectPath, "*.csproj").FirstOrDefault();

            if (csprojFile == null)
                return null;

            var metadata = new ProjectMetadata
            {
                Name = projectName,
                Path = projectPath,
                ProjectType = DetermineProjectType(projectName),
                TestCount = await CountTestsAsync(projectPath),
                Dependencies = await AnalyzeDependenciesAsync(csprojFile),
                FeatureFiles = await FindFeatureFilesAsync(projectPath),
                PageObjects = await FindPageObjectsAsync(projectPath)
            };

            return metadata;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error analyzing project {projectPath}: {ex.Message}");
            return null;
        }
    }

    private string DetermineProjectType(string projectName)
    {
        return projectName.Contains("UITests") ? "UITests" : "APITests";
    }

    private async Task<int> CountTestsAsync(string projectPath)
    {
        var testMethods = 0;
        
        try
        {
            var csFiles = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);
            
            foreach (var file in csFiles)
            {
                var content = await File.ReadAllTextAsync(file);
                // Count [Test] attributes or test methods
                testMethods += System.Text.RegularExpressions.Regex.Matches(
                    content, 
                    @"\[Test\]|\[Fact\]|public\s+async\s+Task\s+.*Test"
                ).Count;
            }
        }
        catch { }
        
        return testMethods;
    }

    private async Task<List<string>> AnalyzeDependenciesAsync(string csprojFile)
    {
        var dependencies = new List<string>();
        
        try
        {
            var content = await File.ReadAllTextAsync(csprojFile);
            var doc = XDocument.Parse(content);
            
            var packageRefs = doc.Descendants("PackageReference")
                .Select(x => x.Attribute("Include")?.Value)
                .Where(x => x != null)
                .ToList();

            dependencies.AddRange(packageRefs!);
        }
        catch { }
        
        return dependencies;
    }

    private async Task<List<string>> FindFeatureFilesAsync(string projectPath)
    {
        try
        {
            return Directory.GetFiles(projectPath, "*.feature", SearchOption.AllDirectories)
                .Select(f => Path.GetRelativePath(projectPath, f))
                .ToList();
        }
        catch
        {
            return new();
        }
    }

    private async Task<List<string>> FindPageObjectsAsync(string projectPath)
    {
        try
        {
            var pageObjects = new List<string>();
            var projectFolder = Path.Combine(projectPath, "Project");
            
            if (Directory.Exists(projectFolder))
            {
                var csFiles = Directory.GetFiles(projectFolder, "*Page*.cs", SearchOption.AllDirectories);
                pageObjects.AddRange(csFiles.Select(f => Path.GetRelativePath(projectPath, f)));
            }
            
            return pageObjects;
        }
        catch
        {
            return new();
        }
    }

    public async Task<MigrationAssessment> AssessProjectAsync(ProjectMetadata project)
    {
        var assessment = new MigrationAssessment
        {
            ProjectName = project.Name,
            Complexity = DetermineComplexity(project),
            Risks = IdentifyRisks(project),
            Dependencies = project.Dependencies,
            EstimatedHours = CalculateEffort(project)
        };

        return assessment;
    }

    private string DetermineComplexity(ProjectMetadata project)
    {
        var complexity = 0;
        if (project.TestCount > 50) complexity += 2;
        if (project.Dependencies.Any(d => d.Contains("Selenium"))) complexity++;
        if (project.PageObjects.Count > 10) complexity++;
        
        return complexity switch
        {
            <= 1 => "Low",
            2 => "Medium",
            _ => "High"
        };
    }

    private List<string> IdentifyRisks(ProjectMetadata project)
    {
        var risks = new List<string>();
        
        if (project.TestCount > 100)
            risks.Add("Large number of tests may require phased migration");
            
        if (project.Dependencies.Any(d => d.Contains("Selenium")))
            risks.Add("Active Selenium dependencies must be removed");
            
        if (project.PageObjects.Count > 20)
            risks.Add("Complex page object hierarchy requires careful refactoring");

        return risks;
    }

    private int CalculateEffort(ProjectMetadata project)
    {
        var hours = 1; // Base
        hours += (project.TestCount / 10); // ~1 hour per 10 tests
        hours += (project.PageObjects.Count / 5); // ~1 hour per 5 page objects
        
        if (project.Dependencies.Any(d => d.Contains("Selenium")))
            hours += 2; // Remove dependencies
            
        return hours;
    }
}

public class MigrationAssessment
{
    public string ProjectName { get; set; } = string.Empty;
    public string Complexity { get; set; } = string.Empty;
    public List<string> Risks { get; set; } = new();
    public List<string> Dependencies { get; set; } = new();
    public int EstimatedHours { get; set; }
}
