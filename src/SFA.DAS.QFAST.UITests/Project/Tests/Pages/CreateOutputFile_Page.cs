

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class CreateOutputFile_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download a record of all active and archived funding requests" })).ToBeVisibleAsync();
}
