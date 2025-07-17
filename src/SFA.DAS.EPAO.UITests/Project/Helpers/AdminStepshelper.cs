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
}