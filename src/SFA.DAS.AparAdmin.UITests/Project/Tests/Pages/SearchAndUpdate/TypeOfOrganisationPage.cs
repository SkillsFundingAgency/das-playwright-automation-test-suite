using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class TypeOfOrganisationPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Choose the type of organisation for");
    }
    public async Task<ProviderDetailsPage> YesChangeTypeofOrganisation(string orgtype)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = orgtype }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }

}
