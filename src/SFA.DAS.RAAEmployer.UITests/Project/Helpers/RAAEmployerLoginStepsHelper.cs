using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Helpers
{
    public class RAAEmployerLoginStepsHelper(ScenarioContext context)
    {
        private readonly EmployerHomePageStepsHelper _homePageStepsHelper = new(context);

        internal async Task<HomePage> GotoEmployerHomePage() => await _homePageStepsHelper.GotoEmployerHomePage();

        internal async Task<HomePage> GoToHomePage(EasAccountUser loginUser) => await _homePageStepsHelper.Login(loginUser);

        internal async Task<CreateAnAdvertHomePage>  GoToCreateAnAdvertHomePage(RAAEmployerUser user)
        {
            await GoToHomePage(user);

            _ = new InterimCreateAnAdvertHomePage(context);

            return new CreateAnAdvertHomePage(context);
        }

        internal async Task<YourApprenticeshipAdvertsHomePage>  GoToRecruitmentHomePage(RAAEmployerUser user)
        {
            await GoToHomePage(user);

            return await NavigateToRecruitmentHomePage();
        }

        internal async Task<YourApprenticeshipAdvertsHomePage> GoToRecruitmentHomePage() => await GoToRecruitmentHomePage(context.GetUser<RAAEmployerUser>());

        internal async Task<YourApprenticeshipAdvertsHomePage> NavigateToRecruitmentHomePage()
        {
            return await VerifyPageHelper.VerifyPageAsync(context, () => new YourApprenticeshipAdvertsHomePage(context, true));
        }
    }
}
