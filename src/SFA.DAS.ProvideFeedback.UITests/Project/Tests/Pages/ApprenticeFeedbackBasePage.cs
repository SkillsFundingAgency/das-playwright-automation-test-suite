using NUnit.Framework;

namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;

public abstract class ApprenticeFeedbackBasePage(ScenarioContext context) : BasePage(context)
{
    public async Task<ApprenticeFeedbackSelectProviderPage> NavigateToGiveFeedbackOnYourTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Feedback on your training" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackSelectProviderPage(context));
    }
}

public class ApprenticeFeedbackHomePage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm my apprenticeship details");
}

public class ApprenticeFeedbackSelectProviderPage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select a training provider");

    private static string SelectTrainingProvider => ("a.govuk-link[href*='start']");

    public async Task<ApprenticeFeedbackGiveFeedbackPage> SelectATrainingProvider()
    {
        var locators = await page.Locator(SelectTrainingProvider).AllAsync();

        var locator = RandomDataGenerator.GetRandomElementFromListOfElements(locators.ToList());

        var href = await locator.GetAttributeAsync("href");

        var items = href?.Split("/");

        objectContext.SetProviderUkprn(items[^1]);

        await locator.ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackGiveFeedbackPage(context));
    }

    public async Task VerifyFeedbackStatusAsPending()
    {
        var locators = await page.Locator(".govuk-tag--blue").AllAsync();

        var tags = locators.Select(async l => await l.TextContentAsync());

        var texts = await Task.WhenAll(tags);

        CollectionAssert.Contains(texts, "YOU CAN SUBMIT");
    }

    public async Task VerifyFeedbackStatusAsSubmitted()
    {
        var locators = await page.Locator(".govuk-tag--green").AllAsync();

        var tags = locators.Select(async l => await l.TextContentAsync());

        var texts = await Task.WhenAll(tags);

        CollectionAssert.Contains(texts, "SUBMITTED");
    }
}

public class ApprenticeFeedbackGiveFeedbackPage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Give feedback on");

    public async Task<ApprenticeFeedbackDoYouThinkPage> StartNow()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackDoYouThinkPage(context));
    }
}

public class ApprenticeFeedbackDoYouThinkPage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("providing the following aspects of your apprenticeship training to a reasonable standard?");

    private static string RadioItems => (".govuk-radios__item");

    public async Task<ApprenticeFeedbackHowWouldYouRatePage> ProvideFeedback()
    {
        var locators = await page.Locator(RadioItems).AllAsync();

        for (int i = 0; i < locators.Count; i += 2)
        {
            var x = RandomDataGenerator.GetRandomElementFromListOfElements([locators[i], locators[i + 1]]);

            await x.ClickAsync();

        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackHowWouldYouRatePage(context));
    }

    public async Task<ApprenticeFeedbackCheckYourAnswersPage> GoToCheckYourAnswersPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackCheckYourAnswersPage(context));
    }
}

public class ApprenticeFeedbackHowWouldYouRatePage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How would you rate");

    private static string RatingRadioItems => ("[id*='overall-rating']");

    public async Task<ApprenticeFeedbackCheckYourAnswersPage> ProvideRating()
    {
        var locators = await page.Locator(RatingRadioItems).AllAsync();

        var locator = RandomDataGenerator.GetRandomElementFromListOfElements(locators.ToList());

        await locator.ClickAsync();

        return await GoToCheckYourAnswersPage();
    }

    public async Task<ApprenticeFeedbackCheckYourAnswersPage> GoToCheckYourAnswersPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackCheckYourAnswersPage(context));
    }
}

public class ApprenticeFeedbackCheckYourAnswersPage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your answers");

    public async Task<ApprenticeFeedbackDoYouThinkPage> ChangeFeedbackAttribute()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   what you told us" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackDoYouThinkPage(context));
    }

    public async Task<ApprenticeFeedbackHowWouldYouRatePage> ChangeOverallRating()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   how you rated" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackHowWouldYouRatePage(context));
    }

    public async Task<ApprenticeFeedbackCompletePage> SubmitAnswers()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeFeedbackCompletePage(context));
    }
}

public class ApprenticeFeedbackCompletePage(ScenarioContext context) : ApprenticeFeedbackBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Feedback complete");
}
