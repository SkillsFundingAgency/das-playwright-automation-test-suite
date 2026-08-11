namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class CheckYourAnswer_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Check your answers" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task VerifyLevelSelection()
    {
        var levelLocator = page.GetByText("Level(s)", new() { Exact = true }).Locator("..").Locator("dd.govuk-summary-list__value");
        var actualLevel = (await levelLocator.InnerTextAsync()).Trim();
        var expectedLevels = new[]
        {
        "Entry level",
        "Level 1",
        "Level 1/Level 2",
        "Level 2",
        "Level 3",
        "Level 4",
        "Level 5",
        "Level 6"
        };  
        foreach (var level in expectedLevels)
        {
            if (!actualLevel.Contains(level, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                $"Incorrect level selection. Expected '{level}' to be selected, " +
                $"but the actual selected levels are: '{actualLevel}'.");
            }
        }
    }
    public async Task VerifyTypeSelection()
    {
        var typeLocator = page.GetByText("Type(s)", new() { Exact = true }).Locator("..").Locator("dd.govuk-summary-list__value");
        var actualType = (await typeLocator.InnerTextAsync()).Trim();
        var expectedTypes = new[]
        {
        "Advanced Extension Award",
        "Alternative Academic Qualification",
        "Digital Functional Skills Qualification",
        "English For Speakers of Other Languages",
        "Essential Digital Skills",
        "Functional Skills",
        "GCE A Level",
        "GCE AS Level",
        "GCSE (9 to 1)",
        "Occupational Qualification",
        "Other General Qualification",
        "Other Life Skills Qualification",
        "Other Vocational Qualification",
        "Performing Arts Graded Examination",
        "Project",
        "Technical Qualification",
        "Vocationally-Related Qualification"
        };
        foreach (var type in expectedTypes)
        {
            if (!actualType.Contains(type, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    $"Incorrect type selection. Expected '{type}' to be selected, " +
                    $"but the actual selected types are: '{actualType}'.");
            }
        }
    }
    public async Task VerifySSASelection()
    {
        var expectedMessage = "You have selected all SSAs and excluded 0 SSAs.";
        var actualMessage = (await page.Locator("p").Filter(new() { HasText = "You have selected all SSAs" }).InnerTextAsync()).Trim();
        if (!string.Equals(
            actualMessage,
            expectedMessage,
            StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Incorrect SSA selection. Expected '{expectedMessage}', " +
                $"but the actual message was '{actualMessage}'.");
        }
    }
    public async Task VerifyAOSelection()
    {
        var expectedMessage = "You have selected all AOs and excluded 0 AOs.";
        var actualMessage = (await page.Locator("p").Filter(new() { HasText = "You have selected all AOs" }).InnerTextAsync()).Trim();
        if (!string.Equals(
            actualMessage,
            expectedMessage,
            StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Incorrect AO selection. Expected '{expectedMessage}', " +
                $"but the actual message was '{actualMessage}'.");
        }
    }
}