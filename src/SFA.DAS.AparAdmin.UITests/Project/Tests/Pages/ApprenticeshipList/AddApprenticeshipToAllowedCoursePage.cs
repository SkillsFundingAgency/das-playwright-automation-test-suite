namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

    public class AddApprenticeshipToAllowedCoursePage(ScenarioContext context) :  AparAdminEditBasePage(context)
    {
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add an apprenticeship");
    }
    }