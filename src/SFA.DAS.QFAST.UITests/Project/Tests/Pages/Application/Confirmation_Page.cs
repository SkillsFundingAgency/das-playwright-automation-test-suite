namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class Confirmation_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Application submitted" })).ToBeVisibleAsync();    
    }
}
