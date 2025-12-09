
using SFA.DAS.ProviderLogin.Service.Project;

namespace SFA.DAS.RAA.Service.Project.Pages;

public class ApprenticeshipServiceDevHubPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Using Apprenticeship service APIs");
    }

    public async Task<DisplayAdvertAPIPage> ClickDisplayAdvertApiLink()
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = "Display Advert API" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new DisplayAdvertAPIPage(context));

    }

    public async Task<RecruitmentAPIPage> ClickRecruitmentApiLink()
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = "Recruitment API", Exact = true }).GetByRole(AriaRole.Link).ClickAsync();
        return await VerifyPageAsync(() => new RecruitmentAPIPage(context));
    }

    public async Task<DevHubSignInPage> ClickDevHubSignInLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "sign in to get an API key" }).ClickAsync();

        return await VerifyPageAsync(() => new DevHubSignInPage(context));
    }
}

public class DevHubSignInPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sign in");
    }

    public async Task SignIn()
    {
        var providerConfig = context.GetProviderConfig<ProviderConfig>();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(providerConfig.Username);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync(providerConfig.Password);

        await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();

    }
}

public class RecruitmentAPIPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Recruitment API");
    }

    public async Task VerifyEndpointTitles()
    {
        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("Endpoints");

        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("AccountLegalEntities");
        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("ReferenceData");
        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("Vacancy");

    }
}


public class DisplayAdvertAPIPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Display Advert API");
    }

    public async Task VerifyEndpointTitles()
    {
        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("Endpoints");

        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("Vacancy");
        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("ReferenceData");
        await Assertions.Expect(page.Locator("#mosaic-provider-react-aria-0-1")).ToContainTextAsync("AccountLegalEntities");
    }
}