using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SupportGuidanceStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private ApprenticeHomePage _apprenticeHomePage;
        private IPage Page => context.Get<Driver>().Page;

        [When(@"the apprentice is on the Home tab")]
        public async Task WhenTheApprenticeIsOnTheHomeTab()
        {
            _apprenticeHomePage = await _stepsHelper.NavigateToHomeTabAsync();
        }

        [Then(@"all support and guidance article links are displayed")]
        public async Task ThenAllSupportAndGuidanceArticleLinksAreDisplayed()
        {
            await _apprenticeHomePage.VerifyAllSupportAndGuidanceLinksAsync();
        }

        [When(@"the apprentice opens the ""(.*)"" support category")]
        public async Task WhenTheApprenticeOpensTheSupportCategory(string categoryName)
        {
            var categoryLink = Page.Locator($"a.app-stack__link:has-text('{categoryName}')").First;
            await categoryLink.ClickAsync();
        }

        [When(@"the apprentice opens an article and saves it if not already saved")]
        public async Task WhenTheApprenticeOpensAnArticleAndSavesItIfNotAlreadySaved()
        {
            var showAllButton = Page.Locator("button:has-text('Show all sections'), button.govuk-accordion__show-all").First;
            if (await showAllButton.IsVisibleAsync())
            {
                await showAllButton.ClickAsync();
            }

            var firstArticleLink = Page.Locator(".govuk-accordion__section-header button, .govuk-accordion__section-heading-text, a.app-stack__link").First;
            await firstArticleLink.ClickAsync();

            var saveButton = Page.Locator("form.app-article-actions__form button.app-article-actions__button")
                                 .Filter(new() { Visible = true })
                                 .First;
            await saveButton.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });

            await Task.WhenAll(
                Page.WaitForResponseAsync(resp => resp.Url.Contains("/Support/UpdateSavedArticle") && resp.Status == 200),
                saveButton.ClickAsync()
            );
        }

        [Then(@"the article appears under ""Your saved articles""")]
        public async Task ThenTheArticleAppearsUnderYourSavedArticles()
        {
            await _stepsHelper.NavigateToHomeTabAsync();

            var savedArticlesLink = Page.Locator("a[href*='/Support/SavedArticles']").First;
            await savedArticlesLink.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });
            await savedArticlesLink.ClickAsync();

            var savedArticleItem = Page.Locator("main h2, main h3, .govuk-accordion__section-heading, .app-stack__link").First;
            await savedArticleItem.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });

            Assert.That(await savedArticleItem.IsVisibleAsync(), Is.True, "Expected saved article to be visible under 'Your saved articles'.");
        }

        [When(@"the apprentice removes the article from the saved list")]
        public async Task WhenTheApprenticeRemovesTheArticleFromTheSavedList()
        {
            await _stepsHelper.NavigateToHomeTabAsync();

            var savedArticlesLink = Page.Locator("a[href*='/Support/SavedArticles']").First;
            await savedArticlesLink.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });
            await savedArticlesLink.ClickAsync();

            var showAllButton = Page.Locator("button.govuk-accordion__show-all").First;
            if (await showAllButton.IsVisibleAsync())
            {
                var expanded = await showAllButton.GetAttributeAsync("aria-expanded");
                if (expanded != "true")
                {
                    await showAllButton.ClickAsync();
                }
            }

            var removeButtons = Page.Locator("button:has-text('Remove'), a:has-text('Remove'), button.app-article-actions__button.unsave")
                                    .Filter(new() { Visible = true });

            while (await removeButtons.CountAsync() > 0)
            {
                await Task.WhenAll(
                    Page.WaitForResponseAsync(resp => resp.Url.Contains("/Support/UpdateSavedArticle") && resp.Status == 200),
                    removeButtons.First.ClickAsync()
                );

                await Page.WaitForTimeoutAsync(500);
            }
        }

        [Then(@"the saved articles section should be empty")]
        public async Task ThenTheSavedArticlesSectionShouldBeEmpty()
        {
            await _stepsHelper.NavigateToHomeTabAsync();

            var savedArticlesLink = Page.Locator("a[href*='/Support/SavedArticles']").First;
            await savedArticlesLink.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });
            await savedArticlesLink.ClickAsync();

            var emptyStateMessage = Page.Locator("text=You have no saved articles");
            await emptyStateMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });

            Assert.That(await emptyStateMessage.IsVisibleAsync(), Is.True, "Expected saved articles section to be empty.");
        }
    }
}