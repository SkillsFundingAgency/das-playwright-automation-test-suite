namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class StartApplication_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { NameRegex = new("Start application for:") })).ToBeVisibleAsync();

    }
}
