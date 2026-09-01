using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
public class ViewMangeRestrictedCoursesPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("View and manage restricted courses");
    } 
}
