namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public partial class ProviderHomePage : InterimProviderBasePage
{
    public async Task<ProviderLandingPage> SignsOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderLandingPage(context));
    }

    public async Task<ProviderNotificationSettingsPage> GoToProviderNotificationSettingsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Settings" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Notification settings" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderNotificationSettingsPage(context));
    }

    public async Task<ProviderEmployersAndPermissionsPage> GoToProviderEmployersAndPermissionsPagePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "More" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Organisations and agreements" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderEmployersAndPermissionsPage(context));
    }

    public async Task<ManageApprenticeshipsServiceHelpPage> GoToManageApprenticeshipsServiceHelpPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Help" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageApprenticeshipsServiceHelpPage(context));
    }

    public async Task VerifyProviderFooterFeedbackPage()
    {
        var page2 = await page.RunAndWaitForPopupAsync(async () =>
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Give feedback" }).ClickAsync();
        });

        await Assertions.Expect(page2.Locator("legend")).ToContainTextAsync("Which of the below describes you?");

        await page2.CloseAsync();
    }

    public async Task<ProviderPrivacyPage> GoToProviderFooterPrivacyPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Privacy" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderPrivacyPage(context));
    }

    public async Task<ProviderCookiesPage> GoToProviderFooterCookiesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Cookies", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderCookiesPage(context));
    }

    public async Task<ProviderTermsOfUsePage> GoToProviderFooterTermsOfUsePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Terms of use" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderTermsOfUsePage(context));
    }
}

public class ProviderTermsOfUsePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Terms of use");
    }

    public async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}


public class ProviderCookiesPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Cookies");
    }


    public async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}


public class ProviderPrivacyPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Privacy");
    }


    public async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}

public class ManageApprenticeshipsServiceHelpPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage apprenticeships service help");
    }

    public async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}

public class ProviderEmployersAndPermissionsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View employers and manage permissions");
    }

    public async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}


public class ProviderNotificationSettingsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Notification settings");


    //private static By NotificationOptions => By.CssSelector(".selection-button-radio");

    //private static By Alert => By.CssSelector(".green-box-alert");

    //public ProviderNotificationSettingsPage ChooseToReceiveEmails() => SelectReceiveEmailsOptions("NotificationSettings-true-0");

    //public ProviderNotificationSettingsPage ChooseNotToReceiveEmails() => SelectReceiveEmailsOptions("NotificationSettings-false-0");

    //public bool IsSettingsUpdated() => pageInteractionHelper.IsElementDisplayed(Alert);

    //private ProviderNotificationSettingsPage SelectReceiveEmailsOptions(string option)
    //{
    //    formCompletionHelper.SelectRadioOptionByForAttribute(NotificationOptions, option);
    //    Continue();
    //    return this;
    //}

    public async Task<ProviderHomePage> ClickCancel()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Cancel" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}