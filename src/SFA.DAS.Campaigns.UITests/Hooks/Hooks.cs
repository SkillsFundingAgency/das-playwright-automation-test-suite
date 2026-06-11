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

        var page = context.Get<Driver>().Page;

        var acceptButton = page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" });

        var closeButton = page.Locator("#fiu-cb-close-accept");

        await page.AddLocatorHandlerAsync(acceptButton, async () => { await acceptButton.ClickAsync(); await closeButton.ClickAsync(); });
    }
}