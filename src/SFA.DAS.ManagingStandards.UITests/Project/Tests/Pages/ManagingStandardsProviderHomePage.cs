using Microsoft.Playwright;
using SFA.DAS.Framework;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.YourContactInformationForThisStandardPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;

public class ManagingStandardsProviderHomePage(ScenarioContext context) : ProviderHomePage(context)
{
    public new async Task<YourStandardsAndTrainingVenuesPage> NavigateToYourStandardsAndTrainingVenuesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your standards and training" }).ClickAsync();

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
        await page.GetByRole(AriaRole.Link, new() { Name = "The standards you deliver" }).ClickAsync();

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
        await Assertions.Expect(page.Locator("#header-standards")).ToContainTextAsync("Manage the standards you deliver");
    }

    public async Task<ManageAStandard_TeacherPage> AccessTeacherLevel6()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Teacher (Level 6)" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<RegulatedStandardPage> AccessRegulatorApprovalLinkFromTheSTandardsTable()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Regulator's approval needed" }).ClickAsync();

        return await VerifyPageAsync(() => new RegulatedStandardPage(context));
    }

    public async Task<YourStandardsAndTrainingVenuesPage> ReturnToYourStandardsAndTrainingVenues()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }
    //public SelectAStandardPage AccessAddStandard()
    //{
    //    formCompletionHelper.ClickLinkByText("Add a standard");

    //    return await VerifyPageAsync(() => new SelectAStandardPage(context));
    //}

    //public ManageAStandardPage AccessActuaryLevel7(string standardName)
    //{
    //    formCompletionHelper.ClickLinkByText(standardName);

    //    return await VerifyPageAsync(() => new ManageAStandardPage(context, standardName));
    //}
}

public class RegulatedStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("This is a regulated standard");
    }

    public async Task<ManageTheStandardsYouDeliverPage> ApproveStandard_FromStandardsPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
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

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

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

    //public WhereWillThisStandardBeDeliveredPage Add_ContactInformation()
    //{
    //    CompleteContactInfo();

    //    return new WhereWillThisStandardBeDeliveredPage(context);
    //}

    private async Task CompleteContactInfo()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync(managingStandardsDataHelpers.ContactNumber);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Contact page" }).FillAsync(managingStandardsDataHelpers.ContactWebsite);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Your website page" }).FillAsync(managingStandardsDataHelpers.Website);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).ClickAsync();

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

    public async Task<PostcodePage> AccessAddANewTrainingVenue()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a new training venue" }).ClickAsync();

        return await VerifyPageAsync(() => new PostcodePage(context));
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
    //public async Task<AddAstandardPage> Save_NewTrainingVenue_Continue(string standardname)
    //{
    //    Continue();

    //    return await VerifyPageAsync(() => new AddAstandardPage(context));
    //}
    //public async Task<VenueAndDeliveryPage> AccessSeeANewTrainingVenue_AddStandard()
    //{
    //    formCompletionHelper.ClickLinkByText("See training venues");

    //    return await VerifyPageAsync(() => new VenueAndDeliveryPage(context));
    //}
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
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Test Demo Automation Venue");
    }

    public async Task<VenueDetailsPage> Click_UpdateContactDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Update contact details" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueDetailsPage(context));
    }
}

public class VenueDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Venue details");
    }

    public async Task<VenueAddedPage> UpdateVenueDetailsAndSubmit()
    {
        await page.Locator("#Website").FillAsync(managingStandardsDataHelpers.UpdatedWebsite);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new VenueAddedPage(context));
    }
}


public class PostcodePage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is the postcode of this training venue?");
    }

    public async Task<ChooseTheAddressPage> EnterPostcodeAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync(managingStandardsDataHelpers.PostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Find address" }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseTheAddressPage(context));
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
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add venue details");
    }

    public async Task<TrainingVenuesPage> AddVenueDetailsAndSubmit()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Venue name" }).FillAsync(managingStandardsDataHelpers.VenueName);

        await page.Locator("#Website").FillAsync(managingStandardsDataHelpers.ContactWebsite);

        await page.Locator("#EmailAddress").FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.Locator("#PhoneNumber").FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }
}