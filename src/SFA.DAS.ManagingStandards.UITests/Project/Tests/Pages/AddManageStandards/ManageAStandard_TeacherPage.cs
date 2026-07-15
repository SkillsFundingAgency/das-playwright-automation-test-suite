using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.VenueAndDelivery_ApprenticeshipUnitPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManageAnAppUnitPage(ScenarioContext context, string appUnitName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(appUnitName);
    }

    public async Task<AreyouSureDeleteAppUnitPage> ClickDeleteAnApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Delete apprenticeship unit" }).ClickAsync();

        return await VerifyPageAsync(() => new AreyouSureDeleteAppUnitPage(context));
    }

    public async Task<ManageYourAppUnitPage> GoToManageYourAppUnitPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to managing your" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageYourAppUnitPage(context));
    }

    public async Task VerifyUpdatedWedsite()
    {
        await Assertions.Expect(page.GetByLabel("Contact details").GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(managingStandardsDataHelpers.UpdatedWebsite);
    }

    public async Task VerifyUpdatedRegion(string regionName)
    {
        await Assertions.Expect(page.GetByLabel("List of training locations").GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(regionName);
    }

    public async Task<ApprenticeshipUnit_SavedPage> Save_NewApprenticeshipUnit_Continue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save apprenticeship unit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipUnit_SavedPage(context));
    }
    
    public async Task<YourContactInformationForThisAppUnit> EditContactDetails()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = "Email address" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisAppUnit(context));
    }

    public async Task<AnyWhereInEngland_ApprenticeshipUnitPage> EditNationalProviderDetails()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = "National provider" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEngland_ApprenticeshipUnitPage(context));
    }

     public async Task<WhereWillThisAppUnitBeDeliveredPage> EditFromOnlineTo()
    {
        await page.GetByLabel("List of training locations").GetByRole(AriaRole.Link, new() { Name = "Change" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereWillThisAppUnitBeDeliveredPage(context));
    }
    
}

public class ManageAStandard_TeacherPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(standardName);
    }

    public ManageAStandard_TeacherPage(ScenarioContext context) : this(context, ManagingStandardsDataHelpers.StandardsTestData.StandardName) { }

    public async Task<ApprenticeshipUnit_SavedPage> Save_NewApprenticeshipUnit_Continue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save apprenticeship unit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipUnit_SavedPage(context));
    }

    public async Task<RegulatedStandardPage> AccessApprovedByRegulationOrNot()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   the regulated" }).ClickAsync();

        return await VerifyPageAsync(() => new RegulatedStandardPage(context));
    }
    public async Task<ManageTheStandardsYouDeliverPage> Return_StandardsManagement()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to manage your standards" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }

    public async Task<WhereWillThisStandardBeDeliveredPage> AccessWhereYouWillDeliverThisStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   where you deliver" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereWillThisStandardBeDeliveredPage(context));
    }

    public async Task<WhereCanYouDeliverTrainingPage> AccessEditTheseRegions()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Edit these regions" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereCanYouDeliverTrainingPage(context));
    }

    public async Task<TrainingVenuesPage> AccessEditTrainingLocations()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Edit training locations" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }

    public async Task<YourContactInformationForThisStandardPage> UpdateTheseContactDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Update these contact details" }).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisStandardPage(context));

    }
    public async Task VerifyUpdatedContactDetailsVisibleInStandard(string expectedEmail = null, string expectedPhone = null)
    {
        var emailCell = page.Locator("table.govuk-table tr:has-text('Email address') td");
        var phoneCell = page.Locator("table.govuk-table tr:has-text('Telephone number') td");

        if (!string.IsNullOrEmpty(expectedEmail))
        {
            await Assertions.Expect(emailCell).ToContainTextAsync(expectedEmail);
        }

        if (!string.IsNullOrEmpty(expectedPhone))
        {
            await Assertions.Expect(phoneCell).ToContainTextAsync(expectedPhone);
        }
    }
}
