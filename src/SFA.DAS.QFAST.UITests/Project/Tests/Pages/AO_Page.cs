namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class AO_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("a.govuk-button:has-text(\"Start new application\")")).ToBeVisibleAsync();

}