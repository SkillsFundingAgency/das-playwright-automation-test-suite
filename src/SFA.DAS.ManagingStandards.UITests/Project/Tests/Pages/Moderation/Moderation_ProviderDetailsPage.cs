namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.Moderation;

public class Moderation_ProviderDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync($"Provider details for {MS_DataHelper.ProviderName}");
    }

    public async Task<Moderation_UpdateProviderPage> ChangeProviderDetail()
    {
        await page.Locator("a.govuk-link:has-text(\"Change\")").ClickAsync();

        return await VerifyPageAsync(() => new Moderation_UpdateProviderPage(context));
    }
}
