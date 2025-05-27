using Microsoft.Playwright;
using SFA.DAS.Framework;
using System;
using System.Security.Cryptography;
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

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Your finances", Exact = true })).ToBeVisibleAsync();

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Your funding reservations", Exact = true })).ToBeVisibleAsync();
    }

    public async Task VerifyYourFinancesSectionLinksForALevyUser()
    {
        await VerifyPage();

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Your finances", Exact = true })).ToBeVisibleAsync();
    }
}


public class HomePageFinancesSection_YourFinance(ScenarioContext context) : HomePageFinancesSection(context)
{
    public async Task<FinancePage> NavigateToFinancePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your finances" }).ClickAsync();

        return await VerifyPageAsync(() => new FinancePage(context));
    }
}


public class FinancePage(ScenarioContext context) : HomePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Finance");
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

    public static string ExpectedFundsSpentLabelConstant()
    {
        DateTime dt = DateTime.Now;
        int previousYear = dt.Year - 1;
        return $"Funds spent since {dt:MMM} {previousYear}";
    }

    public async Task GetCurrentFundsLabel() => await Assertions.Expect(page.Locator("#lbl-current-funds")).ToContainTextAsync("Current funds");

    public async Task GetFundsSpentLabel() => await Assertions.Expect(page.Locator("#lbl-current-spent-funds")).ToContainTextAsync(ExpectedFundsSpentLabelConstant());

    public async Task GetEstimatedPlannedSpendingText() => await Assertions.Expect(page.Locator("#lbl-estimated-future-funding")).ToContainTextAsync("Estimated total funding for the next 12 months (based on funds entering your Apprenticeship service account, including the 10% top up)");
}

public abstract class EmployerFinanceBasePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Finances", Exact = true })).ToBeVisibleAsync();
    }

    public async Task<FinancePage> GoToFinancePage()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Finance" }).ClickAsync();

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
