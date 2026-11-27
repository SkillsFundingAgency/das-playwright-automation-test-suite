using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SupportGuidanceStepDefinitions(ScenarioContext context)
    {
        private const string SavedArticlesHref = "a[href*='/Support/SavedArticles']";
        private const string SavedArticleUpdatePath = "/Support/UpdateSavedArticle";

        private readonly AppStepsHelper _stepsHelper = new(context);
        private ApprenticeHomePage _apprenticeHomePage;
        private string _articleTitle;
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
            await Page.GetByRole(AriaRole.Link, new() { Name = categoryName }).First.ClickAsync();
        }

        [When(@"the apprentice opens an article and saves it")]
        public async Task WhenTheApprenticeOpensAnArticleAndSavesIt()
        {
            await ExpandArticleSectionsAsync();

            var articleForms = Page.Locator("form.app-article-actions__form");
            if (await articleForms.CountAsync() == 0)
            {
                var articleLink = Page.Locator("a.app-stack__link").First;
                await articleLink.ClickAsync();
                await ExpandArticleSectionsAsync();
                articleForms = Page.Locator("form.app-article-actions__form");
            }

            await Assertions.Expect(articleForms.First).ToBeVisibleAsync();

            var articleForm = articleForms.First;
            _articleTitle = await articleForm.Locator("input[name='entryTitle']").InputValueAsync();
            if (string.IsNullOrWhiteSpace(_articleTitle))
            {
                throw new InvalidOperationException("The selected support article has no title.");
            }

            var actionButton = articleForm.Locator("button.app-article-actions__button");
            var buttonClass = await actionButton.GetAttributeAsync("class") ?? string.Empty;

            if (buttonClass.Contains("unsave", StringComparison.OrdinalIgnoreCase))
            {
                await SubmitArticleActionAsync(actionButton, "resetting");
                await Assertions.Expect(articleForm.Locator("button.app-article-actions__button.save")).ToBeVisibleAsync();
                actionButton = articleForm.Locator("button.app-article-actions__button.save");
            }
            else if (!buttonClass.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Contains("save", StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Could not determine whether article '{_articleTitle}' is saved.");
            }

            await SubmitArticleActionAsync(actionButton, "saving");
            await Assertions.Expect(articleForm.Locator("button.app-article-actions__button.unsave")).ToBeVisibleAsync();
        }

        [Then(@"the article appears under ""Your saved articles""")]
        public async Task ThenTheArticleAppearsUnderYourSavedArticles()
        {
            await OpenSavedArticlesAsync();

            await Assertions.Expect(Page.GetByText(_articleTitle, new() { Exact = true }).First).ToBeVisibleAsync();
        }

        [When(@"the apprentice removes the article from the saved list")]
        public async Task WhenTheApprenticeRemovesTheArticleFromTheSavedList()
        {
            await OpenSavedArticlesAsync();

            var articleForm = await FindArticleFormAsync(_articleTitle);
            var removeButton = articleForm.Locator("button.app-article-actions__button.unsave");
            await Assertions.Expect(removeButton).ToBeVisibleAsync();

            await SubmitArticleActionAsync(removeButton, "removing");
        }

        [Then(@"the article is removed from the saved articles")]
        public async Task ThenTheArticleIsRemovedFromTheSavedArticles()
        {
            await OpenSavedArticlesAsync();

            await Assertions.Expect(Page.GetByText(_articleTitle, new() { Exact = true }).First).ToBeHiddenAsync();
        }

        private async Task OpenSavedArticlesAsync()
        {
            await _stepsHelper.NavigateToHomeTabAsync();
            await Page.Locator(SavedArticlesHref).ClickAsync();
            await Assertions.Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Your saved articles" })).ToBeVisibleAsync();
            await ExpandArticleSectionsAsync();
        }

        private async Task ExpandArticleSectionsAsync()
        {
            var showAllButton = Page.GetByRole(AriaRole.Button, new() { Name = "Show all sections" });
            if (await showAllButton.IsVisibleAsync())
            {
                if (await showAllButton.GetAttributeAsync("aria-expanded") != "true")
                {
                    await showAllButton.ClickAsync();
                }

                return;
            }

            var sectionHeadings = Page.Locator(".govuk-accordion__section-button");
            for (var index = 0; index < await sectionHeadings.CountAsync(); index++)
            {
                var sectionHeading = sectionHeadings.Nth(index);
                if (await sectionHeading.GetAttributeAsync("aria-expanded") != "true")
                {
                    await sectionHeading.ClickAsync();
                }
            }
        }

        private async Task<ILocator> FindArticleFormAsync(string articleTitle)
        {
            var articleForms = Page.Locator("form.app-article-actions__form");
            for (var index = 0; index < await articleForms.CountAsync(); index++)
            {
                var articleForm = articleForms.Nth(index);
                var titleInput = articleForm.Locator("input[name='entryTitle']");
                if (await titleInput.InputValueAsync() == articleTitle)
                {
                    return articleForm;
                }
            }

            throw new InvalidOperationException($"Could not find saved article '{articleTitle}' on the current page.");
        }

        private async Task SubmitArticleActionAsync(ILocator actionButton, string action)
        {
            var responseTask = Page.WaitForResponseAsync(response => response.Url.Contains(SavedArticleUpdatePath));
            await Task.WhenAll(responseTask, actionButton.ClickAsync());

            var response = await responseTask;
            if (!response.Ok)
            {
                throw new InvalidOperationException($"Request for {action} article '{_articleTitle}' failed with HTTP status {response.Status}.");
            }
        }
    }
}
