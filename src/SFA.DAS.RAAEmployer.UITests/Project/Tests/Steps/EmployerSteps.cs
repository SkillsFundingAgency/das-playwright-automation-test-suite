using SFA.DAS.RAAEmployer.UITests.Project.Helpers;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerSteps(ScenarioContext context)
{
    private readonly EmployerStepsHelper _employerStepsHelper = new(context);

    [Then(@"Employer can mark applicant as In Review")]
    public async Task ThenEmployerCanMarkApplicantAsInReview() => await _employerStepsHelper.ApplicantReview();

    [Then(@"Employer can mark the application as interviewing")]
    public async Task ThenEmployerCanMarkTheApplicationAsInterviewing() => await _employerStepsHelper.ApplicantInterviewing();

    [Then(@"Employer can make the application successful")]
    public async Task ThenEmployerCanMakeTheApplicationSuccessful() => await _employerStepsHelper.ApplicantSucessful();

    [Then(@"Employer can make the application unsuccessful")]
    public async Task ThenEmployerCanMakeTheApplicationUnsuccessful() => await _employerStepsHelper.ApplicantUnsucessful();

    [Then(@"Employer can see the withdrawn application")]
    public async Task ThenEmployerCanSeeTheWithdrawnApplication() => await _employerStepsHelper.ApplicantWithdrawn();

    [Then(@"the Employer can close the vacancy")]
    public async Task ThenTheEmployerCanCloseTheVacancy() => await _employerStepsHelper.CloseVacancy();

    [Then(@"the Employer can edit the vacancy")]
    public async Task ThenTheEmployerCanEditTheVacancy() => await _employerStepsHelper.EditVacancyDates();

    [Then(@"the Employer verify '(National Minimum Wage For Apprentices|National Minimum Wage|Fixed Wage Type|Set As Competitive)' the wage option selected in the Preview page")]
    public async Task ThenTheEmployerVerifyTheWageOptionSelectedInThePreviewPage(string wageType) => await _employerStepsHelper.VerifyWageType(wageType);
}
