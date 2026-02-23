using Microsoft.Identity.Client;
using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps;

[Binding]
public class AdminSteps(ScenarioContext context)
{
    private readonly QfastHelpers _qfastHelpers = new(context);
    private readonly Admin_Page _qfastAdminPage = new(context);
    private readonly ViewForms_Page _viewFormsPage = new(context);
    private readonly CreateNewForm_Page _createNewFormPage = new(context);
    private readonly CreateOutputFile_Page _createOutputFilePage = new(context);
    private readonly NewQualifications_Page _newQualificationsPage = new(context);    
    private readonly Application_Details_Page application_Details_Page = new(context);
    private readonly Application_Messages_Page application_Messages_Page = new(context);
    private readonly StartApplication_Page startApplicationPage = new(context);
    private readonly SearchForQualification_Page searchForQualification_Page = new(context);
    private readonly QualificationDetails_Page qualificationDetails_Page = new(context);

    [Given(@"the (.*) user log in to the portal")]
    public async Task GivenTheAdminUserLogInToThePortal(string user)
    {
        var User = (user ?? string.Empty).Trim().ToLowerInvariant();

        switch (User)
        {
            case "admin":
            case "admin user":
                await _qfastHelpers.GoToQfastAdminHomePage();
                break;
            case "ao":
            case "ao user":
            case "aouser":
                await _qfastHelpers.GoToQfastAOHomePage();
                break;
            case "ao user2":           
                await _qfastHelpers.GoToQfastAOHomePage1();
                break;
            case "ifate":
            case "ifate user":
                await _qfastHelpers.GoToQfastIFATEHomePage();
                break;
            case "ofqual":
            case "ofqual user":
                await _qfastHelpers.GoToQfastOFQUALHomePage();
                break;
            case "data importer":
            case "importer":
                await _qfastHelpers.GoToQfastDataImporterHomePage();
                break;
            case "reviewer":
            case "data reviewer":
                await _qfastHelpers.GoToQfastReviewerHomePage();
                break;
            case "form editor":
                await _qfastHelpers.GoToQfastFormEditorHomePage();
                break;
            default:
                await _qfastHelpers.GoToQfastAdminHomePage();
                break;
        }
        //await _qfastAdminPage.AcceptCookieAndAlert();
    }

    [Given(@"I validate opitons on the page with the following expected options")]
    public async Task GivenIValidateOpitonsOnThePageWithTheFollowingExpectedOptions(Table table)
    {
        var expectedOptions = table.Rows.Select(r => r["Option"]).ToArray();
        await _qfastAdminPage.ValidateOptionsAsync(expectedOptions: expectedOptions);
    }

    [When("I select the (.*) option")]
    public async Task WhenISelectTheOption(string option)
    {
        if (string.IsNullOrWhiteSpace(option))
            throw new ArgumentNullException(nameof(option), "Option cannot be null or empty.");

        switch (option)
        {
            case "Create a form":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new ViewForms_Page(context));
                break;

            case "Review applications for funding":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new RequestForFundign_Page(context));
                break;

            case "Review newly regulated qualifications":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new NewQualifications_Page(context));
                break;

            case "Review qualifications with changes":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new ChangedQualifications_Page(context));
                break;

            case "Import data":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new ImportQualifications_Page(context));
                break;

            case "Download an output file":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new CreateOutputFile_Page(context));
                break;

            case "Search for a qualification":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(context, () => new SearchForQualification_Page(context));
                break;


            default:
                throw new ArgumentException($"No matching case found for option: '{option}'", nameof(option));
        }
    }

    [When("I create a new submission form and publish the form")]
    public async Task WhenICreateANewSubmissionFormAndPublish()
    {
        await _viewFormsPage.ClickCreateNewFormButton();
        await _createNewFormPage.CreateForm();
    }

    [When("I Sign out from the portal")]
    public async Task WhenILogOutFromThePortal()
    {
        await _qfastAdminPage.ClickLogOut();
    }

    [When("I verify error message (.*) when date is not selected")]
    public async Task WhenIVerifyErrorMessageWhenDateIsNotSelected(string message)
    {
        await _createOutputFilePage.VerifyErrorMessage(message);
    }

    [When("I verify first option always has present date and download the file with present date")]
    public async Task WhenIVerifyFirstOptionAlwaysHasPresentDate()
    {
        await _createOutputFilePage.VerifyPresentDate();        
    }

    [When("I select a publication date and generate the output file")]
    public async Task WhenISelectAPublicationDateAndGenerateTheOutputFile()
    {
        await _createOutputFilePage.ValidateDateErrorMessage();
        await _createOutputFilePage.EnterFuturPublicationDate();       
    }

    [Then("I verify that QAN number is a clickable link")]
    public async Task ThenIVerifyThatQANNumberIsAClickableLink()
    {
        await _newQualificationsPage.VerifyQANnumberIsLink();
    }    

    [Then("I verify that the QAN number link opens the correct page in a new tab")]
    public async Task ThenIVerifyThatTheQANNumberLinkOpensTheCorrectPageInANewTab()
    {
        await _newQualificationsPage.VerifyClickingOnLinkOpensNewTab();
    }

    [When("I change the funding application status to (.*) for (.*) application")]
    public async Task WhenIChangeTheFundingApplicationStatusToOnHold(string MessageType, string application)
    {
        await application_Details_Page.SelectApplicationAsQfauOfqualAndIfateUser(application);
        await application_Details_Page.ShareApplicaitonWithOfqualUser();
        await application_Details_Page.ShareApplicaitonWithIfatelUser();
        await application_Details_Page.ClickOnViewMessagesLink();
        await application_Messages_Page.SelectMessageType(MessageType);
        await application_Messages_Page.ClickOnPreviewButton();
        await application_Messages_Page.ClickOnConfirmMessageButton();
        await application_Messages_Page.ClickBackLinkOnApplicationMessagePage();
        await application_Details_Page.ClickBackLinkOnApplicationDetailsPage();        
    }

    [Then("I validate as an (.+) application status is (.+) for (.+)")]
    public async Task ThenIValidateApplicationStatusIsWithdrawnAsAnAOUser(string user,string status, string application)
    {
        var User = (user ?? string.Empty).Trim().ToLowerInvariant();
        switch (User)
        {
            case "admin":
                await application_Details_Page.SelectApplicationAsQfauOfqualAndIfateUser(application);
                await startApplicationPage.ValidateStatus(status);
                break;
            case "ao user":
                await startApplicationPage.SelectApplicationAsAOUser(application);
                await startApplicationPage.ValidateStatus(status);
                break;
            case "ofqual":
                await application_Details_Page.SelectApplicationAsQfauOfqualAndIfateUser(application);
                await startApplicationPage.ValidateStatus(status);
                break;
            case "ifate":
                await application_Details_Page.SelectApplicationAsQfauOfqualAndIfateUser(application);
                await startApplicationPage.ValidateStatus(status);
                break;
        }
    }

    [When("I validate the error messages when I search without entering correct details")]
    public async Task WhenIValidateTheErrorMessages()
    {
      await searchForQualification_Page.ValidateErrorMessage();
    }

    [When("I search for a qualification using partial title (.*) and validate the search result has Qualifcation with title containing (.*)")]
    public async Task WhenISearchForAQualificationUsingPartialTitleAndValidateTheSearchResult(string partialtext, string acutaltext)
    {
        await searchForQualification_Page.SearchForQualificationUsingPartialTitle(partialtext, acutaltext);
    }

    [When("I search for a qualification using whitespace in QAN and validate the search result")]
    public async Task WhenISearchForAQualificationUsingPartialQANAndValidateTheSearchResult()
    {
        await searchForQualification_Page.ValidateQAN();
    }

    [When("I assign (.*) and (.*) as reviewer for (.*) application")]
    public async Task WhenIAssignAodpTestAdminAndAodpTestAdminAsReviewerForRADAdvancedVocationalGradedExaminationInDanceApplication(string reviewer1, string reviewer2, string application)
    {
        await application_Details_Page.SelectApplicationAsQfauOfqualAndIfateUser(application);
        await application_Details_Page.AssignReviewers(reviewer1, reviewer2);
    }

    [When("I validate (.*) is a link and opens in a new tab and validate URL is (.*)")]
    public async Task WhenIValidateListOfQualificationsApprovedForFundingIsALinkAndOpensInANewTab(string linkText, string expectedUrl)
    {
        await application_Details_Page.ValidateLinkAndOpenInNewTab(linkText, expectedUrl);
    }

    [Then("I validate user can click on the accociated applications")]
    public async Task ThenIValidateUserCanClickOnTheAccociatedApplications()
    {
        await qualificationDetails_Page.ClickOnFirstAssociatedApplication();
    }
}
