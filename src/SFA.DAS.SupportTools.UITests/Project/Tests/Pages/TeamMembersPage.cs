namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class TeamMembersPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Team members");

        await Assertions.Expect(page.Locator("table.govuk-table")).ToBeVisibleAsync();
    }

    public async Task<UserInformationOverviewPage> GoToUserInformationOverviewPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = config.Name }).ClickAsync();

        return await VerifyPageAsync(() => new UserInformationOverviewPage(context));
    }
}
