

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class SearchForQualification_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Search for a qualification" })).ToBeVisibleAsync();        
    }
}
