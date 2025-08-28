using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.Campaigns.UITests.Hooks;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 30)]
    public async Task SetUpHelpers()
    {
        var objectContext = context.Get<ObjectContext>();

        var mailosaurUser = context.Get<MailosaurUser>();

        var datahelper = new CampaignsDataHelper(mailosaurUser);

        context.Set(datahelper);

        var email = datahelper.Email;

        objectContext.SetDebugInformation($"'{email}' is used");

        mailosaurUser.AddToEmailList(email);

        await Navigate(UrlConfig.CA_BaseUrl);
    }
}