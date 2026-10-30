namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ManageTransferMatchingPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Manage transfers";
    private ILocator CreateTransferPledge => page.GetByRole(AriaRole.Link, new() { Name = "Create a transfers pledge", Exact = true });
    private ILocator ApplyForTransferOpportunity => page.GetByRole(AriaRole.Link, new() { Name = "Apply for transfer opportunities", Exact = true });

    public async Task<ManageTransferMatchingPage> VerifyTransferAllowanceText()
    {
        await Assertions.Expect(page.Locator(".govuk-grid-column-two-thirds").Filter(new() { HasText = "transfer allowance" }).First).ToBeVisibleAsync();
        return this;
    }

    public async Task<AccountHomePage> GoToAccountHomePage() =>
        await FollowLink("Home", () => new AccountHomePage(context));

    public async Task<PledgeAndTransferYourLevyFundsPage> GotoCreateTransfersPledgePage()
    {
        await CreateTransferPledge.ClickAsync();
        return await VerifyPageAsync(() => new PledgeAndTransferYourLevyFundsPage(context));
    }

    public async Task<MyTransferPledgesPage> GoToViewMyTransferPledgePage() =>
        await FollowLink("View my transfer pledges and applications", () => new MyTransferPledgesPage(context));

    public async Task<FindABusinessPage> GoToFindABusinessPage()
    {
        await ApplyForTransferOpportunity.ClickAsync();
        return await VerifyPageAsync(() => new FindABusinessPage(context));
    }

    public async Task<MyApplicationsPage> ViewApplicationsIhaveSubmitted() =>
        await FollowLink("View applications I've submitted", () => new MyApplicationsPage(context));

    public async Task<bool> CanCreateTransferPledge() => await CreateTransferPledge.IsVisibleAsync();
    public async Task<bool> CanApplyForTransferOppurtunity() => await ApplyForTransferOpportunity.IsVisibleAsync();
}
