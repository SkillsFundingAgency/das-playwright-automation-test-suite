namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class AccountOverviewPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage()
    {
        //Doing this to refresh the page as the Header disappears at times - known issue

        await RefreshAccountOverviewPage();

        await Assertions.Expect(page.Locator(".govuk-table")).ToContainTextAsync($"{config.AccountName}", new() { IgnoreCase = true });

        await Assertions.Expect(page.Locator(".govuk-hint").First).ToContainTextAsync($"{config.AccountDetails}", new() { IgnoreCase = true });
    }

    public async Task<TeamMembersPage> ClickTeamMembersLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Team members" }).ClickAsync();

        return await VerifyPageAsync(() => new TeamMembersPage(context));
    }

    public async Task<CommitmentsSearchPage> ClickCommitmentsMenuLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Commitments" }).ClickAsync();

        return await VerifyPageAsync(() => new CommitmentsSearchPage(context));
    }

    private async Task RefreshAccountOverviewPage() => await page.GetByRole(AriaRole.Link, new() { Name = "Organisations" }).ClickAsync();
}
