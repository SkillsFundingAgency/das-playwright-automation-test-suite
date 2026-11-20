using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class NotesStepDefinitions(ScenarioContext context)
    {
        private readonly NotesBasePage _notesBasePage = new(context);
        private readonly AppStepsHelper _appStepsHelper = new(context);

        private string _noteName;

        [When("the apprentice adds, edits, and then deletes a note")]
        public async Task WhenTheApprenticeAddsEditsAndDeletesANote()
        {
            // 1. ADD PHASE
            await _appStepsHelper.NavigateToNotesPageAsync();
            await _notesBasePage.RefreshAsync();

            _noteName = _notesBasePage.GenerateNoteName();
            context["CurrentNoteName"] = _noteName;

            // Open the add note form
            await _notesBasePage.ClickAddNoteButtonAsync();

            await _notesBasePage.AddNoteAsync(
                title: _noteName,
                day: "16",
                month: "11",
                year: "2026",
                noteText: "Automated test note description");

            await _notesBasePage.RefreshAsync();

            // 2. EDIT PHASE
            await _notesBasePage.ClickOnNoteAsync(_noteName);
            await _notesBasePage.EditNoteAndConfirmAsync(_noteName);
            await _notesBasePage.RefreshAsync();

            // 3. DELETE PHASE
            string updatedName = context["UpdatedNoteName"].ToString();
            await _notesBasePage.ClickOnNoteAsync(updatedName);
            await _notesBasePage.DeleteNoteAndConfirmAsync();
            await _notesBasePage.RefreshAsync();
        }

        [Then("the note is completely removed from the list")]
        public async Task ThenTheNoteIsCompletelyRemovedFromTheList()
        {
            string finalTargetNoteName = context.ContainsKey("UpdatedNoteName")
                ? context["UpdatedNoteName"].ToString()
                : _noteName;

            bool isPresent = await _notesBasePage.IsNoteAddedAsync(finalTargetNoteName);

            Assert.IsFalse(
                isPresent,
                $"Verification Failed: Note item '{finalTargetNoteName}' is still visible inside the UI viewport!");
        }
    }
}