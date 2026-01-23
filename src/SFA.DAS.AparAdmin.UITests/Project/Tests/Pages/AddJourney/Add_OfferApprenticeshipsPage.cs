namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

public class Add_OfferApprenticeshipsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Do they offer apprenticeships?");
    }
    public async Task<ApprenticeshipsUnitsPage> YesOfferApprenticeship()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ApprenticeshipsUnitsPage(context));
    }
}
