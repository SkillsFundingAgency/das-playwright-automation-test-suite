namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class DescriptionPage(ScenarioContext context) : RaaBasePage(context)
{
    private static string TrainingDescription => ("#TrainingDescription_ifr");
    private static string VacancyDescription => ("#AdditionalTrainingDescription_ifr");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How the apprentice will train");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> EnterAllDescription()
    {
        await EnterVacancyAndTrainingDetails();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    private async Task EnterVacancyAndTrainingDetails()
    {
        await page.Locator(VacancyDescription).ContentFrame.Locator("#tinymce").FillAsync(rAADataHelper.VacancyShortDescription);

        await page.Locator(TrainingDescription).ContentFrame.Locator("#tinymce").FillAsync(rAADataHelper.TrainingDetails);
    }
}
