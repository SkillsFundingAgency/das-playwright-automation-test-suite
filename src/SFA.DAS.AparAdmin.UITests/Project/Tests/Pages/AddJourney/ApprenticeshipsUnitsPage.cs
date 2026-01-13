namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

public class ApprenticeshipsUnitsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Do they offer apprenticeship units?");
    }
    public async Task<OrganisationsPage> YesOfferApprenticeshipUnits()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new OrganisationsPage(context));
    }
}
