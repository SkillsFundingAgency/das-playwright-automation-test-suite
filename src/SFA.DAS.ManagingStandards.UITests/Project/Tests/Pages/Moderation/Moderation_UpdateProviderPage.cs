using Microsoft.Playwright;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.Moderation;

public class Moderation_UpdateProviderPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"Update the provider description for {MS_DataHelper.ProviderName}");
    }

    public async Task<Moderation_CheckProviderUpdatePage> EnterUpdateDescriptionAndContinue()
    {
        await page.Locator("#ProviderDescription").FillAsync(managingStandardsDataHelpers.UpdateProviderDescriptionText);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new Moderation_CheckProviderUpdatePage(context));
    }

    public async Task VerifyUpdateDescriptionText() => await Assertions.Expect(page.Locator("form")).ToContainTextAsync(managingStandardsDataHelpers.UpdateProviderDescriptionText);
}
