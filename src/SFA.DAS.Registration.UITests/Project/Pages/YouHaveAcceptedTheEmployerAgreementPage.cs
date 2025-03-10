namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class YouHaveAcceptedTheEmployerAgreementPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("accepted the employer agreement");

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Download your accepted agreement" })).ToBeVisibleAsync();
    }

    public async Task<HomePage> ClickOnViewYourAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View your account" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<YourOrganisationsAndAgreementsPage> ClickOnReviewAndAcceptYourOtherAgreementsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "review and accept your other agreements" }).ClickAsync();

        return await VerifyPageAsync(() => new YourOrganisationsAndAgreementsPage(context));
    }
}
