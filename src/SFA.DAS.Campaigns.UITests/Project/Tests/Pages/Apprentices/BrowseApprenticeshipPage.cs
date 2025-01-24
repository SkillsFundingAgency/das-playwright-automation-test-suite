namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class BrowseApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Browse apprenticeships before you apply");

    public async Task<BrowseApprenticeshipResultsPage> SearchForAnApprenticeship()
    {
        await page.GetByLabel("Select an interest").SelectOptionAsync(["Digital"]);

        await page.GetByLabel("Enter your postcode").FillAsync("CV1 2WT");

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return await VerifyPageAsync(() => new BrowseApprenticeshipResultsPage(context));
    }
}
