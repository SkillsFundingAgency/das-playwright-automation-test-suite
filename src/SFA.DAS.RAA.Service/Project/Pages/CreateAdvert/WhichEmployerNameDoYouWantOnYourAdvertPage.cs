

namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class WhichEmployerNameDoYouWantOnYourAdvertPage(ScenarioContext context) : EmployerNameBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "What employer name do you want on your advert?" : "What employer name do you want on the vacancy?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }
}

public abstract class EmployerNameBasePage : RaaBasePage
{
    private string _employerName;

    private static string LegalEntityName => "label[for='legal-entity-name']";

    public EmployerNameBasePage(ScenarioContext context) : base(context) => _employerName = rAADataHelper.EmployerName;

    public async Task<ChooseApprenticeshipLocationPage> ChooseRegisteredName()
    {
        await SelectRadioOptionByForAttribute(RAAConst.LegalEntityName);

        var entityName = await page.Locator(LegalEntityName).TextContentAsync();

        SetEmployerName(EscapePatternHelper.StringEscapePattern(entityName, "(registered name)")?.Trim());

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseApprenticeshipLocationPage(context));
    }

    public async Task<ChooseApprenticeshipLocationPage> ChooseExistingTradingName()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "A different name" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Add a new trading name" }).FillAsync(_employerName);

        SetEmployerName(_employerName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseApprenticeshipLocationPage(context));
    }

    public async Task<ChooseApprenticeshipLocationPage> ChooseAnonymous()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "I do not want an employer" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Brief description of what the" }).FillAsync(rAADataHelper.EmployerDescription);

        SetEmployerName(rAADataHelper.EmployerDescription);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Reason for being anonymous" }).FillAsync(rAADataHelper.EmployerReason);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseApprenticeshipLocationPage(context));
    }

    public async Task<EmployerDescriptionPage> ChooseEmployerNameForEmployerJourney(string employername)
    {
        return employername switch
        {
            RAAConst.ExistingTradingName => await ChooseExistingTradingNameAndGotoEmployerDescriptionPage(),
            RAAConst.Anonymous => await ChooseAnonymousAndGotoEmployerDescriptionPage(),
            _ => await ChooseRegisteredNameAndGotoEmployerDescriptionPage()
        };
    }

    private async Task<EmployerDescriptionPage> ChooseRegisteredNameAndGotoEmployerDescriptionPage()
    {
        await SelectRadioOptionByForAttribute("legal-entity-name");

        await SetLegalEntityAsEmployerName();

        return await GoToEmployerDescriptionPage();
    }

    private async Task SetLegalEntityAsEmployerName()
    {
        var entityName = await page.Locator(LegalEntityName).TextContentAsync();

        _employerName = EscapePatternHelper.StringEscapePattern(entityName, "(registered name)")?.Trim();
    }

    private async Task<EmployerDescriptionPage> ChooseExistingTradingNameAndGotoEmployerDescriptionPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "A different name" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Add a new trading name" }).FillAsync(_employerName);

        return await GoToEmployerDescriptionPage();
    }

    private async Task<EmployerDescriptionPage> ChooseAnonymousAndGotoEmployerDescriptionPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "I do not want an employer" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Brief description of what the" }).FillAsync(rAADataHelper.EmployerDescription);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Reason for being anonymous" }).FillAsync(rAADataHelper.EmployerReason);

        await SetLegalEntityAsEmployerName();

        return await GoToEmployerDescriptionPage(rAADataHelper.EmployerDescription);
    }

    private async Task<EmployerDescriptionPage> GoToEmployerDescriptionPage() => await GoToEmployerDescriptionPage(_employerName);

    private async Task<EmployerDescriptionPage> GoToEmployerDescriptionPage(string employerNameAsShownInTheAdvert)
    {
        SetEmployerName(_employerName);

        objectContext.SetEmployerNameAsShownInTheAdvert(employerNameAsShownInTheAdvert);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerDescriptionPage(context));
    }

    private void SetEmployerName(string value) => objectContext.SetEmployerName(value);
}

public class EmployerDescriptionPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"Information about {objectContext.GetEmployerName()}";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ContactDetailsPage> EnterEmployerDescriptionAndGoToContactDetailsPage(bool _, bool optionalFields)
    {
        await IFrameFillAsync("EmployerDescription_ifr", rAADataHelper.EmployerDescription);

        if (optionalFields)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Website for organisation" }).FillAsync(rAADataHelper.EmployerWebsiteUrl);

            if (!isRaaEmployer) await page.GetByRole(AriaRole.Checkbox, new() { Name = "This employer has signed up" }).CheckAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Checkbox, new() { Name = "This employer has signed up" }).CheckAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ContactDetailsPage(context));
    }
}

public class ContactDetailsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? $"Contact details for {objectContext.GetEmployerName()} (optional)" : "Do you want to add your contact details?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationProcessPage> EnterProviderContactDetails(bool optionalFields)
    {
        await EnterProviderDetails(optionalFields);

        return await GoToApplicationProcessPage();
    }

    public async Task<ApplicationProcessPage> EnterContactDetailsAndGoToApplicationProcessPage(bool optionalFields)
    {
        if (optionalFields) await EnterContactDetails();

        return await GoToApplicationProcessPage();
    }

    private async Task EnterProviderDetails(bool optionalFields)
    {
        if (optionalFields)
        {
            await SelectRadioOptionByForAttribute("contact-details-yes");

            await EnterContactDetails();
        }
        else
        {
            await SelectRadioOptionByForAttribute("contact-details-no");
        }
    }

    private async Task EnterContactDetails()
    {
        await page.Locator(ContactName()).FillAsync(rAADataHelper.ContactName);

        await page.Locator(ContactEmail()).FillAsync(rAADataHelper.Email);

        await page.Locator(ContactPhone()).FillAsync(RAADataHelper.ContactNumber);
    }

    private async Task<ApplicationProcessPage> GoToApplicationProcessPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationProcessPage(context));
    }

    private string ContactName() => isRaaEmployer ? "#EmployerContactName" : "#ProviderContactName";
    private string ContactEmail() => isRaaEmployer ? "#EmployerContactEmail" : "#ProviderContactEmail";
    private string ContactPhone() => isRaaEmployer ? "#EmployerContactPhone" : "#ProviderContactPhone";
}

public class ApplicationProcessPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How would you like to receive applications?");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> SelectApplicationMethod_Employer(bool isFAA) { await SelectApplicationMethod(isFAA); return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context)); }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> SelectApplicationMethod_Provider(bool isFAA) { await SelectApplicationMethod(isFAA); return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context)); }

    private async Task ApplicationMethodFAA() => await page.GetByRole(AriaRole.Radio, new() { Name = "Through the Find an" }).CheckAsync();

    private async Task ApplicationMethodExternal()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Through a different website" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Application website link" }).FillAsync(rAADataHelper.EmployerWebsiteUrl);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "How to apply (optional)" }).FillAsync(rAADataHelper.OptionalMessage);
    }

    private async Task<PreviewYourAdvertOrVacancyPage> SaveAndContinueToPreviewPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    }

    private async Task SelectApplicationMethod(bool isFAA)
    {
        if (isFAA) await ApplicationMethodFAA(); else await ApplicationMethodExternal();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}