using SFA.DAS.Registration.UITests.Project.Pages.InterimPages;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class OrganisationHasBeenAddedPage(ScenarioContext context) : InterimHomeBasePage(context, false)
{
    public override async Task VerifyPage()
    {
        var list = await page.Locator(".das-notification__heading").AllTextContentsAsync();

        CollectionAssert.Contains(list, $"{objectContext.GetRecentlyAddedOrganisationName()} has been added");
    }
}
