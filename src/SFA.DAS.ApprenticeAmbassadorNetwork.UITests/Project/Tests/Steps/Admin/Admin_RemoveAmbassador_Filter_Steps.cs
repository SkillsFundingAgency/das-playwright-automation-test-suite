using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;


[Binding, Scope(Tag = "@aanadmin")]
public class Admin_RemoveAmbassador_Filter_Steps(ScenarioContext context) : AanBaseSteps(context)
{

    [Given(@"user should be able to cancel or remove the ambassador")]
    public async Task GivenUserShouldBeAbleToCancelOrRemoveTheAmbassador()
    {
        var page = await new AdminAdministratorHubPage(context).AccessManageAmbassadors();

        var page1 = await page.AcessMember();

        var page2 = await page1.RemoveAmbassador();

        await page2.SelectReasonsToRemoveAndCancelRemoval();
    }


    [Then(@"the user should be able to successfully filter ambassadors by status")]
    public async Task ThenTheUserShouldBeAbleToSuccessfullyFilterAmbassadorsByStatus()
    {
        var page = await new AdminAdministratorHubPage(context).AccessManageAmbassadors();

        await page.FilterAmbassadorByStatus_New();

        await page.VerifyAMbassadorStatus_Published_New();

        await page.ClearAllFilters();

        await page.FilterEventByAmbassadorStatus_Active();

        await page.VerifyAMbassadorStatus_Published_Active();

        await page.ClearAllFilters();
    }

    [Then(@"the user should be able to successfully filter ambassadors by regions")]
    public async Task ThenTheUserShouldBeAbleToSuccessfullyFilterAmbassadorsByRegions()
    {
        var page = new ManageAmbassadorsPage(context);

        await page.FilterEventByEventRegion_London();

        await page.VerifyEventRegion_London_Filter();

        await page.ClearAllFilters();
    }

    [Then(@"the user should be able to successfully filter ambassadors by multiple combination of filters")]
    public async Task ThenTheUserShouldBeAbleTosuccessfullyFilterAmbassadorsByMultipleCombinationOfFilters()
    {
        var page = new ManageAmbassadorsPage(context);

        await page.FilterAmbassadorByStatus_New();

        await page.FilterEventByAmbassadorStatus_Active();

        await page.FilterEventByEventRegion_London();

        await page.VerifyAMbassadorStatus_Published_New();

        await page.VerifyAMbassadorStatus_Published_Active();

        await page.VerifyEventRegion_London_Filter();

        await page.ClearAllFilters();
    }
}