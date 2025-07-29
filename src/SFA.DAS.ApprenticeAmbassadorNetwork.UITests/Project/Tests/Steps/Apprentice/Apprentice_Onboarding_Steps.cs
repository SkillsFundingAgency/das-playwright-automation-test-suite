namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Apprentice;

[Binding, Scope(Tag = "@aanaprentice")]
public class Apprentice_Onboarding_Steps(ScenarioContext context) : Apprentice_BaseSteps(context)
{
    [Given(@"an apprentice logs into the AAN portal")]
    public async Task GivenAnApprenticeLogsIntoTheAANPortal()
    {
        var page = await GetSignInPage();

        beforeYouStartPage = await page.SubmitValidUserDetails(context.Get<AanApprenticeUser>());
    }

    [When(@"the user provides all the required details for the onboarding journey")]
    public async Task WhenUserProvidesAllRequiredDetails()
    {
        var page = await beforeYouStartPage.StartApprenticeOnboardingJourney();

        var page1 = await page.AcceptTermsAndConditions();

        var page2 = await page1.YesHaveApprovalFromMaanagerAndContinue();

        var page3 = await page2.SelectARegionAndContinue();

        var page4 = await page3.ContinueToAmbassadorProfilePage();

        var page5 = await page4.ContinueToSearchEmployerNamePage();

        var page6 = await page5.EnterAddressManually();

        var page7 = await page6.EnterFullEmployersDetailsAndContinue();

        var page8 = await page7.ConfirmJobtitleAndContinue();

        var page9 = await page8.SelectEventsAndPromotions();

        var page10 = await page9.EnterInformationToJoinNetwork();

        var page11 = await page10.SelectNoAndContinue();

        checkYourAnswersPage = await page11.YesHaveEngagedWithAnAmbassadaorAndContinue();
    }

    [Then(@"the Apprentice onboarding process should be successfully completed")]
    public async Task ThenApprenticeOnboardingProcessShouldBeCompleted() => applicationSubmittedPage = await checkYourAnswersPage.AcceptAndSubmitApplication();

    [Then(@"the user should be redirected to the Hub page")]
    public async Task ThenUserShouldBeRedirectedToHubPage() => await applicationSubmittedPage.ContinueToAmbassadorHub();

    [When(@"the user does not have manager permission")]
    public async Task WhenUserDoesNotHaveManagerPermission()
    {
        var page = await beforeYouStartPage.StartApprenticeOnboardingJourney();

        var page1 = await page.AcceptTermsAndConditions();

        shutterPage = await page1.NoHaveApprovalFromMaanagerAndContinue();
    }

    [Then(@"a shutter page should be displayed")]
    public async Task ThenShutterPageShouldBeDisplayed() => await shutterPage.VerifyApprenticePortalLink();

    [When(@"the user should be able to modify any of the provided answers")]
    public async Task WhenUserShouldBeAbleToModifyAnswers()
    {
        var page = await checkYourAnswersPage.AccessChangeCurrentEmployerAndContinue();

        var page1 = await page.EnterAddressManuallyEdit();

        var page2 = await page1.ChangeVenueNameAndContinue();

        var page3 = await page2.AccessChangeCurrentJobTitleAndContinue();

        var page4 = await page3.ChangeJobtitleAndContinue();

        var page5 = await page4.AccessChangeCurrentRegionAndContinue();

        var page6 = await page5.AddOneMoreRegionAndContinue();

        var page7 = await page6.AccessChangeCurrentAreasOfInterestAndContinue();

        var page8 = await page7.AddMoreEventsAndPromotions();

        var page9 = await page8.AccessChangePreviousEngagementToNoAndContinue();

        await page9.NoHaveEngagedWithAnAmbassadaorAndContinue();
    }

    [Then(@"the user can sign back in to the AAN Apprentice platform")]
    public async Task TheUserCanSignBackIn()
    {
        await Navigate(UrlConfig.AAN_Apprentice_BaseUrl);

        await SubmitUserDetails_OnboardingJourneyComplete(objectContext.GetLoginCredentials());
    }

    [Then(@"the users can reinstate their membership within fourteen days of leaving the network")]
    public async Task ThenTheUsersCanReinstateTheirMembershipWithinFourteenDaysOfLeavingTheNetwork()
    {
        var page = await applicationSubmittedPage.ContinueToAmbassadorHub();

        var page1 = await page.AccessProfileSettings();

        var page2 = await page1.AccessLeaveTheNetwork();

        var page3 = await page2.CompleteFeedbackAboutLeavingAndContinue();

        var page4 = await page3.ConfirmAndLeave();

        var page5 = await page4.AccessRestoreMember();

        await page5.RestoreMember();
    }
}
