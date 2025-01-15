using SFA.DAS.Campaigns.UITests.Helpers;
using SFA.DAS.Framework;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages
{

    public abstract class CampaingnsBasePage(ScenarioContext context) : BasePage(context)
    {
        protected readonly CampaignsDataHelper campaignsDataHelper = context.Get<CampaignsDataHelper>();
    }
}
