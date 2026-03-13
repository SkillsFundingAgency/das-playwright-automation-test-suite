using Azure;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManagingStandardsProviderHomePage(ScenarioContext context) : ProviderHomePage(context)
{
    public new async Task<YourStandardsAndTrainingVenuesPage> NavigateToYourStandardsAndTrainingVenuesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage your training and venues"}).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }
}

public abstract class ManagingStandardsBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly ManagingStandardsDataHelpers managingStandardsDataHelpers = context.Get<ManagingStandardsDataHelpers>();
}
