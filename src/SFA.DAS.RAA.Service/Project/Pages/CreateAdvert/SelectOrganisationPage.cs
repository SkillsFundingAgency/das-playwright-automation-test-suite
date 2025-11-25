namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class SelectOrganisationPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Which organisation is this advert for?" : "What training course will the apprentice take?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<WhichEmployerNameDoYouWantOnYourAdvertPage> SelectOrganisation()
    {
        await SelectRandomOption();

        return await VerifyPageAsync(() => new WhichEmployerNameDoYouWantOnYourAdvertPage(context));
    }

    public async Task<ApprenticeshipTrainingPage> SelectOrganisationMultiOrg()
    {
        await SelectRandomOption();

        return await VerifyPageAsync(() => new ApprenticeshipTrainingPage(context));
    }

    private async Task SelectRandomOption()
    {
        var locators = await page.GetByRole(AriaRole.Radio).AllAsync();

        var locator = RandomDataGenerator.GetRandom(locators);

        await locator.CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}
