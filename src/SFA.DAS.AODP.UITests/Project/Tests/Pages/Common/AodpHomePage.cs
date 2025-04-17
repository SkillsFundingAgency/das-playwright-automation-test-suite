
using System;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.Common
{
    public class AodpHomePage(ScenarioContext context) : AodpLandingPage(context)
    {
        public ILocator NewQualificationsBtn => page.Locator("[value=\"NewQualifications\"]");
        public ILocator ChangedQualificationsBtn => page.Locator("[value=\"ChangedQualifications\"]");
        public ILocator FormsManagementBtn => page.Locator("[value=\"FormsManagement\"]");
        public ILocator ApplicationsReviewBtn => page.Locator("[value=\"ApplicationsReview\"]");
        public ILocator ImportDataBtn => page.Locator("[value=\"ImportData\"]");
        public ILocator OutputFileBtn => page.Locator("[value=\"OutputFile\"]");
        public ILocator AutoRedirectStartBtn => page.GetByText("What do you want to do?");
        public ILocator ContinueBtn => page.GetByText("Continue");


        // public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToHaveTextAsync("Please select a start Page");
        public override async Task VerifyPage() => await Assertions.Expect(AutoRedirectStartBtn).ToBeVisibleAsync();

        public async Task ClickOn(ILocator locator)
        {
            await locator.ClickAsync();
        }

        public async Task<ILocator> GetLocator(string xpath)
        {
            await Task.CompletedTask;
            return page.Locator(xpath);
        }


#pragma warning disable CS1998
        public async Task<string> GetUser(string role)
        {
            var suffix = "@l38cxwya.mailosaur.net";
            return role.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
            {
                "dfe user" => "aodpTestAdmin1" + suffix,
                "dfe reviewer" => "aodpTestAdmin1" + suffix,
                "dfe admin" => "aodpTestAdmin1" + suffix,
                "dfe form builder" => "aodpTestAdmin1" + suffix,
                "ao user" => "aodp_ao_user" + suffix,
                _ => throw new ArgumentException($"No user found with gievn role '{role}'."),
            };
        }

        public async Task<string> GetPassword()
        {
            return "TestApprenticeshipAutomation2025";
        }
#pragma warning restore CS1998

    }
}
