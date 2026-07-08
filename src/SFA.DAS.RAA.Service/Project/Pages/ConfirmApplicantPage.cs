using Allure.Net.Commons;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class ConfirmApplicantPage(ScenarioContext context, string status) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEpc ? "You want to interview applicant" : $"{rAADataHelper.CandidateFullName}'s application status changed to '{status}'.";

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }

    public class ProviderInteviewingApplicantPage(ScenarioContext context) : ConfirmApplicantPage(context, "interviewing")
    {
    }

    public class ProviderAndEmployerReviewingApplicantPage(ScenarioContext context) : ConfirmApplicantPage(context, "in review")
    {
    }

    public class EmployerInteviewingApplicantPage(ScenarioContext context) : ConfirmApplicantPage(context, "interviewing")
    {
    }
}

public class ConfirmEmployerRejectedSharedAppPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = "Application made unsuccessful";

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}
