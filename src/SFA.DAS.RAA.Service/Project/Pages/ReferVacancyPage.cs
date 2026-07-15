
namespace SFA.DAS.RAA.Service.Project.Pages;

public class ReferVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-error-summary__title")).ToContainTextAsync("Edits needed");
    }
}
