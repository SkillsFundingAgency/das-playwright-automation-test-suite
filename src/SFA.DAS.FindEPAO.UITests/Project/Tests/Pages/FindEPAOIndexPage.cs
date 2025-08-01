namespace SFA.DAS.FindEPAO.UITests.Project.Tests.Pages;

public abstract class FindEPAOBasePage(ScenarioContext context) : BasePage(context)
{
    public async Task SearchApprenticeshipStandard(string searchTerm)
    {
        await page.Locator("#SelectedCourseId").ClickAsync();

        await page.Locator("#SelectedCourseId").FillAsync(searchTerm);

        await page.GetByRole(AriaRole.Option, new() { Name = searchTerm }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class FindEPAOIndexPage(ScenarioContext context) : FindEPAOBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Find an end-point assessment organisation for your apprentice");
    }

    public async Task<SearchApprenticeshipTrainingCoursePage> ClickStartButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchApprenticeshipTrainingCoursePage(context));
    }
}

public class SearchApprenticeshipTrainingCoursePage(ScenarioContext context) : FindEPAOBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is the apprenticeship training course?");
    }

    public async Task<EPAOOrganisationsPage> SearchForApprenticeshipStandardInSearchApprenticeshipTrainingCoursePage(string searchTerm)
    {
        await SearchApprenticeshipStandard(searchTerm);

        return await VerifyPageAsync(() => new EPAOOrganisationsPage(context));
    }

    public async Task<ZeroAssessmentOrganisationsPage> SearchForApprenticeshipStandardWithNoEPAO(string searchTerm)
    {
        await SearchApprenticeshipStandard(searchTerm);

        return await VerifyPageAsync(() => new ZeroAssessmentOrganisationsPage(context));
    }

    public async Task<EPAOOrganisationDetailsPage> SearchForApprenticeshipStandardWithSingleEPAO(string searchTerm)
    {
        await SearchApprenticeshipStandard(searchTerm);

        return await VerifyPageAsync(() => new EPAOOrganisationDetailsPage(context));
    }

    public async Task<EPAOOrganisationsPage> SearchForAnIntegratedApprenticeshipStandard(string searchTerm)
    {
        await SearchApprenticeshipStandard(searchTerm);

        return await VerifyPageAsync(() => new EPAOOrganisationsPage(context));
    }

    public async Task<FindEPAOIndexPage> NavigateBackFromSearchApprenticeshipPageToHomePage()
    {
        await NavigateBackToHomePage();

        return await VerifyPageAsync(() => new FindEPAOIndexPage(context));
    }

    public async Task<FindEPAOIndexPage> NavigateBackToHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new FindEPAOIndexPage(context));
    }
}

public class ZeroAssessmentOrganisationsPage(ScenarioContext context) : FindEPAOBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("0 end-point assessment organisations");
    }

    public async Task IsContactESFAButtonDisplayed() => await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Contact us" })).ToBeVisibleAsync();

}


public class EPAOOrganisationsPage(ScenarioContext context) : FindEPAOBasePage(context)
{
    private static string FirstResultLink => (".das-search-results__link");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("end-point assessment organisations");
    }

    public async Task<EPAOOrganisationDetailsPage> SelectFirstEPAOOrganisationFromList()
    {

        var allResult = await page.Locator(FirstResultLink).AllAsync();

        var locator = allResult[0];

        var firstLinkText = await locator.TextContentAsync();

        await locator.ClickAsync();

        return await VerifyPageAsync(() => new EPAOOrganisationDetailsPage(context));
    }

    public async Task<SearchApprenticeshipTrainingCoursePage> NavigateBackFromEPAOOrganisationsPageToSearchApprenticeshipTrainingPage()
    {
        await NavigateBackToSearchApprenticeshipTraining();

        return await VerifyPageAsync(() => new SearchApprenticeshipTrainingCoursePage(context));
    }

    public async Task<SearchApprenticeshipTrainingCoursePage> NavigateBackToSearchApprenticeshipTraining()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new SearchApprenticeshipTrainingCoursePage(context));
    }

    public async Task<EPAOOrganisationDetailsPage> NavigateBackFromEPAOOrgansationPageToDetailsPage()
    {
        await NavigateBackToEPAOOrgansationDetailsPage();

        return await VerifyPageAsync(() => new EPAOOrganisationDetailsPage(context));
    }

    public async Task<EPAOOrganisationDetailsPage> NavigateBackToEPAOOrgansationDetailsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new EPAOOrganisationDetailsPage(context));
    }
}

public class EPAOOrganisationDetailsPage(ScenarioContext context) : FindEPAOBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(objectContext.GetEPAOOrganisationName());
    }

    public async Task<SearchApprenticeshipTrainingCoursePage> NavigateBackFromSingleEPAOOrganisationDetailsPage()
    {
        await NavigateBackToSearchApprenticeshipTraining();

        return new SearchApprenticeshipTrainingCoursePage(context);
    }
    public async Task<SearchApprenticeshipTrainingCoursePage> NavigateBackToSearchApprenticeshipTraining()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
        return new SearchApprenticeshipTrainingCoursePage(context);
    }
    public async Task<EPAOOrganisationsPage> SelectViewOtherEndPointOrganisations()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View the other end-point" }).ClickAsync();

        return new EPAOOrganisationsPage(context);
    }

    public async Task<EPAOOrganisationsPage> NavigateBackFromEPAOOrgansationDetailsPageToOrganisationPage()
    {
        await NavigateBackToEPAOOrgansationPage();

        return new EPAOOrganisationsPage(context);
    }
    public async Task<EPAOOrganisationsPage> NavigateBackToEPAOOrgansationPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return new EPAOOrganisationsPage(context);
    }
}