

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Employer.Onboarding;

public class EmployerAmbassadorApplicationPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Join the Apprentice Ambassador Network as an employer");
    }

    public async Task<TermsAndConditionsOfEmployerPage> StartEmployerAmbassadorApplication()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Join Now" }).ClickAsync();

        return await VerifyPageAsync(() => new TermsAndConditionsOfEmployerPage(context));
    }
}

public class TermsAndConditionsOfEmployerPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Terms and conditions");
    }

    public async Task<FindYourRegionalNetworkPage> AcceptTermsAndConditions()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FindYourRegionalNetworkPage(context));
    }
}

public class FindYourRegionalNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Find your regional network");
    }

    public async Task<YourRegionalNetworkPage> SelectNorthEastRegion_Continue()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "North East" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourRegionalNetworkPage(context));
    }
}

public class YourRegionalNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your regional network");
    }

    public async Task<CreateYourAmbassadorProfilePage> ConfirmRegionalNetwork()
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

    public async Task<HowYouWantToBeInvolvedPage> ConfirmYourAmbassadorProfile()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HowYouWantToBeInvolvedPage(context));
    }
}

public class HowYouWantToBeInvolvedPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How you want to be involved");
    }

    public async Task<MonthlyEmailPage> SelectHowYouWantToBeInvolved()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Share your knowledge," }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Increasing engagement with" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new MonthlyEmailPage(context));
    }

    public async Task<RegistrationConfirmationPage> Add1MoreSelection()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Assist with the delivery of" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Getting started with" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new RegistrationConfirmationPage(context));
    }
}

public class MonthlyEmailPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Do you want to receive a monthly email about upcoming events?");
    }

    public async Task<EventTypesPage> SelectReceiveNotifications()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EventTypesPage(context));
    }
}

public class EventTypesPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Which types of events do you want to be emailed about?");
    }

    public async Task<LocationsPage> SelectEventTypes()
    {
        await page.Locator("#eventType-0").CheckAsync();
        //await page.Locator("#eventType-1").CheckAsync();
        //await page.Locator("#eventType-2").CheckAsync();
        //await page.Locator("#all-checkbox").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new LocationsPage(context));
    }
}

public class LocationsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add locations for in-person events");
    }

    public async Task<EngagementWithApprenticeshipAmbassadorsPage> AddLocation_And_Continue()
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "City, town or postcode" }).FillAsync("Tamarside, Devon");

        await page.GetByRole(AriaRole.Button, new() { Name = "Add location" }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EngagementWithApprenticeshipAmbassadorsPage(context));
    }
}

public class EngagementWithApprenticeshipAmbassadorsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Engagement with apprenticeship ambassadors");
    }
    
    public async Task<RegistrationConfirmationPage> ConfirmEngagement()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new RegistrationConfirmationPage(context));
    }
}

public class RegistrationConfirmationPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Register with the Apprenticeship Ambassador Network");
    }

    public async Task<FindYourRegionalNetworkPage> ChangeRegions()
    {
        await page.Locator("dl").Filter(new() { HasText = "Which regions does your" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new FindYourRegionalNetworkPage(context));
    }

    public async Task<HowYouWantToBeInvolvedPage> ChangeWhatCanYouOffer()
    {
        await page.Locator("dl").Filter(new() { HasText = "What can you offer other" }).GetByRole(AriaRole.Link).First.ClickAsync();

        return await VerifyPageAsync(() => new HowYouWantToBeInvolvedPage(context));
    }

    public async Task<RegistrationComplete_EmployerPage> SubmitApplication()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new RegistrationComplete_EmployerPage(context));
    }
}

public class RegistrationComplete_EmployerPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have joined the Apprenticeship Ambassador Network");
    }

    public async Task<Employer_NetworkHubPage> ContinueToAmbassadorHub()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Go to the online portal" }).ClickAsync();

        return await VerifyPageAsync(() => new Employer_NetworkHubPage(context));
    }
}