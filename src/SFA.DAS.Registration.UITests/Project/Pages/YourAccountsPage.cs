namespace SFA.DAS.Registration.UITests.Project.Pages;

public class YourAccountsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public static string PageTitle => "Your accounts";

    public ILocator PageIdentifier => page.Locator("#main-content");

    public override async Task VerifyPage() => await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);

    public async Task<AddAPAYESchemePage> AddNewAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add new account" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAPAYESchemePage(context));

    }

    public async Task<HomePage> ClickAccountLink(string orgName)
    {
        await page.GetByRole(AriaRole.Row, new() { Name = orgName }).GetByRole(AriaRole.Link).ClickAsync();

        objectContext.SetOrganisationName(orgName);

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<HomePage> OpenAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = $"Open  {objectContext.GetOrganisationName()}" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
