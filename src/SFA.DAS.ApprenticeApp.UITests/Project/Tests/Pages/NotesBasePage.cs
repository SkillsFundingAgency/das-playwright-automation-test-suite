namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class NotesBasePage(ScenarioContext context) : AppBasePage(context)
    {
        private ILocator PageHeader => page.Locator("h1.govuk-heading-xl");
        private ILocator TitleInput => page.GetByLabel("Title");
        private ILocator DayInput => page.Locator("#duedate-day");
        private ILocator MonthInput => page.Locator("#duedate-month");
        private ILocator YearInput => page.Locator("#duedate-year");
        private ILocator NoteTextArea => page.GetByLabel("Note");
        private ILocator SaveAndContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" });
        private ILocator AddNoteLink => page.GetByRole(AriaRole.Link, new() { Name = "Add note" });
        private ILocator AllNotes => page.Locator("a[href*='/Tasks/Edit/']");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(PageHeader).ToContainTextAsync("Notes and evidence");
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
            await AddNoteLink.ClickAsync();
        }

        public async Task AddNoteAsync(string title, string day, string month, string year, string noteText)
        {
            await TitleInput.FillAsync(title);
            await DayInput.FillAsync(day);
            await MonthInput.FillAsync(month);
            await YearInput.FillAsync(year);

            if (!string.IsNullOrEmpty(noteText))
            {
                await NoteTextArea.FillAsync(noteText);
            }

            await SaveAndContinueButton.ClickAsync();
        }

        public async Task ClickOnNoteAsync(string noteTitle)
        {
            await page.GetByRole(AriaRole.Link, new() { Name = noteTitle, Exact = true }).ClickAsync();
        }

        public async Task VerifyNoteRemovedAsync(string noteTitle)
        {
            await Assertions.Expect(page.GetByText(noteTitle, new() { Exact = true })).ToBeHiddenAsync();
        }

        public async Task<string> EditNoteAndConfirmAsync(string originalTitle)
        {
            var updatedTitle = $"{originalTitle}_Edited";

            await TitleInput.ClearAsync();
            await TitleInput.FillAsync(updatedTitle);
            await SaveAndContinueButton.ClickAsync();

            return updatedTitle;
        }

        public async Task DeleteNoteAndConfirmAsync()
        {
            var deleteButton = page.GetByRole(AriaRole.Link, new() { Name = "Delete this note" });

            await Assertions.Expect(deleteButton).ToBeVisibleAsync();
            await deleteButton.ClickAsync();

            var confirmDeleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete this note", Exact = true });

            await Assertions.Expect(confirmDeleteButton).ToBeVisibleAsync();
            await confirmDeleteButton.ClickAsync();
        }

        public async Task CleanupNoteByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return;
            }

            try
            {
                var noteLink = page.GetByRole(AriaRole.Link, new() { Name = title, Exact = true });

                if (await noteLink.IsVisibleAsync())
                {
                    await noteLink.ClickAsync();
                    await DeleteNoteAndConfirmAsync();
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"[Cleanup Warning] Could not delete note '{title}': {ex.Message}");
            }
        }
        public async Task SweepOrphanedNotesAsync()
        {
            try
            {
                await VerifyPage();

                while (await AllNotes.CountAsync() > 0)
                {
                    var note = AllNotes.First;

                    if (!await note.IsVisibleAsync())
                    {
                        break;
                    }

                    await note.ClickAsync();
                    await DeleteNoteAndConfirmAsync();
                    await VerifyPage();
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"[Cleanup Warning] Note sweep encountered an issue: {ex.Message}");
            }
        }
    }
}