using System;
using System.Text.RegularExpressions;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class EventPage(ScenarioContext context) : AppEmpCommonBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToBeVisibleAsync();
    }

    private static string InPersonEventTag => ("//strong[contains(@class,'govuk-tag app-tag app-tag--InPerson')]");
    private static string OnlineEventTag => ("//strong[contains(@class,'govuk-tag app-tag app-tag--Online')]");
    private static string HybridEventTag => ("//strong[contains(@class,'govuk-tag app-tag app-tag--Hybrid')]");

    public async Task GoToEvent((string id, DateTime startdate) eventLink)
    {
        await GoToId(eventLink.id);
    }

    public async Task<SignUpConfirmationPage> SignupForEvent()
    {
        await page.Locator("button").Filter(new() { HasTextRegex = new Regex("^Sign up$") }).ClickAsync();

        return await VerifyPageAsync(() => new SignUpConfirmationPage(context));
    }

    public async Task<SignUpCancelledPage> CancelYourAttendance()
    {
        await page.Locator("button").Filter(new() { HasTextRegex = new Regex("^Cancel your attendance$") }).ClickAsync();

        return await VerifyPageAsync(() => new SignUpCancelledPage(context));
    }

    public async Task VerifyInPersonEventTag()
    {
        await Assertions.Expect(page.Locator(InPersonEventTag)).ToBeVisibleAsync();
    }

    public async Task VerifyOnlineEventTag()
    {
        await Assertions.Expect(page.Locator(OnlineEventTag)).ToBeVisibleAsync();
    }

    public async Task VerifyHybridEventTag()
    {
        await Assertions.Expect(page.Locator(HybridEventTag)).ToBeVisibleAsync();
    }
}

public class SignUpConfirmationPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have successfully signed up to this event");
    }

    public async Task<EventsHubPage> AccessEventsHub()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Events hub" }).ClickAsync();

        return await VerifyPageAsync(() => new EventsHubPage(context));
    }
}
public class SignUpCancelledPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have successfully cancelled your attendance at this event");
    }

    public async Task<EventsHubPage> AccessEventsHubFromCancelledAttendancePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Events hub" }).ClickAsync();

        return await VerifyPageAsync(() => new EventsHubPage(context));
    }
}