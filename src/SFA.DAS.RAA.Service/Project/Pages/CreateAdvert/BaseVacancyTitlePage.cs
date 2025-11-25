namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public abstract class BaseVacancyTitlePage(ScenarioContext context) : RaaBasePage(context)
{
    public async Task<SelectOrganisationPage> EnterAdvertTitleMultiOrg()
    {
        await ChangeVacancyTitle();

        return await VerifyPageAsync(() => new SelectOrganisationPage(context));
    }

    public async Task<ApprenticeshipTrainingPage> EnterVacancyTitle()
    {
        await ChangeVacancyTitle();

        return await VerifyPageAsync(() => new ApprenticeshipTrainingPage(context));
    }

    public async Task<HaveYouAlreadyFoundTrainingPage> EnterVacancyTitleForTheFirstAdvert()
    {
        await ChangeVacancyTitle();

        return await VerifyPageAsync(() => new HaveYouAlreadyFoundTrainingPage(context));
    }

    public async Task<CheckYourAnswersPage> UpdateVacancyTitleAndGoToCheckYourAnswersPage()
    {
        await ChangeVacancyTitle();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }

    private async Task ChangeVacancyTitle()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What do you want to call this" }).FillAsync(rAADataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}
