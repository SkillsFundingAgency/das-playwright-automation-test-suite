using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Common;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeDetails_ProviderPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private readonly string pageTitle;

        #region Locators        
        private ILocator EditApprenticeDetailsLink => page.Locator("#edit-apprentice-link");
        private ILocator ManageYourApprenticesLinks => page.GetByRole(AriaRole.Link, new() { Name = "Manage your apprentices" });
        private ILocator Banner(string Header, string Body) => page.GetByLabel(Header).Locator("div").Filter(new() { HasText = Body });
        private ILocator ReviewChangesLink => page.GetByRole(AriaRole.Link, new() { Name = "Review changes" });
        private ILocator ViewChangesLink => page.GetByRole(AriaRole.Link, new() { Name = "View changes" });
        private ILocator ViewDetailsLink => page.GetByRole(AriaRole.Link, new() { Name = "View details" });
        private ILocator ViewChangeHistoryLink => page.GetByRole(AriaRole.Link, new() { Name = "View change history for this learner." });
        private ILocator ChangeEmployerLink => page.Locator("#change-employer-link");
        private ILocator ChangeVersionLink => page.Locator("#change-version-link");

        #endregion

        internal ApprenticeDetails_ProviderPage(ScenarioContext context, string pageTitle) : base(context)
        {
            this.context = context;
            this.pageTitle = pageTitle; 
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(pageTitle);
        }

        internal async Task<ChangeHistoryPage> ClickOnViewChangeHistoryLink(string learnerName)
        {
            await ViewChangeHistoryLink.ClickAsync();
            return await VerifyPageAsync(() => new ChangeHistoryPage(context, learnerName));
        }

        internal async Task<EditLearnerDetails_ProviderPage> ClickOnEditApprenticeDetailsLink()
        {
            await EditApprenticeDetailsLink.ClickAsync();
            return await VerifyPageAsync(() => new EditLearnerDetails_ProviderPage(context));
        }

        internal async Task<ProviderAccessDeniedPage> ClickOnEditApprenticeDetailsLinkLeadToAccessDeniedPage()
        {
            await EditApprenticeDetailsLink.ClickAsync();
            return await VerifyPageAsync(() => new ProviderAccessDeniedPage(context));
        }

        internal async Task ClickOnReviewChanges() => await ReviewChangesLink.ClickAsync();

        internal async Task ClickOnViewChanges() => await ViewChangesLink.ClickAsync();

        internal async Task ClickOnViewDetails() => await ViewDetailsLink.ClickAsync();

        internal async Task ClickOnChangeEmployerLink() => await ChangeEmployerLink.ClickAsync();

        internal async Task<ManageYourLearners_ProviderPage> ReturnBackToManageYourApprenticesPage()
        {
            await ManageYourApprenticesLinks.ClickAsync();
            return await VerifyPageAsync(() => new ManageYourLearners_ProviderPage(context));
        }

        internal async Task<ApprenticeDetails_ProviderPage> ProviderVerifyApprenticeStatus(ApprenticeshipStatus status, DateTime? date)
        {
            await Assertions.Expect(page.Locator("#apprenticeship-status")).ToContainTextAsync(status.ToString());

            switch (status)
            {
                case ApprenticeshipStatus.Paused:
                    await Assertions.Expect(page.Locator("tr", new() { HasTextString = "Apprenticeship pause date" }).Locator("#apprenticeship-pause-date")).ToHaveTextAsync(date.Value.ToString("d MMM yyyy"));
                    break;
                case ApprenticeshipStatus.Stopped:
                    await Assertions.Expect(page.Locator("tr", new() { HasTextString = "Stop applies from" }).Locator("#apprenticeship-stop-date")).ToHaveTextAsync(date.Value.ToString("MMM yyyy"));
                    break;
                case ApprenticeshipStatus.Completed: 
                    await Assertions.Expect(page.Locator("tr", new() { HasTextString = "Completion payment month" }).Locator("#apprenticeship-completed-date")).ToHaveTextAsync(date.Value.ToString("MMM yyyy"));
                    break;
                default:
                    break;
            }
            return this;
        }

        internal async Task AssertBanner(string Title, string Body) => await Assertions.Expect(Banner(Title, Body)).ToBeVisibleAsync();

        internal async Task AssertBanner2(string Title, string Body)
        {
            await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(Title);
            await Assertions.Expect(page.Locator("#das-pendingUpdate-div-body-text")).ToContainTextAsync(Body);

        }

        internal async Task<bool> IsChangeHistoryLinkVisible() => await ViewChangeHistoryLink.IsVisibleAsync();
        internal async Task<bool> IsChangeOfEmployerLinkVisible() => await ChangeEmployerLink.IsVisibleAsync();
        internal async Task<bool> IsChangeOfVersionLinkVisible() => await ChangeVersionLink.IsVisibleAsync();
        internal async Task<bool> IsEditApprenticeDetailsLinkVisible() => await EditApprenticeDetailsLink.IsVisibleAsync();

    }
}
