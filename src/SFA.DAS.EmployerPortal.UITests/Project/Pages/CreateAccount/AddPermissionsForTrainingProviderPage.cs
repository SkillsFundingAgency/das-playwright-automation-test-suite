namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class AddPermissionsForTrainingProviderPage(ScenarioContext context, ProviderConfig providerConfig) : PermissionBasePageForEmployerPortalPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"Add {providerConfig.Name.ToUpperInvariant()} and set permissions");
    }

    public async Task VerifyDoNotAllowPermissions()
    {
        await SetAddApprentice(AddApprenticePermissions.NoToAddApprenticeRecords);

        await SetRecruitApprentice(RecruitApprenticePermissions.NoToRecruitApprentices);

        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Error: You must select yes for at least one permission for add apprentice records or recruit apprentices");
    }
}
