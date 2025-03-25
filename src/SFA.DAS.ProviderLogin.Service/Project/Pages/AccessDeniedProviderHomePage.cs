namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public partial class ProviderHomePage : InterimProviderBasePage
{
    public async Task<ApimAccessDeniedPage> NavigateToDeveloperAPIsPageGoesToApimAccessDenied()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Developer APIs" }).ClickAsync();

        return await VerifyPageAsync(() => new ApimAccessDeniedPage(context));
    }

    public async Task<ProviderAccessDeniedPage> GotoSelectJourneyPageGoesToAccessDenied()
    {
        await AddNewApprentices();

        return await VerifyPageAsync(() => new ProviderAccessDeniedPage(context));
    }

    public async Task<ProviderAccessDeniedPage> GotoAddNewEmployerStartPageGoesToAccessDenied()
    {
        await ClickAddAnEmployerLink();

        return await VerifyPageAsync(() => new ProviderAccessDeniedPage(context));
    }

    public async Task<ProviderAccessDeniedPage> GoToProviderGetFundingGoesToAccessDenied()
    {
        await ClickFundingLink();

        return await VerifyPageAsync(() => new ProviderAccessDeniedPage(context));
    }

}
