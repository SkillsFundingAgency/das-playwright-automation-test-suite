namespace SFA.DAS.Login.Service.Project;

public abstract class StubAddYourUserDetailsBasePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your name?");
    }

    protected async Task EnterNameAndContinue(string firstName, string lastName)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(firstName);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(lastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}
