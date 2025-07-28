namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages;

public abstract class AanBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly AANDataHelpers aanDataHelpers = context.Get<AANDataHelpers>();

    //protected static By EventTag => By.CssSelector(".govuk-tag.app-tag");

    protected static string DateFormat => "yyyy-MM-dd";
}

