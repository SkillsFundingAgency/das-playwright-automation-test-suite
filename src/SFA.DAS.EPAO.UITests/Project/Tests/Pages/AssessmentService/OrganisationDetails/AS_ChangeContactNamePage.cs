namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

public class AS_ChangeContactNamePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change contact name");

    #region Locators
    private static string PrimaryContactNameRadioButton => "//label[contains(text(),'Mr Preprod Epao0007')]/../input";
    private static string SecondaryContactNameRadioButton => "//label[contains(text(),'Liz Kemp')]/../input";

    #endregion

    public async Task<AS_ConfirmContactNamePage> SelectContactNameRadioButtonAndClickSave()
    {
        bool isSelected = await page.Locator(PrimaryContactNameRadioButton).IsCheckedAsync();

        var radioButtonToClick = isSelected ? SecondaryContactNameRadioButton : PrimaryContactNameRadioButton;

        await page.Locator(radioButtonToClick).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmContactNamePage(context));
    }
}

public class AS_ConfirmContactNamePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm contact name");

    public async Task<AS_ContactNameUpdatedPage> ClickConfirmButtonInConfirmContactNamePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ContactNameUpdatedPage(context));
    }
}

public class AS_ContactNameUpdatedPage(ScenarioContext context) : AS_ChangeOrgDetailsBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Contact name updated");
}
