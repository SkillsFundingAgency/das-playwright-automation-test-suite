using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Pages;

public class RecruitmentDynamicHomePage(ScenarioContext context, bool navigate) : RaaBasePage(context)
{
    #region Helpers and Context
    private readonly VacancyTitleDatahelper _vacancyTitleDataHelper = context.GetValue<VacancyTitleDatahelper>();
    private readonly RAADataHelper _raaDataHelper = context.GetValue<RAADataHelper>();
    #endregion

    public const string DraftStatus = "Draft";
    public const string ClosedStatus = "Closed";
    public const string PendingReviewStatus = "Pending review";
    public const string LiveStatus = "Live";
    public const string RejectedStatus = "Rejected";
    private ILocator AddApprenticeDetails => page.GetByText("Add apprentice details", new PageGetByTextOptions { Exact = false });
    private ILocator TRows => page.Locator("tr");
    private ILocator THeader => page.Locator("th");
    private ILocator TData => page.Locator("td");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(AddApprenticeDetails).ToBeVisibleAsync();
    }

    public async Task ContinueToCreateAdvert() => await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    public async Task<PreviewYourAdvertOrVacancyPage> ReviewYourVacancy()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Review your advert" }).ClickAsync();
        return new PreviewYourAdvertOrVacancyPage(context);
    }

    public async Task<RecruitmentDynamicHomePage> ConfirmVacancyTitleAndStatus(string status)
    {
        var titleText = await GetDetails("Title");
        var statusText = await GetDetails("Status");

        await Assertions.Expect(titleText).ToContainTextAsync(_vacancyTitleDataHelper.VacancyTitle);
        await Assertions.Expect(statusText).ToContainTextAsync(status);
        return this;
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> ContinueCreatingYourAdvert()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue creating your advert" }).ClickAsync();
        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    public async Task<EmployerVacancySearchResultPage> GoToVacancyDashboard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Go to your vacancy dashboard" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "View all adverts" }).ClickAsync();
        
        return await VerifyPageAsync(() => new EmployerVacancySearchResultPage(context));
    }

    public async Task<ManageRecruitPage> GoToManageVacancyPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "application" }).ClickAsync();
        return new ManageRecruitPage(context);
    }

    public async Task<RecruitmentDynamicHomePage> ConfirmVacancyDetails(string status, DateTime dateTime)
    {
        await ConfirmVacancyTitleAndStatus(status);
        return await ConfirmClosedDateAndApplicationsLink(dateTime, status);
    }

    public async Task<RecruitmentDynamicHomePage> ConfirmLiveVacancyDetails(string status)
    {
        await ConfirmVacancyDetails(status, _raaDataHelper.VacancyClosing);
        return await ConfirmAddApprenticeDeatilsButton();
    }

    public async Task<RecruitmentDynamicHomePage> ConfirmClosedVacancyDetails(string status)
    {
        await ConfirmVacancyTitleAndStatus(status);
        await ConfirmClosedDateAndApplicationsLink(DateTime.Today, status);
        return await ConfirmAddApprenticeDeatilsButton();
    }

    private async Task<RecruitmentDynamicHomePage> ConfirmClosedDateAndApplicationsLink(DateTime closingDate, string status)
    {
        var closingDateText = await GetDetails("Closing date");

        var dateString = closingDate.ToString("dd MMMM yyyy");
        var dateStringShort = closingDate.ToString("dd MMM yyyy");

        var actualText = await closingDateText.TextContentAsync() ?? string.Empty;

        if (!actualText.Contains(dateString) && !actualText.Contains(dateStringShort))
        {
            throw new Exception($"Expected date '{dateString}' or '{dateStringShort}' not found in '{actualText}'");
        }

        if (status != PendingReviewStatus)
        {
            var applicationsText = await GetDetails("Applications");
            await Assertions.Expect(applicationsText).ToContainTextAsync("application");
        }

        return this;
    }

    private async Task<RecruitmentDynamicHomePage> ConfirmAddApprenticeDeatilsButton()
    {
        await Assertions.Expect(AddApprenticeDetails).ToBeVisibleAsync();
        return this;
    }

    private async Task<ILocator> GetDetails(string headerName)
    {
        var rows = await TRows.AllAsync();

        foreach (var row in rows)
        {
            var header = row.Locator("th");
            var headerText = await header.TextContentAsync() ?? string.Empty;

            if (headerText.Contains(headerName, StringComparison.OrdinalIgnoreCase))
            {
                return row.Locator("td");
            }
        }

        throw new Exception($"{headerName} not found");
    }

    public async Task NavigateToMenuItem(string name)
    {
        await page.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
    }
}
