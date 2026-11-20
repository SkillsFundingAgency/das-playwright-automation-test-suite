using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class HomePageFinancesSection_YourTransfers(ScenarioContext context) : HomePage(context)
{
    public override async Task VerifyPage()
    {
        await base.VerifyPage();
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Finances", Exact = true })).ToBeVisibleAsync();
    }

    public async Task<ManageTransferMatchingPage> NavigateToTransferMatchingPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Transfers", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new ManageTransferMatchingPage(context));
    }

    public async Task<AccountHomePage> NavigateToAccountHomePage()
    {
        await page.Locator("#navigation > li:nth-child(1) > a").ClickAsync();
        return await VerifyPageAsync(() => new AccountHomePage(context));
    }
}
