using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class TypeOfOrganisationPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Choose the type of organisation for");
    }

    public async Task<SuccessPage> YesChangeTypeofOrganisation()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new SuccessPage(context));
    }

    public async Task<ProviderDetailsPage> YesDoNotChangeTypeofOrganisation()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
}
