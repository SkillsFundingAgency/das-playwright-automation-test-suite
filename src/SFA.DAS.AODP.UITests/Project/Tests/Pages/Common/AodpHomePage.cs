
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
        public ILocator AodpHomePageHeader => page.Locator("//h1[contains(., \"What do you want to do?\")]");
        public ILocator ContinueBtn => page.GetByText("Continue");


        // public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToHaveTextAsync("Please select a start Page");
        public override async Task VerifyPage() => await Assertions.Expect(AodpHomePageHeader).ToBeVisibleAsync();

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
                "dfe admin" => "aodpTestAdmin1" + suffix,
                "qfau_user_approver" => "aodp_approver" + suffix,
                "qfau_user_reviewer" => "aodp_reviewer" + suffix,
                "qfau_admin_data_importer" => "aodp_data_importer" + suffix,
                "qfau_admin_form_editor" => "aodp_form_editor" + suffix,
                "ifate_admin_form_editor" => "aodp_ifate_edit" + suffix,
                "ifate_user_reviewer" => "aodp_ifate_review" + suffix,
                "ofqual_user_reviewer" => "aodp_ofqual_user" + suffix,
                "ao_user" => "aodp_ao_user" + suffix,
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
