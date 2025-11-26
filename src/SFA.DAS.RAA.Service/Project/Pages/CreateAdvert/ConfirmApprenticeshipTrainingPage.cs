namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class ConfirmApprenticeshipTrainingPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm apprenticeship training");
    }

    private static string ExpectedFoundationApprenticeshipText => "Foundation apprenticeship";

    public async Task<EnterTheNameOfTheTrainingProviderPage> ConfirmTrainingproviderAndContinue(bool isFoundationAdvert)
    {
        if (isFoundationAdvert)
        {
            CheckFoundationTag();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EnterTheNameOfTheTrainingProviderPage(context));
    }

    public async Task<ChooseTrainingProviderPage> ConfirmTrainingAndContinue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseTrainingProviderPage(context));
    }

    public async Task<SubmitNoOfPositionsPage> ConfirmAndNavigateToNoOfPositionsPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SubmitNoOfPositionsPage(context));
    }

    public async Task<SummaryOfTheApprenticeshipPage> ConfirmTrainingAndContinueToSummaryPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SummaryOfTheApprenticeshipPage(context));
    }
}
