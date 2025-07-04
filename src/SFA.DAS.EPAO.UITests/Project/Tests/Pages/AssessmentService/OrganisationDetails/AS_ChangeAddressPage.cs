namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

public class AS_ChangeAddressPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change address");

  
    public async Task<AS_ChangeAddressPage> ClickSearchForANewAddressLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search for a new address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangeAddressPage(context));
    }

    public async Task<AS_ChangeAddressPage> ClickEnterTheAddressManuallyLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Enter the address manually" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangeAddressPage(context));
    }

    public async Task<AS_ConfirmContactAddressPage> EnterEmployerAddressAndClickChangeAddressButton()
    {
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street line 1 of" }).FillAsync(EPAODataHelper.GetRandomNumber(3));
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street line 2 of" }).FillAsync("QuintonRoad");
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Town or city" }).FillAsync("Coventry");
        
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "County" }).FillAsync("Coventry");
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync("CV1 2WT");

        await page.GetByRole(AriaRole.Button, new() { Name = "Change address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmContactAddressPage(context));
    }
}

public class AS_ConfirmContactAddressPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm contact address");

    public async Task<AS_ContactAddressUpdatedPage> ClickConfirmAddressButtonInConfirmContactAddressPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ContactAddressUpdatedPage(context));
    }
}

public class AS_ContactAddressUpdatedPage(ScenarioContext context) : AS_ChangeOrgDetailsBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Contact address updated");
}