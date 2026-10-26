using System;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class WhatAreYourSkillsAndStrengthsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What are your skills and strengths?");

    protected override string SubmitSectionButton => "button.govuk-button[type='submit']";

    private static string SkillsAndStrengths => "#SkillsAndStrengths";

    public async Task<WhatAreYourSkillsAndStrengthsPage> CheckValidationOnSkills()
    {
        await page.Locator("#SkillsAndStrengths").FillAsync(RandomDataGenerator.GenerateRandomWords(302));

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Your skills and strengths must be 300 words or less",
            "Select if you have finished this section");

        return await VerifyPageAsync(() => new WhatAreYourSkillsAndStrengthsPage(context));
    }
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

    public async Task<WhatInterestsYouAboutTThisApprenticeshipPage> CheckValiadtionOnInterests()
    {
        await page.Locator(AnswerText).FillAsync(RandomDataGenerator.GenerateRandomWords(302));

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Your interest in this apprenticeship must be 300 words or less",
            "Select if you have finished this section");

        return await VerifyPageAsync(() => new WhatInterestsYouAboutTThisApprenticeshipPage(context));
    }
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

    public async Task CheckValidationErrorsOnInterviewSupport()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Select if you want to request interview support");
    }

    public async Task CheckErrorOnEmptySupport()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Enter your interview support request");
    }

    public async Task CheckErrorOnSupportWordCount()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.Locator(InterviewAdjustmentsDescription).FillAsync(RandomDataGenerator.GenerateRandomWords(502));

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Your answer must be 500 words or less");
    }
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

    public async Task CheckValidationOnDisabilityConfident()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Select if you want to apply under the Disability Confident scheme");
    }
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
    private string Pagetitle = "Where do you want to apply for";

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(Pagetitle);

    public async Task<WhereDoYouWantToApplyForPage> SelectLocationsAndContinue()
    {
        await SelectFirstTwoLocations();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() =>
        {
            var page = new WhereDoYouWantToApplyForPage(context)
            {
                Pagetitle = "Where you want to apply for"
            };

            return page;
        });
    }

    public new async Task<FAA_ApplicationOverviewPage> SelectSectionCompleted()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I've completed this" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    private async Task SelectFirstTwoLocations()
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