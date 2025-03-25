namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public partial class ProviderHomePage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public static string ProviderHomePageIdentifierCss => "#main-content .govuk-hint";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"UKPRN: {ukprn}", new() { Timeout = 300000 });
    }

    //protected static By AddNewApprenticesLink => By.LinkText("Add new apprentices");

    //protected static By AddAnEmployerLink => By.LinkText("Add an employer");

    //protected static By ViewEmployersAndManagePermissionsLink => By.LinkText("View employers and manage permissions");

    //protected static By ProviderManageYourApprenticesLink => By.LinkText("Manage your apprentices");

    //protected static By ProviderRecruitApprenticesLink => By.LinkText("Recruit apprentices");

    //protected static By DeveloperAPIsLink => By.LinkText("Developer APIs");

    //protected static By GetFundingLink => By.LinkText("Get funding for non-levy employers");

    //protected static By ManageYourFundingLink => By.LinkText("Manage your funding reserved for non-levy employers");

    //protected static By ManageEmployerInvitations => By.LinkText("Manage employer invitations");

    //protected static By InviteEmployers => By.LinkText("Send invitation to employer");

    //protected static By RecruitTrainees => By.LinkText("Recruit trainees");

    //protected static By AppsIndicativeEarningsReport => By.LinkText("Apps Indicative earnings report");

    //protected static By YourStandardsAndTrainingVenues => By.LinkText("Your standards and training venues");
    //protected static By YourFeedback => By.LinkText("Your feedback");

    //protected static By ViewEmployerRequestsForTraining => By.LinkText("View employer requests for training");

    public async Task ClickAddAnEmployerLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Add an employer" }).ClickAsync();

    public async Task ClickFundingLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Get funding for non-levy" }).ClickAsync();

    public async Task ClickViewEmployersAndManagePermissionsLink() => await page.GetByRole(AriaRole.Link, new() { Name = "View employers and manage" }).ClickAsync();

}
