
namespace SFA.DAS.RAA.Service.Project.Pages;

public class ProviderDoYouWantToShareAnApplicationPage(ScenarioContext context): RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = "Share an application";

        await Assertions.Expect(page.Locator(".govuk-heading-l")).ToContainTextAsync(PageTitle);
    }

    public async Task<ProviderApplicationSharePage> ConfirmSharing()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderApplicationSharePage(context));
    }
}

public class ProviderApplicationSharePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = "Application shared with employer.";

        await Assertions.Expect(page.Locator(".govuk-notification-banner__heading")).ToContainTextAsync(PageTitle);
    }
}
