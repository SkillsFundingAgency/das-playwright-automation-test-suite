namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages;

public class AO_HomePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Find an assessment opportunity");

        await AcceptAllCookiesIfVisible();
    }

    public async Task VerifyApprovedTab()
    {
        await Assertions.Expect(page.Locator("#tab_approved")).ToContainTextAsync("Approved");

        await Assertions.Expect(page.GetByLabel("Approved", new() { Exact = true }).GetByRole(AriaRole.Heading)).ToContainTextAsync("Approved Standards");
    }

    public async Task<AO_ApprovedStandardDetailsPage> ClickOnAbattoirWorkerApprovedStandardLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Abattoir worker" }).ClickAsync();

        return await VerifyPageAsync(() => new AO_ApprovedStandardDetailsPage(context));
    }

    public async Task ClickInDevelopmentTab()
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = "In-development" }).ClickAsync();
    }

    public async Task<AO_InDevelopmentStandardDetailsPage> ClickOnInDevelopmentStandardLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Interior designer" }).ClickAsync();

        return await VerifyPageAsync(() => new AO_InDevelopmentStandardDetailsPage(context));
    }

    public async Task ClickInProposedTab()
    {
        await page.GetByRole(AriaRole.Tab, new() { Name = "Proposed" }).ClickAsync();
    }

    public async Task<AO_ProposedStandardDetailsPage> ClickOnAProposedStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Fintech Product Manager" }).ClickAsync();

        return await VerifyPageAsync(() => new AO_ProposedStandardDetailsPage(context));
    }
}

public class AO_ApprovedStandardDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Abattoir worker");
    }
}

public class AO_InDevelopmentStandardDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Interior designer");
    }
}

public class AO_ProposedStandardDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Fintech Product Manager");
    }
}