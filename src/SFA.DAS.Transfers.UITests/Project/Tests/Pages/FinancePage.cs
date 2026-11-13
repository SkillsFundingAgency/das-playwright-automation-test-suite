using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;

namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class FinancePage(ScenarioContext context) : InterimFinanceHomePage(context, false)
{
    public async Task<TransfersPage> OpenTransfers()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Transfers", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new TransfersPage(context));
    }

    public async Task<FinancePage> NavigateFromAccount()
    {
        await NavigateToMenuItem("Finance");
        return await VerifyPageAsync(() => new FinancePage(context));
    }
}
