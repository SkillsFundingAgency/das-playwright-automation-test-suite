
using SFA.DAS.RAA.Service.Project.Pages;
using SFA.DAS.RAA.Service.Project.Pages.Reviewer;

namespace SFA.DAS.RAA.Service.Project.Helpers;

public class ReviewerStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private VerifyDetailsBasePage verifyDetailsBasePage;

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

        verifyDetailsBasePage = await page.Approve();

    }

    public async Task Refer()
    {
        var verifyDetailsBasePage = await ReviewVacancy();

        await verifyDetailsBasePage.ReferTitle();
    }

    public async Task VerifyDisabilityConfidenceAndApprove()
    {
        var page = await ReviewVacancy();

        await page.VerifyDisabilityConfident();

        verifyDetailsBasePage = await page.Approve();
    }

    private async Task<Reviewer_VacancyPreviewPage> ReviewVacancy()
    {
        var page = await GoToReviewerHomePage();

        return await page.ReviewVacancy();
    }

    public async Task RAAQASignOut()
    {
        await verifyDetailsBasePage.RAAQASignOut();

        await new ASVacancyQaLandingPage(context).VerifyPage();
    }
}
