namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.EPAOWithdrawalPages;

public class AD_YouhaveApprovedThisWithdrawalNotification(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've approved this withdrawal application");

    public async Task VerifyRegisterWithdrawalBodyText()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("to withdraw from the register of end-point assessment organisations.");
    }

    public async Task<AD_WithdrawalApplicationsPage> ReturnToWithdrawalApplications()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalApplicationsPage(context));
    }
}

public class AD_WithdrawalApplicationsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdrawal applications");

    private static string WithrawalOrgName => "Ingram Limited";

    public async Task<AD_WithdrawalRequestOverviewPage> GoToStandardWithdrawlApplicationOverivewPage()
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = "New" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = "Brewer" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalRequestOverviewPage(context));
    }

    public async Task<AD_WithdrawalRequestOverviewPage> GoToRegisterWithdrawlApplicationOverviewPage()
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = "New" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = WithrawalOrgName }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalRequestOverviewPage(context));
    }

    public async Task<AD_WithdrawalRequestOverviewPage> GoToAmmendedWithdrawalApplicationOverviewPage()
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = "Feedback" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = WithrawalOrgName }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalRequestOverviewPage(context));
    }

    public async Task VerifyAnApplicationAddedToFeedbackTab() => await DoesNotThrow("Feedback");

    public async Task VerifyApprovedTabContainsRegisterWithdrawal() => await DoesNotThrow("Approved");

    private async Task DoesNotThrow(string table)
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = table }).ClickAsync();

        await Assertions.Expect(page.Locator("table").GetByRole(AriaRole.Row).Filter(new() { HasText = WithrawalOrgName, HasTextString = "Withdrawal from register" })).ToContainTextAsync($"{DateTime.Now:dd MMMM yyyy}");
    }
}

public class AD_WithdrawalRequestOverviewPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdrawal application overview");

    public async Task<AD_WithdrawalRequestQuestionsPage> GoToWithdrawalRequestQuestionsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Evaluate withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalRequestQuestionsPage(context));
    }

    public async Task<AD_WithdrawalApplicationsPage> ReturnToWithdrawalApplicationsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalApplicationsPage(context));
    }

    public async Task VerifyAnswerUpdatedTag()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Listitem)).ToContainTextAsync("Answer updated");
    }

    public async Task<AD_CheckTheWithdrawDatePage> ClickCompleteReview()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_CheckTheWithdrawDatePage(context));
    }
}

public class AD_WithdrawalRequestQuestionsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdrawal request questions");

    public async Task<AD_WithdrawalRequestOverviewPage> MarkCompleteAndGoToWithdrawalApplicationOverviewPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalRequestOverviewPage(context));
    }

    public async Task<AD_HowWillYouSupportTheLearnersYouAreNotGoingToAssess> ClickAddFeedbackToHowWillYouSupportLearnersQuestion()
    {
        await page.Locator("dl div").Filter(new() { HasText = "Supporting current learners" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AD_HowWillYouSupportTheLearnersYouAreNotGoingToAssess(context));
    }
}

public class AD_HowWillYouSupportTheLearnersYouAreNotGoingToAssess(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("How will you support the learners you are not going to assess?");

    public async Task<AD_WithdrawalRequestQuestionsPage> AddFeedbackMessage()
    {
        await page.Locator("#FeedbackMessage").FillAsync(EPAODataHelper.GetRandomAlphabeticString(200));

        await page.GetByRole(AriaRole.Button, new() { Name = "Add feedback" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalRequestQuestionsPage(context));
    }
}

public class AD_CheckTheWithdrawDatePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Check the withdrawal date");

    public async Task<AD_CompleteReview> ContinueWithWithdrawalRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_CompleteReview(context));
    }
}

public class AD_CompleteReview(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Complete review");

    public async Task<AD_YouhaveApprovedThisWithdrawalNotification> ClickApproveApplication()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Approve withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_YouhaveApprovedThisWithdrawalNotification(context));
    }

    public async Task<AD_FeedbackSent> ClickAddFeedback()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Ask for more information" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_FeedbackSent(context));
    }
}

public class AD_FeedbackSent(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've asked for more information");

    public async Task<AD_WithdrawalApplicationsPage> ReturnToWithdrawalApplications()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AD_WithdrawalApplicationsPage(context));
    }
}