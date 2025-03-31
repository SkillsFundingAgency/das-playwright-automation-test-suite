namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class CohortDetailsPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#commitmentsHeader")).ToContainTextAsync("Cohort Details");

    public async Task ClickViewUlnLink(string uln)
    {
        await page.GetByRole(AriaRole.Row, new() { Name = uln }).GetByRole(AriaRole.Link).ClickAsync();
    }
}