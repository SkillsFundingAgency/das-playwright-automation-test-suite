using SFA.DAS.EarlyConnectForms.UITests.Project.Tests.Pages;

namespace SFA.DAS.EarlyConnectForms.UITests.Project.Helpers;

public class EarlyConnectStepsHelper(ScenarioContext context)
{
    public async Task GoToEarlyConnectHomePage() => await new EarlyConnectHomePage(context).AcceptCookieAndAlert();
    public async Task GoToEarlyConnectNorthEastAdvisorPage() => await new EarlyConnectHomePage(context).SelectNorthEast();
    public async Task GoToEarlyConnectLancashireAdvisorPage() =>  await new EarlyConnectHomePage(context).SelectLancashire();
    public async Task GoToEarlyConnectLondonAdvisorPage() => await new EarlyConnectHomePage(context).SelectLondon();
    public async Task GoToEarlyConnectEmailPage() => await new EarlyConnectHomePage(context).GoToEmailAddressPage();
    public async Task<EmailAuthCodePage> GoToAddUniqueEmailAddressPage() =>  await new EmailAddressPage(context).EnterNewEmailAddress();
    public async Task<WhatsYourNamePage> GoToCheckEmailAuthCodePage() => await new EmailAuthCodePage(context).EnterValidAuthCode();
    public async Task<DateOfBirthPage> GoToWhatYourNamePage() => await new WhatsYourNamePage(context).EnterFirstAndLastNames();
    public async Task<PostcodePage> GoToWhatIsYourDateOfBirthPage() => await new DateOfBirthPage(context).EnterValidDateOfBirth();
    public async Task<TelephonePage> GoToPostCodePage() => await new PostcodePage(context).EnterValidPostcode();
    public async Task<AreasOfWorkInterestPage> GoToWhatYourTelephonePage() => await new TelephonePage(context).EnterValidTelephoneNumber();
    public async Task<SchoolCollegePage> GoToAreasOfWorkInterestPage() => await new AreasOfWorkInterestPage(context).SelectAnyAreaInterestToYou();
    public async Task<ApprenticeshipsLevelPage> GoToEnterNameOfSchoolCollegePage() => await new SchoolCollegePage(context).EnterInvalidSchoolOrCollegeName();
    public async Task<ApprenticeshipsLevelPage> GoToNameOfSchoolCollegePage() => await new SchoolCollegePage(context).SearchValidSchoolOrCollegeName();
    public async Task<HaveYouAppliedPage> GoToApprencticeshipLevelPage() => await new ApprenticeshipsLevelPage(context).SelectAnyApprenticeshipLevelInterestToYou();
    public async Task<AreaOfEnglandPage> GoToHaveYouAppliedPage() => await new HaveYouAppliedPage(context).SelectAnyPastApplications();
    public async Task<SupportPage> GoToAreaOfEnglandPage() => await new AreaOfEnglandPage(context).SelectYesForTheRightApprenticeship();
    public async Task<CheckYourAnswerPage> GoToSupportPage() => await new SupportPage(context).SelectAnySupportThatAppliesToYou();
    public async Task<ApplicantSurveySummitedPage> GoToCheckYourAnswerPage() => await new CheckYourAnswerPage(context).AcceptAndSubmitForm();
}
