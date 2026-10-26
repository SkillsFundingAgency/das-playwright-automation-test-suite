namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

    public class ManageProviderAllowedCoursesPage(ScenarioContext context) :  BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage apprenticeships this provider is allowed to deliver");
        }
    }