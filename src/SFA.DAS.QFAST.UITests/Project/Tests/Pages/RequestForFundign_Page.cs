

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class RequestForFundign_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Requests for funding" })).ToBeVisibleAsync();
    }
}
