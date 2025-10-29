using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages;



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
                                    
                default:
                   
                    await _qfastHelpers.GoToQfastAdminHomePage();
                    break;
            }
        }

        [Given("I validate opitons on the page")]
        public async Task GivenIValidateOpitonsOnThePage()
        {
            await _qfastHomePage.ValidateOptionsAsync();
        }

    }
}
