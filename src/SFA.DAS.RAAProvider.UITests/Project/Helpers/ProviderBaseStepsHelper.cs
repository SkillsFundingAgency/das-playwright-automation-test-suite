
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Helpers;

    public abstract class ProviderBaseStepsHelper(ScenarioContext context)
    {
        protected readonly ScenarioContext _context = context;

        protected async Task<RecruitmentHomePage> GoToRecruitmentHomePage(bool newTab) => await new RecruitmentProviderHomePageStepsHelper(_context).GoToRecruitmentProviderHomePage(newTab);
    }

