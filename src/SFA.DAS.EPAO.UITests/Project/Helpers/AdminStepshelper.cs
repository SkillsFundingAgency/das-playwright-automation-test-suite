using SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.EPAO.UITests.Project.Helpers;

public class AdminStepshelper
{
    public static async Task<OrganisationDetailsPage> SearchEpaoRegister(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.SearchEPAO();

        var page1 = await page.SearchForAnOrganisation();

        return await page1.SelectAnOrganisation();
    }

    public static async Task<OrganisationDetailsPage> AddOrganisation(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.AddOrganisation();

        return await page.EnterDetails();
    }

    public static async Task<CertificateDetailsPage> SearchAssessments(StaffDashboardPage staffDashboardPage, string uln)
    {
        var page = await staffDashboardPage.Search();

        var page1 = await page.SearchFor(uln);
        
        return await page1.SelectACertificate();
    }

    public static async Task<OrganisationDetailsPage> MakeEPAOOrganisationLive(StaffDashboardPage staffDashboardPage)
    {
        var page = await SearchEpaoRegister(staffDashboardPage);

        await page.VerifyOrganisationStatus("New");

        var page1 = await page.EditOrganisation();

        var page2 = await page1.MakeOrgLive();

        await page2.VerifyOrganisationStatus("Live");

        return page2;
    }

    //public static async Task<StaffDashboardPage> ApproveAStandard(StaffDashboardPage staffDashboardPage)
    //{
    //    var page = await staffDashboardPage.GoToNewStandardApplications();

    //    var page1 = await page.GoToNewStandardApplicationOverviewPage();

    //    var page2 = await page1.GoToApplyToAssessAStandardPage();

    //    var page3 = await page2.SelectYesAndContinue();

    //    var page4 = await page3.ReturnToOrganisationApplicationsPage();

    //    staffDashboardPage = await page4.ReturnToDashboard();

    //    var page5 = await staffDashboardPage.GoToInProgressStandardApplication();

    //    var page6 = await page5.GoToInProgressStandardApplicationOverviewPage();

    //    var page7 = await page6.CompleteReview();

    //    var page8 = await page7.ApproveApplication();

    //    var page9 = await page8.ReturnToStandardApplications();

    //    return await page9.ReturnToDashboard();
    //}

    //public static async Task<StaffDashboardPage> ApproveAnOrganisation(StaffDashboardPage staffDashboardPage, bool approveFinancialAssesment)
    //{
    //    var page = await staffDashboardPage.GoToNewOrganisationApplications();

    //    var page1 = await page.GoToNewOrganisationApplicationOverviewPage();

    //    var page2 = await page1.GoToNewOrganisationDetailsPage();

    //    var page3 = await page2.SelectYesAndContinue();

    //    var page4 = await page3.GoToNewOrgDeclarationsPage();

    //    var page5 = await page4.SelectYesAndContinue();

    //    var page6 = await page5.ReturnToOrganisationApplicationsPage();

    //    staffDashboardPage = await page6.ReturnToDashboard();

    //    if (approveFinancialAssesment)
    //    {
    //        var page7 = await staffDashboardPage.GoToNewFinancialAssesmentPage();

    //        var page8 = await page7.GoToNewApplicationOverviewPage();

    //        var page9 = await page8.SelectGoodAndContinue();

    //        var page10 = await page9.ReturnToAccountHome();

    //        staffDashboardPage = await page10.ReturnToDashboard();
    //    }

    //    var page11 = await staffDashboardPage.GoToInProgressOrganisationApplication();

    //    var applicationOverview = await page11.GoToInProgressOrganisationApplicationOverviewPage();

    //    if (approveFinancialAssesment)
    //    {
    //        var page12 = await applicationOverview.GoToFinancialhealthAssesmentPage();

    //        applicationOverview = await page12.SelectYesAndContinue();
    //    }

    //    var page13 = await applicationOverview.CompleteReview();

    //    var page14 = await page13.ApproveApplication();

    //    var page15 = await page14.ReturnToOrganisationApplications();

    //    staffDashboardPage = await page15.ReturnToDashboard();

    //    var page16 = await staffDashboardPage.GoToApprovedOrganisationApplication();

    //    var page17 = await page16.GoToApprovedOrganisationApplicationOverviewPage();

    //    var page18 = await page17.ReturnToOrganisationApplicationsPage();

    //    return await page18.ReturnToDashboard();
    //}
}