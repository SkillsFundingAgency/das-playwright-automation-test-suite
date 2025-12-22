using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]
public class CreateAccountTaskListSteps
{
    private readonly ScenarioContext _context;
    private readonly ObjectContext _objectContext;
    private readonly AccountCreationStepsHelper _accountCreationStepsHelper;
    private readonly AccountCreationTaskListStepsHelper _accountCreationTaskListStepsHelper;

    private StubAddYourUserDetailsPage _stubAddYourUserDetailsPage;
    private ConfirmYourUserDetailsPage _confirmYourUserDetailsPage;
    private CreateYourEmployerAccountPage _createYourEmployerAccountPage;

    public CreateAccountTaskListSteps(ScenarioContext context)
    {
        _context = context;
        _objectContext = _context.Get<ObjectContext>();
        _accountCreationStepsHelper = new AccountCreationStepsHelper(context);
        _accountCreationTaskListStepsHelper = new AccountCreationTaskListStepsHelper(context);
    }

    [StepArgumentTransformation(@"(can|cannot)")]
    public static bool CanToBool(string value)
    {
        return value == "can";
    }

    [StepArgumentTransformation(@"(does|doesn't)")]
    public static bool DoesToBool(string value)
    {
        return value == "does";
    }

    [When(@"user logs out and log back in")]
    public async Task WhenUserLogsOutAndLogsBackIn()
    {
        var loggedInAccountUser = _objectContext.GetLoginCredentials();

        var page = await _createYourEmployerAccountPage.SignOut();

        var page1 = await page.CickSignInInYouveLoggedOutPage();

        var page2 = await page1.GoManageApprenticeLandingPage();

        var page3 = await page2.GoToStubSignInPage();

        var page4 = await page3.Login(loggedInAccountUser.Username, loggedInAccountUser.IdOrUserRef);

        await page4.Continue();
    }

    [Then(@"user can resume employer registration journey")]
    public async Task UserCanResumeEmployerRegistrationJourney()
    {
        _createYourEmployerAccountPage = await VerifyPageHelper.VerifyPageAsync(_context, () => new CreateYourEmployerAccountPage(_context));
    }

    [Given(@"user logs into stub")]
    public async Task<StubAddYourUserDetailsPage> UserLogsIntoStub() => _stubAddYourUserDetailsPage = await _accountCreationStepsHelper.UserLogsIntoStub();

    [Then(@"User is prompted to enter first and last name")]
    public async Task<ConfirmYourUserDetailsPage> UserEntersNameAndContinue() => _confirmYourUserDetailsPage = await _accountCreationTaskListStepsHelper.UserEntersNameAndContinue(_stubAddYourUserDetailsPage);

    [Then(@"user can amend name before submitting it")]
    public async Task<ConfirmYourUserDetailsPage> UserAmendsNameThenSubmits() => _confirmYourUserDetailsPage = await _accountCreationTaskListStepsHelper.UserChangesUserDetails(_confirmYourUserDetailsPage);

    [When(@"user adds name successfully to the account")]
    [Then(@"user adds name successfully to the account")]
    public async Task<CreateYourEmployerAccountPage> UserConfirmsNameAndAcknowledges() => _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.UserClicksContinueButtonToAcknowledge(_confirmYourUserDetailsPage);

    [Then(@"user can change user details from the task list")]
    public async Task<CreateYourEmployerAccountPage> UserChangesUserDetailsFromTaskList() => _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.UserChangesDetailsFromTaskList(_createYourEmployerAccountPage);

    [When(@"user (.*) add PAYE details")]
    public async Task<CreateYourEmployerAccountPage> UserCanAddPAYEFromTaskList(bool doesAdd)
    {
        if (doesAdd)
        {
            _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.AddPAYEFromTaskListForCloseTo3Million(_createYourEmployerAccountPage);
        }

        _createYourEmployerAccountPage = doesAdd
            ? await AccountCreationTaskListStepsHelper.UserCannotAmendPAYEScheme(_createYourEmployerAccountPage)
            : await AccountCreationTaskListStepsHelper.UserCanClickAddAPAYEScheme(_createYourEmployerAccountPage);


        return _createYourEmployerAccountPage;
    }

    [When(@"user (.*) set account name and (.*)")]
    public async Task<CreateYourEmployerAccountPage> UserCanSetAccountNameFromTaskList(bool canSetAccountName, bool doesSet)
    {
        if (canSetAccountName)
            _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.UserCanClickAddAccountName(_createYourEmployerAccountPage);
        else
            await _createYourEmployerAccountPage.VerifySetYourAccountNameStepCannotBeStartedYet();

        if (canSetAccountName && doesSet)
        {
            _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.ConfirmEmployerAccountName(_createYourEmployerAccountPage);
        }
        return _createYourEmployerAccountPage;
    }

    [Then(@"user can update account name")]
    public async Task<CreateYourEmployerAccountPage> UserCanUpdateAccountName()
    {
        _createYourEmployerAccountPage = await _accountCreationTaskListStepsHelper.UpdateEmployerAccountName(_createYourEmployerAccountPage);
        return _createYourEmployerAccountPage;
    }

    [When(@"user acknowledges the employer agreement")]
    public async Task<CreateYourEmployerAccountPage> UserAcknowledgesEmployerAgreement()
    {
        _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.UserAcknowledgesEmployerAgreement(_createYourEmployerAccountPage);
        return _createYourEmployerAccountPage;
    }

    [When(@"user (.*) accept the employer agreement and (.*)")]
    public async Task<CreateYourEmployerAccountPage> UserCanAcceptEmployerAgreement(bool canAcceptAgreement, bool doesAccept)
    {

        if (canAcceptAgreement)
            _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.UserCanClickAcceptEmployerAgreement(_createYourEmployerAccountPage);
        else
            await _createYourEmployerAccountPage.VerifyYourEmployerAgreementStepCannotBeStartedYet();

        if (canAcceptAgreement && doesAccept)
        {
            return _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.AcceptEmployerAgreement(_createYourEmployerAccountPage);
        }
        return _createYourEmployerAccountPage;
    }

    [Then(@"user accepts agreement from the home page")]
    public async Task UserAcceptsAgreementFromTheHomePage() => await AccountCreationStepsHelper.SignAgreementFromHomePage(new HomePage(_context));

    [When(@"user (.*) add training provider and (.*), the user (.*) grant training provider permissions")]
    public async Task UserAddTrainingProviderAndGrantPermission(bool canAddTrainingProvider, bool doesAdd, bool doesGrant)
    {
        if (canAddTrainingProvider)
            _createYourEmployerAccountPage = await AccountCreationTaskListStepsHelper.UserCanClickTrainingProvider(_createYourEmployerAccountPage);
        else
            await _createYourEmployerAccountPage.VerifyAddTrainingProviderStepCannotBeStartedYet();

        //Doesn't add in this scenario means don't do anything(does not mean the user selects AddTrainingProviderLater)
        if (doesAdd && doesGrant)
        {
            await AccountCreationTaskListStepsHelper.AddTrainingProviderAndGrantPermission(_createYourEmployerAccountPage, _context.GetProviderConfig<ProviderConfig>());
        }
    }
}
