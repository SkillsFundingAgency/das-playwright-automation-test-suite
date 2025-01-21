namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public abstract class CampaingnsBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly CampaignsDataHelper campaignsDataHelper = context.Get<CampaignsDataHelper>();
}
