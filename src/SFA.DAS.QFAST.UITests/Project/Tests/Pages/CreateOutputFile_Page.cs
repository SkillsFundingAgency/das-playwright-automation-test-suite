namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;
public class CreateOutputFile_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download a record of all active and archived funding requests" })).ToBeVisibleAsync();
   

    public async Task VerifyErrorMessage(string message)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        var errorLocator = page.Locator($".govuk-error-message:has-text(\"{message}\")");
        await Assertions.Expect(errorLocator).ToBeVisibleAsync();
    }

    public async Task VerifyPresentDate()
    {
        var today = DateTime.UtcNow.ToString("dd MMMM yyyy");
        var expectedText = $"Yes, use {today}";
        var locator = page.Locator("label.govuk-radios__label[for='datechoice-today']");
        await Assertions.Expect(locator).ToHaveTextAsync(expectedText);
        await page.Locator("#datechoice-today").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();        
        await VerifyFileDowloadMessage();
    }
    public async Task EnterFuturPublicationDate()
    {
        var futureDate = DateTime.UtcNow.AddDays(15);
        await page.Locator("#datechoice-manual").ClickAsync();
        await page.Locator("#Day").FillAsync(futureDate.Day.ToString("D2"));
        await page.Locator("#Month").FillAsync(futureDate.Month.ToString("D2"));
        await page.Locator("#Year").FillAsync(futureDate.Year.ToString());
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        await VerifyFileDowloadMessage();
    }
    public async Task<CreateOutputFile_Page> VerifyFileDowloadMessage()
    {
        var filedownloadedMessage = page.Locator(".govuk-notification-banner__content").Locator("p.govuk-body");
        var expectedMessage = "Your file has been downloaded";
        await Assertions.Expect(filedownloadedMessage).ToHaveTextAsync(expectedMessage);
        return await VerifyPageAsync(() => new CreateOutputFile_Page(context));
    }
    public async Task ValidateDateErrorMessage()
    {
        await page.Locator("#datechoice-manual").ClickAsync();
        await page.Locator("#Day").FillAsync("20");
        await page.Locator("#Month").FillAsync("");
        await page.Locator("#Month").FillAsync("");
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        var monthYearError = page.Locator(".govuk-error-message:has-text(\"Publication date must include a month and year.\")");
        await Assertions.Expect(monthYearError).ToBeVisibleAsync();

        await page.Locator("#Day").FillAsync("");
        await page.Locator("#Month").FillAsync("12");
        await page.Locator("#Year").FillAsync("");
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        var dayYearError = page.Locator(".govuk-error-message:has-text(\"Publication date must include a day and year.\")");
        await Assertions.Expect(dayYearError).ToBeVisibleAsync();

        await page.Locator("#Day").FillAsync("");
        await page.Locator("#Month").FillAsync("");
        await page.Locator("#Year").FillAsync("2025");
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        var dayMonthError = page.Locator(".govuk-error-message:has-text(\"Publication date must include a day and month.\")");
        await Assertions.Expect(dayMonthError).ToBeVisibleAsync();

        await page.Locator("#Day").FillAsync("");
        await page.Locator("#Month").FillAsync("");
        await page.Locator("#Year").FillAsync("");
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        var fullDateError = page.Locator(".govuk-error-message:has-text(\"Enter a publication date\")");
        await Assertions.Expect(fullDateError).ToBeVisibleAsync();

        await page.Locator("#Day").FillAsync("32");
        await page.Locator("#Month").FillAsync("13");
        await page.Locator("#Year").FillAsync("2025");
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
        var invalidDateError = page.Locator(".govuk-error-message:has-text(\"Publication date must be a real date\")");
        await Assertions.Expect(invalidDateError).ToBeVisibleAsync();

        var previousDate = DateTime.Today.AddDays(-1);
        string formattedDate = previousDate.ToString("ddMMyyyy");
        await page.Locator("#Day").FillAsync(formattedDate.Substring(0, 2));
        await page.Locator("#Month").FillAsync(formattedDate.Substring(2, 2));
        await page.Locator("#Year").FillAsync(formattedDate.Substring(4, 4));
        await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();  
        var pastDateError = page.Locator(".govuk-error-message:has-text(\"Publication date must be today or in the future\")");
    }
}