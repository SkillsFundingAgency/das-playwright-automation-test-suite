using Microsoft.Playwright;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SignUpSteps(ScenarioContext context)
    {
        [Given(@"the employer navigates to Sign Up Page")]
        public async Task GivenTheEmployerNavigatesToSignUpPage()
        {
            await Assertions.Expect(context.Get<IPage>().GetByRole(AriaRole.Link, new() { Name = "Apprentices", Exact = true })).ToBeVisibleAsync();

            await context.Get<IPage>().ScreenshotAsync(new()
            {
                Path = context.Get<ObjectContext>().GetDirectory() + "/screenshot2.png",
                FullPage = true,
            });
        }
    }
}
