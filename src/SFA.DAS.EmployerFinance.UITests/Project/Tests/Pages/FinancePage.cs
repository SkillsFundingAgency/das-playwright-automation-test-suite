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

    public async Task<FundingProjectionPage> GoToFundingProjectionPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Funding projection" }).ClickAsync();

        return await VerifyPageAsync(() => new FundingProjectionPage(context));
    }

    public static string ExpectedFundsSpentLabelConstant()
    {
        DateTime dt = DateTime.Now;
        int previousYear = dt.Year - 1;
        return $"Funds spent since {dt:MMM} {previousYear}";
    }

    public async Task GetCurrentFundsLabel() => await Assertions.Expect(page.Locator("#lbl-current-funds")).ToContainTextAsync("Current funds");

    public async Task GetFundsSpentLabel() => await Assertions.Expect(page.Locator("#lbl-current-spent-funds")).ToContainTextAsync(ExpectedFundsSpentLabelConstant());

    public async Task GetEstimatedTotalFundsText() => await Assertions.Expect(page.Locator("#lbl-estimated-spending")).ToContainTextAsync("Estimated planned spending for the next 12 months");

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


public class FundingProjectionPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Funding projection");
    }

    public async Task<EstimateFundingProjectionPage> GoToEstimateFundingProjectionPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Estimate apprenticeships you" }).ClickAsync();

        return await VerifyPageAsync(() => new EstimateFundingProjectionPage(context));
    }
}

public class EstimateFundingProjectionPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Estimate apprenticeships you could fund");
    }

    public async Task ClickStart()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Start" }).ClickAsync();
    }
}

public class AddApprenticeshipsToEstimateCostPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprenticeships to estimate cost");
    }

    public async Task<EstimatedCostsPage> Add()
    {
        var date = DateTime.Now;

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Use transfer allowance" }).CheckAsync();

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Choose apprenticeship" }).FillAsync("Retail manager");
        await page.GetByRole(AriaRole.Option, new() { Name = "Retail manager, Level: 4 (" }).ClickAsync();

        var locator = page.GetByRole(AriaRole.Spinbutton, new() { Name = "Number of apprentices" });

        await locator.PressSequentiallyAsync("1");

        await locator.PressAsync("Tab");

        locator = page.GetByRole(AriaRole.Spinbutton, new() { Name = "Month", Exact = true });

        await locator.PressSequentiallyAsync(date.Month.ToString());

        await locator.PressAsync("Tab");

        locator = page.GetByRole(AriaRole.Spinbutton, new() { Name = "Year" });

        await locator.PressSequentiallyAsync(date.Year.ToString());

        await locator.PressAsync("Tab");

        await Assertions.Expect(page.GetByRole(AriaRole.Spinbutton, new() { Name = "Number of months" })).ToHaveValueAsync("12");

        await page.GetByRole(AriaRole.Button, new() { Name = "Check if I can fund these" }).ClickAsync();

        return await VerifyPageAsync(() => new EstimatedCostsPage(context));
    }
}


public class EstimatedCostsPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Estimated costs");
    }

    public async Task VerifyTabs()
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = "Account funds" }).ClickAsync();

        await Assertions.Expect(page.GetByLabel("Account funds")).ToContainTextAsync("This table shows your estimated costs against your account projection");

        await page.GetByRole(AriaRole.Tab, new() { Name = "Apprenticeships added" }).ClickAsync();
    }

    public async Task<AddApprenticeshipsToEstimateCostPage> AddApprenticeships()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add more apprenticeships to" }).ClickAsync();

        return await VerifyPageAsync(() => new AddApprenticeshipsToEstimateCostPage(context));
    }

    public async Task<EditApprenticeshipsPage>  EditApprenticeships()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Edit" }).First.ClickAsync();

        return await VerifyPageAsync(() => new EditApprenticeshipsPage(context));

    }

    public async Task<RemoveApprenticeshipsPage> RemoveApprenticeships()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove" }).First.ClickAsync();

        return await VerifyPageAsync(() => new RemoveApprenticeshipsPage(context));
    }

    public async Task<int> ExistingApprenticeships() => await page.GetByRole(AriaRole.Link, new() { Name = "Remove" }).CountAsync();

}

public class RemoveApprenticeshipsPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Remove apprenticeship");
    }

    public async Task<EstimatedCostsPage> ConfirmRemoveApprenticeship()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I do want to remove this" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EstimatedCostsPage(context));
    }
}


public class EditApprenticeshipsPage(ScenarioContext context) : EmployerFinanceBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Edit apprenticeships in your current estimate");
    }

    public async Task<EstimatedCostsPage> Edit()
    {
        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Number of apprentices" }).FillAsync("2");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Total cost" }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Check if I can fund these" }).ClickAsync();

        return await VerifyPageAsync(() => new EstimatedCostsPage(context));
    }
}