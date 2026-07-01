using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Common;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class LearnerDetailsPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private readonly string pageTitle;

        #region Locators
        private ILocator BackPageLink => page.GetByRole(AriaRole.Link, new() { Name = "Back to manage your apprentices" });
        private ILocator ViewChangeHistoryLink => page.GetByRole(AriaRole.Link, new() { Name = "View change history for this learner." });
        private ILocator EditStatusLink => page.GetByRole(AriaRole.Link, new() { Name = "Edit status" });
        private ILocator ChangePymtStatusLink => page.Locator("#change-payments-link");
        private ILocator ChangeProviderLink => page.GetByRole(AriaRole.Link, new() { Name = "Change   training provider" });
        private ILocator ChangeVersionLink => page.Locator("a", new () { HasTextString = "Change version" });
        private ILocator EditApprenticeDetailsLink => page.GetByRole(AriaRole.Link, new() { Name = "Edit   apprentice details" });
        private ILocator EditPlannedTrainingEndDateLink => page.GetByRole(AriaRole.Link, new() { Name = "Edit   end date" });
        private ILocator ApprenticeStatusTag => page.Locator("tr", new() { HasTextString = "Status" }).Locator("strong.govuk-tag");
        private ILocator PaymentsStatusTag => page.Locator("tr", new() { HasTextString = "Payments" }).Locator("strong.govuk-tag");
        private ILocator StatusDateTitle => page.Locator("table:nth-of-type(1) tr:nth-of-type(2) th").First;
        private ILocator StatusDateValue => page.Locator("table:nth-of-type(1) tr:nth-of-type(2) td").First;
        #endregion


        internal LearnerDetailsPage(ScenarioContext context, string pageTitle) : base(context)
        {
            this.context = context;
            this.pageTitle = pageTitle;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(pageTitle);
        }

        internal async Task<ManageYourLearnersPage> ReturnBackToManageYourApprenticesPage()
        {
            await BackPageLink.ClickAsync();
            return await VerifyPageAsync(() => new ManageYourLearnersPage(context));
        }

        internal async Task<EditLearnerDetailsPage> ClickOnEditApprenticeDetailsLink()
        { 
            await EditApprenticeDetailsLink.ClickAsync();
            return await VerifyPageAsync(() => new EditLearnerDetailsPage(context));
        }

        internal async Task<ChangeHistoryPage> ClickOnViewChangeHistoryLink(string learnerName)
        {
            await ViewChangeHistoryLink.ClickAsync();
            return await VerifyPageAsync(() => new ChangeHistoryPage(context, learnerName));
        }

        internal async Task<LearnerDetailsPage> EmployerVerifyApprenticeStatus(ApprenticeshipStatus status, string rowName, DateTime date)
        {
            await Assertions.Expect(ApprenticeStatusTag).ToContainTextAsync(status.ToString());
            await Assertions.Expect(StatusDateTitle).ToContainTextAsync(rowName);
            await Assertions.Expect(StatusDateValue).ToContainTextAsync(date.ToString("MMMM yyyy"));
            return this;
        }

        internal async Task<LearnerDetailsPage> EmployerVerifyApprenticeStatusAndDetails(ApprenticeshipStatus status, string type, string apprenticeStatus)
        {
            await Assertions.Expect(ApprenticeStatusTag).ToContainTextAsync(status.ToString());
            await Assertions.Expect(StatusDateTitle).ToContainTextAsync(type);
            await Assertions.Expect(StatusDateValue).ToContainTextAsync(apprenticeStatus);
            return this;
        }

        internal async Task<bool> IsEditStatusLinkAvailable() => await EditStatusLink.IsVisibleAsync();
        internal async Task<bool> IsEditPaymentStatusLinkAvailable() => await ChangePymtStatusLink.IsVisibleAsync();   
        internal async Task<bool> IsChangeProviderLinkAvailable() => await ChangeProviderLink.IsVisibleAsync();
        internal async Task<bool> IsEditApprenticeDetailsLinkAvailable() => await EditApprenticeDetailsLink.IsVisibleAsync();
        internal async Task<bool> IsEditVersionLinkAvailable() => await ChangeVersionLink.IsVisibleAsync();
        internal async Task<bool> IsEditPlannedTrainingEndDateLinkAvailable() => await EditPlannedTrainingEndDateLink.IsVisibleAsync();


    }
}
