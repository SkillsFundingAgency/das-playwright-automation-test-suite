using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;


namespace SFA.DAS.AparAdmin.UITests.Project.Helpers;

public class ApprenticeshipListHelper
{
    private readonly ScenarioContext _context;
  
    public ApprenticeshipListHelper(ScenarioContext context)
    {
        _context = context; 
    }

    public async Task<ProviderRestrictedCoursesPage>OpenManageProviderCoursePage()
    {
        var home = new SearchforATrainingProviderPage(_context);
        await home.ClickManageRestrictedCourses();
        return await new ProviderRestrictedCoursesPage(_context)
            .VerifyPageAsync( () => new ProviderRestrictedCoursesPage(_context));
    }

    public async Task<RestrictACoursesPage>ClickRestrictACourse()
    {
        var home = new ProviderRestrictedCoursesPage(_context);
        await home.ClickRestrictACourse();
        return await new RestrictACoursesPage(_context)
            .VerifyPageAsync( () => new RestrictACoursesPage(_context));
    }

    public async Task<ChangeRestrictionPage>ClickChangeCourseDateDetails(string larsCode)
    {
        var home = new ProviderRestrictedCoursesPage(_context);
        await home.ClickChangeCourseDateDetails(larsCode);
        return await new ChangeRestrictionPage(_context)
            .VerifyPageAsync( () => new ChangeRestrictionPage(_context));
    }

    public async Task<SetLastStartDatePage>ConfirmEdit()
    {
        var home = new ChangeRestrictionPage(_context);
        await home.Confirm();
        return await new SetLastStartDatePage(_context)
            .VerifyPageAsync( () => new SetLastStartDatePage(_context));
    }

    public async Task<RemoveRestrictionPage>ConfirmRemoveCourseRestriction()
    {
        var home = new ChangeRestrictionPage(_context);
        await home.Confirm();
        return await new RemoveRestrictionPage(_context)
            .VerifyPageAsync( () => new RemoveRestrictionPage(_context));
    }

    public async Task<ManageTrainingProviderInformationPage>OpenManageTrainingProviderPage()
    {
        var home = new AparAdminHomePage(_context);
        await home.ClickManageTrainingProvidersAndRestrictedCourses();
        return await new ManageTrainingProviderInformationPage(_context)
            .VerifyPageAsync( () => new ManageTrainingProviderInformationPage(_context));
    }

    public async Task<AcademicProfessionalPage>OpenAcademicProfessionalPage(string larsCode)
    {
        var home = new ViewMangeRestrictedCoursesPage(_context);
        await home.SelectRestrictedManageProviders(larsCode);
        return await new AcademicProfessionalPage(_context)
            .VerifyPageAsync( () => new AcademicProfessionalPage(_context));
    }
}