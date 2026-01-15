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

    public async Task<CheckYourAnswersPage> ReturnToPreviousPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to previous page to submit" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }

    //public async Task<ResubmittedVacancyReferencePage> ResubmitVacancy()
    //{
    //    await page.Locator(Submit).ClickAsync();

    //    return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    //}


    private string ContactDetails() => isRaaEmployer ? "a[data-automation='link-employer-contact-details']" : "a[data-automation='link-provider-contact-details']";

}

public class DeleteVacancyQuestionPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string SubTitle = isRaaEmployer ? "Are you sure you want to delete this advert?" : "Are you sure you want to delete the vacancy?";

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(SubTitle);
    }

    public async Task YesDeleteAdvert()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, delete this advert now" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await Assertions.Expect(page.GetByLabel("Success").Locator("h3")).ToContainTextAsync("has been deleted.");

    }

    //public async Task<ProviderVacancySearchResultPage> YesDeleteVacancy()
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, delete this advert now" }).CheckAsync();

    //    await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    //    return await VerifyPageAsync(() => new ProviderVacancySearchResultPage(context));
    //}

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> NoDeleteVacancy()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, do not delete this advert" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}
