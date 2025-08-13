namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class Specific_TrainingProviderPage(ScenarioContext context, string providerName) : FATeBasePage(context)
{
    public override async Task VerifyPage()
    {
        await VerifyPage(providerName);
    }

    public async Task VerifyPage(string providerName)
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(providerName);
    }
    public async Task ClickOnCourse(string courseName)
    {
        var linkLocator = page.GetByRole(AriaRole.Link, new() { Name = courseName });
        await Assertions.Expect(linkLocator).ToBeVisibleAsync();
        await linkLocator.ClickAsync();
    }
    public async Task VerifyCourseHeading(string expectedCourseName)
    {
        var headingLocator = page.Locator("h2.govuk-heading-l", new() { HasTextString = expectedCourseName });
        await Assertions.Expect(headingLocator).ToBeVisibleAsync();
    }

    public async Task VerifyLocationTextIsVisible(string expectedLocation)
    {
        var locationLocator = page.Locator("strong", new() { HasTextString = expectedLocation });
        await Assertions.Expect(locationLocator).ToBeVisibleAsync();
    }
    public async Task<Specific_TrainingProviderPage> GoToTrainingProviderCoursePage()
    {
        var linkLocator = page.GetByText("View", new() { Exact = false })
                              .Filter(new LocatorFilterOptions { HasTextString = "courses delivered by this training provider" });

        await Assertions.Expect(linkLocator).ToBeVisibleAsync();
        await linkLocator.ClickAsync();
        await ClickOnCourse("Software developer (level 4)");
        await VerifyCourseHeading("Software developer (level 4)");
        return await VerifyPageAsync(() => new Specific_TrainingProviderPage(context, "BARKING AND DAGENHAM COLLEGE"));
    }
    public async Task<Specific_TrainingProviderPage> EnterLocationAndSearchForTrainingOptions()
    {
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ClickContinue();
        await VerifyCourseHeading("Software developer (level 4)");
        await VerifyLocationTextIsVisible(fateDataHelper.PostCodeDetails);
        return await VerifyPageAsync(() => new Specific_TrainingProviderPage(context, "BARKING AND DAGENHAM COLLEGE"));

    }
    public async Task<Specific_TrainingProviderPage> RemoveAndUpdateLocation()
    {
        var linkLocator = page.Locator("a", new() { HasTextString = "remove or change your location" });
        await linkLocator.ClickAsync();
        await EnterApprenticeWorkLocation(fateDataHelper.Location, fateDataHelper.LocationDetails);
        await ClickContinue();
        await VerifyCourseHeading("Software developer (level 4)");
        await VerifyLocationTextIsVisible(fateDataHelper.LocationDetails);
        await linkLocator.ClickAsync();
        return await VerifyPageAsync(() => new Specific_TrainingProviderPage(context, "BARKING AND DAGENHAM COLLEGE"));
    }

    public async Task<Specific_TrainingProviderPage> ViewReviewsWithFeedback()
    {
        var linkLocator = page.Locator("summary:has(span.govuk-details__summary-text:has-text(\"View reviews with feedback from the last 5 years\"))");
        await linkLocator.ClickAsync();
        return await VerifyPageAsync(() => new Specific_TrainingProviderPage(context, "BARKING AND DAGENHAM COLLEGE"));
    }
    public async Task ClickAllFeedbackTabs()
    {
        // List of all tab labels in the order they appear
        var tabLabels = new[]
        {
        "2025 to today",
        "2024 to 2025",
        "2023 to 2024",
        "2022 to 2023",
        "2021 to 2022",
        "Overall reviews"
    };

        foreach (var label in tabLabels)
        {
            var tabLocator = page.Locator("a.govuk-tabs__tab", new() { HasTextString = label });
            await tabLocator.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }
    }
    public async Task ChangeToTableView()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Change to table and" }).First.ClickAsync();
    }


    public async Task ChangeToGraphView()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Change to graph view" }).First.ClickAsync();
    }
}
