namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class SearchForAnEmployerPage(ScenarioContext context) : ToolSupportBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Search for an Employer.");

    public async Task EnterHashedAccountId(string hashedAccountId)
    {
        await page.GetByRole(AriaRole.Textbox).FillAsync(hashedAccountId);
    }

    public async Task SelectAllRecords()
    {
        await Assertions.Expect(page.Locator("#searchResultsForm")).ToContainTextAsync("Showing");

        await page.GetByRole(AriaRole.Row, new() { Name = "Name Email Role Account Status" }).GetByLabel("").CheckAsync();
    }

    public async Task ClickSubmitButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
    }

    public async Task<SuspendUsersPage> ClickSuspendUserButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Suspend user(s)" }).ClickAsync();

        return await VerifyPageAsync(() => new SuspendUsersPage(context));
    }

    public async Task<ReinstateUsersPage> ClickReinstateUserButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Reinstate user(s)" }).ClickAsync();

        return await VerifyPageAsync(() => new ReinstateUsersPage(context));
    }


    public class SuspendUsersPage(ScenarioContext context) : ToolSupportBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Suspend users");

        public async Task ClicSuspendUsersbtn()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Suspend user(s)" }).ClickAsync();
        }

        public async Task VerifyStatusColumn(string status) => await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(status);
    }

    public class ReinstateUsersPage(ScenarioContext context) : ToolSupportBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Reinstate users");

        public async Task ClickReinstateUsersbtn()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Reinstate user(s)" }).ClickAsync();
        }

        public async Task VerifyStatusColumn(string status) => await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(status);
    }
}
