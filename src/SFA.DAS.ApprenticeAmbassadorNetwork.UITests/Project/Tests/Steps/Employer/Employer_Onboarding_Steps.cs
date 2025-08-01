using Azure;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Employer;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Employer.Onboarding;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Employer;


[Binding, Scope(Tag = "@aanemployer")]
public class Employer_Onboarding_Steps(ScenarioContext context) : Employer_BaseSteps(context)
{
    private EmployerAmbassadorApplicationPage employerAmbassadorApplicationPage;

    private RegistrationConfirmationPage registrationConfirmationPage;

    private RegistrationComplete_EmployerPage applicationSubmitted_EmployerPage;

    protected readonly AANSqlHelper aANSqlHelper = context.Get<AANSqlHelper>();

    [Given(@"an employer without onboarding logs into the AAN portal")]
    public async Task AnEmployerWithoutOnboardingLogsIntoTheAANPortal()
    {
        var user = context.GetUser<AanEmployerUser>();

        await aANSqlHelper.ResetEmployerOnboardingJourney(user.Username);

        await EmployerSign(user);

        employerAmbassadorApplicationPage = new EmployerAmbassadorApplicationPage(context);

        await employerAmbassadorApplicationPage.VerifyPage();
    }

    [When(@"the employer provides all the required details for the employer onboarding journey")]
    public async Task WhenTheEmployerProvidesAllTheRequiredDetailsForTheEmployerOnboardingJourney()
    {
        var page = await employerAmbassadorApplicationPage.StartEmployerAmbassadorApplication();

        var page1 = await page.AcceptTermsAndConditions();

        var page2 = await page1.SelectNorthEastRegion_Continue();

        var page3 = await page2.ConfirmRegionalNetwork();

        var page4 = await page3.ConfirmYourAmbassadorProfile();

        var page5 = await page4.SelectHowYouWantToBeInvolved();

        var page6 = await page5.SelectReceiveNotifications();

        var page7 = await page6.SelectEventTypes();

        var page8 = await page7.AddLocation_And_Continue();

        registrationConfirmationPage = await page8.ConfirmEngagement();
    }

    [Then(@"the employer onboarding process should be successfully completed")]
    public async Task ThenTheEmployerOnboardingProcessShouldBeSuccessfullyCompleted()
    {
        applicationSubmitted_EmployerPage = await registrationConfirmationPage.SubmitApplication();
    }

    [Then(@"the employer should be redirected to the employer Hub page")]
    public async Task ThenTheEmployerShouldBeRedirectedToTheEmployerHubPage()
    {
        await applicationSubmitted_EmployerPage.ContinueToAmbassadorHub();
    }

    [When(@"the employer should be able to modify any of the provided answers")]
    public async Task WhenTheEmployerShouldBeAbleToModifyAnyOfTheProvidedAnswers()
    {
        var page = await employerAmbassadorApplicationPage.StartEmployerAmbassadorApplication();

        var page1 = await page.AcceptTermsAndConditions();

        var page2 = await page1.SelectNorthEastRegion_Continue();

        var page3 = await page2.ConfirmRegionalNetwork();

        var page4 = await page3.ConfirmYourAmbassadorProfile();

        var page5 = await page4.SelectHowYouWantToBeInvolved();

        var page6 = await page5.SelectReceiveNotifications();

        var page7 = await page6.SelectEventTypes();

        var page8 = await page7.AddLocation_And_Continue();

        var page9 = await page8.ConfirmEngagement();

        var page10 = await page9.ChangeWhatCanYouOffer();

        registrationConfirmationPage = await page10.Add1MoreSelection();
    }

    [Then(@"the user can sign back in to the AAN Employer platform to verify the hub page")]
    public async Task ThenTheUserCanSignBackInToTheAANEmployerPlatformToVerifyTheHubPage()
    {
        var page = await applicationSubmitted_EmployerPage.ContinueToAmbassadorHub();

        var page1 = await page.GoToEmployerHomePage();

        await page1.GoToAanHomePage();

        await new Employer_NetworkHubPage(context).VerifyPage();
    }

    [Then(@"the users can reinstate their membership within fourteen days of leaving the network")]
    public async Task ThenTheUsersCanReinstateTheirMembershipWithinFourteenDaysOfLeavingTheNetwork()
    {
        var page = await applicationSubmitted_EmployerPage.ContinueToAmbassadorHub();

        var page1 = await page.AccessProfileSettings();

        var page2 = await page1.AccessLeaveTheNetwork();

        var page3 = await page2.CompleteFeedbackAboutLeavingAndContinue();

        var page4 = await page3.ConfirmAndLeave();

        var page5 = await page4.AccessRestoreMember();

        await page5.RestoreMember();
    }
}