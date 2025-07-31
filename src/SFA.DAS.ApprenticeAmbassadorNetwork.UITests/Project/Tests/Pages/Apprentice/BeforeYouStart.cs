namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Apprentice;

public class BeforeYouStartPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Join the Apprenticeship Ambassador Network as an apprentice");
    }

    public async Task<TermsAndConditionsPage> StartApprenticeOnboardingJourney()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Join Now" }).ClickAsync();

        return await VerifyPageAsync(() => new TermsAndConditionsPage(context));
    }
}

public class TermsAndConditionsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Terms and conditions");
    }

    public async Task<RequiresLineManagerApprovalPage> AcceptTermsAndConditions()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new RequiresLineManagerApprovalPage(context));
    }
}

public class RequiresLineManagerApprovalPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Do you have approval from your line manager to join the network?");
    }

    public async Task<FindYourRegionalNetworkPage> YesHaveApprovalFromMaanagerAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FindYourRegionalNetworkPage(context));
    }
    public async Task<ShutterPage> NoHaveApprovalFromMaanagerAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ShutterPage(context));
    }

    public async Task<AccessDeniedPage> YesHaveApprovalFromMaanagerAndNonPrivateBetaUser()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }
}

public class AccessDeniedPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You do not have access to this area of the website");
    }

    public async Task VerifyHomeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();
    }
}

public class FindYourRegionalNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Find your regional network");
    }

    public async Task<YourRegionalNetworkPage> SelectARegionAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "London" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourRegionalNetworkPage(context));
    }

    public async Task<CheckYourAnswersPage> AddOneMoreRegionAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "North East" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }
}

public class YourRegionalNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your regional network");
    }

    public async Task<CreateYourAmbassadorProfilePage> ContinueToAmbassadorProfilePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourAmbassadorProfilePage(context));
    }
}

public class CreateYourAmbassadorProfilePage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create your ambassador profile");
    }

    public async Task<SearchEmployerNamePage> ContinueToSearchEmployerNamePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchEmployerNamePage(context));
    }
}

public class SearchEmployerNamePage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Who do you work for?");
    }

    public async Task<EditEmployerDetailsPage> EnterPostcodeAndContinue()
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Search Term" }).ClickAsync();

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Search Term" }).FillAsync(aanDataHelpers.PostCode);

        await page.GetByRole(AriaRole.Option, new() { Name = aanDataHelpers.PostCode, Exact = false }).First.ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EditEmployerDetailsPage(context));
    }

    public async Task<EmployerDetailsPage> EnterAddressManually()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Enter address manually" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerDetailsPage(context));
    }

    public async Task<EditEmployerDetailsPage> EnterAddressManuallyEdit()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Enter address manually" }).ClickAsync();

        return await VerifyPageAsync(() => new EditEmployerDetailsPage(context));
    }
}

public class EditEmployerDetailsPage(ScenarioContext context) : EmployerDetailsPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check the details of your employer");
    }
}

public class EmployerDetailsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add the details of your employer");
    }

    public async Task<CurrentJobTitlePage> EnterEmployersDetailsAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer name" }).FillAsync(aanDataHelpers.VenueName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CurrentJobTitlePage(context));
    }

    public async Task<CurrentJobTitlePage> EnterFullEmployersDetailsAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer name" }).FillAsync(aanDataHelpers.VenueName);
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street address" }).FillAsync(aanDataHelpers.AddressLine1);
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Address Line" }).FillAsync(aanDataHelpers.AddressLine2);
        await page.GetByRole(AriaRole.Textbox, new() { Name = "County" }).FillAsync(aanDataHelpers.County);
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Town or city" }).FillAsync(aanDataHelpers.Town);
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync(aanDataHelpers.PostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CurrentJobTitlePage(context));
    }

    public async Task<CheckYourAnswersPage> ChangeVenueNameAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer name" }).FillAsync(aanDataHelpers.NewVenueName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }
}

public class CurrentJobTitlePage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your job title?");
    }

    public async Task<AreasOfInterestPage> ConfirmJobtitleAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Job Title" }).FillAsync(aanDataHelpers.JobTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AreasOfInterestPage(context));
    }

    public async Task<CheckYourAnswersPage> ChangeJobtitleAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Job Title" }).FillAsync(aanDataHelpers.NewJobTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }
}

public class AreasOfInterestPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How you want to be involved");
    }

    public async Task<WhyDoYouWantToJoinNetworkPage> SelectEventsAndPromotions()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Networking at in-person events" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Carrying out and writing up" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhyDoYouWantToJoinNetworkPage(context));
    }

    public async Task<CheckYourAnswersPage> AddMoreEventsAndPromotions()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Presenting at online events" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Distributing communications" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }
}

public class WhyDoYouWantToJoinNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Why do you want to join the network?");
    }

    public async Task<ReceiveNotificationsPage> EnterInformationToJoinNetwork()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Reasons for joining the" }).FillAsync(aanDataHelpers.UpdateProviderDescriptionText);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ReceiveNotificationsPage(context));
    }
}

public class ReceiveNotificationsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Do you want to get a monthly email about upcoming events?");
    }

    public async Task<EngagedWithAmbassadorPage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EngagedWithAmbassadorPage(context));
    }
}

public class EngagedWithAmbassadorPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Engagement with apprenticeship ambassadors");
    }

    public async Task<CheckYourAnswersPage> YesHaveEngagedWithAnAmbassadaorAndContinue()
    {

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }

    public async Task<CheckYourAnswersPage> NoHaveEngagedWithAnAmbassadaorAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }
}

public class CheckYourAnswersPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Register with the Apprenticeship Ambassador Network");
    }

    private static string ChangelinkEmployer => ("a[href='/onboarding/employer-search']");
    private static string ChangelinkJobtitle => ("a[href='/onboarding/current-job-title']");
    private static string ChangelinkAreasOfInterest => ("a[href='/onboarding/areas-of-interest']");
    private static string ChangelinkPreviousEngagement => ("a[href='/onboarding/previous-engagement']");

    public async Task<SearchEmployerNamePage> AccessChangeCurrentEmployerAndContinue()
    {
        await page.Locator(ChangelinkEmployer).ClickAsync();

        return await VerifyPageAsync(() => new SearchEmployerNamePage(context));
    }
    public async Task<CurrentJobTitlePage> AccessChangeCurrentJobTitleAndContinue()
    {
        await page.Locator(ChangelinkJobtitle).ClickAsync();

        return await VerifyPageAsync(() => new CurrentJobTitlePage(context));
    }
    public async Task<FindYourRegionalNetworkPage> AccessChangeCurrentRegionAndContinue()
    {
        await page.Locator("dl").Filter(new() { HasText = "Which region do you work in?" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new FindYourRegionalNetworkPage(context));
    }
    public async Task<AreasOfInterestPage> AccessChangeCurrentAreasOfInterestAndContinue()
    {
        await page.Locator(ChangelinkAreasOfInterest).ClickAsync();

        return await VerifyPageAsync(() => new AreasOfInterestPage(context));
    }
    public async Task<EngagedWithAmbassadorPage> AccessChangePreviousEngagementToNoAndContinue()
    {
        await page.Locator(ChangelinkPreviousEngagement).ClickAsync();


        return await VerifyPageAsync(() => new EngagedWithAmbassadorPage(context));
    }
    public async Task<RegistrationCompletePage> AcceptAndSubmitApplication()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new RegistrationCompletePage(context));
    }
}

public class RegistrationCompletePage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have joined the Apprenticeship Ambassador Network");
    }

    public async Task<Apprentice_NetworkHubPage> ContinueToAmbassadorHub()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Go to the online portal" }).ClickAsync();

        return await VerifyPageAsync(() => new Apprentice_NetworkHubPage(context));
    }

    public async Task SignOut() => await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();
}

public class ShutterPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Talk to your line manager before continuing");
    }

    public async Task VerifyApprenticePortalLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "continue to join the" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm my apprenticeship details");
    }

}