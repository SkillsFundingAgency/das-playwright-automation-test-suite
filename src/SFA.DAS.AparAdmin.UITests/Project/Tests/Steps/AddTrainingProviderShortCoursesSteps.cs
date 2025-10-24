using SFA.DAS.AparAdmin.UITests.Project.Helpers;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding]
public class AddTrainingProviderShortCoursesSteps(ScenarioContext context)
{

    [Then(@"the user verifies links available in Manage Training Provider page")]
    public async Task ThenTheUserVerifiesLinksAvailableInManageTrainingProviderPage()
    {
        var page = new AparAdminHomePage(context);
        await page.ClickAddOrSearchForProvider();

        var managePage = new ManageTrainingProviderInformationPage(context);

        var page1 = await managePage.ClickSearchForATrainingProvider();
        var page2 = await page1.GoBackToManageTrainingProvider();
        var page3 = await page2.ClickAddUkprnToAllowList();
        var page4 = await page3.GoBackToManageTrainingProvider();
        var page5 = await page4.ClickAddNewTrainingProvider();
        await page5.GoBackToManageTrainingProvider();
    }
}

