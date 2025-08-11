namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public class ManageAmbassadorsPage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage ambassadors");
    }

    private static string MemberLink => ("Alan Burns");

    public async Task<MemberProfilePage> AcessMember()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = MemberLink }).ClickAsync();

        return await VerifyPageAsync(() => new MemberProfilePage(context));
    }
    public new async Task FilterAmbassadorByStatus_New()
    {
        await base.FilterAmbassadorByStatus_New();
    }

    public new async Task FilterEventByAmbassadorStatus_Active()
    {
        await base.FilterEventByAmbassadorStatus_Active();
    }

    public new async Task FilterEventByEventRegion_London()
    {
        await base.FilterEventByEventRegion_London();
    }

    public new async Task ClearAllFilters()
    {
        await base.ClearAllFilters();
    }

    public new async Task VerifyAMbassadorStatus_Published_New()
    {
        await base.VerifyAMbassadorStatus_Published_New();
    }

    public new async Task VerifyAMbassadorStatus_Published_Active()
    {
        await base.VerifyAMbassadorStatus_Published_Active();
    }

    public new async Task VerifyEventRegion_London_Filter()
    {
        await base.VerifyEventRegion_London_Filter();
    }
}


public class MemberProfilePage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Alan Burns");
    }

    public async Task<RemoveAmbassadorPage> RemoveAmbassador()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove ambassador" }).ClickAsync();

        return await VerifyPageAsync(() => new RemoveAmbassadorPage(context));

    }
}

public class RemoveAmbassadorPage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Remove ambassador");
    }
    
    public async Task<MemberProfilePage> SelectReasonsToRemoveAndCancelRemoval()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Breach of code of conduct" }).CheckAsync();

        await page.GetByRole(AriaRole.Radio, new() { Name = "Inactivity" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Cancel" }).ClickAsync();

        return await VerifyPageAsync(() => new MemberProfilePage(context));
    }
}