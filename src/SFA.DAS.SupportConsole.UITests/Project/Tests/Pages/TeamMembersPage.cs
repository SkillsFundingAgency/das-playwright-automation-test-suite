namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Pages;

public class TeamMembersPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#content")).ToContainTextAsync("Team members");

        await Assertions.Expect(page.Locator("table.responsive")).ToBeVisibleAsync();
    }

    public async Task<UserInformationOverviewPage> GoToUserInformationOverviewPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = config.Name }).ClickAsync();

        return await VerifyPageAsync(() => new UserInformationOverviewPage(context));
    }
}
