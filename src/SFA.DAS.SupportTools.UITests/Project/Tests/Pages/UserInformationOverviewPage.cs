namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class UserInformationOverviewPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("table.govuk-table").First).ToContainTextAsync(config.Name);
}