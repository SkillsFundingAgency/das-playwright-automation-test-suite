
namespace SFA.DAS.RAAProvider.UITests.Project.Helpers;

public class ProviderApiKeyStepsHelper(ScenarioContext context) : ProviderBaseStepsHelper(context)
{
    public async Task<KeyforApiPage> ViewRecruitmentApiKeyPage()
    {
        var page = await NavigateToAPIListPage();
        return await page.ClickViewRecruitmentAPILink();
    }

    private async Task<ApiListPage> NavigateToAPIListPage()
    {
        var page = await GoToRecruitmentHomePage(false);
        var page1 = await page.NavigateToRecruitmentAPIs();
        return await page1.ClickAPIKeysHereLink();
    }


}
