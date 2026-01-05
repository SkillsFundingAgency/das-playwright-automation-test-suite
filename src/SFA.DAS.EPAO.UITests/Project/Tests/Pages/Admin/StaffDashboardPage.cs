using SFA.DAS.EPAO.UITests.Project.Tests.Pages.EPAOWithdrawalPages;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

public class StaffDashboardPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Staff dashboard");

    private static string NewWithdrawalApplications => ("a.govuk-link[href='/WithdrawalApplication#new']");
    private static string FeedbackWithdrawalApplications => ("a.govuk-link[href='/WithdrawalApplication#feedback']");

    public async Task<SearchPage> Search()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new SearchPage(context));
    }

    public async Task<OrganisationSearchPage> SearchEPAO()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search organisations" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationSearchPage(context));
    }

    public async Task<AddOrganisationPage> AddOrganisation()
    {
        var uri = UriHelper.GetAbsoluteUri(UrlConfig.Admin_BaseUrl, $"register/add-organisation");

        await Navigate(uri);
        await Navigate(uri);
        await Navigate(uri);

        return await VerifyPageAsync(() => new AddOrganisationPage(context));
    }

    public async Task<BatchSearchPage> SearchEPAOBatch()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Batch search" }).ClickAsync();

        return await VerifyPageAsync(() => new BatchSearchPage(context));
    }

    public async Task<AD_WithdrawalApplicationsPage> GoToNewWithdrawalApplications()
    {
        await page.Locator(NewWithdrawalApplications).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalApplicationsPage(context));
    }

    public async Task<AD_WithdrawalApplicationsPage> GoToFeedbackWithdrawalApplications()
    {
        await page.Locator(FeedbackWithdrawalApplications).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalApplicationsPage(context));
    }
}

public class SearchPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Search");

    public async Task<SearchResultsPage> SearchFor(string keyword)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search standards" }).FillAsync(keyword);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search standard certificates" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchResultsPage(context));
    }
}

public class SearchResultsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Search results");

    public async Task<CertificateDetailsPage> SelectACertificate()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = ePAOAdminDataHelper.GivenNames }).ClickAsync();

        return await VerifyPageAsync(() => new CertificateDetailsPage(context));
    }
}


public class CertificateDetailsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Standard");


    public async Task<AreYouSureYouWantToDeletePage> ClickDeleteCertificateLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Delete certificate" }).ClickAsync();

        return await VerifyPageAsync(() => new AreYouSureYouWantToDeletePage(context));
    }

    public async Task<AmendReasonPage> ClickAmendCertificateLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Amend certificate information" }).ClickAsync();

        return await VerifyPageAsync(() => new AmendReasonPage(context));
    }

    public async Task<ReprintReasonPage> ClickReprintCertificateLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Request certificate reprint" }).ClickAsync();

        return await VerifyPageAsync(() => new ReprintReasonPage(context));
    }

    public async Task ClickShowAllHistory()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Show all history" }).ClickAsync();
    }

    public async Task VerifyActionHistoryItem(int logIndex, string action)
    {
        var by = ($".govuk-table__row:nth-child({logIndex}) td[data-label='Action']");

        await Assertions.Expect(page.Locator(by)).ToContainTextAsync(action);

    }

    public async Task VerifyIncidentNumber(int logIndex, string incidentNumber)
    {
        var by = ($".govuk-table__row:nth-child({logIndex}) td[data-label='Change'] p:nth-child(2)");

        await Assertions.Expect(page.Locator(by)).ToContainTextAsync(incidentNumber);
    }

    public async Task VerifyFirstReason(int logIndex, string reason)
    {
        var by = ($".govuk-table__row:nth-child({logIndex}) td[data-label='Change'] li");

        await Assertions.Expect(page.Locator(by)).ToContainTextAsync(reason);
    }
}

public class ReprintReasonPage(ScenarioContext context) : ConfirmReasonBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Are you sure this certificate needs reprinting?");
}

public class AreYouSureYouWantToDeletePage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Are you sure you want to delete");

    public async Task<AuditDetailsPage> ClickYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AuditDetailsPage(context));
    }
}

public class AuditDetailsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Audit details");

    public async Task<CheckYourAnswersBeforeDeletingThisCertificatePage> EnterAuditDetails()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What’s the reason for" }).FillAsync("EAPO Entered incorrect details");

        await page.Locator("#IncidentNumber").FillAsync("INC-014589527");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersBeforeDeletingThisCertificatePage(context));
    }
}

public class CheckYourAnswersBeforeDeletingThisCertificatePage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your answers before deleting this certificate");

    public async Task<YouHaveSuccessfullyDeletedPage> ClickDeleteCertificateButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Delete certificate" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveSuccessfullyDeletedPage(context));
    }
}

public class YouHaveSuccessfullyDeletedPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("You’ve successfully deleted");

    public async Task<StaffDashboardPage> ClickReturnToDashboard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to staff dashboard" }).ClickAsync();

        return await VerifyPageAsync(() => new StaffDashboardPage(context));
    }
}