using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class DoTheyOfferOtherQualificationsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Do they offer Qualifications?");
    }

    public async Task<SuccessPage> YesOfferQualifications()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new SuccessPage(context));
    }

    public async Task<ProviderDetailsPage> NoDoNotOfferQualifications()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
}
