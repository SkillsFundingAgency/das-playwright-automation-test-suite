using SFA.DAS.RAA.Service.Project.Pages;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;
using System;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerRecruitmentAPISteps(ScenarioContext context)
{
    private ApiListPage _apiListPage;
    private KeyforApiPage _keyforAPIPage;
    private ApprenticeshipServiceDevHubPage _apprenticeshipServiceDevHubPage;
    private DisplayAdvertAPIPage _displayAdvertAPIPage;
    private RecruitmentAPIPage _recruitmentAPIPage;

    [When(@"the employer selects the Recruitment API list page")]
    [Given(@"the employer selects the Recruitment API list page")]
    public async Task GivenTheEmployerSelectsTheRecruitmentAPIListPage()
    {
        var page = new YourApprenticeshipAdvertsHomePage(context);

        await page.VerifyPage();

        var page1 = await page.ClickRecruitmentAPILink();

        _apiListPage = await page1.ClickAPIKeysHereLink();
    }

    [Then(@"the employer selects Recruitment API from the list")]
    [When(@"the employer selects Recruitment API from the list")]

    public async Task WhenTheEmployerSelectsRecruitmentAPIFromTheList() => _keyforAPIPage = await _apiListPage.ClickViewRecruitmentAPILink();

    //[When(@"the Employer navigates to 'Recruit dashboard' Page")]
    //public async Task WhenTheEmployerNavigatesToRecruitDashboardPage()
    //{
    //    var page = new VacancyConfirmationPage(context);

    //    await page.VerifyPage();

    //    var page1 = await page.ClickReturnToDashboard();

    //    var page2 = await page1.ClickRecruitmentAPILink();

    //    _apiListPage = await page2.ClickAPIKeysHereLink();
    //}

    [When(@"the employer selects Recruitment Sandbox API from the list")]
    public async Task WhenTheEmployerSelectsRecruitmentSandboxAPIFromTheList() => _keyforAPIPage = await _apiListPage.ClickViewRecruitmentAPISandBoxLink();

    [When(@"the employer selects Display API from the list")]
    public async Task WhenTheEmployerSelectsDisplayAPIFromTheList() => _keyforAPIPage = await _apiListPage.ClickViewDisplayAPILink();

    [Then(@"the employer can renew the API key")]
    public async Task ThenTheEmployerCanRenewTheAPIKey()
    {
        var page = await _keyforAPIPage.ClickRenewKeyLink();

        _keyforAPIPage = await page.RenewAPIKey();

        await _keyforAPIPage.VerifyApikeyRenewed();
    }

    [When(@"the employer navigates to Adverts page")]
    public async Task ThenTheEmployerNavigatesToAdvertsPage() => await _keyforAPIPage.ClickAdvertsLink();

    //[Then(@"the employer selects the 'Manage your emails' link")]
    //public async Task ThenTheEmployerSelectsTheManageYourEmailsLink() => new YourApprenticeshipAdvertsHomePage(context).ClickMangeYourEmailsLink();

    [Given(@"the employer selects the developer get started page")]
    public async Task GivenTheEmployerSelectsTheDeveloperGetStartedPage() 
    {
        var page = new YourApprenticeshipAdvertsHomePage(context);

        await page.VerifyPage();

        var page1 = await page.ClickRecruitmentAPILink();

        _apprenticeshipServiceDevHubPage = await page1.ClickDeveloperGetStartedPageLink();
    }


    [When(@"the employer selects '(.*)' link")]
    public async Task WhenTheEmployerSelectsLink(string linkName)
    {
        switch (linkName)
        {
            case "Display Advert API":
                _displayAdvertAPIPage = await _apprenticeshipServiceDevHubPage.ClickDisplayAdvertApiLink();
                break;
            case "Recruitment API":
                _recruitmentAPIPage = await _apprenticeshipServiceDevHubPage.ClickRecruitmentApiLink();
                break;
            default:
                throw new ArgumentException($"Unknown link name: {linkName}");
        }
    }

    [When(@"the employer signs in to dev hub")]
    public async Task WhenTheEmployerSignsInToDevHub()
    {
        var page = await _apprenticeshipServiceDevHubPage.ClickDevHubSignInLink();

        await page.SignIn();
    }

    [Then(@"the employer can view the '(.*)' page")]
    public async Task ThenTheEmployerCanViewThePage(string pageName)
    {
        switch (pageName)
        {
            case "Display advert API":
                await _displayAdvertAPIPage.VerifyEndpointTitles();
                break;
            case "Recruitment API":
                await _recruitmentAPIPage.VerifyEndpointTitles();
                break;
            case "API list":
                var page = new ApiListPage(context);
                await page.VerifyPage();
                await page.VerifyDisplayAdvertApiText();
                break;
            default:
                throw new ArgumentException($"Unknown page name: {pageName}");
        }
    }
}
