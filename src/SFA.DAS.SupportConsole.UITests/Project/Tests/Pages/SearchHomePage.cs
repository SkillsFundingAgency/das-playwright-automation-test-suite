namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Pages;

public class SearchHomePage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Search", new() { Timeout = LandingPageTimeout });

    private ILocator NextPage => page.Locator(".page-navigation .next");

    public async Task<UserInformationOverviewPage> SearchByNameAndView() => await SearchAndViewUserInformation(config.Name);

    public async Task<UserInformationOverviewPage> SearchByEmailAddressAndView() => await SearchAndViewUserInformation(config.EmailAddress);

    public async Task<AccountOverviewPage> SearchByPublicAccountIdAndViewAccount() => await SearchAndViewAccount(config.PublicAccountId);

    public async Task<AccountOverviewPage> SearchByHashedAccountIdAndViewAccount() => await SearchAndViewAccount(config.HashedAccountId);

    public async Task<AccountOverviewPage> SearchByAccountNameAndViewAccount() => await SearchAndViewAccount(config.AccountName);

    public async Task<AccountOverviewPage> SearchByPayeSchemeAndViewAccount() => await SearchAndViewAccount(config.PayeScheme);

    private async Task<AccountOverviewPage> SearchAndViewAccount(string criteria)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Accounts" }).CheckAsync();

        await page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter account name, account" }).FillAsync(criteria);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await driver.SelectRowFromTable("view", config.PublicAccountId, NextPage);

        return await VerifyPageAsync(() => new AccountOverviewPage(context));
    }

    private async Task<UserInformationOverviewPage> SearchAndViewUserInformation(string criteria)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Users" }).CheckAsync();

        await page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter name or email address" }).FillAsync(criteria);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await driver.SelectRowFromTable("view", config.EmailAddress, NextPage);

        return await VerifyPageAsync(() => new UserInformationOverviewPage(context));
    }
}
