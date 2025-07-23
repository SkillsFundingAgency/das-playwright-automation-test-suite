using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class ProfileSettingsPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Profile Settings");
    }

    //public YourAmbassadorProfilePage AccessYourAmbassadorProfile()
    //{
    //    formCompletionHelper.ClickLinkByText("Your ambassador profile");
    //    return new YourAmbassadorProfilePage(context);
    //}

    public async Task<LeavingTheNetworkPage> AccessLeaveTheNetwork()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Leave the network" }).ClickAsync();

        return await VerifyPageAsync(() => new LeavingTheNetworkPage(context));
    }

}


public class LeavingTheNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Leaving the network");
    }

    public async Task<ConfirmLeaveTheNetworkPage> CompleteFeedbackAboutLeavingAndContinue()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "I am unable to commit the" }).CheckAsync();
        await page.GetByRole(AriaRole.Radio, new() { Name = "Excellent" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Professional networking" }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ConfirmLeaveTheNetworkPage(context));
    }
}

public class ConfirmLeaveTheNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to leave?");
    }

    public async Task<ConfirmedLeftNetworkPage> ConfirmAndLeave()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Leave network" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmedLeftNetworkPage(context));
    }
}

public class ConfirmedLeftNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have successfully left the network");
    }

    public async Task<RejoinNetworkPage> AccessRestoreMember()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "restore your membership" }).ClickAsync();

        return await VerifyPageAsync(() => new RejoinNetworkPage(context));
    }
}

public class RejoinNetworkPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Would you like to rejoin the network?");
    }

    public async Task RestoreMember()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Restore membership" }).ClickAsync();
    }
}