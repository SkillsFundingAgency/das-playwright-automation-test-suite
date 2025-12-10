namespace SFA.DAS.RAA.Service.Project.Pages;

public class GetStartedWithRecruitmentAPIsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get started with the recruitment APIs");
    }

    public async Task<ApiListPage> ClickAPIKeysHereLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "API keys here" }).ClickAsync();

        return await VerifyPageAsync(() => new ApiListPage(context));
    }

    public async Task<ApprenticeshipServiceDevHubPage> ClickDeveloperGetStartedPageLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "developer get started page" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipServiceDevHubPage(context));
    }
}

public class ApiListPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("API list");
    }

    public async Task<KeyforApiPage> ClickViewRecruitmentAPILink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View key  for the Recruitment API", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new KeyforApiPage(context));
    }

    public async Task<KeyforApiPage> ClickViewRecruitmentAPISandBoxLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View key  for the Recruitment API Sandbox" }).ClickAsync();

        return await VerifyPageAsync(() => new KeyforApiPage(context));
    }

    public async Task<KeyforApiPage> ClickViewDisplayAPILink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View key  for the Display" }).ClickAsync();

        return await VerifyPageAsync(() => new KeyforApiPage(context));
    }

    public async Task VerifyDisplayAdvertApiText()
    {
        await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync("Display Advert API");
    }
}

public class KeyforApiPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Key for API");
    }

    public async Task VerifyApikeyRenewed() => await Assertions.Expect(page.Locator("#renew-confirmation-banner")).ToContainTextAsync("Key renewed");

    public async Task<AreYouSureYouWantToRenewThisAPIKeyPage> ClickRenewKeyLink()
    {
        await page.Locator("summary").ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Renew API key" }).ClickAsync();

        return await VerifyPageAsync(() => new AreYouSureYouWantToRenewThisAPIKeyPage(context));
    }

    public async Task ClickAdvertsLink() => await page.GetByRole(AriaRole.Menuitem, new() { Name = "Adverts" }).ClickAsync();
}

public class AreYouSureYouWantToRenewThisAPIKeyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to renew this API key?");
    }

    public async Task<KeyforApiPage> RenewAPIKey() => await GoToKeyforAPIPage("Yes");
    public async Task<KeyforApiPage> DoNotRenewApiKey() => await GoToKeyforAPIPage("No");

    private async Task<KeyforApiPage> GoToKeyforAPIPage(string option)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = option }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new KeyforApiPage(context));
    }
}