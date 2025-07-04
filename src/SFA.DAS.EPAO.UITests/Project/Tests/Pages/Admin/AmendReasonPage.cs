using Azure;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

public class AmendReasonPage(ScenarioContext context) : ConfirmReasonBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Are you sure this certificate needs amending?");
}

public abstract class ConfirmReasonBasePage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public async Task<CheckAndSubmitAssessmentDetailsPage> EnterTicketRefeferenceAndSelectReason(string ticketReference, string reasonForReprint)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Enter the ticket reference" }).FillAsync(ticketReference);

        await page.GetByRole(AriaRole.Checkbox, new() { Name = reasonForReprint }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckAndSubmitAssessmentDetailsPage(context));
    }
}


public class CheckAndSubmitAssessmentDetailsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check and submit the assessment details");

    public async Task<CertificateSendToPage> ClickChangeSendToLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change certificate recevier" }).ClickAsync();

        return await VerifyPageAsync(() => new CertificateSendToPage(context));
    }

    public async Task<ConfirmationAmendPage> ClickConfirmAmend()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmationAmendPage(context));
    }

    public async Task<ConfirmationReprintPage> ClickConfirmReprint()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmationReprintPage(context));
    }

    public async Task VerifyRecipient(string recipient) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Recipient's Name {recipient}");

    public async Task VerifyEmployer(string employer) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Address {employer}");

    public async Task VerifyRecipientIsApprentice()
    {
        static string GetBySummaryValueLocator(string displayName) => ($"//dt[contains(text(),\"{displayName}\")]/following-sibling::dd[@class='govuk-summary-list__value']");

        var givenName = await page.Locator(GetBySummaryValueLocator("Given name")).TextContentAsync();

        var familyName = await page.Locator(GetBySummaryValueLocator("Family name")).TextContentAsync();

        var fullName = $"{givenName} {familyName}";

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Recipient's Name {fullName}");
    }
}

public class ConfirmationReprintPage(ScenarioContext context) : ConfirmationAmendReprintBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Reprint certificate");
}

public class ConfirmationAmendPage(ScenarioContext context) : ConfirmationAmendReprintBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Confirmation");
}

public abstract class ConfirmationAmendReprintBasePage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public async Task<SearchPage> ClickSearchAgain()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search Again" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchPage(context));
    }
}

public class CertificateSendToPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Who would you like us to send the certificate to?");

    public async Task<CertificateAddressPage> ChangeSendToRadioButtonAndContinue(string currentSendTo, string newSendTo)
    {
        switch (currentSendTo)
        {
            case "apprentice":
                await Assertions.Expect(page.GetByRole(AriaRole.Radio, new() { Name = "Apprentice" })).ToBeCheckedAsync();

                break;

            case "employer":
                await Assertions.Expect(page.GetByRole(AriaRole.Radio, new() { Name = "Employer" })).ToBeCheckedAsync();

                break;
        }

        switch (newSendTo)
        {
            case "apprentice":
                await page.GetByRole(AriaRole.Radio, new() { Name = "Apprentice" }).CheckAsync();
                break;

            case "employer":
                await page.GetByRole(AriaRole.Radio, new() { Name = "Employer" }).CheckAsync();
                break;
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CertificateAddressPage(context, "Add"));
    }
}

public class CertificateAddressPage(ScenarioContext context, string pageTitlePrefix) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync(PageTitle);

    protected string PageTitle => $"{pageTitlePrefix} the address that you'd like us to send the certificate to";

    public async Task EnterRecipientName(string recipientName)
    {
        await EnterText("Recipient's name", recipientName);
    }

    public async Task EnterEmployerName(string employer)
    {
        await EnterText("Employer name", employer);
    }

    private async Task EnterText(string textBoxName, string text) { if (!(string.IsNullOrEmpty(text))) await page.GetByRole(AriaRole.Textbox, new() { Name = textBoxName }).FillAsync(text); }

    public async Task EnterAddress()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street line 1 of" }).FillAsync(EPAODataHelper.AddressLine1);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street line 2 of" }).FillAsync(EPAODataHelper.AddressLine2);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street line 3 of" }).FillAsync(EPAODataHelper.AddressLine3);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Town or city" }).FillAsync(EPAODataHelper.TownName);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync(EPAODataHelper.PostCode);
    }

    public async Task<CheckAndSubmitAssessmentDetailsPage> EnterReasonForChangeAndContinue(string reasonForChange)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Reason for change" }).FillAsync(reasonForChange);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        
        return new(context);
    }
}