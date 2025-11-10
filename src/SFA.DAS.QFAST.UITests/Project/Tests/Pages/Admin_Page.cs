using System.Linq;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class Admin_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What do you want to do?");
    public async Task ValidateOptionsAsync(IEnumerable<string> expectedOptions)
    {
        if (expectedOptions is null || !expectedOptions.Any())
            throw new ArgumentException("No options availabel", nameof(expectedOptions));

        var mainOptions = expectedOptions.ToArray();

        for (var i = 0; i < mainOptions.Length; i++)
        {
            var optionText = mainOptions[i];
            var optionLocator = page.Locator($"label.govuk-radios__label:has-text(\"{optionText}\")");
            await Assertions.Expect(optionLocator).ToBeVisibleAsync();
        }
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Continue" })).ToBeVisibleAsync();
    }
}


