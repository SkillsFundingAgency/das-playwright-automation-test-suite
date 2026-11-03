using SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;
using EmployerHomePage = SFA.DAS.EmployerPortal.UITests.Project.Pages.HomePage;
using StubSignInEmployerPage = SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages.StubSignInEmployerPage;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Steps;

[Binding]
public class TransferMatchingSteps(ScenarioContext context)
{
    private readonly ObjectContext objectContext = context.Get<ObjectContext>();
    private readonly TransferMatchingSqlDataHelper sql = context.Get<TransferMatchingSqlDataHelper>();
    private readonly EmployerHomePageStepsHelper employerHome = new(context);
    private readonly TransferMatchingJobsHelper jobs = new(context);
    private PledgeVerificationPage verification;
    private PledgeAndTransferYourLevyFundsPage createPledge;
    private TransferPledgePage transferPledge;
    private ManageTransferMatchingPage manageTransferMatching;
    private EasAccountUser senderUser;
    private EasAccountUser receiverUser;
    private string sender;
    private string receiver;
    private bool anonymous;
    private bool immediate;
    private IPage Page => context.Get<Driver>().Page;
    private string PledgeId => objectContext.GetPledgeDetail().PledgeId;

    [Given("the levy employer who are currently sending transfer funds login")]
    public async Task LoginSendingEmployer() => await LoginSender(context.GetUser<TransfersUser>());
    [Given("the levy employer who are not currently sending transfer funds login")]
    public async Task LoginEmployerWithoutFunds() => await LoginSender(context.GetUser<TransfersUserNoFunds>());
    [Given("the levy employer logins using existing transfer matching account")]
    public async Task LoginTransferMatchingEmployer() => await LoginSender(context.GetUser<TransferMatchingUser>());
    [Given("the levy employer login using existing transactor user account")]
    public async Task LoginTransactor() => await LoginSender(context.GetUser<TransactorUser>());
    [Given("the levy employer creates a pledge")]
    public async Task CreateAsLevyEmployer()
    {
        await LoginSender(context.GetUser<LevyUser>());
        await CreateAndRecordPledge(false, false, false);
    }
    [Given("the another levy employer creates a pledge")]
    public async Task CreateAsAnotherEmployer()
    {
        await SignOut();
        await LoginSender(context.GetUser<TransactorUser>());
        await CreateAndRecordPledge(false, false, false);
    }

    [Given("the levy employer can create pledge using default criteria")]
    [Then("the levy employer can create pledge using default criteria")]
    public async Task CreateDefaultPledge() => await CreateAndRecordPledge(false, false, false);
    [Given("the levy employer can create pledge for immediate approval using default criteria")]
    [Then("the levy employer can create pledge for immediate approval using default criteria")]
    public async Task CreateImmediatePledge() => await CreateAndRecordPledge(false, true, false);
    [Given("the levy employer can create pledge using non default criteria and immediate approval")]
    [Then("the levy employer can create pledge using non default criteria and immediate approval")]
    public async Task CreateImmediateNonDefaultPledge() => await CreateAndRecordPledge(true, true, false);
    [Then("the levy employer can create anonymous pledge using non default criteria")]
    public async Task CreateAnonymousPledge() => await CreateAndRecordPledge(true, false, false);
    [Given("the levy employer can create pledge using minimal funding")]
    [Then("the levy employer can create pledge using minimal funding")]
    public async Task CreateMinimalPledge() => await CreateAndRecordPledge(false, false, true);
    [Then("the levy employer can create pledge with funding equal to course cost")]
    public async Task CreateExactCostPledge() => await CreateAndRecordPledge(false, false, false, true);

    [When("the levy employer is viewing pledges from verification page")]
    [Then("the levy employer can view pledges from verification page")]
    public async Task ViewCreatedPledge() => await (await verification.ViewYourPledges()).ConfirmStatus("Active");

    [When("the receiver levy employer applies for the pledge")]
    public async Task LevyApply() => await Apply(context.GetUser<LevyUser>());
    [When("the non levy employer applies for the pledge")]
    [Then("the non levy employer can apply for the pledge")]
    public async Task NonLevyApply() => await Apply(context.GetUser<NonLevyUser>());
    [Then("the non levy employer can apply for the pledge but is not immediately autoapproved")]
    public async Task ApplyWithoutAutoApproval() => await Apply(context.GetUser<NonLevyUser>(), false);

    [Then("the pledge is available to apply")]
    public async Task PledgeAvailable() => await ApplyForTransferFunds();
    [When("the transfer receiver applies for the pledge")]
    public async Task PairedReceiverApply()
    {
        var accountUser = senderUser as MultipleEasAccountUser
            ?? throw new InvalidOperationException("This scenario requires a sender with a second employer account.");
        await ApplyForTransferFunds();
        objectContext.SetOrganisationName(receiver);
        var signedIn = await new StubSignInEmployerPage(context).Login(accountUser);
        await signedIn.Continue();
        var funding = await VerifyPageHelper.VerifyPageAsync(context, () => new MyAccountTransferFundingPage(context));
        await Submit(await funding.GoToCreateATransfersApplicationPage(receiver));
        receiverUser = accountUser;
    }

    [Then("the levy employer can approve the application")]
    public async Task Approve() => await (await ApprovalDetails()).ManuallyApproveApplication();
    [Then("the levy employer can approve the application and verify costing model")]
    public async Task ApproveAndVerifyCosts()
    {
        var approved = await (await ApprovalDetails()).ManuallyApproveApplication();
        await (await approved.ClickBackButton()).VerifyCostingModel();
    }
    [Then("the levy employer can auto approve the application")]
    public async Task AutoApprove() => await (await ApprovalDetails()).AutoApproveApplication();
    [Then("the levy employer can reject the application")]
    public async Task Reject() => await (await ApplicationToReview()).RejectApplication();
    [Then("the levy employer can view the approved application")]
    public async Task ViewAutoApproved()
    {
        await (await SenderPledge()).ConfirmApplicationStatus("Auto approval: Awaiting acceptance by applicant");
        await SignOut();
    }
    [Then("the levy employer can view the awaiting your approval application")]
    public async Task ViewPending() => await (await SenderPledge()).ConfirmApplicationStatus("Awaiting your approval");

    [Then("the non levy employer can accept funding")]
    [Then("the receiver employer can accept funding")]
    public async Task AcceptFunding()
    {
        await LoginReceiverAgain();
        var details = await OpenApplication(ApplicationStatus.Approved.GetLabelForReceiver());
        var accepted = await details.VerifyAgreeToTermsIsMandatoryAndAcceptFunding();
        await (await accepted.ViewMyApplications()).OpenPledgeApplication(ApplicationStatus.Accepted.GetLabelForReceiver());
    }
    [Then("the non levy employer can withdraw funding")]
    public async Task WithdrawFunding()
    {
        await LoginReceiverAgain();
        var application = await OpenApplication(ApplicationStatus.Approved.GetLabelForReceiver());
        await (await application.WithdrawFunding()).ReturnToMyAccount();
        await OpenApplication(ApplicationStatus.Withdrawn.GetLabelForReceiver());
    }
    [Then("the non levy employer can withdraw funding before approval")]
    public async Task WithdrawBeforeApproval()
    {
        await LoginReceiverAgain();
        var application = await OpenApplication(ApplicationStatus.Pending.GetLabelForReceiver());
        await (await application.WithdrawBeforeApproval()).ReturnToMyAccount();
        await OpenApplication(ApplicationStatus.Withdrawn.GetLabelForReceiver());
    }
    [Then("the non levy employer can open approved pledge application")]
    public async Task OpenApproved() => await OpenReceiverApplication(ApplicationStatus.Approved);
    [Then("the receiver can open approved pledge application")]
    public async Task OpenApprovedReceiver()
    {
        await LoginReceiverAgain();
        await OpenApplication(ApplicationStatus.Approved.GetLabelForReceiver());
    }
    [Then("the non levy employer can open awaiting approval pledge application")]
    public async Task OpenPending() => await OpenReceiverApplication(ApplicationStatus.Pending);

    [Then("the levy employer can close the pledge")]
    public async Task ClosePledge() => await (await (await (await SenderPledge()).ClosePledge()).ConfirmClose()).ConfirmStatus("Closed");
    [Then("the levy employer doesn't close the pledge")]
    public async Task KeepPledgeOpen() => await (await (await SenderPledge()).ClosePledge()).DontClose();
    [Then("the levy employer confirms the pledge displays as closed")]
    public async Task VerifyClosed()
    {
        await LoginSenderAgain();
        var pledges = await (await NavigateToMatching()).GoToViewMyTransferPledgePage();
        await pledges.ConfirmStatus("Closed");
        await (await pledges.GoToTransferPledgePage()).VerifyClosePledgeButtonIsNotDisplayed();
    }
    [Then("the levy employer can download excel file")]
    public async Task DownloadExcel() => await (await SenderPledge()).DownloadExcel();
    [Then("the levy employer can sort the pledges")]
    public async Task SortApplications() => await (await SenderPledge()).SortByApplicant();
    [Then("the levy employer can bulk reject application")]
    public async Task BulkReject()
    {
        var pledge = await SenderPledge();
        pledge = await (await pledge.SelectBulkReject()).CancelBulkReject();
        await (await pledge.SelectBulkReject()).BulkReject();
    }
    [Then("the levy employer can view pleged amount")]
    public async Task VerifyAmount() => await (await SenderPledge()).VerifyPledgeAmount();
    [Then("Then the levy employer can view transfer allowance")]
    public async Task VerifyAllowance() => await (await NavigateToMatching()).VerifyTransferAllowanceText();
    [Then("the levy employer can filter pledges")]
    public async Task FilterPledges() =>
        await (await (await (await NavigateToMatching()).GoToFindABusinessPage()).GoToOpportunitiesPage()).SelectAndApplyFilters();
    [Then("the levy employer can view the task")]
    public async Task ViewTask()
    {
        await LoginSenderAgain();
        await (await (await NavigateToMatching()).GoToAccountHomePage()).ClickTask();
    }

    [Then("the levy employer cannot exceed the maximum funding available")]
    public async Task InvalidPledgeAmount() =>
        await AssertError(await (await EnterPledgeAmount()).EnterInValidAmount(), "You need to enter an amount greater than");
    [Then("the non levy employer cannot exceed the available pledge funding")]
    public async Task InvalidApplicationAmount()
    {
        await SignInToApply(context.GetUser<NonLevyUser>());
        var training = await new CreateATransfersApplicationPage(context).GoToApprenticeshipTrainingPage();
        await AssertError(await training.EnterAmountMoreThanAvailableFunding(), "Cost of training exceeds the amount remaining in this pledge");
    }
    [Then("the viewer cannot create pledge")]
    [Then("the user can not create transfer pledge")]
    public async Task CannotCreate() => Assert.That(await (await NavigateToMatching()).CanCreateTransferPledge(), Is.False);
    [Then("the user can create transfer pledge")]
    public async Task CanCreate() => Assert.That(await (await NavigateToMatching()).CanCreateTransferPledge(), Is.True);
    [Then("the user can view transfer pledge")]
    public async Task ViewPledges() => await (manageTransferMatching ?? await NavigateToMatching()).GoToViewMyTransferPledgePage();
    [Then("the levy employer currently receiving funds can create pledge")]
    public async Task ReceiverCreatePermission()
    {
        var accounts = await new EmployerHomePage(context).GoToYourAccountsPage();
        await accounts.ClickAccountLink(receiver);
        Assert.That(await (await NavigateToMatching()).CanCreateTransferPledge(), Is.True);
    }
    [Then("the levy employer currently receiving funds can not create pledge")]
    public async Task ReceiverCannotCreate()
    {
        var accounts = await new EmployerHomePage(context).GoToYourAccountsPage();
        await accounts.ClickAccountLink(receiver);
        Assert.That(await (await NavigateToMatching()).CanCreateTransferPledge(), Is.False);
    }
    [Then("the levy employer can apply for transfer opportunities")]
    public async Task ApplyForOpportunities() => await (await NavigateToMatching()).GoToFindABusinessPage();
    [Then("the levy employer is able to apply for transfer opportunities")]
    public async Task CanApply() => Assert.That(await (await NavigateToMatching()).CanApplyForTransferOppurtunity(), Is.True);
    [Then("the levy employer can not apply for transfer opportunities")]
    public async Task CannotApply() => Assert.That(await (await NavigateToMatching()).CanApplyForTransferOppurtunity(), Is.False);

    [Then("wait for 6 weeks")]
    public async Task AgeForAutoApproval()
    {
        await sql.UpdateCreatedDateForApplicationTo6WeeksAgo(PledgeId);
        await jobs.RunApplicationsWithAutomaticApprovalJob();
    }
    [When("Six weeks have passed since the application was approved")]
    public async Task AgeForAutoDecline()
    {
        await sql.UpdateCreatedDateForApplicationTo6WeeksAgo(PledgeId);
        await sql.UpdateApprovedDateToBeSixWeeksAgo(PledgeId);
        await jobs.RunApplicationsWithAutoDeclineJob();
    }
    [When("3 months have passed since the funds accepted")]
    public async Task AgeAcceptedFunds()
    {
        await sql.UpdateCreatedDateForApplicationTo3MonthsAgo(PledgeId);
        await sql.UpdateAcceptedDateToBe3MonthsAgo(PledgeId);
        await jobs.RunApplicationsWithAutoExpireJob();
    }
    [Then("the accepted funds should expire")]
    public async Task VerifyExpired() => await OpenReceiverApplication(ApplicationStatus.FundsExpired);
    [Then("the approved application should auto decline")]
    public async Task VerifyDeclined() => await (await SenderPledge()).ConfirmApplicationStatus(ApplicationStatus.Declined.GetLabelForSender());
    [Then("the expired funds should be automatically refunded to the sending employer's account")]
    public async Task VerifyExpiredRefund()
    {
        transferPledge = await SenderPledge();
        await VerifyRefund();
    }
    [Then("the deducted funds should be automatically refunded to the sending employer's account")]
    public async Task VerifyRefund() => Assert.That(await transferPledge.GetEstimatedRemainingFunds(), Is.EqualTo(await transferPledge.GetTotalPledgeFunds()));
    [Then("the application is seven days from being auto approved")]
    public async Task AgeFiveWeeks() => await sql.UpdateCreatedDateForApplicationTo5WeeksAgo(PledgeId);
    [Then("the status of the application will change from 'Auto approval' to 'Auto approval due on XXX'")]
    public async Task VerifyAutoApprovalDate() =>
        await (await SenderPledge()).ConfirmApplicationStatus("Auto approval on " + DateTime.Now.AddDays(7).ToString("dd MMM yyyy").ToUpper());
    [Then("It is 7 days before an application reaches 3 months without any action")]
    public async Task AgeBeforeRejection() => await sql.UpdateCreatedDateForApplicationToAWeekLessThan3MonthsAgo(PledgeId);
    [Then("the levy employer will be able to view auto rejected date of application under status tag 'Application expires on dd/mm/yy'")]
    public async Task VerifyAutoRejectionDate() =>
        await (await SenderPledge()).ConfirmApplicationStatus("Application expires on " + DateTime.Now.AddDays(7).ToString("dd MMM yyyy").ToUpper());
    [Then("Application has not been actioned for 3 months")]
    public async Task AgeForRejection()
    {
        await sql.UpdateCreatedDateForApplicationTo3MonthsAgo(PledgeId);
        await jobs.RunApplicationsWithAutomaticRejectionJob();
    }
    [Then("Application should be auto rejected")]
    public async Task VerifyAutoRejected() => Assert.That(await sql.GetPledgeApplicationStatusByDetails(PledgeId), Is.EqualTo((int)ApplicationStatus.Rejected));
    [Then("the Pledge balance drops below £2,000 Pledge should be auto closed")]
    public async Task VerifyAutoClosed()
    {
        var (status, amount) = await sql.GetPledgeDetails(PledgeId);
        Assert.Multiple(() =>
        {
            Assert.That(amount, Is.LessThanOrEqualTo(2000));
            Assert.That(status, Is.EqualTo((int)PledgeStatus.Closed));
        });
    }
    [Then("Pledge wont be shown publicly")]
    public async Task VerifyNotPublic()
    {
        await NewTab(UrlConfig.TransferMacthingApplyUrl(PledgeId));
        await new TransferFundDetailsInErrorPage(context).VerifyPage();
    }

    [Then("Employer navigates to Create a Transfer Pledge page")]
    public async Task NavigateCreate() => createPledge = await (await NavigateToMatching()).GotoCreateTransfersPledgePage();
    [Then(@"Standard gov\.uk footer should be displayed at the bottom of the page in Transfers")]
    public async Task Footer() => await createPledge.ValidateFooter();
    [Then("the Help widget is displayed on bottom right hand corner in Transfers")]
    public async Task HelpWidget() => await createPledge.ValidateHelpWidget();
    [Then("the employer can navigate to Accessibility statement page from Transfers")]
    public async Task AccessibilityStatement() => await createPledge.GoToAccessibilityStatementPage();

    private async Task CreateAndRecordPledge(bool nonDefault, bool autoApproval, bool minimal, bool exactCost = false)
    {
        anonymous = nonDefault;
        immediate = autoApproval;
        var amount = await EnterPledgeAmount();
        var details = await amount.EnterInValidAmountForCreateAPledge(minimal, exactCost);
        var name = await details.GoToPledgeOrganisationNamePageOptionPage();
        var matching = await (await name.EnterValidOrgNameChoice(!anonymous)).GoToPledge100PercentMatchPage();
        var pledge = await matching.EnterValidMatchChoice(immediate);
        if (nonDefault)
        {
            pledge = await (await pledge.GoToAddtheLocationPage()).EnterLocation();
            pledge = await (await pledge.GoToChoosetheSectorPage()).SelectSetorAndContinue();
            pledge = await (await pledge.GoToChooseTheTypesOfJobPage()).SelectTypeOfJobAndContinue();
            pledge = await (await pledge.GoToChooseTheLevelPage()).SelectLevelAndContinue();
        }
        else
        {
            Assert.That(await pledge.GetCriteriaValue(CreateATransferPledgePage.LocationLink), Is.EqualTo("All of England").IgnoreCase);
            Assert.That(await pledge.GetCriteriaValue(CreateATransferPledgePage.SectorLink), Is.EqualTo("All sectors and industries").IgnoreCase);
            Assert.That(await pledge.GetCriteriaValue(CreateATransferPledgePage.TypeOfJobRoleLink), Is.EqualTo("All apprenticeship job roles").IgnoreCase);
            Assert.That(await pledge.GetCriteriaValue(CreateATransferPledgePage.LevelLink), Is.EqualTo("All qualification levels").IgnoreCase);
        }
        verification = await pledge.ContinueToPledgeVerificationPage();
        await verification.SetPledgeDetail();
    }

    private async Task<PledgeAmountPage> EnterPledgeAmount() =>
        await (await (await (await (await NavigateToMatching()).GotoCreateTransfersPledgePage()).StartCreatePledge()).GoToPledgeAmountPage()).CaptureAvailablePledgeAmount();
    private async Task<ManageTransferMatchingPage> NavigateToMatching() =>
        manageTransferMatching = await new HomePageFinancesSection_YourTransfers(context).NavigateToTransferMatchingPage();
    private async Task<TransferPledgePage> SenderPledge()
    {
        await LoginSenderAgain();
        transferPledge = await (await (await NavigateToMatching()).GoToViewMyTransferPledgePage()).GoToTransferPledgePage();
        return transferPledge;
    }
    private async Task<ApprovingTheApprenticeshipDetailsPage> ApprovalDetails() =>
        await (await ApplicationToReview()).GoToApprovingTheApprenticeshipDetailsPage();
    private async Task<ApproveAppliationPage> ApplicationToReview()
    {
        var pledge = await SenderPledge();
        objectContext.SetOrganisationName(receiver
            ?? throw new InvalidOperationException("A receiving employer must apply before its application can be reviewed."));
        return await pledge.GoToApproveAppliationPage();
    }
    private async Task<ApplicationsDetailsPage> OpenApplication(string status) =>
        await (await (await NavigateToMatching()).ViewApplicationsIhaveSubmitted()).OpenPledgeApplication(status);
    private async Task OpenReceiverApplication(ApplicationStatus status)
    {
        await employerHome.GotoEmployerHomePage();
        await OpenApplication(status.GetLabelForReceiver());
    }
    private async Task Apply(EasAccountUser user, bool? approved = null)
    {
        await SignInToApply(user);
        await Submit(new CreateATransfersApplicationPage(context), approved);
    }
    private async Task Submit(CreateATransfersApplicationPage page, bool? approved = null)
    {
        await SubmitApplicationHelper.SubmitApplication(page, PledgeId);
        var application = await OpenApplication((approved ?? immediate) ? ApplicationStatus.Approved.GetLabelForReceiver() : ApplicationStatus.Pending.GetLabelForReceiver());
        await application.SetPledgeApplication();
    }
    private async Task SignInToApply(EasAccountUser user)
    {
        var signIn = await ApplyForTransferFunds();
        receiverUser = user;
        receiver = user.OrganisationName;
        objectContext.SetOrganisationName(receiver);
        var signedIn = await signIn.Login(user.Username, user.IdOrUserRef);
        await signedIn.Continue();
        await new CreateATransfersApplicationPage(context).VerifyPage();
    }
    private async Task<StubSignInEmployerPage> ApplyForTransferFunds()
    {
        await SignOut();
        await NewTab(UrlConfig.TransferMacthingApplyUrl(PledgeId));
        objectContext.SetOrganisationName(sender);
        var details = new TransferFundDetailsPage(context, anonymous);
        await details.VerifyPage();
        return await details.ApplyForTransferFunds();
    }
    private async Task LoginSender(EasAccountUser user)
    {
        senderUser = user;
        sender = user.OrganisationName;
        if (user is MultipleEasAccountUser multiple)
        {
            if (receiverUser == null) receiver = multiple.SecondOrganisationName;
            await new MultipleAccountsLoginHelper(context, multiple).Login(user, true);
        }
        else await new CreateAccountEmployerPortalLoginHelper(context).Login(user, true);
    }
    private async Task LoginSenderAgain()
    {
        await SignOut();
        await LoginSender(senderUser ?? context.GetUser<TransferMatchingUser>());
    }
    private async Task LoginReceiverAgain()
    {
        await SignOut();
        if (receiverUser is MultipleEasAccountUser multiple)
            await new MultipleAccountsLoginHelper(context, multiple) { OrganisationName = receiver }.Login(multiple, true);
        else
            await new CreateAccountEmployerPortalLoginHelper(context).Login(receiverUser ?? context.GetUser<NonLevyUser>(), false);
    }
    private async Task SignOut()
    {
        await employerHome.NavigateToEmployerApprenticeshipService();
        var link = Page.GetByRole(AriaRole.Link, new() { Name = "Sign out", Exact = true });
        if (await link.CountAsync() > 0) await link.ClickAsync();
    }
    private async Task NewTab(string url)
    {
        var driver = context.Get<Driver>();
        driver.Page = await driver.BrowserContext.NewPageAsync();
        await driver.Page.GotoAsync(url);
    }
    private static async Task AssertError(TransferMatchingBasePage page, string message)
    {
        var error = await page.GetErrorMessage();
        Assert.That(error, Does.Contain("There is a problem").And.Contain(message));
    }
}
