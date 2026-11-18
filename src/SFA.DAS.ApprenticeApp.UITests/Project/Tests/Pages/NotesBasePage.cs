namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class NotesBasePage : AppBasePage
    {
        private const string PageHeader = "h1.govuk-heading-xl";

        private readonly ILocator _titleInput;
        private readonly ILocator _dayInput;
        private readonly ILocator _monthInput;
        private readonly ILocator _yearInput;
        private readonly ILocator _noteTextArea;
        private readonly ILocator _linkKsbsButton;
        private readonly ILocator _saveAndContinueButton;

        public NotesBasePage(ScenarioContext context) : base(context)
        {
            _titleInput = page.Locator("#Task_Title");
            _dayInput = page.Locator("#duedate-day");
            _monthInput = page.Locator("#duedate-month");
            _yearInput = page.Locator("#duedate-year");
            _noteTextArea = page.Locator("#note");
            _linkKsbsButton = page.Locator("button[formaction*='/Tasks/SaveTaskAndRedirectToLinkKsbs']");
            _saveAndContinueButton = page.Locator("button[type='submit']", new() { HasText = "Save and continue" });
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(PageHeader)).ToContainTextAsync("Notes and evidence");
        }

        public async Task RefreshAsync()
        {
            await page.ReloadAsync();
        }

        public string GenerateNoteName()
        {
            return "AutoNote_" + Guid.NewGuid().ToString()[..8];
        }

        public async Task ClickAddNoteButtonAsync()
        {
            await page.Locator("a[href='/Tasks/Add']").ClickAsync();
        }

        public async Task AddNoteAsync(string title, string day, string month, string year, string noteText)
        {
            await _titleInput.FillAsync(title);
            await _dayInput.FillAsync(day);
            await _monthInput.FillAsync(month);
            await _yearInput.FillAsync(year);

            if (!string.IsNullOrEmpty(noteText))
            {
                await _noteTextArea.FillAsync(noteText);
            }

            await _saveAndContinueButton.ClickAsync();
        }

        public async Task ClickOnNoteAsync(string noteTitle)
        {
            await page.Locator($"a:has-text('{noteTitle}')").First.ClickAsync();
        }

        public async Task<bool> IsNoteAddedAsync(string noteTitle)
        {
            return await page.Locator($"//h2[text()='{noteTitle}'] | //a[contains(text(), '{noteTitle}')]").IsVisibleAsync();
        }

        public async Task EditNoteAndConfirmAsync(string originalTitle)
        {
            string updatedTitle = $"{originalTitle}_Edited";
            context["UpdatedNoteName"] = updatedTitle;

            await _titleInput.ClearAsync();
            await _titleInput.FillAsync(updatedTitle);
            await _saveAndContinueButton.ClickAsync();
        }

        public async Task DeleteNoteAndConfirmAsync()
        {
            var deleteButton = page.Locator("a:has-text('Delete this note'), button:has-text('Delete this note')").First;

            if (await deleteButton.IsVisibleAsync())
            {
                await deleteButton.ScrollIntoViewIfNeededAsync();
                await deleteButton.ClickAsync();

                var confirmDeleteButton = page.Locator("button.govuk-button--warning:has-text('Delete this note'), button:has-text('Delete this note')").First;

                await confirmDeleteButton.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
                await confirmDeleteButton.ScrollIntoViewIfNeededAsync();
                await confirmDeleteButton.ClickAsync();
            }
            else
            {
                throw new Exception("The initial 'Delete this note' link/button was not visible on the page.");
            }
        }

        public async Task CleanupNoteByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title)) return;

            try
            {
                var noteLink = page.Locator($"a:has-text('{title}')").First;
                if (await noteLink.IsVisibleAsync())
                {
                    await noteLink.ClickAsync();
                    await DeleteNoteAndConfirmAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup Warning] Could not delete note '{title}': {ex.Message}");
            }
        }
        public async Task SweepOrphanedNotesAsync()
        {
            try
            {
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                var allNotes = page.Locator("a[href*='/Tasks/Edit/']");

                while (await allNotes.CountAsync() > 0)
                {
                    var note = allNotes.First;
                    if (await note.IsVisibleAsync())
                    {
                        await note.ClickAsync();
                        await DeleteNoteAndConfirmAsync();
                        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup Warning] Note sweep encountered an issue: {ex.Message}");
            }
        }

        //public async Task SweepOrphanedNotesAsync()
        //{
        //    try
        //    {
        //        var allNotes = page.Locator("a[href*='/Tasks/Edit/']");

        //        while (await allNotes.CountAsync() > 0)
        //        {
        //            var note = allNotes.First;
        //            if (await note.IsVisibleAsync())
        //            {
        //                await note.ClickAsync();
        //                await DeleteNoteAndConfirmAsync();
        //                await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"[Cleanup Warning] Note sweep encountered an issue: {ex.Message}");
        //    }
        //}
    }
}