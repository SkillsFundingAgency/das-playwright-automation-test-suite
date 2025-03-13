using System.Text.RegularExpressions;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;


public enum AddApprenticePermissions
{
    [ToString("Yes, employer will review records")]
    YesAddApprenticeRecords,
    [ToString("No")]
    NoToAddApprenticeRecords
}

public enum RecruitApprenticePermissions
{
    [ToString("Yes")]
    YesRecruitApprentices,
    [ToString("Yes, employer will review adverts")]
    YesRecruitApprenticesButEmployerWillReview,
    [ToString("No")]
    NoToRecruitApprentices
}


public abstract class PermissionBasePageForEmployerPortalPage(ScenarioContext context) : EmployerPortalBasePage(context)
{

    public async Task<ManageTrainingProvidersPage> AddOrSetPermissions((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new ManageTrainingProvidersPage(context));
    }

    public async Task<EmployerAccountCreatedPage> AddOrSetPermissionsAndCreateAccount((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new EmployerAccountCreatedPage(context));
    }

    protected async Task SetAddApprentice(AddApprenticePermissions permission)
    {
        if (permission == AddApprenticePermissions.YesAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, but I want final review" }).CheckAsync();
        }

        if (permission == AddApprenticePermissions.NoToAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Group, new() { Name = "Add apprentice records" }).GetByLabel("No").CheckAsync();
        }
    }

    protected async Task SetRecruitApprentice(RecruitApprenticePermissions permission)
    {
        if (permission == RecruitApprenticePermissions.YesRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes", Exact = true }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, but I want to review" }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.NoToRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Group, new() { Name = "Recruit apprentices" }).GetByLabel("No").CheckAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex("Confirm|Send request") }).ClickAsync();
    }
}

public class ManageTrainingProvidersLinkHomePage(ScenarioContext context) : HomePage(context)
{
    public async Task<ManageTrainingProvidersPage> OpenRelationshipPermissions()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage training providers" }).ClickAsync();

        return new ManageTrainingProvidersPage(context);
    }
}


public class ManageTrainingProvidersPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage training providers");

    private static string ChangePermissionsLink(string ukprn) => $"a[href*='providers/{ukprn}/changePermissions?']";

    private static string NotificationBanner => ".govuk-notification-banner";

    public async Task VerifyYouHaveAddedNotification(string providerName)
    {
        await Assertions.Expect(page.Locator(NotificationBanner)).ToContainTextAsync($"You've added {providerName} and set their permissions.");
    }

    public async Task VerifyYouHaveDeclinedNotification(string providerName)
    {
        await Assertions.Expect(page.Locator(NotificationBanner)).ToContainTextAsync($"You've declined {providerName}’s permission request.");
    }

    public async Task VerifyYouHaveSetPermissionNotification(string providerName)
    {
        await Assertions.Expect(page.Locator(NotificationBanner)).ToContainTextAsync($"You've set {providerName}’s permissions.");
    }

    public async Task VerifyYouHaveSetPermissionNotification()
    {
        await Assertions.Expect(page.Locator(NotificationBanner)).ToContainTextAsync("You've set permissions for");
    }


    public async Task<AddAsATrainingProviderPage> ViewProviderRequests(ProviderConfig providerConfig, string requestId)
    {
        await OpenRequest(providerConfig, requestId);

        return new(context, providerConfig);
    }

    public async Task<ReviewPermissionsFromProviderPage> ReviewProviderRequests(ProviderConfig providerConfig, string requestId)
    {
        await OpenRequest(providerConfig, requestId);

        return new(context, providerConfig);
    }

    public async Task<EnterYourTrainingProviderNameReferenceNumberUKPRNPage> SelectAddATrainingProvider()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Add a training provider" }).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourTrainingProviderNameReferenceNumberUKPRNPage(context));
    }

    public async Task<SetPermissionsForTrainingProviderPage> SelectChangePermissions(string ukprn)
    {
        await page.Locator(ChangePermissionsLink(ukprn)).ClickAsync();

        return await VerifyPageAsync(() => new SetPermissionsForTrainingProviderPage(context));
    }

    public async Task<string> GetPermissions(ProviderConfig providerConfig)
    {
        return await page.GetByRole(AriaRole.Row, new() { Name = providerConfig.Name.ToUpperInvariant() }).TextContentAsync();
    }

    private async Task OpenRequest(ProviderConfig providerConfig, string requestId)
    {
        await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(providerConfig.Name.ToUpperInvariant());

        await page.Locator($"a[href*='{requestId}']").ClickAsync();
    }
}

public class SetPermissionsForTrainingProviderPage(ScenarioContext context) : PermissionBasePageForEmployerPortalPage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($" Set permissions");

}


public class AddAsATrainingProviderPage(ScenarioContext context, ProviderConfig providerConfig) : AddOrReviewRequestFromProvider(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Add {providerConfig.Name.ToUpperInvariant()} as a training provider");

}

public class ReviewPermissionsFromProviderPage(ScenarioContext context, ProviderConfig providerConfig) : AddOrReviewRequestFromProvider(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Review permissions request from {providerConfig.Name.ToUpperInvariant()}");

}

public abstract class AddOrReviewRequestFromProvider(ScenarioContext context) : PermissionBasePageForEmployerPortalPage(context)
{
    public async Task<ManageTrainingProvidersPage> AcceptProviderRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTrainingProvidersPage(context));
    }

    public async Task<AreYouSureYouDoNotWantToAddPage> DeclineAddRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new AreYouSureYouDoNotWantToAddPage(context));
    }

    public async Task<ManageTrainingProvidersPage> DeclinePermissionRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTrainingProvidersPage(context));
    }
}

public class AreYouSureYouDoNotWantToAddPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you do not want to add");

    public async Task<TrainingProvidertNotAddedPage> ConfirmDeclineRequest()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Do not add training provider" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingProvidertNotAddedPage(context));
    }
}

public class TrainingProvidertNotAddedPage(ScenarioContext context) : PermissionBasePageForEmployerPortalPage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("This training provider has not been added to your account");

}