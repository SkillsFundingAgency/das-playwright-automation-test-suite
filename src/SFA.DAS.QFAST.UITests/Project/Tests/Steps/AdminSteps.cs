using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages;
using System.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;



namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps
{
    [Binding]
    public class AdminSteps(ScenarioContext context)
    {
       private readonly QfastHelpers _qfastHelpers = new(context);
       private readonly Admin_Page _qfastHomePage = new(context);
       
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
        }

        [Given(@"I validate opitons on the page with the following expected options")]
        public async Task GivenIValidateOpitonsOnThePageWithTheFollowingExpectedOptions(Table table)
        {
            var expectedOptions = table.Rows.Select(r => r["Option"]).ToArray();
            await _qfastHomePage.ValidateOptionsAsync(expectedOptions: expectedOptions);
        }

    }
}
