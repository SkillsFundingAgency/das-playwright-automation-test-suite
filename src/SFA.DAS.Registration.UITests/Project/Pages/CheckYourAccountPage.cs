namespace SFA.DAS.Registration.UITests.Project.Pages;

public class CheckYourAccountPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => YourAccountsPage.PageTitle;

    protected override ILocator PageLocator => new YourAccountsPage(context).PageIdentifier;
}
