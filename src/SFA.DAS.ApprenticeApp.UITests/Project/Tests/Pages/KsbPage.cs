
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class KsbPage(ScenarioContext context) : AppBasePage(context)
    {
        // --- Core Page Locators ---
        private ILocator KsbHeader => page.GetByRole(AriaRole.Heading, new() { Name = "Knowledge, skills and behaviours (KSBs)", Exact = true });

        // Tab Headers
        private ILocator KnowledgeTab => page.GetByRole(AriaRole.Tab, new() { Name = "Knowledge" });
        private ILocator SkillsTab => page.GetByRole(AriaRole.Tab, new() { Name = "Skills" });
        private ILocator BehavioursTab => page.GetByRole(AriaRole.Tab, new() { Name = "Behaviours" });

        // Tab Panels
        private ILocator KnowledgePanel => page.GetByRole(AriaRole.Tabpanel, new() { Name = "Knowledge" });
        private ILocator SkillsPanel => page.GetByRole(AriaRole.Tabpanel, new() { Name = "Skills" });
        private ILocator BehavioursPanel => page.GetByRole(AriaRole.Tabpanel, new() { Name = "Behaviours" });

        // --- Filter Panel Components ---
        private ILocator FilterAccordionToggle => page.GetByText("Show search and filters");
        private ILocator KeywordSearchInput => page.GetByLabel("Keywords");
        private ILocator LinkedToTaskCheckbox => page.GetByLabel("Linked to a task");
        private ILocator ApplyFiltersButton => page.GetByRole(AriaRole.Link, new() { Name = "Apply filters" });

        public override async Task VerifyPage()
        {
            await Assertions.Expect(KsbHeader).ToBeVisibleAsync();
        }

        #region Tab Navigation Helpers

        public async Task SelectKnowledgeTabAsync()
        {
            await KnowledgeTab.ClickAsync();
            await Assertions.Expect(KnowledgePanel).ToBeVisibleAsync();
        }

        public async Task SelectSkillsTabAsync()
        {
            await SkillsTab.ClickAsync();
            await Assertions.Expect(SkillsPanel).ToBeVisibleAsync();
        }

        public async Task SelectBehavioursTabAsync()
        {
            await BehavioursTab.ClickAsync();
            await Assertions.Expect(BehavioursPanel).ToBeVisibleAsync();
        }

        #endregion

        #region Filter Action Helpers

        public async Task ApplyKeywordFilterAsync(string keyword)
        {
            if (!await KeywordSearchInput.IsVisibleAsync())
            {
                await FilterAccordionToggle.ClickAsync();
            }

            await KeywordSearchInput.FillAsync(keyword);
            await ApplyFiltersButton.ClickAsync();
        }

        public async Task FilterByStatusAsync(string statusCheckboxId)
        {
            var statusCheckbox = page.Locator($"#{statusCheckboxId}");

            if (!await statusCheckbox.IsVisibleAsync())
            {
                await FilterAccordionToggle.ClickAsync();
            }

            await statusCheckbox.CheckAsync();
            await ApplyFiltersButton.ClickAsync();
        }

        #endregion
    }
}