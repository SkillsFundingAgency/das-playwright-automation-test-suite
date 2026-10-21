namespace SFA.DAS.FAA.UITests.Project.Tests.Pages.ApplicationOverview;

public class ApplicationSubmittedPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Application submitted");

    public async Task ClickSignOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

    }

    public async Task<SexAndGenderIdentityPage> AnswerEqualityQuestions()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, answer the equality questions (4 questions)" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SexAndGenderIdentityPage(context));
    }
}

public class SexAndGenderIdentityPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l")).ToContainTextAsync("Sex and gender identity");

    public async Task<EthnicGroupPage> SubmitSexAndGenderIdentity()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Female" }).CheckAsync();

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EthnicGroupPage(context));
    }
}

public class EthnicGroupPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your ethnic group?");

    public async Task<CheckYourAnswersToEqualityQuestionsPage> SubmitEthnicGroup()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Prefer not to say" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersToEqualityQuestionsPage(context));
    }
}

public class CheckYourAnswersToEqualityQuestionsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your answers to the equality questions");

    public async Task<FAA_SubmittedApplicationPage> SaveAndContinue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_SubmittedApplicationPage(context));
    }
}

