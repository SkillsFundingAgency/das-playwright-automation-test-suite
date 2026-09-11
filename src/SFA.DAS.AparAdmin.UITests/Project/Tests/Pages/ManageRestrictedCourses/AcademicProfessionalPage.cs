using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class AcademicProfessionalPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Academic professional (Level 7)");
    }

    public async Task ClickChange(string firstTime, string UKPRN)
    {
        var link = "/restricted-courses/272/providers/";
        string lastPartOfLink = firstTime switch
        {
            "yes" => "/set-last-start-date",
            "no"  => "/change-restriction",
             _        => throw new ArgumentException($"Unknown message type: {firstTime}")
        };
        var fullLink = $"{link}{UKPRN}{lastPartOfLink}";
        await  page.Locator($"a[href='{fullLink}']").ClickAsync();
    }

    public async Task VerifyDateUpdate(string UKPRN, string date)
    {
        await Assertions.Expect(page.Locator("#govuk-notification-banner-title")).ToContainTextAsync("Success");
        var child = page.Locator("dd", new PageLocatorOptions { HasTextString = UKPRN });
        var listItem = child.Locator("../..");
        var dateDisplay = listItem.Locator(".govuk-\\!-margin-bottom-0");
        await Assertions.Expect(dateDisplay.Locator(".das-definition-list__definition")).ToContainTextAsync(date);
    }

    public async Task AssertBanner(string UKPRN, string bannerMessage)
    {
        var child = page.Locator("dd", new PageLocatorOptions { HasTextString = UKPRN });
        var listItem = child.Locator("../..");
        string banner = bannerMessage switch
        {
            "Last start date added" => ".govuk-tag--orange",
            "Closed to new starts"  => ".govuk-tag--grey",
            "Open to new starts"   => ".govuk-tag--green",
             _        => throw new ArgumentException($"Unknown message type: {bannerMessage}")
        };
        await Assertions.Expect(listItem.Locator(banner)).ToContainTextAsync(bannerMessage);   
    }
}