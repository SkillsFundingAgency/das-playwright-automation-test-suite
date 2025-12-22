using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ApprenticeDetailsPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private readonly string pageTitle;
        //status
        //training provider
        //apprenticedetails
        //planned training end date

        #region Locators
        private ILocator BackPageLink => page.GetByRole(AriaRole.Link, new() { Name = "Back to manage your apprentices" });
        private ILocator EditStatusLink => page.GetByRole(AriaRole.Link, new() { Name = "Edit status" });
        private ILocator ChangeProviderLink => page.GetByRole(AriaRole.Link, new() { Name = "Change   training provider" });
        private ILocator EditApprenticeDetailsLink => page.GetByRole(AriaRole.Link, new() { Name = "Edit   apprentice details" });
        private ILocator EditPlannedTrainingEndDateLink => page.GetByRole(AriaRole.Link, new() { Name = "Edit   end date" });

        private ILocator ApprenticeStatusTag => page.Locator(".govuk-tag");
        #endregion


        internal ApprenticeDetailsPage(ScenarioContext context, string pageTitle) : base(context)
        {
            this.context = context;
            this.pageTitle = pageTitle;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(pageTitle);
        }

        internal async Task<ManageYourApprenticesPage> ReturnBackToManageYourApprenticesPage()
        {
            await BackPageLink.ClickAsync();
            return await VerifyPageAsync(() => new ManageYourApprenticesPage(context));
        }

        internal async Task<EditApprenticeDetailsPage> ClickOnEditApprenticeDetailsLink()
        { 
            await EditApprenticeDetailsLink.ClickAsync();
            return await VerifyPageAsync(() => new EditApprenticeDetailsPage(context));
        }

        internal async Task<ApprenticeDetailsPage> EmployerVerifyApprenticeStatus(ApprenticeshipStatus status, string rowName, DateTime date)
        {
            await Assertions.Expect(ApprenticeStatusTag).ToContainTextAsync(status.ToString());
            await Assertions.Expect(page.Locator("table:nth-of-type(1) tr:nth-of-type(2) th").First).ToContainTextAsync(rowName);
            await Assertions.Expect(page.Locator("table:nth-of-type(1) tr:nth-of-type(2) td").First).ToContainTextAsync(date.ToString("MMMM yyyy"));
            return this;
        }

        internal async Task AssertRecordIsReadOnlyExceptEndDate()
        { 
            if (await EditStatusLink.IsVisibleAsync())
                throw new Exception("Edit Status link is visible, expected to be hidden in read-only mode.");
            
            if (await ChangeProviderLink.IsVisibleAsync())
                throw new Exception("Change Provider link is visible, expected to be hidden in read-only mode.");
            
            if (await EditApprenticeDetailsLink.IsVisibleAsync())
                throw new Exception("Edit Apprentice Details link is visible, expected to be hidden in read-only mode.");
            
            if (!await EditPlannedTrainingEndDateLink.IsVisibleAsync())
                throw new Exception("Edit Planned Training End Date link is not visible, expected to be visible in read-only mode.");

        }


    }
}
