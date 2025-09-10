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