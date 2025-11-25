namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class PreviewYourAdvertOrVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Preview your advert" : "Preview your vacancy";

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(PageTitle);
    }

    private static string Submit => (".govuk-button[data-automation='submit-button']");
    private static string DeleteVacancyButton => ("a[data-automation='delete-button']");

    //public async Task<DeleteVacancyQuestionPage> DeleteVacancy()
    //{
    //    await page.Locator(DeleteVacancyButton).ClickAsync();

    //    return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    //}


    //public async Task<ResubmittedVacancyReferencePage> ResubmitVacancy()
    //{
    //    await page.Locator(Submit).ClickAsync();

    //    return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    //}


    private string ContactDetails() => isRaaEmployer ? "a[data-automation='link-employer-contact-details']" : "a[data-automation='link-provider-contact-details']";

}

