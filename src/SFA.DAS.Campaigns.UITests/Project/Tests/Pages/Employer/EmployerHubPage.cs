namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class EmployerHubPage(ScenarioContext context) : HubBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Hire an apprentice");


    public async Task VerifySubHeadings() => await VerifyFiuCards(() => NavigateToEmployerHubPage());

    public async Task<UnderstandingApprenticeshipBenefitsFundingPage> NavigateToUnderstandingApprenticeshipBenefitsAndFunding()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Understanding apprenticeship" }).ClickAsync();

        return new UnderstandingApprenticeshipBenefitsFundingPage(context);
    }

    public async Task<SignUpPage> NavigateToSignUpPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign up to emails" }).ClickAsync();

        return await VerifyPageAsync(() => new SignUpPage(context));
    }
}
