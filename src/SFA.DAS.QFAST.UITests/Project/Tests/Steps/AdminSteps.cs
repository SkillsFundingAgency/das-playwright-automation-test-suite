using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps;

[Binding]
public class AdminSteps(ScenarioContext context)
{
    private readonly QfastHelpers _qfastHelpers = new(context);
    private readonly Admin_Page _qfastAdminPage = new(context);
    private readonly ViewForms_Page _viewFormsPage = new(context);
    private readonly CreateNewForm_Page _createNewFormPage = new(context);

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
            case "Create a submission form":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(() => new ViewForms_Page(context));
                break;

            case "Review funding requests":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(() => new RequestForFundign_Page(context));
                break;

            case "Review newly regulated qualifications":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(() => new NewQualifications_Page(context));
                break;

            case "Review regulated qualifications with changes":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(() => new ChangedQualifications_Page(context));
                break;

            case "Import data":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(() => new ImportQualifications_Page(context));
                break;

            case "Create an output file":
                await _qfastAdminPage.SelectOptions(option);
                await VerifyPageHelper.VerifyPageAsync(() => new CreateOutputFile_Page(context));
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
    public void WhenILogOutFromThePortal()
    {
      _qfastAdminPage.ClickLogOut().GetAwaiter().GetResult();
    }

}
