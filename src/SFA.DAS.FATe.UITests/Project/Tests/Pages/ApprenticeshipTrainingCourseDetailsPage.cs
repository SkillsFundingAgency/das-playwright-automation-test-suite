namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class ApprenticeshipTrainingCourseDetailsPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage()
    {
        var expectedCourseTitle = objectContext.GetTrainingCourseName();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(expectedCourseTitle);
    }

    public async Task<TrainingProvidersPage> ViewProvidersForThisCourse()
    {
        var viewProvidersButton = page.GetByRole(AriaRole.Link, new() { Name = "View providers for this course" });
        await Assertions.Expect(viewProvidersButton).ToBeVisibleAsync();
        await viewProvidersButton.ClickAsync();
        return await VerifyPageAsync(() => new TrainingProvidersPage(context));
    }

    public async Task<TrainingProvidersPage> ViewProvidersForThisCourse(bool filterByLocation, string location)
    {
        if (filterByLocation)
        {
            location = string.IsNullOrEmpty(location) ? RandomDataGenerator.RandomTown() : location;

            await page.GetByRole(AriaRole.Combobox, new() { Name = "Enter a city or postcode" }).FillAsync(location);

            await page.GetByRole(AriaRole.Option, new() { Name = location, Exact = false }).First.ClickAsync();

            await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Apprentice can travel:");

        }

        await page.GetByRole(AriaRole.Link, new() { Name = "View providers for this course" }).ClickAsync();

        return new TrainingProvidersPage(context);
    }

    public async Task<TrainingProvidersPage> EnterLocationAndViewProviders_PartialPostcode_AcrossEngland()
    {
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await VerifyWorkLocationAndTravelDistance("Apprentice's work location:", "TW14 Hounslow");
        await VerifyWorkLocationAndTravelDistance("Apprentice can travel:", "10 miles");
        await ViewProvidersForThisCourse();
        return await VerifyPageAsync(() => new TrainingProvidersPage(context));
    }
    public async Task<ApprenticeshipTrainingCourseDetailsPage> EnterLocationAndViewProviders_10miles_Coventry()
    {
        await EnterApprenticeWorkLocation(fateDataHelper.Location, fateDataHelper.LocationDetails);
        await VerifyWorkLocationAndTravelDistance("Apprentice's work location:", "Coventry, West Midlands");
        await VerifyWorkLocationAndTravelDistance("Apprentice can travel:", "10 miles");
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCourseDetailsPage(context));
    }
    public async Task VerifyWorkLocationAndTravelDistance(string headingLabel, string expectedValue)
    {
        var heading = page.Locator($":is(h2, h3):has-text(\"{headingLabel}\")");
        var value = heading.Locator("span[class*='govuk-!-font-weight-regular']");

        await Assertions.Expect(value).ToBeVisibleAsync();
        await Assertions.Expect(value).ToHaveTextAsync(expectedValue);
    }
    public async Task ClickRemoveLocation()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove location" }).ClickAsync();
    }
    public async Task ClickViewKnowledgeSkillsAndBehaviours()
    {
        await page.GetByText("View knowledge, skills and").ClickAsync();
    }
    public async Task VerifyIFATELinkOpensInNewTab()
    {
        var ifateLink = page.Locator("a", new()
        {
            HasTextString = "from the Institute for Apprenticeships and Technical Education"
        });

        var popup = await page.RunAndWaitForPopupAsync(async () =>
        {
            await ifateLink.First.ClickAsync();
        });

        await popup.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var popupUrl = popup.Url;

        if (!popupUrl.Contains("skillsengland"))
        {
            throw new Exception($"Expected URL to contain 'skillsengland', but got: {popupUrl}");
        }
    }
    public async Task VerifyWorkLocationAndTravelDistanceNotPresent()
    {
        var workLocationHeading = page.Locator("h3:has-text(\"Apprentice's work location:\")");
        var travelDistanceHeading = page.Locator("h3:has-text(\"Apprentice can travel:\")");

        await Assertions.Expect(workLocationHeading).ToHaveCountAsync(0);
        await Assertions.Expect(travelDistanceHeading).ToHaveCountAsync(0);
    }
}
