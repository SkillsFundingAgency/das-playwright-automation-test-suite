namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

public class ManageYourEmailsEmployerPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = "Manage your advert notifications";
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ManageYourEmailsEmployerPage> SelectAndSaveEmailPreferences()
    {
        await SelectRadioOptionByForAttribute("approved-rejected-mine");
        await SelectRadioOptionByForAttribute("applications-mine");
        await SelectRadioOptionByForAttribute("notify-now");

        var saveButton = page.GetByRole(AriaRole.Button, new() { Name = "Save settings" });
        await saveButton.ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        return this;
    }

    public async Task<ManageYourEmailsEmployerPage> VerifyEmailSettingsConfirmationBanner()
    {
        var expectedBannerTitle = "Success";
        var expectedBannerHeading = "Advert notification settings saved.";

        var titleLocator = page.GetByRole(AriaRole.Heading, new() { Name = expectedBannerTitle });
        await Assertions.Expect(titleLocator).ToBeVisibleAsync();

        await Assertions.Expect(page.Locator(".govuk-notification-banner__heading")).ToContainTextAsync(expectedBannerHeading);

        return this;
    }
}
