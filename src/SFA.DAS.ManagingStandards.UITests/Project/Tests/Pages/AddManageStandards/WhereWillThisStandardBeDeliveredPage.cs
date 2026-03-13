namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class WhereWillThisStandardBeDeliveredPage(ScenarioContext context, string pageTitle = "Where will this standard be delivered?") : ManagingStandardsBasePage(context)
{
    private readonly string _pageTitle = pageTitle;

    public override async Task VerifyPage()
    {
        var heading = page.Locator(".govuk-heading-xl, .govuk-fieldset__heading");

        await Assertions.Expect(heading).ToContainTextAsync(_pageTitle);
    }

    public async Task<TrainingLocation_ConfirmVenuePage> ConfirmAtOneofYourTrainingLocations()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At one of your training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingLocation_ConfirmVenuePage(context));
    }

    public async Task<AnyWhereInEnglandPage> ConfirmAtAnEmployersLocation_ManageStandard()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At an employer’s location" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEnglandPage(context));
    }

    public async Task<AnyWhereInEnglandPage> ConfirmAtAnEmployersLocation_AddStandard()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At an employer’s location" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEnglandPage(context));
    }

    public async Task<TrainingLocation_ConfirmVenuePage> ConfirmStandardWillDeliveredInBoth()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Both" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingLocation_ConfirmVenuePage(context));
    }

    public async Task<TrainingVenuesPage> ConfirmAtOneofYourTrainingLocations_AddStandard()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At one of your training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }
    public async Task<VenueAndDelivery_ApprenticeshipUnitPage> ConfirmAtOneofYourTrainingLocations_AddApprenticeshipUnit()
    {
        var option = page.Locator("input[type='checkbox'][value='ProviderLocation']").First;

        await option.CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueAndDelivery_ApprenticeshipUnitPage(context));
    }

    public async Task<AnyWhereInEngland_ApprenticeshipUnitPage> ConfirmAtEmployersLocations_AddApprenticeshipUnit()
    {
        var option = page.Locator("input[type='checkbox'][value='EmployerLocation']").First;

        await option.CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEngland_ApprenticeshipUnitPage(context));
    }
    public async Task<AddAstandardPage> ConfirmOnline_AddApprenticeshipUnit(string standardname)
    {
        var option = page.Locator("input[type='checkbox'][value='Online']").First;

        await option.CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAstandardPage(context, standardname));
    }
}
