using SFA.DAS.QFAST.UITests.Project.Helpers;
using System.IO;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;
public class CreateOutputFile_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download a record of all active and archived funding requests" })).ToBeVisibleAsync();
    protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

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
    }
    public async Task EnterFuturPublicationDate()
    {
        var futureDate = DateTime.UtcNow.AddDays(15);
        await page.Locator("#datechoice-manual").ClickAsync();
        await page.Locator("#Day").FillAsync(futureDate.Day.ToString("D2"));
        await page.Locator("#Month").FillAsync(futureDate.Month.ToString("D2"));
        await page.Locator("#Year").FillAsync(futureDate.Year.ToString());
    }
    public async Task VerifyFileDownload()
    {
        var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.FullName;
        var downloadsDir = Path.Combine(projectDir, "Downloads");
        Directory.CreateDirectory(downloadsDir);
        try
        {
            var download = await page.RunAndWaitForDownloadAsync(async () =>
            {
                await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
            });
            var datePrefix = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var filename = $"{datePrefix}-AOdPOutputFile";
            var filePath = Path.Combine(downloadsDir, filename);
            await download.SaveAsAsync(filePath);
            if (!File.Exists(filePath))
            {
                throw new IOException($"File not downloaded: {filePath}");
            }
        }
        finally
        {
            {
                const int maxRetries = 3;
                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    try
                    {
                        if (Directory.Exists(downloadsDir))
                        {
                            Directory.Delete(downloadsDir, true);
                        }
                        break;
                    }
                    catch (IOException)
                    {
                        await Task.Delay(100);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        await Task.Delay(100);
                    }
                    catch
                    {
                        break;
                    }
                }
            }

        }
    }
    public async Task VerifyFileDownloadForFuturePublicationDate()
    {
        var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.FullName;
        var downloadsDir = Path.Combine(projectDir, "Downloads");
        Directory.CreateDirectory(downloadsDir);
        try
        {
            var download = await page.RunAndWaitForDownloadAsync(async () =>
            {
                await page.GetByRole(AriaRole.Button, new() { Name = "Download files" }).ClickAsync();
            });
            var datePrefix = DateTime.UtcNow.AddDays(15).ToString("yyyy-MM-dd");
            var filename = $"{datePrefix}-AOdPOutputFile";
            var filePath = Path.Combine(downloadsDir, filename);
            await download.SaveAsAsync(filePath);
            if (!File.Exists(filePath))
            {
                throw new IOException($"File not downloaded: {filePath}");
            }
        }
        finally
        {
            {
                const int maxRetries = 3;
                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    try
                    {
                        if (Directory.Exists(downloadsDir))
                        {
                            Directory.Delete(downloadsDir, true);
                        }
                        break;
                    }
                    catch (IOException)
                    {
                        await Task.Delay(100);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        await Task.Delay(100);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
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