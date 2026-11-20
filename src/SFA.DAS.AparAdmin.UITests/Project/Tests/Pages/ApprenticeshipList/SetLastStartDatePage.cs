using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class SetLastStartDatePage(ScenarioContext context) : AparAdminEditBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("The provider can deliver this course until the last start date");
    }
}