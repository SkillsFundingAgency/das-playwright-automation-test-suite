namespace SFA.DAS.Login.Service.Project.Helpers;

public abstract class StubSignInBasePage(ScenarioContext context) : BasePage(context)
{
    protected async Task EnterLoginDetails(string email, string userref)
    {
        objectContext.SetDebugInformation($"Entering - Userref:'{userref}' and Email:'{email}'");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "ID" }).FillAsync(userref);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(email);
    }

    protected async Task ClickSignIn() => await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();

    protected async Task EnterLoginDetailsAndClickSignIn(string email, string userref)
    {
        await EnterLoginDetails(email, userref);

        await ClickSignIn();
    }
}
