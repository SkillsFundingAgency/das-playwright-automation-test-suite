namespace SFA.DAS.Login.Service.Project.Helpers
{
    public interface IReLoginHelper
    {
        Task<bool> IsSignInPageDisplayed();

        Task<bool> IsLandingPageDisplayed();
    }
}
