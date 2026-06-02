
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class TasksBasePage(ScenarioContext context) : AppBasePage(context)
    {
        #region Selectors

        // Tabs
        private const string ToDoTab = "a.app-tabs__tab.todo";
        private const string DoneTab = "a.app-tabs__tab.done";
        private const string AddTaskBtn = "a.govuk-button.app-fab[href='/Tasks/Add']";

        // Task form
        private const string TaskTitleInput = "input#title, input[id*='Title']";
        private const string DateDayInput = "input[id$='day'], input[id*='date-day']";
        private const string DateMonthInput = "input[id$='month'], input[id*='date-month']";
        private const string DateYearInput = "input[id$='year'], input[id*='date-year']";
        private const string TimeInput = "input#Time, input#time";
        private const string NoteTextArea = "#note";
        private const string ReminderNoneRadio = "label[for='ReminderValueNone']";
        private const string SaveAndContinueButton = "button.govuk-button:has-text('Save and continue')";

        // Task card actions
        private const string DeleteButton = "a[href*='/Tasks/ConfirmDelete/']";
        private const string ConfirmDeleteButton = "button.govuk-button--warning";

        // Task card display
        private const string TaskTitleCards = "h2.app-card__heading";

        // Target an anchor card that encloses a specific header title text
        private ILocator TaskCardAnchor(string title) =>
            page.Locator("a.app-card").Filter(new() { Has = page.Locator("h2", new() { HasText = title }) });

        // Targets automated tasks for the current decade
        private ILocator AnyAutomatedTaskCard =>
            page.Locator("a.app-card").Filter(new() { Has = page.Locator("h2", new() { HasText = "Task 202" }) });

        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Tasks");
        }

        #region Helpers

        public static string GenerateTaskName() => $"Task {DateTime.Now:yyyyMMddHHmmss}";

        public async Task RefreshAsync()
        {
            await page.ReloadAsync(new PageReloadOptions { WaitUntil = WaitUntilState.NetworkIdle });
        }

        #endregion

        #region Tab Navigation

        public async Task<TasksBasePage> ClickToDoTabAsync()
        {
            if (!page.Url.Contains("/Tasks/Index") && !page.Url.EndsWith("/Tasks"))
                await RefreshAsync();

            await page.Locator(ToDoTab).ClickAsync();
            return this;
        }

        public async Task<TasksBasePage> ClickDoneTabAsync()
        {
            if (!page.Url.Contains("/Tasks/Index") && !page.Url.EndsWith("/Tasks"))
                await RefreshAsync();

            await page.Locator(DoneTab).ClickAsync();
            return this;
        }

        #endregion

        #region Task CRUD

        public async Task<TasksBasePage> AddTaskAsync(
            bool isToDo, string title, string date, string time,
            string ksb, string ksbId, string categoryValue, string status, string note)
        {
            // Playwright auto-waits for clickability seamlessly
            await page.Locator(AddTaskBtn).ClickAsync();

            var titleLocator = page.Locator(TaskTitleInput);
            await titleLocator.ClearAsync();
            await titleLocator.FillAsync(title);

            if (!string.IsNullOrEmpty(date) && date.Contains("/"))
            {
                var parts = date.Split('/');
                await page.Locator(DateDayInput).FillAsync(parts[0]);
                await page.Locator(DateMonthInput).FillAsync(parts[1]);
                await page.Locator(DateYearInput).FillAsync(parts[2]);
            }
            else
            {
                await page.Locator(DateDayInput).FillAsync(date);
            }

            await page.Locator(TimeInput).FillAsync(time);

            if (!string.IsNullOrEmpty(note))
            {
                await page.Locator(NoteTextArea).ClearAsync();
                await page.Locator(NoteTextArea).FillAsync(note);
            }

            await page.Locator(ReminderNoneRadio).ClickAsync();

            if (!string.IsNullOrEmpty(categoryValue))
            {
                // Scopes lookup into the radio container and avoids raw XPath string generation
                await page.Locator(".govuk-radios").GetByText(categoryValue, new() { Exact = true }).ClickAsync();
            }

            await page.Locator(SaveAndContinueButton).ClickAsync();

            try
            {
                await page.WaitForURLAsync(
                    url => url.Contains("/Tasks/Index") || url.EndsWith("/Tasks") || url.Contains("status="),
                    new PageWaitForURLOptions { Timeout = 15_000 });
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"[Pipeline Error] Form failed to redirect. Currently on: {page.Url}");
                await ClickToDoTabAsync();
                await RefreshAsync();

                if (page.Url.Contains("/Tasks/Add"))
                    throw new PlaywrightException($"Form submission failed to redirect. Stranded on: {page.Url}");
            }

            return this;
        }

        public async Task<string> OpenTaskByTitleAsync(string taskTitle)
        {
            var card = TaskCardAnchor(taskTitle);

            // Single precautionary refresh replace the loop if data takes a moment to persist
            if (await card.CountAsync() == 0)
            {
                await RefreshAsync();
            }

            // Get text via the inner heading element
            string actualTitle = await card.Locator("h2").InnerTextAsync();
            await card.ClickAsync();
            return actualTitle;
        }

        public async Task SetTaskTitleAsync(string updatedName)
        {
            await page.Locator(TaskTitleInput).ClearAsync();
            await page.Locator(TaskTitleInput).FillAsync(updatedName);
        }

        public async Task ClickSaveAndContinueAsync()
            => await page.Locator(SaveAndContinueButton).ClickAsync();

        public async Task DeleteTaskAsync()
        {
            await page.Locator(DeleteButton).ClickAsync();
            await page.Locator(ConfirmDeleteButton).ClickAsync();
        }

        #endregion

        #region Assertions

        public async Task<bool> IsTaskAddedAsync(string title)
        {
            var texts = await page.Locator(TaskTitleCards).AllInnerTextsAsync();
            return texts.Any(t => t.Contains(title));
        }

        public async Task<bool> IsTaskRemovedAsync(string title)
            => await TaskCardAnchor(title).CountAsync() == 0;

        #endregion

        #region Teardown Sweepers

        public async Task CleanUpTaskByTitleAsync(string taskTitle)
        {
            var card = TaskCardAnchor(taskTitle);
            if (await card.CountAsync() > 0)
            {
                await card.ClickAsync();
                await DeleteTaskAsync();
                await RefreshAsync();
            }
        }

        public async Task SweepOrphanedTasksAsync()
        {
            const int safetyMax = 100;

            // Phase 1: To-Do tab
            Console.WriteLine("[CleanUp] Routing to 'To do' tab...");
            await ClickToDoTabAsync();
            await ExecuteSweepAsync(safetyMax);

            // Phase 2: Done tab
            Console.WriteLine("[CleanUp] Switching to 'Done' tab...");
            await ClickDoneTabAsync();
            await ExecuteSweepAsync(safetyMax);

            // Phase 3: Reset to To-Do
            Console.WriteLine("[CleanUp Reset] Returning session to 'To do' tab...");
            await ClickToDoTabAsync();
            await RefreshAsync();
        }

        private async Task ExecuteSweepAsync(int safetyMax)
        {
            int loopCount = 0;
            // Directly querying AnyAutomatedTaskCard ensures Playwright checks the live state on every loop condition evaluation
            while (await AnyAutomatedTaskCard.CountAsync() > 0 && loopCount < safetyMax)
            {
                try
                {
                    string text = await AnyAutomatedTaskCard.Locator("h2").InnerTextAsync();
                    Console.WriteLine($"[CleanUp Card {loopCount + 1}] Target: '{text.Trim()}'");

                    await AnyAutomatedTaskCard.First.ClickAsync();
                    await DeleteTaskAsync();
                    await RefreshAsync();
                    loopCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CleanUp Interrupted]: {ex.Message}");
                    await RefreshAsync();
                    break;
                }
            }
        }

        #endregion
    }
}