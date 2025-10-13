namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate
{
    public class ProviderDetailsPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Organisation details");
        }

        public async Task<StatusChangePage> GoToStatusChange()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Change", Exact = false })
                      .Locator("xpath=preceding::text()[contains(., 'Status')]")
                      .ClickAsync();
            return await VerifyPageAsync(() => new StatusChangePage(context));
        }

        public async Task<TypeOfOrganisationPage> GoToTypeOfOrganisationChange()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Change", Exact = false })
                      .Locator("xpath=preceding::text()[contains(., 'Type of organisation')]")
                      .ClickAsync();
            return await VerifyPageAsync(() => new TypeOfOrganisationPage(context));
        }

        public async Task<OfferApprenticeshipsPage> GoToApprenticeshipsChange()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Change", Exact = false })
                      .Locator("xpath=preceding::text()[contains(., 'Offers apprenticeships')]")
                      .ClickAsync();
            return await VerifyPageAsync(() => new OfferApprenticeshipsPage(context));
        }

        public async Task<DoTheyOfferOtherQualificationsPage> GoToOtherQualificationsChange()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Change", Exact = false })
                      .Locator("xpath=preceding::text()[contains(., 'Offers other qualifications')]")
                      .ClickAsync();
            return await VerifyPageAsync(() => new DoTheyOfferOtherQualificationsPage(context));
        }

        public async Task<TypeOfQualificationsPage> GoToTypeOfQualificationsChange()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Change", Exact = false })
                      .Locator("xpath=preceding::text()[contains(., 'Type of qualifications')]")
                      .ClickAsync();
            return await VerifyPageAsync(() => new TypeOfQualificationsPage(context));
        }
    }
}
