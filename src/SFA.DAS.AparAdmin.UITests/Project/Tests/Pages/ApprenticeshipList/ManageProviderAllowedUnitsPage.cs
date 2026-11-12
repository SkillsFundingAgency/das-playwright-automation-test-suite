namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

    public class ManageProviderAllowedUnitsPage(ScenarioContext context) :  AparAdminBasePage(context)
    {
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage apprenticeship units this provider is allowed to deliver");
    }
    public async Task ClickAddAnAppreticeshipUnit()
    {
        await page.GetByRole(AriaRole.Link, new() {Name = "Add an apprenticeship unit"}).ClickAsync();
    }
    }