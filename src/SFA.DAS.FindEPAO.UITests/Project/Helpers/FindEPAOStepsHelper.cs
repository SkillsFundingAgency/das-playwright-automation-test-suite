using SFA.DAS.FindEPAO.UITests.Project.Tests.Pages;

namespace SFA.DAS.FindEPAO.UITests.Project.Helpers;

class FindEPAOStepsHelper(ScenarioContext context)
{
    public async Task<EPAOOrganisationsPage> SearchForApprenticeshipStandard(string standard)
    {
        var page = await FindEPAO();
        
        return await page.SearchForApprenticeshipStandardInSearchApprenticeshipTrainingCoursePage(standard);
    }

    public async Task<ZeroAssessmentOrganisationsPage> SearchForApprenticeshipStandardWithNoEPAO(string standard)
    {
        var page = await FindEPAO();

        return await page.SearchForApprenticeshipStandardWithNoEPAO(standard);
    }

    public async Task<EPAOOrganisationDetailsPage> SearchForApprenticeshipStandardWithSingleEPAO(string standard)
    {
        var page = await FindEPAO();

        return await page.SearchForApprenticeshipStandardWithSingleEPAO(standard);
    }

    public async Task<EPAOOrganisationsPage> SearchForIntegratedApprenticeshipStandard(string standard)
    {
        var page = await FindEPAO();

        return await page.SearchForAnIntegratedApprenticeshipStandard(standard);
    }

    private async Task<SearchApprenticeshipTrainingCoursePage> FindEPAO()
    {
        return await new FindEPAOIndexPage(context).ClickStartButton();
    }
}
