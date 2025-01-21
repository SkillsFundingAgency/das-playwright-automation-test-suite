namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class UnderstandingApprenticeshipBenefitsFundingPage(ScenarioContext context) : EmployerBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Understanding apprenticeship benefits and funding");

    public async Task SelectUnder3Million() => await CalculateFunding(false);

    public async Task SelectOver3Million() => await CalculateFunding(true);

    private async Task CalculateFunding(bool IsOver3Million)
    {
        await driver.Page.GetByLabel(IsOver3Million ? "Over £3 million" : "Under £3 million").CheckAsync();

        await driver.Page.GetByLabel("What training course do you").FillAsync("soft");

        await driver.Page.GetByRole(AriaRole.Option, new() { Name = "Software developer (Level 4)" }).ClickAsync();

        await driver.Page.GetByLabel("How many roles do you have").FillAsync("2");

        await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Calculate funding" }).ClickAsync();

        await Assertions.Expect(driver.Page.Locator("#funding")).ToContainTextAsync("Your estimated funding");
    }
}
