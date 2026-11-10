using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class AllowedCourseAddLastDatePage(ScenarioContext context) : AparAdminEditBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Stop this provider from delivering this apprenticeship");
    }
}