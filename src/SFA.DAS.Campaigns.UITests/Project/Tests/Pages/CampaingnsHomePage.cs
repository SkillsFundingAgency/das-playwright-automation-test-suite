using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class CampaingnsHomePage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Apprentices", Exact = true })).ToBeVisibleAsync();

    public async Task<CampaingnsHomePage> AcceptCookieAndAlert()
    {
        var acceptButton = page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" });

        if (await acceptButton.IsVisibleAsync())
        {
            await acceptButton.ClickAsync();
            await acceptButton.WaitForAsync(new() { State = WaitForSelectorState.Hidden });
        }

        var closeButton = page.Locator("#fiu-cb-close-accept");

        try
        {
            await acceptButton.ClickAsync(new() { Timeout = 5000 });
        }
        catch (Exception)
        {
            Console.WriteLine("Accept button not found or already clicked.");
        }
        try
        {
            await closeButton.ClickAsync(new() { Timeout = 2000 });
        }
        catch (Exception)
        {
            Console.WriteLine("Close button not found or already hidden.");
        }

        return await VerifyPageAsync(() => new CampaingnsHomePage(context));
    }

    public async Task<ApprenticeHomePage> GoToApprenticePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Learn more becoming an" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeHomePage(context));
    }
}
