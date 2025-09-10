namespace SFA.DAS.FAA.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 32)]
    public void SetUpHelpers()
    {
        bool isCloneVacancy = context.ScenarioInfo.Tags.Contains("clonevacancy");


        context.Set(new VacancyTitleDatahelper(isCloneVacancy));

        var mailosaurUser = context.Get<MailosaurUser>();

        var mailosaurEmaildomain = mailosaurUser.DomainName;

        context.Set(new FAAUserNameDataHelper(mailosaurEmaildomain));

        context.Set(new FAADataHelper());

        context.Set(new VacancyReferenceHelper(_objectContext));

        if (context.ScenarioInfo.Tags.Contains("faaapplytestdataprep")) context.Set(new AdvertDataHelper());
    }
}
