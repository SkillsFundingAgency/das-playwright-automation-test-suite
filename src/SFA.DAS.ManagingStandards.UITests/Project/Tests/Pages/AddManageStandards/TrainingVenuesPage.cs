using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.VenueAndDelivery_ApprenticeshipUnitPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class TrainingVenuesPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Training venues");
    }

    public async Task<YourStandardsAndTrainingVenuesPage> NavigateBackToReviewYourDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }

    public async Task<SelectAddressPage> AccessAddANewTrainingVenue()
    {
        await page.Locator("a.govuk-button", new PageLocatorOptions { HasTextString = "Add a training venue" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectAddressPage(context));
    }
    public async Task<VenueAddedPage> AccessNewTrainingVenue_Added()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Test Demo Automation Venue" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueAddedPage(context));
    }

    public async Task<VenueAndDeliveryPage> AccessSeeTrainingVenue()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "See training venues" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueAndDeliveryPage(context));
    }

    public async Task<ManageAStandard_TeacherPage> NavigateBackToStandardPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<AddAstandardPage> Save_NewTrainingVenue_Continue(string standardname)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAstandardPage(context, standardname));
    }

    public async Task<VenueAndDeliveryPage> AccessSeeANewTrainingVenue_AddStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "See training venues" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueAndDeliveryPage(context));
    }
}
