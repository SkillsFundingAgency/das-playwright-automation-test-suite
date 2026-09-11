using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class AcademicProfessionalPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Academic professional (Level 7)");
    }

    public async Task ClickChange(string UKPRN)
    {
        var link = "/restricted-courses/272/providers/";
        var lastPartOfLink = "/set-last-start-date";
        var fullLink = $"{link}{UKPRN}{lastPartOfLink}";
        await  page.Locator($"a[href='{fullLink}']").ClickAsync();
    }

    public async Task VerifyDateUpdate(string UKPRN, string date)
    {
        await Assertions.Expect(page.Locator("#govuk-notification-banner-title")).ToContainTextAsync("Success");
        var child = page.Locator("dd", new PageLocatorOptions { HasTextString = UKPRN });
        var listItem = child.Locator("../..");
        var dateDisplay = listItem.Locator("das-definition-list das-definition-list--meta govuk-!-margin-bottom-0");
        await Assertions.Expect(dateDisplay.Locator(".das-definition-list__definition")).ToContainTextAsync(date);
    }

    public async Task AssertBanner(string UKPRN, string bannerMessage)
    {
        var child = page.Locator("dd", new PageLocatorOptions { HasTextString = UKPRN });
        var listItem = child.Locator("../..");
        var banner = listItem.Locator(".govuk-tag app-tag");
        await Assertions.Expect(banner).ToContainTextAsync(bannerMessage);   
    }
}