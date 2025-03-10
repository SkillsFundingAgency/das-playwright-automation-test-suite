using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using System;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class HomePage(ScenarioContext context, bool navigate) : InterimHomeBasePage(context, navigate)
{
    //    #region Locators

    //    protected static By FindApprenticeshipLink => By.LinkText("Find apprenticeship training and manage requests");
    //    protected static By StartNowButton => By.LinkText("Start now");
    //    protected static By YourFundingReservationsLink => By.LinkText("Your funding reservations");
    //    protected static By YourTransfersLink => By.LinkText("Your transfers");
    //    private static By SucessSummary => By.CssSelector(".das-notification");
    //    private static By AcceptYourAgreementLink => By.LinkText("Accept your agreement");
    //    private static By ContinueTo => By.LinkText("Continue");
    //    private static By SetUpAnApprenticeshipSectionHeader => By.Id("set-up-an-apprenticeship");
    //    protected static By FinancesSectionHeading => By.XPath("//h2[text()='Finances']");
    //    protected static By YourFinancesLink => By.LinkText("Your finances");
    //    protected static By AANLink => By.LinkText("Join the Apprentice Ambassador Network");
    //    private static By TransferRequestViewDetailsLink => By.XPath("//li[contains(span, 'Transfer request received')]/span/a[text()='View details']");
    //    private static By TransferConnectionRequestViewDetailsLink => By.XPath("//li[contains(span, 'connection requests to review')]/span/a[text()='View details']");
    //    #endregion

    public override async Task VerifyPage()
    {
        await retryHelper.RetryOnEmpHomePage(
            async () => await Assertions.Expect(page.GetByRole(AriaRole.Menuitem, new() { Name = "Your organisations and" })).ToBeVisibleAsync(), ReloadPageAsync);

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(objectContext.GetOrganisationName());
    }

    public HomePage(ScenarioContext context) : this(context, false) { }

    //public void GoToAanHomePage() => formCompletionHelper.Click(AANLink);

    public async Task VerifyAccountName(string name)
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Account renamed");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You successfully updated the account name");

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(name);
    }

    public async Task<AboutYourAgreementPage> ClickAcceptYourAgreementLinkInHomePagePanel()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Accept your agreement" }).ClickAsync();

        return await VerifyPageAsync(() => new AboutYourAgreementPage(context));
    }

    //public void ContinueToCreateAdvert() => formCompletionHelper.ClickElement(ContinueTo);

    public async Task VerifySetupAnApprenticeshipSection()
    {
        await Assertions.Expect(page.Locator("#set-up-an-apprenticeship")).ToContainTextAsync("Set up an apprenticeship");

        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Start now" })).ToBeVisibleAsync();

    }

    public async Task VerifyLevyDeclarationDueTaskMessageShown()
    {
        var messageText = $"Levy declaration due by 19 {DateTime.Now:MMMM}";

        await VerifyTaskList(messageText);
    }

    public async Task VerifyApprenticeChangeToReviewMessageShown(int numberOfChanges)
    {
        var messageText = numberOfChanges == 1 ? "1 apprentice change to review" : $"{numberOfChanges} apprentice changes to review";

        await VerifyTaskList(messageText);
    }

    public async Task VerifyCohortsReadyToReviewMessageShown(int numberOfChanges)
    {
        var messageText = numberOfChanges == 1 ? "1 apprentice request ready for review" : $"{numberOfChanges} apprentice requests ready for review";

        await VerifyTaskList(messageText);
    }

    public async Task VerifyTransferPledgeApplicationsToReviewMessageShown(int numberOfChanges)
    {
        var messageText = numberOfChanges == 1 ? "1 transfer pledge application awaiting your approval" : $"{numberOfChanges} transfer pledge applications awaiting your approval";

        await VerifyTaskList(messageText);
    }

    public async Task VerifyTransferRequestReceivedMessageShown()
    {
        await VerifyTaskList("Transfer request received");
    }

    public async Task VerifyTransferConnectionRequestsMessageShown(int numberOfChanges)
    {
        var messageText = numberOfChanges == 1 ? "1 connection request to review" : $"{numberOfChanges} connection requests to review";

        await VerifyTaskList(messageText);
    }

    private async Task VerifyTaskList(string messageText)
    {
        await Assertions.Expect(page.GetByLabel("Tasks").GetByRole(AriaRole.List)).ToContainTextAsync(messageText);
    }

    public async Task<ManageYourApprenticesPage> ClickViewChangesForApprenticeChangesToReview(int numberOfChanges)
    {
        var linkText = numberOfChanges == 1 ? "change" : "changes";

        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = $"apprentice {linkText} to review" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new ManageYourApprenticesPage(context));
    }

    public async Task<ApprenticeRequestsPage> ClickViewCohortsForCohortsReadyToReview()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "ready for review" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
    }

    public async Task<TransfersPage> ClickViewDetailsForTransferRequests()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Transfer request received" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new TransfersPage(context));
    }

    public async Task<TransfersPage> ClickViewDetailsForTransferConnectionRequests()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "connection requests to review" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new TransfersPage(context));
    }

    public async Task<MyTransferPledgesPage> ClickViewTransferPledgeApplications()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "transfer pledge" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new MyTransferPledgesPage(context));
    }
}

public class ManageYourApprenticesPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprentices");
    }
}


public class ApprenticeRequestsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
    }
}

public class TransfersPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Transfers");
    }
}

public class MyTransferPledgesPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("My transfer pledges");
    }
}
