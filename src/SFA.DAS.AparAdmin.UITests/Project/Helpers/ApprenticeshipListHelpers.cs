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
        var home = new ProviderDetailsPage(_context);
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

    public async Task<RestrictProviderPage>ChangeHowWeManageProvider()
    {
         var home = new ProviderRestrictedCoursesPage(_context);
        await home.ChangeProviderDelivery();
        return await new RestrictProviderPage(_context)
            .VerifyPageAsync( () => new RestrictProviderPage(_context));
    }

    public async Task<ConfirmRestrictProviderPage>ControlProviderDelivery()
    {
       var home = new RestrictProviderPage(_context);
       await home.NavigateToConfirmControlProviderDelivery();
       return await new ConfirmRestrictProviderPage(_context)
            .VerifyPageAsync( () => new ConfirmRestrictProviderPage(_context));

    }
    
    public async Task<AllowedCoursesPage>ConfirmControlProviderDelivery()
    {
        var home = new ConfirmRestrictProviderPage(_context);
        await home.ConfirmProviderRestricted();
        return await new AllowedCoursesPage(_context)
            .VerifyPageAsync( () => new AllowedCoursesPage(_context));

    }

    public async Task<AllowedCoursesPage>OpenManageAllowedCoursePage()
    {
         var home = new ProviderDetailsPage(_context);
        await home.ClickManageApprovedCourses();
        return await new AllowedCoursesPage(_context)
            .VerifyPageAsync( () => new AllowedCoursesPage(_context));
    }

    public async Task<ManageProviderAllowedUnitsPage>OpenManageApprenticeshipUnitsPage()
    {
        var home = new ProviderDetailsPage(_context);
        await home.ClickManageApprovedUnits();
        return await new ManageProviderAllowedUnitsPage(_context)
            .VerifyPageAsync( () => new ManageProviderAllowedUnitsPage(_context));
    }

    public async Task<AddApprenticeshipToAllowedCoursePage>AddAnApprenticeship()
    {
        var home = new AllowedCoursesPage(_context);
        await home.ClickAddAnAppreticeship();
        return await new AddApprenticeshipToAllowedCoursePage(_context)
            .VerifyPageAsync( () => new AddApprenticeshipToAllowedCoursePage(_context)); 
    }

    public async Task<AddApprenticeshipUnitToAllowedPage>AddAnApprenticeshipUnit()
    {
        var home = new ManageProviderAllowedUnitsPage(_context);
        await home.ClickAddAnAppreticeshipUnit();
        return await new AddApprenticeshipUnitToAllowedPage(_context)
            .VerifyPageAsync( () => new AddApprenticeshipUnitToAllowedPage(_context));
    }

    public async Task<ProviderDetailsPage>GoBackToProviderDetailsPage()
    {
        var home = new AllowedCoursesPage(_context);
        await home.GoBackToProviderDetailsPage();
        return await new ProviderDetailsPage(_context)
            .VerifyPageAsync( () => new ProviderDetailsPage(_context));
    }
}