using Azure;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.EmployerPortal.UITests.Project.Tests.Pages;


namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class HomePage(ScenarioContext context, bool navigate) : InterimHomeBasePage(context, navigate)
{
    #region Locators
    private ILocator PageHeading => page.Locator(".govuk-heading-xl");
    #endregion

    public override async Task VerifyPage()
    {
        await retryHelper.RetryOnEmpHomePage(
            async () => await Assertions.Expect(page.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Your organisations and" })).ToBeVisibleAsync(), ReloadPageAsync);
        
        await Assertions.Expect(PageHeading).ToContainTextAsync(objectContext.GetOrganisationName());
    }

    public HomePage(ScenarioContext context) : this(context, false) { }

    public async Task<bool> IsPageDisplayed()
    {
        try
        {
            await VerifyPage();
            objectContext.SetDebugInformation($"'{await PageHeading.First.TextContentAsync()}' page is displayed");
            return true;
        }
        catch (Exception ex)
        {
            objectContext.SetDebugInformation($"CheckPage for Home Page resulted in {ex.Message}");
            return false;
        }

    }

    public async Task GoToAanHomePage() => await page.GetByRole(AriaRole.Link, new() { Name = "Join the Apprentice" }).ClickAsync();

    public async Task VerifyAccountName(string name)
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Account renamed");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You successfully updated the account name");

        await Assertions.Expect(PageHeading).ToContainTextAsync(name);
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

    public async Task VerifyTransfersAvailableToAddAnApprenticeMessageShown(int numberOfChanges)
    {
        var messageText = numberOfChanges == 1 ? "1 transfer available to add an apprentice" : $"{numberOfChanges} transfers available to add an apprentice";

        await VerifyTaskList(messageText);
    }

    public async Task VerifyTransfersToAcceptMessageShown(int numberOfChanges)
    {
        var messageText = numberOfChanges == 1 ? "1 transfer to accept" : $"{numberOfChanges} transfers to accept";

        await VerifyTaskList(messageText);
    }

    public async Task VerifyTransferRequestReceivedMessageShown()
    {
        await VerifyTaskList("Transfer request received");
    }
    public async Task<UseTransferFundsPage> ClickViewTransfersAvailableToAddApprentice()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "transfer available to add apprentice" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new UseTransferFundsPage(context));
    }

    public async Task<MyTransferApplicationsPage> ClickViewMultipleTransfersAvailableToAddApprenticeLink()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "transfers available to add an apprentice" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new MyTransferApplicationsPage(context));
    }

    public async Task<ApplicationDetailsPage> ClickViewTransferToAccept()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "transfer to accept" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationDetailsPage(context));
    }

    public async Task<MyTransferApplicationsPage> ClickViewMultipleTransfersToAccept()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "transfers to accept" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new MyTransferApplicationsPage(context));
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

    public async Task<TransferPage> ClickViewDetailsForTransferRequests()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Transfer request received" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new TransferPage(context));
    }

    public async Task<TransferPage> ClickViewDetailsForTransferConnectionRequests()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "connection requests to review" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new TransferPage(context));
    }

    public async Task<MyTransferPledgesPage> ClickViewTransferPledgeApplications()
    {
        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "transfer pledge" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new MyTransferPledgesPage(context));
    }

    public class ManageYourApprenticesPage(ScenarioContext context) : EmployerPortalBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprentices");
        }
    }

    public class ApprenticeRequestsPage(ScenarioContext context) : EmployerPortalBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
        }
    }

    public class TransferPage(ScenarioContext context) : EmployerPortalBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Transfers");
        }
    }

    public class MyTransferPledgesPage(ScenarioContext context) : EmployerPortalBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("My transfer pledges");
        }
    }
}
