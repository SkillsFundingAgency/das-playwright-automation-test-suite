namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class SchoolCollegeAndUniversityQualificationsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("School, college and university qualifications");

    public async Task<WhatIsYourMostRecentQualificationPage> SelectYesAndContinue()
    {
        var locator = page.GetByRole(AriaRole.Link, new() { Name = "Delete   qualification" }).First;

        if (await locator.IsVisibleAsync())
        {
            await locator.ClickAsync();

            var deletelocator = page.GetByRole(AriaRole.Button, new() { Name = "Yes, delete" });

            if (await deletelocator.IsVisibleAsync())
            {
                await deletelocator.ClickAsync();
            }
        }

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsYourMostRecentQualificationPage(context));
    }

    public async Task<FAA_ApplicationOverviewPage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }
}

public class WhatIsYourMostRecentQualificationPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your most recent qualification?");

    public async Task<AddQualificationSubjectPage> SelectAQualificationAndContinue()
    {
        var options = await AllTextAsync(".govuk-radios__label");

        var qualification = RandomDataGenerator.GetRandomElementFromListOfElements(options.Take(5).ToList());

        await page.GetByRole(AriaRole.Radio, new() { Name = qualification }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddQualificationSubjectPage(context, qualification));
    }
}

public class AddQualificationSubjectPage(ScenarioContext context, string qualification) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"{qualification}");

    public async Task<SchoolCollegeAndUniversityQualificationsPage> AddQualificationDetailsAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Subject" }).FillAsync(faaDataHelper.QualificationSubject);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Grade" }).FillAsync(faaDataHelper.QualificationGrade);

        if (qualification.CompareToIgnoreCase("btec"))
        {
            await page.GetByLabel("Level").SelectOptionAsync(["2"]);
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SchoolCollegeAndUniversityQualificationsPage(context));
    }
}

