using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class PledgeVerificationPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Your pledge has been created as pledge";
    protected override ILocator PageHeader => page.Locator(".govuk-panel__title");

    public async Task<PledgeVerificationPage> SetPledgeDetail()
    {
        var accountId = page.Url.Split("/accounts/")[1].Split("/pledges")[0];
        var publicAccountId = context.Get<AccountsDbSqlHelper>().GetPublicHashedAccountIdByHashedId(accountId);
        var pledgeId = (await PageHeader.InnerTextAsync()).Replace(PageTitle, "").Trim();
        objectContext.SetPledgeDetail(pledgeId, publicAccountId);
        return this;
    }

    public async Task<MyTransferPledgesPage> ViewYourPledges() =>
        await FollowLink("View your pledges", () => new MyTransferPledgesPage(context));
}
