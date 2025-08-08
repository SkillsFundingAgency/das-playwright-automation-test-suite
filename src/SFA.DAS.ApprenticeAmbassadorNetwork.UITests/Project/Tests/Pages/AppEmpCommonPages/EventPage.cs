using System.Text.RegularExpressions;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class EventPage(ScenarioContext context) : AppEmpCommonBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToBeVisibleAsync();
    }

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