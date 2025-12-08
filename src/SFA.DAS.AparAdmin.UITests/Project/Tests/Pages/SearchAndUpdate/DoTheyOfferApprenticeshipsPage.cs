using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class OfferApprenticeshipsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Do they offer apprenticeships?");
    }
    public async Task<DoTheyOfferApprenticeshipUnitsPage> YesOfferApprenticeships()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new DoTheyOfferApprenticeshipUnitsPage(context));
    }
    public async Task<DoTheyOfferApprenticeshipUnitsPage> NoDoNotOfferApprenticeships()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new DoTheyOfferApprenticeshipUnitsPage(context));
    }
}
