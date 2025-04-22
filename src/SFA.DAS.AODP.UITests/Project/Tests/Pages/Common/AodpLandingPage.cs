
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.Common
{
    public class AodpLandingPage(ScenarioContext context) : BasePage(context)
    {

        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToHaveTextAsync("Please select a start Page");


    }
}
