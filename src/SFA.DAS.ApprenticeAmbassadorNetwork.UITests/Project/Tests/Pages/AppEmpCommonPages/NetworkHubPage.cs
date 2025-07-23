using Microsoft.Playwright;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public abstract class NetworkHubPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your network hub");
    }

    public async Task<HomePage> GoToEmployerHomePage()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    //public async Task<EventsHubPage> AccessEventsHub()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Events hub" }).ClickAsync();

    //    return await VerifyPageAsync(() => new EventsHubPage(context));
    //}

    //public async Task<NetworkDirectoryPage> AccessNetworkDirectory()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Network directory" }).ClickAsync();

    //    return await VerifyPageAsync(() => new NetworkDirectoryPage(context));
    //}

    public async Task<ProfileSettingsPage> AccessProfileSettings()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Profile settings" }).ClickAsync();

        return await VerifyPageAsync(() => new ProfileSettingsPage(context));
    }

    //public async Task<ContactUsPage> AccessContactUs()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Contact us" }).ClickAsync();

    //    return await VerifyPageAsync(() => new ContactUsPage(context));
    //}
}