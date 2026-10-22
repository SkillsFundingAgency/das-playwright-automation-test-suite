namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class SettingPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    public override async Task VerifyPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Settings" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Settings");
    }

    public async Task<DeleteAccountPage> DeleteMyAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Delete my account" }).ClickAsync();

        return await VerifyPageAsync(() => new DeleteAccountPage(context));
    }

    public async Task VerifyPersonalDetails()
    {
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync($"{fAAUserNameDataHelper.FirstName} {fAAUserNameDataHelper.LastName}");

        await Assertions.Expect(page.Locator("main")).ToContainTextAsync(fAAUserNameDataHelper.FaaNewUserDob.ToString("d MMMM yyyy"));

        await Assertions.Expect(page.Locator("main")).ToContainTextAsync(fAAUserNameDataHelper.FaaNewUserMobilePhone);
    }

    public async Task<ChangeYourNamePage> ChangeName()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change name" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeYourNamePage(context));
    }

    public async Task<ChangeDateOfBirthPage> ChangeDateOfBirth()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change date of birth" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeDateOfBirthPage(context));
    }

    public async Task<ChangeYourAddressPage> ChangeAddress()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change address" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeYourAddressPage(context));
    }

    public async Task<ChangeYourTelephoneNumberPage> ChangeTelephoneNumber()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change telephone number" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeYourTelephoneNumberPage(context));
    }

    public async Task<ChangeEmailPage> ViewEmailSettings()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change Email address" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeEmailPage(context));
    }

    public async Task<ChangeNotificationPreferencesPage> ChangeNotificationPreferences()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change notification preferences" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeNotificationPreferencesPage(context));
    }

    public async Task<SearchAlertsPage> GoToSearchAlerts()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search alerts" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchAlertsPage(context));
    }

    public async Task<EqualityQuestionsSettingsPage> ViewEqualityQuestionAnswers()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change your answers to the equality questions" }).ClickAsync();

        return await VerifyPageAsync(() => new EqualityQuestionsSettingsPage(context));
    }
}

public class EqualityQuestionsSettingsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your answers to the equality questions");

    public async Task<SettingPage> Save()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
    }
}

public class ChangeEmailPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your email address");

    public async Task<SettingPage> GoBackToSettings()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back" , Exact = true}).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
    }
}

public class ChangeNotificationPreferencesPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l")).ToContainTextAsync("Change if you get reminders about your unfinished applications");

    public async Task<SettingPage> SelectNoAndSave()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
    }
}

public class SearchAlertsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l")).ToContainTextAsync("Search alerts");

    public async Task VerifyNoSearchAlertsCreated() =>
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("You have not created any search alerts.");
}

public class DeleteAccountPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Delete your Find an apprenticeship account");

    public async Task<ConfirmDeleteAccountPage> ContinueDeleteMyAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmDeleteAccountPage(context));
    }
    public async Task<ConfirmWithdrawBeforeDeleteAccountPage> ContinueToDeleteMyAccounWithApplication()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmWithdrawBeforeDeleteAccountPage(context));
    }
}


public class ConfirmDeleteAccountPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Confirm your account deletion");

    public async Task<FAASignedOutLandingpage> ConfirmDeleteMyAccount()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Your email address" }).FillAsync(fAAUserNameDataHelper.FaaNewUserEmail);

        await page.GetByRole(AriaRole.Button, new() { Name = "Delete my account" }).ClickAsync();

        return await VerifyPageAsync(() => new FAASignedOutLandingpage(context));
    }
}


public class ConfirmWithdrawBeforeDeleteAccountPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Withdraw your outstanding applications");

    public async Task<ConfirmDeleteAccountPage> WithdrawBeforeDeletingMyAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmDeleteAccountPage(context));
    }

}