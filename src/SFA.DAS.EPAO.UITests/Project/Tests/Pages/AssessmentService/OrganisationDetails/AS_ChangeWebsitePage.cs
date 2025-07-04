namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

public class AS_ChangeWebsitePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change website address");

    public async Task<AS_ConfirmWebsiteAddressPage> EnterRandomWebsiteAddressAndClickUpdate()
    {
        await page.Locator("#WebsiteLink").FillAsync(ePAOAssesmentServiceDataHelper.RandomWebsiteAddress);

        await page.GetByRole(AriaRole.Button, new() { Name = "Update address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmWebsiteAddressPage(context));
    }
}

public class AS_ConfirmWebsiteAddressPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm website address");

    public async Task<AS_WebsiteAddressUpdatedPage> ClickConfirmButtonInConfirmWebsiteAddressPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WebsiteAddressUpdatedPage(context));
    }
}

public class AS_WebsiteAddressUpdatedPage(ScenarioContext context) : AS_ChangeOrgDetailsBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Website address updated");
}