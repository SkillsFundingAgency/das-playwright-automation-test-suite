namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

public class UKPRNPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("What is the provider's UKPRN?");
    }
}
