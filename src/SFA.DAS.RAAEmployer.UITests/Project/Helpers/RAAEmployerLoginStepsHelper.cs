
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project.Helpers;
using System.Linq;

namespace SFA.DAS.RAAEmployer.UITests.Project.Helpers
{
    public class RAAEmployerLoginStepsHelper(ScenarioContext context)
    {
        private readonly EmployerHomePageStepsHelper _homePageStepsHelper = new(context);

        internal async Task<HomePage> GotoEmployerHomePage()
        {
            if(context.ScenarioInfo.Tags.Contains("raaapiemployer"))
            {
                var user = context.GetUser<RAAApiEmployerUser>();

                if (user != null)
                {
                    context.Get<ObjectContext>().Set("loggedinuserobject", new LoggedInAccountUser
                    {
                        Username = user.Username,
                        IdOrUserRef = user.IdOrUserRef,
                        OrganisationName = user.OrganisationName
                    });

                    context.Get<ObjectContext>().Replace("organisationname", user.OrganisationName);
                }
            }
            return await _homePageStepsHelper.GotoEmployerHomePage();
        }

        internal async Task<HomePage> GoToHomePage(EasAccountUser loginUser) => await _homePageStepsHelper.Login(loginUser);

        internal async Task<CreateAnAdvertHomePage> GoToCreateAnAdvertHomePage(RAAEmployerUser user)
        {
            await GoToHomePage(user);

            _ = new InterimCreateAnAdvertHomePage(context);

            return new CreateAnAdvertHomePage(context);
        }

        internal async Task<YourApprenticeshipAdvertsHomePage> GoToRecruitmentHomePage(EasAccountUser user)
        {
            await GoToHomePage(user);

            return await NavigateToRecruitmentHomePage();
        }

        internal async Task<YourApprenticeshipAdvertsHomePage> GoToRecruitmentHomePage() 
        {
            EasAccountUser user;

            if (context.ScenarioInfo.Tags.Contains("raaapiemployer"))
            {
                user = context.GetUser<RAAApiEmployerUser>();
            }
            else
            {
                user = context.GetUser<RAAEmployerUser>();
            }

            return await GoToRecruitmentHomePage(user);
        }

        internal async Task<YourApprenticeshipAdvertsHomePage> NavigateToRecruitmentHomePage()
        {
            return await VerifyPageHelper.VerifyPageAsync(context, () => new YourApprenticeshipAdvertsHomePage(context, true));
        }
    }
}
