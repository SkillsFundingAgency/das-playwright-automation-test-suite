namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;
    public class OrganisationsDetailPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Organisation's details");
        }

    public async Task<ProviderRoutePage> ConfirmOrganisationDetails()
    {
        await page.Locator("#continue").ClickAsync();
        return await VerifyPageAsync(() => new ProviderRoutePage(context));
    }
}

