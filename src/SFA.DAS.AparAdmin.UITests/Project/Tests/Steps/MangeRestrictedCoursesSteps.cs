using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;
using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;


[Binding, Scope(Tag = "apar")]
public class MangeRestrictedCoursesSteps
{
    private readonly ScenarioContext _context;

    public MangeRestrictedCoursesSteps(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"Verifies the Filters functionality")]
    public async Task GivenVerifiesTheFiltersFunctionality()
    {
        var manageTrainingProviderPage = await OpenManageTrainingProviderPage();
        var searchPage = await manageTrainingProviderPage.NavigateToRestrictedCourses();
    }

    private async Task<ManageTrainingProviderInformationPage> OpenManageTrainingProviderPage()
    {
        var home = new AparAdminHomePage(_context);
        await home.ClickAddOrSearchForProvider();
        return await new ManageTrainingProviderInformationPage(_context)
            .VerifyPageAsync(() => new ManageTrainingProviderInformationPage(_context));
    }
}

