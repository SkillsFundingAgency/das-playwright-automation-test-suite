using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class SetLastStartDatePage(ScenarioContext context) : AparAdminEditBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator(".govuk-caption-xl")).ToContainTextAsync("last start date");
    }

    public async Task ConfirmLastStartDate()
    {
        await page.Locator(".govuk-button").ClickAsync();
    }
}