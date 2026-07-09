namespace SFA.DAS.SeleniumToPlaywrightMigration;

/// <summary>
/// Provides utilities and conversion helpers for Selenium to Playwright migration
/// </summary>
public class MigrationHelper
{
    /// <summary>
    /// Maps Selenium By strategies to Playwright locator equivalents
    /// </summary>
    public static readonly Dictionary<string, PlaywrightLocatorEquivalent> SeleniumToPlaywrightLocators = new()
    {
        ["By.Id"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.GetByRole() or page.Locator(\"#id\")",
            Preferred = "page.GetByLabel() or page.GetByRole() when possible",
            Example = "By.Id(\"submitBtn\") → await page.GetByRole(AriaRole.Button, new() { Name = \"Submit\" }).ClickAsync()",
            Priority = "High"
        },
        ["By.ClassName"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.Locator(\".className\")",
            Preferred = "page.GetByRole() or page.GetByLabel()",
            Example = "By.ClassName(\"error-message\") → page.Locator(\".error-message\")",
            Priority = "Medium"
        },
        ["By.CssSelector"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.Locator(\"selector\")",
            Preferred = "Keep CSS selectors (similar API)",
            Example = "By.CssSelector(\"div.content > button\") → page.Locator(\"div.content > button\")",
            Priority = "Low"
        },
        ["By.XPath"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.Locator(\"//xpath\")",
            Preferred = "Convert to role-based or text-based locators",
            Example = "By.XPath(\"//button[text()='Click me']\") → page.GetByRole(AriaRole.Button, new() { Name = \"Click me\" })",
            Priority = "High"
        },
        ["By.LinkText"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.GetByRole(AriaRole.Link, new() { Name = \"text\" })",
            Preferred = "page.GetByRole(AriaRole.Link)",
            Example = "By.LinkText(\"Home\") → page.GetByRole(AriaRole.Link, new() { Name = \"Home\" })",
            Priority = "Low"
        },
        ["By.Name"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.Locator(\"[name='value']\")",
            Preferred = "page.GetByLabel() when associated with label",
            Example = "By.Name(\"email\") → page.GetByLabel(\"Email\") or page.Locator(\"[name='email']\")",
            Priority = "Medium"
        },
        ["By.TagName"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.Locator(\"tagname\")",
            Preferred = "Combine with other attributes",
            Example = "By.TagName(\"button\") → page.GetByRole(AriaRole.Button)",
            Priority = "Low"
        },
        ["By.PartialLinkText"] = new PlaywrightLocatorEquivalent
        {
            Equivalent = "page.Locator(\"a:has-text('partial')\")",
            Preferred = "page.GetByRole(AriaRole.Link).Filter()",
            Example = "By.PartialLinkText(\"Home\") → page.GetByRole(AriaRole.Link).Filter(new() { HasText = \"Home\" })",
            Priority = "Low"
        }
    };

    /// <summary>
    /// Maps Selenium actions to Playwright equivalents
    /// </summary>
    public static readonly Dictionary<string, ActionConversion> SeleniumToPlaywrightActions = new()
    {
        ["Click"] = new ActionConversion
        {
            Selenium = "element.Click()",
            Playwright = "await locator.ClickAsync()",
            Features = "Auto-waits for element to be clickable"
        },
        ["SendKeys"] = new ActionConversion
        {
            Selenium = "element.SendKeys(\"text\")",
            Playwright = "await locator.FillAsync(\"text\") or await locator.TypeAsync(\"text\")",
            Features = "FillAsync clears first, TypeAsync simulates typing character-by-character"
        },
        ["Clear"] = new ActionConversion
        {
            Selenium = "element.Clear()",
            Playwright = "await locator.FillAsync(string.Empty) or await locator.ClearAsync()",
            Features = "FillAsync with empty string or dedicated ClearAsync"
        },
        ["Submit"] = new ActionConversion
        {
            Selenium = "element.Submit()",
            Playwright = "await locator.PressAsync(\"Enter\") or await form.EvaluateAsync(\"form => form.submit()\")",
            Features = "Manual form submission via Enter key or JavaScript"
        },
        ["GetText"] = new ActionConversion
        {
            Selenium = "element.Text",
            Playwright = "await locator.TextContentAsync() or await locator.InnerTextAsync()",
            Features = "TextContentAsync includes hidden text, InnerTextAsync is rendered text"
        },
        ["IsDisplayed"] = new ActionConversion
        {
            Selenium = "element.Displayed",
            Playwright = "await locator.IsVisibleAsync()",
            Features = "Checks if element is visible in viewport"
        },
        ["IsEnabled"] = new ActionConversion
        {
            Selenium = "element.Enabled",
            Playwright = "await locator.IsEnabledAsync()",
            Features = "Checks if element is enabled and clickable"
        },
        ["Hover"] = new ActionConversion
        {
            Selenium = "new Actions(driver).MoveToElement(element).Perform()",
            Playwright = "await locator.HoverAsync()",
            Features = "Direct hover without action chains"
        },
        ["DoubleClick"] = new ActionConversion
        {
            Selenium = "new Actions(driver).DoubleClick(element).Perform()",
            Playwright = "await locator.DblClickAsync()",
            Features = "Direct double-click support"
        },
        ["RightClick"] = new ActionConversion
        {
            Selenium = "new Actions(driver).ContextClick(element).Perform()",
            Playwright = "await locator.ClickAsync(new() { Button = MouseButton.Right })",
            Features = "Context click with button specification"
        },
        ["Drag and Drop"] = new ActionConversion
        {
            Selenium = "new Actions(driver).DragAndDrop(source, target).Perform()",
            Playwright = "await source.DragToAsync(target)",
            Features = "Native drag and drop support"
        },
        ["SelectDropdown"] = new ActionConversion
        {
            Selenium = "new SelectElement(element).SelectByText(\"option\")",
            Playwright = "await locator.SelectOptionAsync(new[] { \"option\" })",
            Features = "Direct select option handling"
        }
    };

    /// <summary>
    /// Provides wait strategy conversions
    /// </summary>
    public static string GetWaitConversion(string seleniumWaitPattern)
    {
        return seleniumWaitPattern switch
        {
            var s when s.Contains("ExpectedConditions.PresenceOfElement") =>
                "await locator.WaitForAsync() - Playwright auto-waits for presence",
            
            var s when s.Contains("ExpectedConditions.VisibilityOfElement") =>
                "await locator.IsVisibleAsync() - Check visibility directly",
            
            var s when s.Contains("ExpectedConditions.ElementToBeClickable") =>
                "No explicit wait needed - Playwright auto-waits for clickable state",
            
            var s when s.Contains("ExpectedConditions.InvisibilityOfElement") =>
                "await locator.IsHiddenAsync() - Wait for element to hide",
            
            var s when s.Contains("Thread.Sleep") =>
                "Avoid fixed delays - Use page.WaitForLoadStateAsync(LoadState.NetworkIdle)",
            
            _ => "Review wait strategy and convert to Playwright's auto-waiting where possible"
        };
    }

    /// <summary>
    /// Generates a conversion guide for a specific Selenium pattern
    /// </summary>
    public static ConversionGuide GenerateConversionGuide(string seleniumCode)
    {
        var guide = new ConversionGuide { OriginalCode = seleniumCode };

        // Identify pattern type
        if (seleniumCode.Contains("FindElement"))
            guide.PatternType = "Locator";
        else if (seleniumCode.Contains("SendKeys") || seleniumCode.Contains("Click"))
            guide.PatternType = "Action";
        else if (seleniumCode.Contains("Wait") || seleniumCode.Contains("ExpectedConditions"))
            guide.PatternType = "Wait";
        else if (seleniumCode.Contains("Actions"))
            guide.PatternType = "ActionChain";
        else
            guide.PatternType = "Other";

        guide.Recommendations = guide.PatternType switch
        {
            "Locator" => GenerateLocatorRecommendations(seleniumCode),
            "Action" => GenerateActionRecommendations(seleniumCode),
            "Wait" => GenerateWaitRecommendations(seleniumCode),
            "ActionChain" => GenerateActionChainRecommendations(seleniumCode),
            _ => new List<string> { "Review code manually for conversion needs" }
        };

        return guide;
    }

    private static List<string> GenerateLocatorRecommendations(string code)
    {
        return new List<string>
        {
            "1. Identify the By strategy (Id, XPath, CSS, etc.)",
            "2. Look for accessible attributes (role, label, text, placeholder)",
            "3. Prefer accessibility-first locators (GetByRole, GetByLabel)",
            "4. Fallback to GetByText() for custom elements",
            "5. Use test-id only as last resort: GetByTestId()",
            "6. Make locators resilient to minor DOM changes"
        };
    }

    private static List<string> GenerateActionRecommendations(string code)
    {
        return new List<string>
        {
            "1. Replace element.Click() with await locator.ClickAsync()",
            "2. Replace SendKeys() with FillAsync() or TypeAsync()",
            "3. Remove GetAttribute() calls - use InputValueAsync() for inputs",
            "4. Replace .Displayed with await locator.IsVisibleAsync()",
            "5. Replace .Enabled with await locator.IsEnabledAsync()",
            "6. Add async/await to all action methods",
            "7. Verify auto-waiting behavior meets test needs"
        };
    }

    private static List<string> GenerateWaitRecommendations(string code)
    {
        return new List<string>
        {
            "1. Remove WebDriverWait declarations - Playwright auto-waits",
            "2. Replace ExpectedConditions with direct checks (IsVisibleAsync, IsEnabledAsync)",
            "3. Use page.WaitForLoadStateAsync() for navigation waits",
            "4. Use page.WaitForSelectorAsync() only if absolutely needed",
            "5. For custom wait conditions, use page.WaitForFunctionAsync()",
            "6. Replace Thread.Sleep with proper wait methods"
        };
    }

    private static List<string> GenerateActionChainRecommendations(string code)
    {
        return new List<string>
        {
            "1. Replace Actions chain with individual locator calls",
            "2. Hover: Use locator.HoverAsync()",
            "3. DoubleClick: Use locator.DblClickAsync()",
            "4. RightClick: Use locator.ClickAsync(new() { Button = MouseButton.Right })",
            "5. Drag/Drop: Use source.DragToAsync(target)",
            "6. Key combinations: Use locator.PressAsync() with modifiers"
        };
    }
}

public class PlaywrightLocatorEquivalent
{
    public string Equivalent { get; set; } = string.Empty;
    public string Preferred { get; set; } = string.Empty;
    public string Example { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
}

public class ActionConversion
{
    public string Selenium { get; set; } = string.Empty;
    public string Playwright { get; set; } = string.Empty;
    public string Features { get; set; } = string.Empty;
}

public class ConversionGuide
{
    public string OriginalCode { get; set; } = string.Empty;
    public string PatternType { get; set; } = string.Empty;
    public List<string> Recommendations { get; set; } = new();
}
