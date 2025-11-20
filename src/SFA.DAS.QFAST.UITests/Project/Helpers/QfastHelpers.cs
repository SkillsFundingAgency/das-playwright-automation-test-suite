using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;

namespace SFA.DAS.QFAST.UITests.Project.Helpers;

public class QfastHelpers(ScenarioContext context)
{    
    protected readonly ScenarioContext context = context;

    public async Task<Admin_Page> GoToQfastAdminHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsAdmin();

        return await VerifyPageHelper.VerifyPageAsync(() => new Admin_Page(context));
    }
    public async Task<AO_Page> GoToQfastAOHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsAOUser();

        return await VerifyPageHelper.VerifyPageAsync(() => new AO_Page(context));
    }
    public async Task<IFATE_Page> GoToQfastIFATEHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsIFATEUser();

        return await VerifyPageHelper.VerifyPageAsync(() => new IFATE_Page(context));
    }
    public async Task<OFQUAL_Page> GoToQfastOFQUALHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsOFQUALUser();

        return await VerifyPageHelper.VerifyPageAsync(() => new OFQUAL_Page(context));
    }
    public async Task<Admin_Page> GoToQfastDataImporterHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsDataImporterUser();

        return await VerifyPageHelper.VerifyPageAsync(() => new Admin_Page(context));
    }
    public async Task<Admin_Page> GoToQfastReviewerHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsReviewerUser();

        return await VerifyPageHelper.VerifyPageAsync(() => new Admin_Page(context));
    }
    public async Task<Admin_Page> GoToQfastFormEditorHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).LoginToQfastAsFormEditorUser();

        return await VerifyPageHelper.VerifyPageAsync(() => new Admin_Page(context));
    }
}
