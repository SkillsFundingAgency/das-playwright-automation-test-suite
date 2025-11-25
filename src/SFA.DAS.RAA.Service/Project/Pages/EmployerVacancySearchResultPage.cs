//namespace SFA.DAS.RAA.Service.Project.Pages;

//public abstract class VacancySearchResultPage(ScenarioContext context) : RaaBasePage(context)
//{
//    protected static By Filter => By.CssSelector("#Filter");
//    private static By SearchInput => By.CssSelector("input#search-input");
//    protected static By VacancyStatusSelector => By.CssSelector("[data-label='Status']");

//    protected static By VacancyActionSelector => By.CssSelector("[id^='manage']");
//    protected static By RejectedVacancyActionSelector => By.CssSelector("[data-label='Action']");
//    private static By SearchButton => By.CssSelector(".govuk-button.das-search-form__button");

//    public void VerifyAdvertStatus(string expected)
//    {
//        VerifyElement(() => tableRowHelper.GetColumn(vacancyTitleDataHelper.VacancyTitle, VacancyStatusSelector), expected, () => new SearchVacancyPageHelper(context).SearchVacancy());
//    }

//    protected void DraftVacancy()
//    {
//        formCompletionHelper.SelectFromDropDownByValue(Filter, "Draft");
//        pageInteractionHelper.WaitforURLToChange($"Filter=Draft");
//        formCompletionHelper.EnterText(SearchInput, vacancyTitleDataHelper.VacancyTitle);
//        formCompletionHelper.Click(SearchButton);
//        tableRowHelper.SelectRowFromTable("Edit and submit", vacancyTitleDataHelper.VacancyTitle);
//    }

//    public VacancyCompletedAllSectionsPage GoToVacancyCompletedPage()
//    {
//        formCompletionHelper.ClickElement(VacancyActionSelector);

//        return new VacancyCompletedAllSectionsPage(context);
//    }
//    public VacancyCompletedAllSectionsPage GoToRejectedVacancyCompletedPage()
//    {
//        formCompletionHelper.ClickElement(RejectedVacancyActionSelector);

//        return new VacancyCompletedAllSectionsPage(context);
//    }
//    public ManageRecruitPage GoToVacancyManagePage()
//    {
//        formCompletionHelper.ClickElement(VacancyActionSelector);

//        return new ManageRecruitPage(context);
//    }
//}

//public class EmployerVacancySearchResultPage(ScenarioContext context) : VacancySearchResultPage(context)
//{
//    protected override string PageTitle => "Your adverts";

//    protected override By PageHeader => By.CssSelector(".govuk-heading-xl");
//    private static By Applicant => By.CssSelector("a[data-label='application_review']");
//    private static By ApplicantStatus => By.CssSelector("td[data-label='Status'] > strong");
//    public CreateAnApprenticeshipAdvertOrVacancyPage CreateAnApprenticeshipAdvertPage()
//    {
//        DraftVacancy();
//        return new CreateAnApprenticeshipAdvertOrVacancyPage(context);
//    }

//    public ManageApplicantPage NavigateToManageApplicant()
//    {
//        GoToVacancyManagePage();
//        if (IsFoundationAdvert)
//        {
//            CheckFoundationTag();
//        }
//        formCompletionHelper.Click(Applicant);
//        return new ManageApplicantPage(context);
//    }
//    public void CheckApplicantStatus(string status)
//    {
//        GoToVacancyManagePage();
//        if (IsFoundationAdvert)
//        {
//            CheckFoundationTag();
//        }
//        pageInteractionHelper.CheckText(ApplicantStatus, status);
//    }
//    public ViewVacancyPage NavigateToViewAdvertPage()
//    {
//        GoToVacancyManagePage();
//        string linkTest = isRaaEmployer ? "View advert" : "View vacancy";
//        formCompletionHelper.ClickLinkByText(linkTest);

//        return new ViewVacancyPage(context);
//    }
//}