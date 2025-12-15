namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAAApplySteps(ScenarioContext context)
{
    private readonly FAAStepsHelper _faaStepsHelper = new(context);

    protected bool IsFoundationAdvert => context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];

    [When(@"the Applicant can apply for a Vacancy in FAA")]
    [Then(@"the Applicant can apply for a Vacancy in FAA")]
    public async Task TheApplicantCanApplyForAVacancyInFAA()
    {
        FAAPortalUser user = IsFoundationAdvert ? context.GetUser<FAAFoundationUser>() : context.GetUser<FAAApplyUser>();

        var page = await _faaStepsHelper.ApplyForAVacancy("both", user, false);

        var page1 = await page.PreviewApplication();

        await page1.SubmitApplication();
    }

    [When(@"the Applicant can apply for a Vacancy with multiple locations in FAA")]
    [Then(@"the Applicant can apply for a Vacancy with multiple locations in FAA")]
    public async Task TheApplicantCanApplyForAVacancyWithMultipleLocationsInFAA()
    {
        var user = context.GetUser<FAAApplyUser>();

        var page = await _faaStepsHelper.ApplyForAVacancy("both", user, true);

        var page1 = await page.PreviewApplication();

        await page1.SubmitApplication();
    }

    [When(@"the Applicant can apply for a foundation vacancy in FAA")]
    [Then(@"the Applicant can apply for a foundation vacancy in FAA")]
    public async Task TheApplicantCanApplyForAFoundationVacancyInFAA()
    {
        var user = context.GetUser<FAAFoundationUser>();

        var page = await _faaStepsHelper.ApplyForAVacancy("both", user, false);

        var page1 = await page.PreviewApplication();

        await page1.SubmitApplication();
    }

    //[When(@"the ineligible applicant can not apply for a foundation vacancy in FAA")]
    //[Then(@"the ineligible applicant can not apply for a foundation vacancy in FAA")]
    //public async Task TheIneligibleApplicantCanNotApplyForAFoundationVacancyInFAA()
    //{
    //    var user = context.GetUser<FAAApplyUser>();
    //    _faaStepsHelper.IneligibleUserApplyForAVacancy(user);
    //}

    [When("the apprentice has submitted their first application")]
    public async Task GivenTheApprenticeHasSubmittedTheirFirstApplication()
    {
        var page = await _faaStepsHelper.ApplyForAVacancyWithNewAccount(true, true, true, true, true, true);

        var page1 = await page.PreviewApplication();
        
        await page1.SubmitApplication();
    }

    [When(@"the Applicant can apply for a Vacancy in FAA with ""(.*)"" additional questions")]
    public async Task TheApplicantCanApplyForAVacancyInFAA(string numberOfQuestions)
    {
        var user = context.GetUser<FAAApplyUser>();

        var page = await _faaStepsHelper.ApplyForAVacancy(numberOfQuestions, user, false);

        var page1 = await page.PreviewApplication();

        await page1.SubmitApplication();
    }

    [When(@"the Applicant can apply for a multiple locations Vacancy in FAA with ""(.*)"" additional questions")]
    public async Task TheApplicantCanApplyForAVacancyWithMultipleLocationsInFAA(string numberOfQuestions)
    {
        var user = context.GetUser<FAAApplyUser>();

        var page = await _faaStepsHelper.ApplyForAVacancy(numberOfQuestions, user, true);

        var page1 = await page.PreviewApplication();

        await page1.SubmitApplication();
    }

    [When(@"multiple Applicants can apply for a Vacancy in FAA")]
    public async Task MultipleApplicantsCanApplyForAVacancyInFAA()
    {
        var user = context.GetUser<FAAApplyUser>();

        var page = await _faaStepsHelper.ApplyForAVacancy("both", user, false);

        var page1 = await page.PreviewApplication();

        var page2 = await page1.SubmitApplication();

        await page2.ClickSignOut();

        var user1 = context.GetUser<FAAApplySecondUser>();

        var page3 = await _faaStepsHelper.ApplyForAVacancy("both", user1, false);

        var page4 = await page3.PreviewApplication();

        await page4.SubmitApplication();
    }
}
