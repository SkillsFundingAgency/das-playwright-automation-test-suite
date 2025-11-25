using SFA.DAS.RAA.DataGenerator.Project;

namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class CheckYourAnswersPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Check your answers" : "Check your answers before submitting your vacancy";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<PreviewYourAdvertOrVacancyPage> PreviewAdvert()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Preview advert before" }).ClickAsync();

        return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    }

    public async Task<PreviewYourAdvertOrVacancyPage> PreviewVacancy()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Preview advert before" }).ClickAsync();

        return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    }

    public async Task<VacancyReferencePage> SubmitAdvert()
    {
        if (IsFoundationAdvert)
        {
            CheckFoundationTag();
        }
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit advert" }).ClickAsync();

        return await VerifyPageAsync(() => new VacancyReferencePage(context));
    }

    public async Task<AdditionalQuestionsPage> UpdateAdditionalQuestion()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   additional questions" }).ClickAsync();

        return await VerifyPageAsync(() => new AdditionalQuestionsPage(context));
    }
}

public class VacancyReferencePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(VacancyReferenceNumber)).ToContainTextAsync("VAC");
    }

    private static string VacancyConfirmationSelector => (".govuk-panel--confirmation");

    protected static string VacancyReferenceNumber => (".govuk-panel--confirmation strong");

    public async Task SetVacancyReference()
    {
        string referenceNumber = await page.Locator(VacancyReferenceNumber).TextContentAsync();

        objectContext.SetVacancyReference(RegexHelper.GetVacancyReference(referenceNumber));
    }

    public async Task<string> GetConfirmationMessage() => await page.Locator(VacancyConfirmationSelector).TextContentAsync();
}