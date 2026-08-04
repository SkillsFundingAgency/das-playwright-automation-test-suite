using Azure;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.Formatters.PayloadProcessing.Cucumber;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.Digicerts.UITests.Project.Helpers;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using System;
using System.Net.Security;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


[Binding]
public class certificateDetailsSteps(ScenarioContext context)
{

    [Given(@"^The (.*) is logged into Apprenticeship Certificate Service after valid authentication$")]
    public async Task GivenTheApprenticeIsLoggedIntoApprenticeshipCertificateServiceAfterValidAuthentication(string user)
    {
        var homePage = await new DigiCertsHomePage(context).clickStart();

        DigitalCertUser digiCertUser;

        switch (user)
        {
            case "MultiStandardUser":
                digiCertUser = context.GetUser<DigiCertMultiStandardUser>();
                break;

            case "MultiFrameworkUser":
                digiCertUser = context.GetUser<DigiCertMultiFrameworkUser>();
                break;

            case "FrameworkUser":
                digiCertUser = context.GetUser<DigiCertFrameworkUser>();
                break;

            case "StandardUser":
                digiCertUser = context.GetUser<DigiCertStandardUser>();
                break;

            default:
                throw new ArgumentException($"Invalid user type: {user}");
        }

        await homePage.RemoveAuthenticationAsync(digiCertUser);

        var signedInPage = await homePage.enterLogin(digiCertUser);

        var authorisationStartPage = await signedInPage.clickContinue();

        await authorisationStartPage.verifyAuthorisationJourney();
    }


    [When(@"^(.*) answers the correct questions related to apprenticeship$")]
    public async Task WhenUserAnswersTheCorrectQuestionsRelatedToApprenticeship(string user)
    {
        var objectContext = context.Get<ObjectContext>();
        var dbConfig = context.Get<DbConfig>();
        var sqlHelper = new AssessorSqlHelper(objectContext, dbConfig);
        var dataHelper = new DigiCertsDataHelper();

        await new DigiCertsClearMatchesPage(context).NavigatetoClearCache();

        var authorisationPage = await new DigiCertsAuthorisationStartPage(context).clickContinue();

        (string firstName, string lastName) = user switch
        {
            "StandardUser" => GetUserName(context.GetUser<DigiCertStandardUser>().Email),
            "FrameworkUser" => GetUserName(context.GetUser<DigiCertFrameworkUser>().Email),
            "MultiStandardUser" => GetUserName(context.GetUser<DigiCertMultiStandardUser>().Email),
            "MultiFrameworkUser" => GetUserName(context.GetUser<DigiCertMultiFrameworkUser>().Email),
            _ => throw new ArgumentException($"Invalid user type: {user}")
        };            

        switch (user)
        {
            case "StandardUser":
                {
                    var cert = await sqlHelper.SingleCertificateAuthorisationdetailsfromuser(firstName, lastName);
                    var learner = await authorisationPage.enterLearner(cert.Uln);
                    var answers = await learner.selectCourse(cert.StandardName);
                    await answers.clickSubmitandViewStandard();
                    break;
                }

            case "FrameworkUser":
                {
                    var cert = await sqlHelper.SingleFrameworkAuthorisationdetailsfromuser(firstName, lastName);
                    var learner = await authorisationPage.SelectNoForLearner();
                    var course = await learner.SelectCourseForLongAuthJourney(cert.FrameworkName);
                    var provider = await course.selectYear(cert.CertificationYear);
                    var answers = await provider.selectProvider(cert.ProviderName);
                    await answers.clickSubmitandViewFramework();
                    break;
                }

            case "MultiStandardUser":
            case "MultiFrameworkUser":
                {
                    var cert = await sqlHelper.MultiCertificateAuthorisationdetailsfromuser(firstName, lastName);
                    var learner = await authorisationPage.enterLearner(cert.Uln);
                    var answers = await learner.selectCourse(cert.StandardName);
                    await answers.clickSubmit();
                    break;
                }
        }

        (string FirstName, string LastName) GetUserName(string email)
        {
            dataHelper.GetNameFromEmail(email);
            return (dataHelper.UserFirstName, dataHelper.UserLastName);
        }

    }

    [Then(@"User is able to view the correct Standard learner certificate details")]
    public async Task ThenUserIsAbleToViewTheCorrectStandardLearnerCertificateDetails()
    {

        await new DigiCertsStandardDetailsPage(context).verifyStandardCertificateDetails();     
    }

    [Then(@"User is able to view the correct Framework learner certificate details")]
    public async Task ThenUserIsAbleToViewTheCorrectFrameworkLearnerCertificateDetails()
    {
        await new DigiCertsFrameworkDetailsPage(context).verifyFrameworkCertificateDetails();
    }

    [Then(@"User is able to view the correct multiple Standard learner certificate details")]
    public async Task ThenUserIsAbleToViewTheCorrectMultipleStandardLearnerCertificateDetails()
    {
        var dashboardPage = await new DigiCertsDashboardPage(context).checkStandardDashboardPageElements();

        var certificateDetailsPage = await dashboardPage.clickStandardCertificate();

        await certificateDetailsPage.verifyMultiStandardCertificateDetails();
    }

    [Then(@"User is able to view the correct multiple Framework learner certificate details")]
    public async Task ThenUserIsAbleToViewTheCorrectMultipleFrameworkLearnerCertificateDetails()
    {
        var dashboardPage = await new DigiCertsDashboardPage(context).checkFrameworkDashboardPageElements();

        var certificateDetailsPage = await dashboardPage.clickFrameworkCertificate();

        await certificateDetailsPage.verifyMultiFrameworkCertificateDetails();
    }

    [Then(@"^the authorised (.*) is successfully verified$")]
    public async Task ThenTheAuthorisedUserIsSuccessfullyVerified(string user)
    {
        await new DigiCertsSignedInPage(context).ClickSignOut();

        await new DigiCertsClearMatchesPage(context).NavigatetoHomePage();

        var homePage = await new DigiCertsHomePage(context).clickStart();

        DigitalCertUser digiCertUser = user switch
        {
            "MultiStandardUser" => context.GetUser<DigiCertMultiStandardUser>(),
            "MultiFrameworkUser" => context.GetUser<DigiCertMultiFrameworkUser>(),
            "FrameworkUser" => context.GetUser<DigiCertFrameworkUser>(),
            "StandardUser" => context.GetUser<DigiCertStandardUser>(),
            _ => throw new ArgumentException($"Invalid user type: {user}")
        };

        var signedInPage = await homePage.enterLogin(digiCertUser);

        var authorisationStartPage = await signedInPage.clickContinue();

        switch (user)
        {
            case "MultiStandardUser":
            case "MultiFrameworkUser":
                await authorisationStartPage.verifyDashBoardPage();
                break;

            case "FrameworkUser":
                await authorisationStartPage.verifyFrameworkDetailsPage();
                break;

            case "StandardUser":
                await authorisationStartPage.verifyStandardDetailsPage();
                break;

            default:
                throw new ArgumentException($"Invalid user type: {user}");
        }

    }


}
