

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.Moderation;

public class Moderation_CheckProviderUpdatePage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync($"Check the provider description entry for {MS_DataHelper.ProviderName}");
    }

    public async Task<Moderation_ProviderDetailsPage> ContinueOnCheckProviderUpdatePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new Moderation_ProviderDetailsPage(context));
    }
}
