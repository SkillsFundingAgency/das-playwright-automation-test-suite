namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class RaaBasePage : BasePage
{
    protected readonly VacancyTitleDatahelper vacancyTitleDataHelper;

    protected readonly RAADataHelper rAADataHelper;

    protected readonly AdvertDataHelper advertDataHelper;
    protected readonly bool isRaaEmployer;

    //protected override By ContinueButton => By.CssSelector(".save-button");

    //protected override By PageHeader => By.CssSelector($"{PageHeaderSelector}, .govuk-label--xl");

    //protected virtual By SaveAndContinueButton => By.ClassName("govuk-button");

    //protected static By MultipleCandidateFeedback => By.CssSelector("#provider-multiple-candidate-feedback");
    //protected static By CandidateFeedback => By.CssSelector("#CandidateFeedback");

    public RaaBasePage(ScenarioContext context) : base(context)
    {
        isRaaEmployer = tags.Contains("raaemployer");

        vacancyTitleDataHelper = context.GetValue<VacancyTitleDatahelper>();

        rAADataHelper = context.GetValue<RAADataHelper>();

        advertDataHelper = context.GetValue<AdvertDataHelper>();
    }
    
    protected bool IsFoundationAdvert => context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];
    
    public async Task CheckFoundationTag()
    {
        await Assertions.Expect(page.Locator(".govuk-tag--pink")).ToContainTextAsync("Foundation");
    }

    //protected virtual void SaveAndContinue() => formCompletionHelper.ClickButtonByText(SaveAndContinueButton, "Save and continue");

    //protected void VerifyPanelTitle(string text) => pageInteractionHelper.VerifyText(PanelTitle, text);

    protected async Task SelectRadioOptionByForAttribute(string value)
    {
        await page.Locator($"label[for='{value}']").ClickAsync();
    }

    protected async Task IFrameFillAsync(string locatoriD, string text)
    {
        ILocator locator = page.Locator($"iframe[id='{locatoriD}']");

        await Assertions.Expect(locator).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 10000});

        await locator.ContentFrame.Locator(".mce-content-body").FillAsync(text);
    }

    public async Task EmployerCancelAdvert() => await page.GetByRole(AriaRole.Link, new() { Name = "Cancel" }).ClickAsync();
}
