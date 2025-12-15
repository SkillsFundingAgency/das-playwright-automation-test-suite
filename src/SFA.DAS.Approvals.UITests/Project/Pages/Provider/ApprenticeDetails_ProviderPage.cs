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
        private ILocator ChangeEmployerLink => page.GetByRole(AriaRole.Link, new() { Name = "Change" });


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

        internal async Task ClickOnEditApprenticeDetailsLink() => await EditApprenticeDetailsLink.ClickAsync();

        internal async Task ClickOnReviewChanges() => await ReviewChangesLink.ClickAsync();

        internal async Task ClickOnViewChanges() => await ViewChangesLink.ClickAsync();

        internal async Task ClickOnViewDetails() => await ViewDetailsLink.ClickAsync();

        internal async Task ClickOnChangeEmployerLink() => await ChangeEmployerLink.ClickAsync();

        internal async Task<ManageYourApprentices_ProviderPage> ReturnBackToManageYourApprenticesPage()
        {
            await ManageYourApprenticesLinks.ClickAsync();
            return await VerifyPageAsync(() => new ManageYourApprentices_ProviderPage(context));
        }

        internal async Task AssertBanner(string Title, string Body) => await Assertions.Expect(Banner(Title, Body)).ToBeVisibleAsync();

        internal async Task AssertBanner2(string Title, string Body)
        {
            await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(Title);
            await Assertions.Expect(page.Locator("#das-pendingUpdate-div-body-text")).ToContainTextAsync(Body);

        }




    }
}
