using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;
using SFA.DAS.Framework.Hooks;
using SFA.DAS.RAA.Service.Project.Pages;
using SFA.DAS.RAA.Service.Project.Pages.Reviewer;

namespace SFA.DAS.RAA.Service.Project.Helpers;

public class ReviewerStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
{
    public async Task<Reviewer_HomePage> GoToReviewerHomePage()
    {
        await OpenNewTab();

        await Navigate(UrlConfig.RAAQA_BaseUrl);

        await new DfeAdminLoginStepsHelper(context).CheckAndLoginToASVacancyQa();

        return new Reviewer_HomePage(context);
    }

    public async Task VerifyEmployerNameAndApprove()
    {
        var page = await ReviewVacancy();

        await page.VerifyEmployerName();

        var page1 = await page.Approve();

        await RAAQASignOut(page1);
    }

    public async Task Refer()
    {
        var page = await ReviewVacancy();

        await page.ReferTitle();

        await RAAQASignOut(page);
    }

    public async Task VerifyDisabilityConfidenceAndApprove()
    {
        var page = await ReviewVacancy();

        await page.VerifyDisabilityConfident();

        var page1 = await page.Approve();

        await RAAQASignOut(page1);
    }

    private async Task<Reviewer_VacancyPreviewPage> ReviewVacancy()
    {
        var page = await GoToReviewerHomePage();

        return await page.ReviewVacancy();
    }

    private async Task RAAQASignOut(VerifyDetailsBasePage basePage)
    {
        await basePage.RAAQASignOut();

        await new ASVacancyQaLandingPage(context).VerifyPage();
    }
}
