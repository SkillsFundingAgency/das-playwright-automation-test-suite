namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class YourAmbassadorProfilePage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your ambassador profile");
    }

    public async Task VerifyYourAmbassadorProfile(string value) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("London");

    public async Task<YourPersonalDetailsPage> AccessChangeForPersonalDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change" }).First.ClickAsync();

        return await VerifyPageAsync(() => new YourPersonalDetailsPage(context));
    }

    public async Task<InterestIinTheNetworkPage> AccessChangeForInterestInNetwork()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change" }).Nth(1).ClickAsync();

        return await VerifyPageAsync(() => new InterestIinTheNetworkPage(context));
    }

    public async Task<YourApprenticeshipInformationPage> AccessChangeForApprenticeshipInformation()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change" }).Nth(2).ClickAsync();

        return await VerifyPageAsync(() => new YourApprenticeshipInformationPage(context));
    }

    public async Task<ContactDetailsPage> AccessChangeForContactDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change" }).Nth(3).ClickAsync();

        return await VerifyPageAsync(() => new ContactDetailsPage(context));
    }
}

public class ContactDetailsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Contact details");
    }

    public async Task<YourAmbassadorProfilePage> ChangeLinkedlnUrlAndContinue()
    {
        await page.Locator("#LinkedinUrl").FillAsync(RandomDataGenerator.GenerateRandomAlphabeticString(12));

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }

    public async Task<YourAmbassadorProfilePage> HideLinkedlnInformation()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Display LinkedIn information" }).UncheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }
    public async Task<YourAmbassadorProfilePage> DisplayLinkedlnInformation()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Display LinkedIn information" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }
}


public class YourApprenticeshipInformationPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your apprenticeship information");
    }

    public async Task<YourAmbassadorProfilePage> HideApprenticeshipInformation()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Display all my organisation" }).UncheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }

    public async Task<YourAmbassadorProfilePage> DisplayApprenticeshipInformation()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Display all my organisation" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return new YourAmbassadorProfilePage(context);
    }
}

public class InterestIinTheNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your areas of interest as an ambassador");
    }

    public async Task<YourAmbassadorProfilePage> SelectProjectManagementAndContinue()
    {
        var locator = page.GetByRole(AriaRole.Checkbox, new() { Name = "Champion the delivery of" });

        if (await locator.IsCheckedAsync())
        {
            await locator.UncheckAsync();
        }
        else
        {
            await locator.CheckAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }
}

public class YourPersonalDetailsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Your personal details");
    }

    public async Task<YourAmbassadorProfilePage> ChangePersonalDetailsAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Job title" }).FillAsync(RandomDataGenerator.GenerateRandomAlphabeticString(12));

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Add a biography" }).FillAsync(RandomDataGenerator.GenerateRandomAlphabeticString(50));

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }
    public async Task<YourAmbassadorProfilePage> HideJobtitleAndBiography()
    {
        await page.Locator("#ShowJobTitle").UncheckAsync();

        await page.Locator("#ShowBiography").UncheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }
    public async Task<YourAmbassadorProfilePage> DisplayJobtitleAndBiography()
    {
        await page.Locator("#ShowBiography").CheckAsync();

        await page.Locator("#ShowJobTitle").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save changes" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAmbassadorProfilePage(context));
    }
}