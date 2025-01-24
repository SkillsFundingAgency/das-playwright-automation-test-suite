using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class CampaingnsHomePage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Apprentices", Exact = true })).ToBeVisibleAsync();

    public async Task<CampaingnsHomePage> AcceptCookieAndAlert()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).ClickAsync();

        await page.Locator("#fiu-cb-close-accept").ClickAsync();

        return await VerifyPageAsync(() => new CampaingnsHomePage(context));
    }

    public async Task<ApprenticeHomePage> GoToApprenticePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Learn more becoming an" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeHomePage(context));
    }
}
