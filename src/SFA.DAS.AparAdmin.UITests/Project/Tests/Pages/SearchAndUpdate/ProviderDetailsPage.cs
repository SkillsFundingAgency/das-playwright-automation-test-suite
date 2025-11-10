using Azure;
using System;
using System.Collections.Generic;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate
{
    public class ProviderDetailsPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Details for");
        }

        public async Task<StatusChangePage> ClickChangeStatusOfProvider()
        {
            var changeLink = page.Locator("//tr[th[normalize-space()='Status']]//a[normalize-space()='Change']");

            await Assertions.Expect(changeLink).ToBeVisibleAsync();
            await changeLink.ClickAsync();

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
        public async Task VerifyProviderStatus(string expectedStatus)
        {
            var el = await page
                .Locator("table.govuk-table tr:has(th:text('Status')) strong.govuk-tag")
                .First.ElementHandleAsync();

            if (el == null)
                throw new Exception("❌ Could not find the status element on the Provider Details page.");

            var actual = (await el.InnerTextAsync()).Trim();

            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["active but not taking on apprentices"] = "Active - not taking new apprentices",
                ["active not taking on apprentices"] = "Active - not taking new apprentices",
            };

            var expectedDisplay = map.TryGetValue((expectedStatus ?? string.Empty).Trim(), out var mapped) ? mapped : expectedStatus?.Trim();

            if (!string.Equals(actual, expectedDisplay, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"❌ Expected status '{expectedStatus}' but found '{actual}'.");

            Console.WriteLine($"✅ Verified provider status: {actual}");
        }
    }
}
