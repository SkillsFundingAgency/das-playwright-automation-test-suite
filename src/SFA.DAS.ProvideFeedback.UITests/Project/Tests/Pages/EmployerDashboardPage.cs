using Azure;
using Microsoft.Playwright;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project;


namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;

public class EmployerDashboardPage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("PRO LIMITED");

    public async Task<EmployerFeedbackSelectProviderPage> ClickFeedbackLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Feedback on training providers" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackSelectProviderPage(context));
    }

    public async Task<EmployerFeedbackHomePage> OpenFeedbackLinkWithSurveyCode()
    {
        await OpenFeedbackUsingSurveyCode();
        
        return await VerifyPageAsync(() => new EmployerFeedbackHomePage(context));
    }
}

public class EmployerFeedbackSelectProviderPage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select a training provider");

    private static string SelectLink(string ukprn) => ($"a[href*='/providers/{ukprn}']");

    public async Task<ApprenticeFeedbackConfirmProviderPage> SelectTrainingProvider()
    {
        // await page.Locator(SelectLink(objectContext.GetProviderUkprn())).ClickAsync();

        var selectLocator = page.Locator("a:has-text('Select')");

        if (await selectLocator.CountAsync() == 0)
        {
            var objectContext = context.Get<ObjectContext>();
            var dbConfig = context.Get<DbConfig>();
            
            var sqlHelper = new EmployerFeedbackSqlHelper(objectContext, dbConfig);
            await sqlHelper.UpdateEmployerFeedbackResult();
            await page.ReloadAsync();
        }

        await selectLocator.First.ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackConfirmProviderPage(context));
    }
}

public class ApprenticeFeedbackConfirmProviderPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm training provider");

    public async Task<EmployerFeedbackHomePage> ConfirmTrainingProvider()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackHomePage(context));
    }
}

public class EmployerFeedbackHomePage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Give feedback");

    public async Task<EmployerFeedbackStrengthsPage> StartNow()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackStrengthsPage(context));
    }
}

public class EmployerFeedbackStrengthsPage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("strengths");

    private static string Options => (".govuk-label.govuk-checkboxes__label");

    public async Task<EmployerFeedbackCheckYourAnswersPage> ContinueToCheckYourAnswers()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackCheckYourAnswersPage(context));
    }

    public async Task<EmployerFeedbackImprovePage> SelectOptionsForDoingWell()
    {
        await SelectOptionAndContinue(Options);

        return await VerifyPageAsync(() => new EmployerFeedbackImprovePage(context));
    }
}

public class EmployerFeedbackImprovePage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("improve?");

    private static string Options => (".govuk-label.govuk-checkboxes__label");

    public async Task<EmployerFeedbackOverallRatingPage> ContinueToOverallRating()
    {
        await SelectOptionAndContinue(Options);

        return await VerifyPageAsync(() => new EmployerFeedbackOverallRatingPage(context));
    }

    public async Task<EmployerFeedbackCheckYourAnswersPage> ContinueToCheckYourAnswers()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackCheckYourAnswersPage(context));
    }
}

public class EmployerFeedbackOverallRatingPage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("legend")).ToContainTextAsync("service overall?");

    public async Task<EmployerFeedbackCheckYourAnswersPage> SelectVPoorAndContinue() => await GoToProvideFeedbackCheckYourAnswersPage("Very Poor");

    public async Task<EmployerFeedbackCheckYourAnswersPage> SelectGoodAndContinue() => await GoToProvideFeedbackCheckYourAnswersPage("Good");

    private async Task<EmployerFeedbackCheckYourAnswersPage> GoToProvideFeedbackCheckYourAnswersPage(string selector)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = selector }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackCheckYourAnswersPage(context));
    }
}

public class EmployerFeedbackCheckYourAnswersPage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your answers");

    public async Task<EmployerFeedbackStrengthsPage> ChangeQuestionOne()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change strength answers" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackStrengthsPage(context));
    }

    public async Task<EmployerFeedbackImprovePage> ChangeQuestionTwo()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change improvements answers" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackImprovePage(context));
    }

    public async Task<EmployerFeedbackOverallRatingPage> ChangeQuestionThree()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change quality rating answer" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerFeedbackOverallRatingPage(context));
    }

    public async Task<FeedbackCompletePage> SubmitAnswersNow()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit answers" }).ClickAsync();

        return new(context);
    }
}

public class FeedbackCompletePage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Feedback complete");

}

public class EmployerFeedbackAlreadySubmittedPage(ScenarioContext context) : EmployerFeedbackBasePage(context)
{
    public override async Task VerifyPage()
    {
        await OpenFeedbackUsingSurveyCode();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Feedback already submitted");
    }
}