namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Pages;

public class FinancePage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#content")).ToContainTextAsync("Finance");

    public async Task<LevyDeclarationsPage> ViewLevyDeclarations()
    {
        var paye = config.PayeScheme.ToCharArray();

        string obscurepaye = string.Empty;

        for (int i = 0; i < paye.Length; i++)
        {
            obscurepaye += ((i == 0 || i == paye.Length - 1 || paye[i].ToString() == "/") ? paye[i].ToString() : "*");
        }

        await page.GetByRole(AriaRole.Row, new() { Name = obscurepaye }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new LevyDeclarationsPage(context));
    }

    public async Task ViewTransactions()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Transactions" }).ClickAsync();

        await Assertions.Expect(page.Locator(".data__purple-block")).ToContainTextAsync("Current balance");
    }
}
