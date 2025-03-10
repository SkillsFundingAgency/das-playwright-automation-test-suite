namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class YourAgreementsWithTheEducationAndSkillsFundingAgencyPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your agreements with the Department for Education (DfE)");
    }

    public async Task<ReviewYourDetailsPage> ClickUpdateTheseDetailsLinkInReviewYourDetailsPage()
    {
        await ShowSection();

        await page.GetByRole(AriaRole.Link, new() { Name = "Update these details" }).ClickAsync();

        return await VerifyPageAsync(() => new ReviewYourDetailsPage(context));
    }

    public async Task VerifyIfUpdateTheseDetailsLinkIsHidden()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Update these details" })).ToBeHiddenAsync();
    }

    private async Task ShowSection()
    {
        if (await page.GetByRole(AriaRole.Button, new() { Name = "Show all sections" }).IsVisibleAsync())
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Show all sections" }).ClickAsync();
        }
    }
}
