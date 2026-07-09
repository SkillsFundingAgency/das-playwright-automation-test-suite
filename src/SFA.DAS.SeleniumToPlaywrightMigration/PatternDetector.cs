using System.Text.RegularExpressions;

namespace SFA.DAS.SeleniumToPlaywrightMigration;

/// <summary>
/// Detects Selenium patterns and code structures for targeted migration assistance
/// </summary>
public class PatternDetector
{
    private static readonly Dictionary<string, PatternInfo> SeleniumPatterns = new()
    {
        ["FindElement"] = new PatternInfo 
        { 
            Pattern = @"FindElement\(By\.", 
            Severity = "High",
            Recommendation = "Convert to Playwright's page.GetByRole(), page.GetByLabel(), etc.",
            ReplacementStrategy = "Use Playwright locators based on accessibility"
        },
        ["WebDriverWait"] = new PatternInfo 
        { 
            Pattern = @"WebDriverWait|Wait\.Until", 
            Severity = "High",
            Recommendation = "Remove explicit waits - Playwright has built-in auto-waiting",
            ReplacementStrategy = "Actions automatically wait for element to be actionable"
        },
        ["SendKeys"] = new PatternInfo 
        { 
            Pattern = @"\.SendKeys\(|\.Text\s*=", 
            Severity = "Medium",
            Recommendation = "Use page.FillAsync() or page.TypeAsync()",
            ReplacementStrategy = "await locator.FillAsync(text)"
        },
        ["Click"] = new PatternInfo 
        { 
            Pattern = @"\.Click\(\)", 
            Severity = "Low",
            Recommendation = "Convert to async ClickAsync()",
            ReplacementStrategy = "await locator.ClickAsync()"
        },
        ["ActionChains"] = new PatternInfo 
        { 
            Pattern = @"Actions\(|ActionChains|MoveToElement|DoubleClick", 
            Severity = "High",
            Recommendation = "Use Playwright's action methods",
            ReplacementStrategy = "await locator.HoverAsync(); await locator.DblClickAsync();"
        },
        ["SelectElement"] = new PatternInfo 
        { 
            Pattern = @"new SelectElement|\.SelectByText|\.SelectByValue", 
            Severity = "Medium",
            Recommendation = "Use page.SelectOptionAsync()",
            ReplacementStrategy = "await locator.SelectOptionAsync(new[] { value })"
        },
        ["ScreenshotAsFile"] = new PatternInfo 
        { 
            Pattern = @"GetScreenshot|SaveAsFile", 
            Severity = "Low",
            Recommendation = "Use Playwright's built-in screenshot",
            ReplacementStrategy = "await page.ScreenshotAsync(new() { Path = \"file.png\" })"
        },
        ["ImplicitWait"] = new PatternInfo 
        { 
            Pattern = @"Manage\(\)\.Timeouts\(\)\.ImplicitWait", 
            Severity = "Medium",
            Recommendation = "Configure timeout in Playwright context creation",
            ReplacementStrategy = "Use context.SetDefaultNavigationTimeoutAsync()"
        },
        ["IAlert"] = new PatternInfo 
        { 
            Pattern = @"SwitchTo\(\)\.Alert|IAlert", 
            Severity = "Medium",
            Recommendation = "Use Playwright's dialog event handling",
            ReplacementStrategy = "page.Dialog += async dialog => await dialog.AcceptAsync();"
        },
        ["Frame/IFrame"] = new PatternInfo 
        { 
            Pattern = @"SwitchTo\(\)\.Frame|FrameElement|IWebElement.*iframe", 
            Severity = "High",
            Recommendation = "Use Playwright's frameLocator API",
            ReplacementStrategy = "var frame = page.FrameLocator(selector); await frame.GetByRole(...).ClickAsync();"
        }
    };

    public PatternDetectionResult AnalyzeFile(string filePath)
    {
        var result = new PatternDetectionResult { FilePath = filePath };

        try
        {
            var content = File.ReadAllText(filePath);
            
            foreach (var (patternName, patternInfo) in SeleniumPatterns)
            {
                var matches = Regex.Matches(content, patternInfo.Pattern, RegexOptions.IgnoreCase);
                
                if (matches.Count > 0)
                {
                    result.DetectedPatterns.Add(new PatternOccurrence
                    {
                        PatternName = patternName,
                        Count = matches.Count,
                        Severity = patternInfo.Severity,
                        FirstMatch = ExtractContext(content, matches[0].Index),
                        Recommendation = patternInfo.Recommendation,
                        ReplacementStrategy = patternInfo.ReplacementStrategy
                    });
                }
            }
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
        }

        return result;
    }

    public async Task<MigrationImpactSummary> AnalyzeProjectPatternsAsync(ProjectMetadata project)
    {
        var summary = new MigrationImpactSummary { ProjectName = project.Name };

        var csFiles = Directory.GetFiles(project.Path, "*.cs", SearchOption.AllDirectories);
        var patternCounts = new Dictionary<string, int>();

        foreach (var file in csFiles)
        {
            var result = AnalyzeFile(file);
            
            foreach (var occurrence in result.DetectedPatterns)
            {
                if (patternCounts.ContainsKey(occurrence.PatternName))
                    patternCounts[occurrence.PatternName] += occurrence.Count;
                else
                    patternCounts[occurrence.PatternName] = occurrence.Count;
                    
                summary.AllOccurrences.Add(occurrence);
            }
        }

        // Calculate migration difficulty
        var highSeverity = summary.AllOccurrences.Count(p => p.Severity == "High");
        var mediumSeverity = summary.AllOccurrences.Count(p => p.Severity == "Medium");

        summary.MigrationDifficulty = (highSeverity, mediumSeverity) switch
        {
            (> 50, _) => "Very High",
            (> 20, _) => "High",
            (_, > 50) => "High",
            (_, > 20) => "Medium",
            _ => "Low"
        };

        return summary;
    }

    private string ExtractContext(string content, int matchIndex)
    {
        var start = Math.Max(0, matchIndex - 50);
        var end = Math.Min(content.Length, matchIndex + 100);
        var context = content.Substring(start, end - start);
        return "..." + context.Replace("\n", " ").Trim() + "...";
    }
}

public class PatternInfo
{
    public string Pattern { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    public string ReplacementStrategy { get; set; } = string.Empty;
}

public class PatternOccurrence
{
    public string PatternName { get; set; } = string.Empty;
    public int Count { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string FirstMatch { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    public string ReplacementStrategy { get; set; } = string.Empty;
}

public class PatternDetectionResult
{
    public string FilePath { get; set; } = string.Empty;
    public List<PatternOccurrence> DetectedPatterns { get; set; } = new();
    public string? Error { get; set; }
}

public class MigrationImpactSummary
{
    public string ProjectName { get; set; } = string.Empty;
    public List<PatternOccurrence> AllOccurrences { get; set; } = new();
    public string MigrationDifficulty { get; set; } = string.Empty;

    public Dictionary<string, int> GetPatternSummary()
    {
        return AllOccurrences
            .GroupBy(p => p.PatternName)
            .ToDictionary(g => g.Key, g => g.Sum(p => p.Count));
    }
}
