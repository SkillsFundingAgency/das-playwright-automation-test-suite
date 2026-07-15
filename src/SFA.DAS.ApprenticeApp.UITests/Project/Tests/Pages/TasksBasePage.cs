
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class TasksBasePage : AppBasePage
    {
        private readonly ScenarioContext _context;
        private readonly string _threadIsolationToken;

        #region Constructor

        public TasksBasePage(ScenarioContext context) : base(context)
        {
            _context = context;
            _threadIsolationToken = Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();
        }

        #endregion

        #region Selectors

        // Tabs
        private const string ToDoTab = "a.app-tabs__tab.todo";
        private const string DoneTab = "a.app-tabs__tab.done";
        private const string AddTaskBtn = "a.govuk-button.app-fab[href='/Tasks/Add']";

        // Task form fields
        private const string TaskTitleInput = "input#title, input[id*='Title']";
        private const string DateDayInput = "input[id$='day'], input[id*='date-day']";
        private const string DateMonthInput = "input[id$='month'], input[id*='date-month']";
        private const string DateYearInput = "input[id$='year'], input[id*='date-year']";
        private const string TimeInput = "input#Time, input#time";
        private const string NoteTextArea = "#note";
        private const string ReminderNoneRadio = "label[for='ReminderValueNone']";
        private const string SaveAndContinueButton = "button.govuk-button:has-text('Save and continue')";

        // Task card deletion hooks
        private const string DeleteButton = "a[href*='/Tasks/ConfirmDelete/']";
        private const string ConfirmDeleteButton = "button.govuk-button--warning";

        // Task card layout selectors
        private const string TaskTitleCards = "h2.app-card__heading";

        private ILocator TaskCardAnchor(string title) => page.Locator("a.app-card").Filter(new() { Has = page.Locator("h2", new() { HasText = title }) });

        private ILocator AnyAutomatedTaskCard => page.Locator("a.app-card:visible").Filter(new() { Has = page.Locator("h2", new() { HasText = $"Auto-{_threadIsolationToken}" }) });

        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Tasks");
        }

        #region Helpers

        public string GenerateTaskName() => $"Task Auto-{_threadIsolationToken}-{DateTime.Now:ss}";

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
                Console.WriteLine($"[Pipeline Error] Form submission failed to redirect. Currently on: {page.Url}");
                await ClickToDoTabAsync();
                await RefreshAsync();

                if (page.Url.Contains("/Tasks/Add"))
                    throw new PlaywrightException($"Form submission failed to redirect. Stranded on: {page.Url}");
            }

            return this;
        }

        public async Task ClickOnTaskAsync(string taskTitle)
        {
            var card = TaskCardAnchor(taskTitle);

            try
            {
                await card.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"[Sync Warning] Task '{taskTitle}' not visible yet. Attemping layout refresh...");
                await RefreshAsync();
                await card.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
            }

            await card.ClickAsync();
        }

        public async Task SetTaskTitleAsync(string updatedName)
        {
            var titleInput = page.Locator(TaskTitleInput);
            await titleInput.ClearAsync();
            await titleInput.FillAsync(updatedName);
        }

        public async Task ClickSaveAndContinueAsync()
            => await page.Locator(SaveAndContinueButton).ClickAsync();

        public async Task EditTaskAndConfirmAsync(string currentTaskName)
        {
            string updatedName = $"{currentTaskName} - Edited";
            await SetTaskTitleAsync(updatedName);
            _context["UpdatedTaskName"] = updatedName;
            await ClickSaveAndContinueAsync();

            try
            {
                await page.WaitForURLAsync(
                    url => url.Contains("/Tasks/Index") || url.EndsWith("/Tasks") || url.Contains("status="),
                    new PageWaitForURLOptions { Timeout = 10_000 });
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"[Sync Warning] Edit submission redirect timed out. Forcing navigation reload...");
                await ClickToDoTabAsync();
            }
        }

        public async Task DeleteTaskAndConfirmAsync()
        {
            await page.Locator(DeleteButton).WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
            await page.Locator(DeleteButton).ClickAsync();

            await page.Locator(ConfirmDeleteButton).WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
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
                await DeleteTaskAndConfirmAsync();
                await RefreshAsync();
            }
        }

        public async Task SweepOrphanedTasksAsync()
        {
            const int safetyMax = 100;

            // Phase 1: To-Do tab sweep
            Console.WriteLine("[CleanUp] Routing to 'To do' tab...");
            await ClickToDoTabAsync();
            await RefreshAsync();
            await ExecuteSweepAsync(safetyMax);

            // Phase 2: Done tab sweep
            Console.WriteLine("[CleanUp] Switching to 'Done' tab...");
            await ClickDoneTabAsync();
            await RefreshAsync();
            await ExecuteSweepAsync(safetyMax);

            // Phase 3: Session Reset
            Console.WriteLine("[CleanUp Reset] Returning session to 'To do' tab...");
            await ClickToDoTabAsync();
            await RefreshAsync();
        }

        private async Task ExecuteSweepAsync(int safetyMax)
        {
            int loopCount = 0;

            while (await AnyAutomatedTaskCard.CountAsync() > 0 && loopCount < safetyMax)
            {
                var targetCard = AnyAutomatedTaskCard.First;

                try
                {
                    await targetCard.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 2_000 });

                    string text = await targetCard.Locator("h2").InnerTextAsync();
                    Console.WriteLine($"[CleanUp Card {loopCount + 1}] Sweeping visible target: '{text.Trim()}'");

                    await targetCard.ClickAsync();
                    await DeleteTaskAndConfirmAsync();
                    await RefreshAsync();
                    loopCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CleanUp Skip] Could not process item index {loopCount + 1}: {ex.Message}");
                    break;
                }
            }
        }

        #endregion
    }
}