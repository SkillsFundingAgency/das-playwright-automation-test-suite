using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class VacancySearchResultPage(ScenarioContext context) : RaaBasePage(context)
{
    //protected static By Filter => By.CssSelector("#Filter");
    //private static By SearchInput => By.CssSelector("input#search-input");
    //protected static By VacancyStatusSelector => By.CssSelector("[data-label='Status']");

    //protected static By VacancyActionSelector => By.CssSelector("[id^='manage']");
    //protected static By RejectedVacancyActionSelector => By.CssSelector("[data-label='Action']");
    //private static By SearchButton => By.CssSelector(".govuk-button.das-search-form__button");

    protected async Task DraftVacancy()
    {
        //await page.GetByLabel("Filter adverts by").SelectOptionAsync(new[] { "All" });

        //await page.GetByLabel("Filter adverts by").SelectOptionAsync(new[] { "Draft" });

        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Draft adverts");

        //await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        //await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await SearchVacancyMultipleTimes();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and submit" }).ClickAsync();
    }

    protected async Task TransferredDraftVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Draft adverts");

        await SearchVacancyMultipleTimes();

        if (isRaaTransfer)
        {
            await Assertions.Expect(page.Locator(".govuk-tag--purple")).ToContainTextAsync("Transferred from provider");
        }

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and submit" }).ClickAsync();

        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Check your answers before submitting your advert");
    }

    protected async Task ClosedVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Closed adverts");

        await SearchVacancyMultipleTimes();

        if (isRaaTransfer)
        {
            await Assertions.Expect(page.Locator(".govuk-tag--purple")).ToContainTextAsync("Transferred from provider");
        }

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Manage" }).ClickAsync();

        await Assertions.Expect(page.Locator(".govuk-summary-list__row")
            .Filter(new() { Has = page.GetByText("Status", new() { Exact = true }) })
            .Locator(".govuk-tag--grey"))
            .ToContainTextAsync("Closed");
    }

    protected async Task RejectedVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Rejected adverts");

        //await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        //await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await SearchVacancyMultipleTimes();

        if (isRaaTransfer)
        {
            await Assertions.Expect(page.Locator(".govuk-tag--purple")).ToContainTextAsync("Transferred from provider");

            await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and resubmit" }).ClickAsync();

            await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Check your answers before submitting your advert");
        } 
        else
        {
            await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Manage" }).ClickAsync();

            await Assertions.Expect(page.Locator(".govuk-summary-list__row")
                .Filter(new() { Has = page.GetByText("Status", new() { Exact = true }) })
                .Locator(".govuk-tag--grey"))
                .ToContainTextAsync("Rejected");
        }
    }

    protected async Task ArchivedVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Archived adverts");

        //await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        //await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await SearchVacancyMultipleTimes();

        if (isRaaTransfer)
        {
            await Assertions.Expect(page.Locator(".govuk-tag--purple")).ToContainTextAsync("Transferred from provider");
        }

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Manage" }).ClickAsync();

        await Assertions.Expect(page.Locator(".govuk-summary-list__row")
            .Filter(new() { Has = page.GetByText("Status", new() { Exact = true }) })
            .Locator(".govuk-tag--grey"))
            .ToContainTextAsync("Archived");
    }

    protected async Task ReviewVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Adverts with shared applications");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Review" }).ClickAsync();
    }

    public async Task <VacancyCompletedAllSectionsPage> GoToVacancyCompletedPage()
    {
        await page.Locator("[id^='manage']").ClickAsync();

        return await VerifyPageAsync(() => new VacancyCompletedAllSectionsPage(context));
    }

    public async Task<ManageRecruitPage> GoToVacancyManagePage()
    {
        string linkText = (isRaaEpc || isRaaTransfer) ? "Review" : "Manage";
        await page.GetByRole(AriaRole.Link, new() { Name = linkText}).First.ClickAsync();

        return await VerifyPageAsync(() => new ManageRecruitPage(context));
    }

    public async Task<SharedApplicatinsForAVacancyPage> GoToSharedAppsManagePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Review" }).First.ClickAsync();

        return await VerifyPageAsync(() => new SharedApplicatinsForAVacancyPage(context));
    }

    protected async Task SearchVacancyMultipleTimes()
    {
        var advertCountMessage = page.Locator(".govuk-body.govuk-\\!-font-weight-bold");

        for (int attempt = 1; attempt <= 20; attempt++)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).ClearAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" })
                .FillAsync(vacancyTitleDataHelper.VacancyTitle);

            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            var messageText = await advertCountMessage.TextContentAsync();

            if (!string.IsNullOrWhiteSpace(messageText) &&
                messageText.Contains($"{vacancyTitleDataHelper.VacancyTitle}") &&
                messageText.Trim().StartsWith('1'))
            {
                break;
            }

            await page.WaitForTimeoutAsync(2000);
        }
    }
}

public class EmployerVacancySearchResultPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("All adverts");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertPage()
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

        var newApplicationRow = page.Locator("tr.govuk-table__row", new() { Has = page.Locator("strong.govuk-tag", new() { HasTextString = "New" })}).First;

        await newApplicationRow.Locator("a[data-label='application_review']").ClickAsync();

        return await VerifyPageAsync(() => new ManageApplicantPage(context));
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

    public async Task<ViewVacancyPage> NavigateToViewAdvertPage()
    {
        await GoToVacancyManagePage();

        string linkTest = isRaaEmployer ? "View advert" : "View vacancy";

        await page.GetByRole(AriaRole.Link, new() { Name = linkTest, Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ViewVacancyPage(context));
    }
}

public class EmployerDraftVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Draft adverts");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertPage()
    {
        await DraftVacancy();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    public async Task ManageDraftAdvert()
    {
        await TransferredDraftVacancy();
    }
}

public class EmployerClosedVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Closed adverts");
    }

    public async Task ManageClosedAdvert()
    {
        await ClosedVacancy();
    }
}

public class EmployerRejectedVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Rejected adverts");
    }

    public async Task ManageRejectedAdvert()
    {
        await RejectedVacancy();
    }
}

public class EmployerArchivedVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Archived adverts");
    }

    public async Task ManageArchivedAdvert()
    {
        await ArchivedVacancy();
    }
}

public class EmployerSharedApplicationsVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Adverts with shared applications");
    }

    public async Task<ManageApplicantPage> NavigateToManageApplicant()
    {
        await GoToSharedAppsManagePage();

        var newApplicationRow = page.Locator("tr.govuk-table__row", new() { Has = page.Locator("strong.govuk-tag", new() { HasTextString = "Response needed" }) }).First;

        await newApplicationRow.Locator("a[data-label='application_review']").ClickAsync();

        return await VerifyPageAsync(() => new ManageApplicantPage(context));
    }
}