using System;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class WhatAreYourSkillsAndStrengthsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What are your skills and strengths?");

    protected override string SubmitSectionButton => "button.govuk-button[type='submit']";

    private static string SkillsAndStrengths => "#SkillsAndStrengths";

    public async Task<FAA_ApplicationOverviewPage> SelectYesAndCompleteSection()
    {
        await page.Locator(SkillsAndStrengths).FillAsync(faaDataHelper.Strengths);

        return await SelectSectionCompleted();
    }
}

public class WhatInterestsYouAboutTThisApprenticeshipPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What interests you about this apprenticeship?");

    private static string AnswerText => ("#AnswerText");

    public async Task<FAA_ApplicationOverviewPage> SelectYesAndCompleteSection()
    {
        await page.Locator(AnswerText).FillAsync(faaDataHelper.HobbiesAndInterests);

        return await SelectSectionCompleted();
    }
}

public class AdditionQuestion1Page(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Application questions");

    private static string AdditionalQuestionAnswer => ("#AdditionalQuestionAnswer");

    public async Task<FAA_ApplicationOverviewPage> SelectYesAndCompleteSection()
    {
        await page.Locator(AdditionalQuestionAnswer).FillAsync(faaDataHelper.AdditionalQuestions1Answer);

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I’ve completed this" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }
}

public class AdditionQuestion2Page(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Application questions");

    private static string AdditionalQuestionAnswer => ("#AdditionalQuestionAnswer");

    public async Task<FAA_ApplicationOverviewPage> SelectYesAndCompleteSection()
    {
        await page.Locator(AdditionalQuestionAnswer).FillAsync(faaDataHelper.AdditionalQuestions2Answer);

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I’ve completed this" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }
}

public class AskForSupportAtAnInterviewPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Ask for support at an interview");

    private static string InterviewAdjustmentsDescription => ("#InterviewAdjustmentsDescription");

    public async Task<GetSupportAtAnInterviewPage> SelectYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.Locator(InterviewAdjustmentsDescription).FillAsync(faaDataHelper.InterviewSupport);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new GetSupportAtAnInterviewPage(context));
    }

    public async Task<GetSupportAtAnInterviewPage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I don't need support or I" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new GetSupportAtAnInterviewPage(context));
    }
}

public class GetSupportAtAnInterviewPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get support at an interview");

    protected override string SubmitSectionButton => ("button.govuk-button[type='submit']");
}

public class DisabilityConfidentSchemePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Disability Confident scheme");

    protected override string SubmitSectionButton => ("button.govuk-button[type='submit']");

    public async Task<DisabilityConfidentSchemePage> SelectYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DisabilityConfidentSchemePage(context));
    }

    public async Task<DisabilityConfidentSchemePage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DisabilityConfidentSchemePage(context));
    }
}

public class WhereDoYouWantToApplyForPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where do you want to apply for");

    public async Task<WhereDoYouWantToApplyForPage> SelectLocationsAndContinue()
    {
        await SelectFirstTwoLocations();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereDoYouWantToApplyForPage(context));
    }

    protected async Task SelectFirstTwoLocations()
    {
        var multipleLocationsCheckboxes = await page.GetByRole(AriaRole.Checkbox).AllAsync();

        var selectedMultipleLocationsCheckboxes = multipleLocationsCheckboxes.Take(2);

        foreach (var checkbox in selectedMultipleLocationsCheckboxes)
        {
            if (!await checkbox.IsCheckedAsync())
            {
                await checkbox.CheckAsync();
            }
        }
    }
}