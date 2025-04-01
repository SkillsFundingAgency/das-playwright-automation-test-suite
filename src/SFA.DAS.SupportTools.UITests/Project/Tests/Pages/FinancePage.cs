namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class FinancePage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Finance");

    public async Task<LevyDeclarationsPage> ViewLevyDeclarations()
    {
        var payeScheme = config.PayeScheme.ToCharArray();

        var obscurePaye = string.Empty;

        for (var index = 0; index < payeScheme.Length; index++)
        {
            obscurePaye += index == 0 || index == payeScheme.Length - 1 || payeScheme[index].ToString() == "/" ? payeScheme[index].ToString() : "*";
        }

        await page.GetByRole(AriaRole.Row, new() { Name = obscurePaye }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new LevyDeclarationsPage(context));
    }

    public async Task ViewTransactions()
    {
        await page.Locator("#tab_tab-2").ClickAsync();

        await Assertions.Expect(page.Locator(".govuk-panel__body")).ToContainTextAsync("Current balance");
    }
}
