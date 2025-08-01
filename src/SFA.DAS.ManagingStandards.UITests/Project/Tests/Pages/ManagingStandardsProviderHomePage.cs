

using System;
using Azure;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;

public class ManagingStandardsProviderHomePage(ScenarioContext context) : ProviderHomePage(context)
{
    public new async Task<YourStandardsAndTrainingVenuesPage> NavigateToYourStandardsAndTrainingVenuesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your standards and training venues" }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }
}

public abstract class ManagingStandardsBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly ManagingStandardsDataHelpers managingStandardsDataHelpers = context.Get<ManagingStandardsDataHelpers>();
}

public class YourStandardsAndTrainingVenuesPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your standards and training venues");
    }

    public async Task<TrainingVenuesPage> AccessTrainingLocations()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Training venues" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }

    public async Task<ManageTheStandardsYouDeliverPage> AccessStandards()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Standards" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }

    public async Task<TrainingProviderOverviewPage> AccessProviderOverview()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Provider overview" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingProviderOverviewPage(context));
    }

}

public class TrainingProviderOverviewPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your training provider overview");
    }

    public async Task<YourStandardsAndTrainingVenuesPage> NavigateBackToReviewYourDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }

}


public class ManageTheStandardsYouDeliverPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#header-standards")).ToContainTextAsync("Manage your standards");
    }

    public async Task<ManageAStandard_TeacherPage> AccessPodiatrist()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Podiatrist (level 6)" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<YourStandardsAndTrainingVenuesPage> ReturnToYourStandardsAndTrainingVenues()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }
    public async Task<ManageTheStandardsYouDeliverPage> VerifyStandardPresence(string standardName, bool shouldExist = true)
    {
        var locator = page.Locator($"//a[contains(@class, 'govuk-link') and normalize-space(text())='{standardName}']");

        var count = await locator.CountAsync();

        if (shouldExist && count == 0)
            throw new Exception($"Expected to find the standard '{standardName}', but it was not listed.");

        if (!shouldExist && count > 0)
            throw new Exception($"The standard '{standardName}' was found on the page, but it should NOT be listed.");

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }
    public async Task<ManageTheStandardsYouDeliverPage> VerifyOrangeMoreDetailsNeededTagForStandardAsync(string standardName, bool shouldExist = true)
    {
        var locator = page.Locator($@"
        //td[
            .//strong[contains(@class, 'govuk-tag--orange') and normalize-space(text())='More details needed']
            and .//a[normalize-space(text())='{standardName}']
        ]");

        var count = await locator.CountAsync();

        if (shouldExist && count == 0)
            throw new Exception($"Expected to find an orange 'More details needed' tag for the standard '{standardName}', but it was not found.");

        if (!shouldExist && count > 0)
            throw new Exception($"Found an orange 'More details needed' tag for the standard '{standardName}', but it should NOT be present.");

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }


    public async Task<SelectAStandardPage> AccessAddStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a standard" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectAStandardPage(context));

    }

    public async Task<ManageAStandardPage> AccessActuaryLevel7(string standardName)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = standardName }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandardPage(context, standardName));
    }
}

public class ManageAStandardPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(standardName);
    }

    public async Task<AreYouSureDeleteStandardPage> ClickDeleteAStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Delete standard" }).ClickAsync();

        return await VerifyPageAsync(() => new AreYouSureDeleteStandardPage(context));
    }
}

public class AreYouSureDeleteStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to delete this standard?");
    }

    public async Task<ManageTheStandardsYouDeliverPage> DeleteStandard()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Delete standard" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }
}


public class SelectAStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select a standard");
    }

    public async Task<AddAstandardPage> SelectAStandardAndContinue(string standardName)
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "To add a standard, start" }).FillAsync(standardName);

        await page.GetByRole(AriaRole.Option, new() { Name = standardName }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAstandardPage(context, standardName));
    }
}

public class AddAstandardPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(standardName);
    }

    public async Task<YourContactInformationForThisStandardPage> YesStandardIsCorrectAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisStandardPage(context));
    }

    public async Task<ManageTheStandardsYouDeliverPage> Save_NewStandard_Continue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save standard" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }
}


public class RegulatedStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("This is a regulated standard");
    }

    public async Task<ManageAStandard_TeacherPage> ApproveStandard_FromStandardsPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<YouMustBeApprovePage> DisApproveStandard()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouMustBeApprovePage(context));
    }
}

public class YouMustBeApprovePage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You must be approved by the regulator to deliver this standard");
    }

    public async Task<ManageAStandard_TeacherPage> ContinueToTeacher_ManageStandardPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }
}

public class ManageAStandard_TeacherPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(standardName);
    }

    public ManageAStandard_TeacherPage(ScenarioContext context) : this(context, ManagingStandardsDataHelpers.StandardsTestData.StandardName) { }

    public async Task<RegulatedStandardPage> AccessApprovedByRegulationOrNot()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   the regulated" }).ClickAsync();

        return await VerifyPageAsync(() => new RegulatedStandardPage(context));
    }
    public async Task<ManageTheStandardsYouDeliverPage> Return_StandardsManagement()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to standards management" }).ClickAsync();

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
}

public class WhereWillThisStandardBeDeliveredPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where will this standard be delivered?");
    }

    public async Task<TrainingLocation_ConfirmVenuePage> ConfirmAtOneofYourTrainingLocations()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At one of your training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingLocation_ConfirmVenuePage(context));
    }

    public async Task<AnyWhereInEnglandPage> ConfirmAtAnEmployersLocation()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At an employer’s location" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

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
}

public class TrainingLocation_ConfirmVenuePage(ScenarioContext context) : ManagingStandardsBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Training venues");
    }

    public async Task<ManageAStandard_TeacherPage> ConfirmVenueDetailsAndDeliveryMethod_AtOneOFYourTrainingLocation()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<AnyWhereInEnglandPage> ConfirmVenueDetailsAndDeliveryMethod_AtBoth()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEnglandPage(context));
    }

}

public class AnyWhereInEnglandPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Can you deliver this training anywhere in England?");
    }

    public async Task<ManageAStandard_TeacherPage> YesDeliverAnyWhereInEngland()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I can deliver training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    //public async Task<AddAstandardPage> YesDeliverAnyWhereInEngland(string standardName)
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I can deliver training" }).CheckAsync();

    //    await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

    //    return await VerifyPageAsync(() => new AddAstandardPage(context, standardName));
    //}

    public async Task<WhereCanYouDeliverTrainingPage> NoDeliverAnyWhereInEngland()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I want to select the" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereCanYouDeliverTrainingPage(context));
    }
}


public class WhereCanYouDeliverTrainingPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where can you deliver this training?");
    }

    public async Task<ManageAStandard_TeacherPage> SelectDerbyRutlandRegionsAndConfirm()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Derby", Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Rutland" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }
    public async Task<ManageAStandard_TeacherPage> EditRegionsAddLutonEssexAndConfirm()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Luton" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Essex" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }
}


public class YourContactInformationForThisStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your contact information for this standard");
    }

    public async Task<ManageAStandard_TeacherPage> UpdateContactInformation()
    {
        await CompleteContactInfo();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<WhereWillThisStandardBeDeliveredPage> Add_ContactInformation()
    {
        await CompleteContactInfo();

        return await VerifyPageAsync(() => new WhereWillThisStandardBeDeliveredPage(context));
    }

    private async Task CompleteContactInfo()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync(managingStandardsDataHelpers.ContactNumber);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Contact page" }).FillAsync(managingStandardsDataHelpers.ContactWebsite);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Your website page" }).FillAsync(managingStandardsDataHelpers.Website);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    }
}



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
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

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

public class VenueAndDeliveryPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Venue and delivery");
    }

    public async Task<TrainingVenuesPage> ChooseTheVenueDeliveryAndContinue()
    {
        await page.Locator("#TrainingVenueNavigationId").SelectOptionAsync(["CENTRAL HAIR ESSEX"]);

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Day release" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Block release" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }
}

public class VenueAddedPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Venue details and standards");
    }

    public async Task<VenueDetailsPage> Click_ChangeVenueName()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change venue name" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueDetailsPage(context));
    }
}

public class VenueDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change venue name");
    }

    public async Task<VenueAddedPage> UpdateVenueDetailsAndSubmit()
    {
        await page.Locator("#LocationName").FillAsync(managingStandardsDataHelpers.UpdatedVenueName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueAddedPage(context));
    }
}


public class SelectAddressPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select an address");
    }

    public async Task<AddVenueDetailsPage> EnterPostcodeAndContinue()
    {
        var postcode = managingStandardsDataHelpers.PostCode;
        var fullAddress = managingStandardsDataHelpers.FullAddressDetails;

        await page.Locator("input[role='combobox']").FillAsync(postcode);
        await page.Locator($"li:has-text(\"{fullAddress}\")").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new AddVenueDetailsPage(context));
    }
}
public class ChooseTheAddressPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose the address");
    }

    public async Task<AddVenueDetailsPage> ChooseTheAddressAndContinue()
    {
        await page.Locator("#SelectedAddressUprn").SelectOptionAsync(["100021525713"]);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddVenueDetailsPage(context));
    }
}

public class AddVenueDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add venue name");
    }

    public async Task<TrainingLocation_ConfirmVenuePage> AddVenueDetailsAndSubmit()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Venue name" }).FillAsync(managingStandardsDataHelpers.VenueName);

        //Commented out this for now as we might be resuing them
        //await page.Locator("#Website").FillAsync(managingStandardsDataHelpers.ContactWebsite);

        //await page.Locator("#EmailAddress").FillAsync(managingStandardsDataHelpers.EmailAddress);

        //await page.Locator("#PhoneNumber").FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingLocation_ConfirmVenuePage(context));
    }
}
