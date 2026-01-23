using SFA.DAS.EPAO.UITests.Project.Tests.Pages;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Steps;

[Binding]
public class AssessmentOpportunitySteps(ScenarioContext context) : EPAOBaseSteps(context)
{
    private readonly ScenarioContext _context = context;

    [When(@"the User visits the Assessment Opportunity Application")]
    public async Task WhenTheUserVisitsTheAssessmentOpportunityApplication()
    {
        var url = UriHelper.GetAbsoluteUri(UrlConfig.EPAOAssessmentService_BaseUrl, EPAOConfig.AssessmentOpportunityFinderPath);

        await Navigate(url);

        homePage = new AO_HomePage(_context);
    }

    [Then(@"the Approved tab is displayed and selected")]
    public async Task ThenTheApprovedTabIsDisplayedAndSelected() => await homePage.VerifyApprovedTab();

    [When(@"the User clicks on one of the standards listed under 'Approved' tab to view it")]
    public async Task WhenTheUserClicksOnOneOfTheStandardsListedUnderTab() => await homePage.ClickOnAbattoirWorkerApprovedStandardLink();

    [Then(@"the selected Approved standard detail page is displayed")]
    public async Task TheSelectedApprovedStandardDetailPageIsDisplayed() => await new AO_ApprovedStandardDetailsPage(_context).VerifyPage();

    [Then(@"the User is redirected to 'Assessment Service' application")]
    public async Task ThenTheUserIsRedirectedToAssessmentServiceApplication() => await new AS_LandingPage(_context).VerifyPage();

    [When(@"the User clicks on one of the standards listed under 'In-development' tab to view it")]
    public async Task WhenTheUserClicksOnOneOfTheStandardsListedUnderInDevelopmentTabToViewIt()
    {
        await homePage.ClickInDevelopmentTab();

        await homePage.ClickOnInDevelopmentStandardLink();
    }

    [Then(@"the selected In-development standard detail page is displayed")]
    public async Task ThenTheSelectedInDevelopmentStandardDetailPageIsDisplayed() => await new AO_InDevelopmentStandardDetailsPage(_context).VerifyPage();

    [When(@"the User clicks on one of the standards listed under 'Proposed' tab to view it")]
    public async Task WhenTheUserClicksOnOneOfTheStandardsListedUnderProposedTabToViewIt()
    {
        await homePage.ClickInProposedTab();

        await homePage.ClickOnAProposedStandard();
    }

    [Then(@"the selected Proposed standard detail page is displayed")]
    public async Task ThenTheSelectedProposedStandardDetailPageIsDisplayed() => await new AO_ProposedStandardDetailsPage(_context).VerifyPage();
}
