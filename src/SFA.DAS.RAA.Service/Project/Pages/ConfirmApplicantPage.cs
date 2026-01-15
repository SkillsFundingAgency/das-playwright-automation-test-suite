namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class ConfirmApplicantPage(ScenarioContext context, string status) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"{rAADataHelper.CandidateFullName}'s application status changed to '{status}'.";

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
