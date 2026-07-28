using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class UnderstandingApprenticeshipBenefitsFundingPage(ScenarioContext context) : EmployerBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Understanding apprenticeship benefits and funding");

    public async Task SelectUnder3Million() => await CalculateFunding(false);

    public async Task SelectOver3Million() => await CalculateFunding(true);

    private async Task CalculateFunding(bool isOver3Million)
    {
        await page.GetByLabel(isOver3Million ? "Over £3 million" : "Under £3 million").CheckAsync();

        await page.Locator("#StandardUid").SelectOptionAsync(new[] { new SelectOptionValue { Label = "Software developer (Level 4)" } });

        await page.GetByLabel("How many roles do you have").FillAsync("2");

        await page.GetByRole(AriaRole.Button, new() { Name = "Calculate funding" }).ClickAsync();

        await Assertions.Expect(page.Locator("#funding")).ToContainTextAsync("Your estimated funding");
    }
}