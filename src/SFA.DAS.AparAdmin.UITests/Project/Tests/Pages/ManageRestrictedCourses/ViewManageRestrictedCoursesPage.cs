using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class ViewMangeRestrictedCoursesPage(ScenarioContext context)
    : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View and manage restricted courses");
    }


    public async Task SelectRestrictedManageProviders(string larsCode)
    {
        var link = "/restricted-courses/";
        var fullLink = $"{link}{larsCode}";
        await  page.Locator($"a[href='{fullLink}']").ClickAsync();
    }
}