using Azure;
using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;
using System;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class VacancySearchResultsPage(ScenarioContext context) : RaaBasePage(context)
{
    protected async Task DraftVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Draft vacancies");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by vacancy title, reference" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and submit" }).ClickAsync();
    }

    public async Task<ManageRecruitPage> GoToVacancyManagePage()
    {
        await page.Locator(".govuk-table__cell a").First.ClickAsync();

        return await VerifyPageAsync(() => new ManageRecruitPage(context));
    }
}

public class ProviderVacancySearchResultPage(ScenarioContext context) : VacancySearchResultsPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("All vacancies");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipVacancyPage()
    {
        await DraftVacancy();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    public async Task<ManageApplicantPage> NavigateToManageApplicant()
    {
        await GoToVacancyManagePage();

        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }

        var newApplicationRow = page.Locator("tr.govuk-table__row", new() 
        { 
            Has = page.Locator("strong.govuk-tag:has-text('New'), strong.govuk-tag:has-text('Shared')") 
        }).First;

        await newApplicationRow.Locator("a.govuk-link").ClickAsync();

        return await VerifyPageAsync(() => new ManageApplicantPage(context));
    }

    public async Task<ShareApplicationsPage> NavigateToManageApplicants()
    {
        await GoToVacancyManagePage();

        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "Share multiple applications with employer" }).ClickAsync();
        return await VerifyPageAsync(() => new ShareApplicationsPage(context));
    }

    public async Task<ProviderDoYouWantToShareAnApplicationPage> ProviderShareApplicantWithEmployer()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Share with the employer" }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDoYouWantToShareAnApplicationPage(context));
    }

    public async Task CheckApplicantStatus(string status)
    {
        await GoToVacancyManagePage();

        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }

        await Assertions.Expect(page.Locator("td[data-label='Status'] > strong")).ToContainTextAsync(status);
    }

    public async Task <ManageMultiApplicationsUnsuccessfulPage> NavigateToManageAllApplicantsAndMakeUnsuccessful()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Make multiple applications unsuccessful" }).ClickAsync();
        await page.Locator("#app-checkbox-select-all").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        await page.Locator("#provider-multiple-candidate-feedback").FillAsync(rAADataHelper.OptionalMessage);
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageMultiApplicationsUnsuccessfulPage(context));
    }

    public async Task<ViewVacancyPage> NavigateToViewAdvertPage()
    {
        await GoToVacancyManagePage();

        string linkTest = isRaaEmployer ? "View advert" : "View vacancy";

        await page.GetByRole(AriaRole.Link, new() { Name = linkTest, Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ViewVacancyPage(context));
    }
}

public class ProviderDraftVacanciesListPage(ScenarioContext context) : VacancySearchResultsPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Draft vacancies");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipVacancyPage()
    {
        await DraftVacancy();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}

public class ShareApplicationsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-fieldset__heading")).ToContainTextAsync("Share applications");
    }

    public async Task<ShareMultipleApplicationsReviewPage> SelectMultipleApplicationsAndShareWithEmployer()
    {
        var applications = page.Locator("#app-checkbox-select-all");
        await applications.CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ShareMultipleApplicationsReviewPage(context));
    }
}

public class ShareMultipleApplicationsReviewPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-l")).ToContainTextAsync("Share multiple applications");
    }
    public async Task<ProviderApplicationSharePage> ConfirmSharingMultipleApplications()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderApplicationSharePage(context));
    }
}

public class ManageMultiApplicationsUnsuccessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Make multiple applications unsuccessful");
    }
    public async Task<ApplicationUnsuccessfulPage> FeedbackForMultipleUnsuccessful()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ApplicationUnsuccessfulPage(context));
    }
}
