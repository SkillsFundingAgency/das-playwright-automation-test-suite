using Microsoft.Playwright;
using SFA.DAS.Framework;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerFinance.UITests.Project.Tests.Pages;

public class HomePageFinancesSection(ScenarioContext context) : HomePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Finances", Exact = true })).ToBeVisibleAsync();
    }

    public async Task VerifyYourFinancesSectionLinksForANonLevyUser()
    {
        await VerifyPage();

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Funding and payments", Exact = true })).ToBeVisibleAsync();

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Funding reservations", Exact = true })).ToBeVisibleAsync();
    }

    public async Task VerifyYourFinancesSectionLinksForALevyUser()
    {
        await VerifyPage();

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Funding and payments", Exact = true })).ToBeVisibleAsync();
    }
}


public class HomePageFinancesSection_YourFinance(ScenarioContext context) : HomePageFinancesSection(context)
{
    public async Task<FinancePage> NavigateToFinancePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Funding and payments" }).ClickAsync();

        return await VerifyPageAsync(() => new FinancePage(context));
    }
}


public class FinancePage(ScenarioContext context) : HomePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Funding and payments");
    }

    public async Task IsViewTransactionsLinkPresent()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "View transactions", Exact = true })).ToBeVisibleAsync();
    }

    public async Task<YourTransactionsPage> GoToViewTransactionsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View transactions" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTransactionsPage(context));
    }

    public async Task IsDownloadTransactionsLinkPresent()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "View transactions", Exact = true })).ToBeVisibleAsync();
    }

    public async Task<DownloadTransactionsPage> GoToDownloadTransactionsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Download transactions" }).ClickAsync();

        return new DownloadTransactionsPage(context);
    }

    public async Task IsTransfersLinkPresent()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Transfers", Exact = true })).ToBeVisibleAsync();
    }

    public async Task<TransfersPage> GoToTransfersPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Transfers" }).ClickAsync();

        return await VerifyPageAsync(() => new TransfersPage(context));
    }

    // Levy summary section (FAI-3628/3629/3630/3631), replacing the old single-month
    // "Total levy" / "Levy declared in {month}" / "Paid from the levy in {month}" labels,
    // which the redesigned page no longer renders. Each figure's value element carries
    // aria-labelledby pointing at the adjacent title's id - there is no id on the value itself.
    private static readonly Regex CurrencyFormat = new(@"^£[\d,]+$");

    public ILocator LevySummaryHeading => page.GetByRole(AriaRole.Heading, new() { Name = "Levy summary", Exact = true });

    public ILocator CommitmentsHeading => page.GetByRole(AriaRole.Heading, new() { Name = "Commitments", Exact = true });

    public ILocator CurrentLevyFundsValue => page.Locator("[aria-labelledby='lbl-current-funds']");

    public ILocator LevyInValue => page.Locator("[aria-labelledby='lbl-levy-in']");

    public ILocator LevySpentValue => page.Locator("[aria-labelledby='lbl-levy-spent']");

    public ILocator ExpiredLevyValue => page.Locator("[aria-labelledby='lbl-expired-levy']");

    public async Task VerifyLevySummarySectionStructure()
    {
        await Assertions.Expect(LevySummaryHeading).ToBeVisibleAsync();

        await Assertions.Expect(CommitmentsHeading).ToBeVisibleAsync();
    }

    public async Task VerifyCurrentLevyFundsIsDisplayed() => await Assertions.Expect(CurrentLevyFundsValue).ToContainTextAsync(CurrencyFormat);

    public async Task VerifyLevyInIsDisplayed() => await Assertions.Expect(LevyInValue).ToContainTextAsync(CurrencyFormat);

    public async Task VerifyLevySpentIsDisplayed() => await Assertions.Expect(LevySpentValue).ToContainTextAsync(CurrencyFormat);

    public async Task VerifyExpiredLevyIsDisplayed() => await Assertions.Expect(ExpiredLevyValue).ToContainTextAsync(CurrencyFormat);

    public async Task VerifyCurrentLevyFundsIsNotZero() => await Assertions.Expect(CurrentLevyFundsValue).Not.ToContainTextAsync("£0", new() { IgnoreCase = false });

    public async Task VerifyLevyInIsNotZero() => await Assertions.Expect(LevyInValue).Not.ToContainTextAsync("£0", new() { IgnoreCase = false });
}

public abstract class EmployerFinanceBasePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Finances", Exact = true })).ToBeVisibleAsync();
    }

    public async Task<FinancePage> GoToFinancePage()
    {
        await page.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Finance" }).ClickAsync();

        return await VerifyPageAsync(() => new FinancePage(context));
    }
}

public class TransfersPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Transfers");
    }
}


public class YourTransactionsPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your transactions");
    }
}

public class DownloadTransactionsPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Download transactions");
    }
}
