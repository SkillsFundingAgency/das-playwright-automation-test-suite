

namespace SFA.DAS.Transfers.UITests.Project.Tests.Steps;

[Binding]
public class TransfersSteps
{
    private readonly ScenarioContext _context;
    private readonly MultipleAccountsLoginHelper _multipleAccountsLoginHelper;
    private readonly EmployerPortalLoginHelper _employerPortalLoginHelper;
    private readonly TransfersUser _transfersUser;
    private HomePage _homePage;

    private readonly string _sender;

    public TransfersSteps(ScenarioContext context)
    {
        _context = context;
        _transfersUser = context.GetUser<TransfersUser>();
        _sender = _transfersUser.OrganisationName;
        _multipleAccountsLoginHelper = new MultipleAccountsLoginHelper(context, _transfersUser);
        _employerPortalLoginHelper = new EmployerPortalLoginHelper(context);
    }

    [Given(@"We have a Sender with sufficient levy funds")]
    public async Task GivenWeHaveASenderWithSufficientLevyFunds() => await Login(_sender);

    [Given(@"We have a Sender with sufficient levy funds without signing an agreement")]
    public async Task GivenWeHaveASenderWithSufficientLevyFundsWithoutSigningAnAgreement()
    {
        _homePage = await _employerPortalLoginHelper.Login(_context.GetUser<AgreementNotSignedTransfersUser>(), true);
    }

    [Then(@"the sender transfer status is (disabled|enabled)")]
    public async Task CheckTheSenderTransferStatus(string expectedtransferStatus)
    {
        var page = await _homePage.GoToYourOrganisationsAndAgreementsPage();

        await page.VerifyTransfersStatus(expectedtransferStatus);
    }

    private async Task Login(string organisationName)
    {
        _multipleAccountsLoginHelper.OrganisationName = organisationName;

        _homePage = await _multipleAccountsLoginHelper.Login(_transfersUser, true);
    }
}