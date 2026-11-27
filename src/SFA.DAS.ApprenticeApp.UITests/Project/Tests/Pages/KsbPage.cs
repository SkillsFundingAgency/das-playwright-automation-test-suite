
using System.Text.RegularExpressions;

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
        private ILocator KnowledgePanel => page.Locator("#knowledge");
        private ILocator SkillsPanel => page.Locator("#skill");
        private ILocator BehavioursPanel => page.Locator("#behaviour");

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

        public async Task SelectTabAsync(string tabName)
        {
            switch (tabName)
            {
                case "Knowledge":
                    await SelectKnowledgeTabAsync();
                    break;
                case "Skills":
                    await SelectSkillsTabAsync();
                    break;
                case "Behaviours":
                    await SelectBehavioursTabAsync();
                    break;
                default:
                    throw new ArgumentException($"Unknown KSB tab: {tabName}", nameof(tabName));
            }
        }

        public async Task VerifyTabHasItemsAsync(string tabName)
        {
            await SelectTabAsync(tabName);

            var panel = tabName switch
            {
                "Knowledge" => KnowledgePanel,
                "Skills" => SkillsPanel,
                "Behaviours" => BehavioursPanel,
                _ => throw new ArgumentException($"Unknown KSB tab panel: {tabName}", nameof(tabName))
            };

            await Assertions.Expect(panel).ToContainTextAsync(new Regex(@"\S"));
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