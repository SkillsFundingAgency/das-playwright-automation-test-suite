using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class EmployerHubPage(ScenarioContext context) : EmployerBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Hire an apprentice");

    public async Task VerifySubHeadings() => await VerifyLinks();

    public async Task<UnderstandingApprenticeshipBenefitsFundingPage> NavigateToUnderstandingApprenticeshipBenefitsAndFunding()
    {
        await NavigateToEmployerCard("Understanding apprenticeship benefits and funding");
        return new UnderstandingApprenticeshipBenefitsFundingPage(context);
    }

    public async Task<SignUpPage> NavigateToSignUpPage()
    {
        await NavigateToEmployerCard("Sign up to emails");
        return await VerifyPageAsync(() => new SignUpPage(context));
    }
}