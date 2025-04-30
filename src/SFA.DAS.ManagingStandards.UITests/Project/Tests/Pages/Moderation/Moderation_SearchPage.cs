namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.Moderation;

public class Moderation_SearchPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Search for an apprenticeship training provider");
    }

    public async Task<Moderation_ProviderDetailsPage> SearchTrainingProviderByUkprn(string text)
    {
        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "UKPRN" }).FillAsync(text);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return await VerifyPageAsync(() => new Moderation_ProviderDetailsPage(context));
    }
}
