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
            await _appStepsHelper.NavigateToNotesPageAsync();

            _noteName = _notesBasePage.GenerateNoteName();
            context["CurrentNoteName"] = _noteName;

            await _notesBasePage.ClickAddNoteButtonAsync();

            await _notesBasePage.AddNoteAsync(
                title: _noteName,
                day: "16",
                month: "11",
                year: "2026",
                noteText: "Automated test note description");

            await _notesBasePage.ClickOnNoteAsync(_noteName);
            var updatedName = await _notesBasePage.EditNoteAndConfirmAsync(_noteName);
            context["UpdatedNoteName"] = updatedName;

            await _notesBasePage.ClickOnNoteAsync(updatedName);
            await _notesBasePage.DeleteNoteAndConfirmAsync();
        }

        [Then("the note is completely removed from the list")]
        public async Task ThenTheNoteIsCompletelyRemovedFromTheList()
        {
            var finalTargetNoteName = context.ContainsKey("UpdatedNoteName")
                ? context["UpdatedNoteName"].ToString()
                : _noteName;

            await _notesBasePage.VerifyNoteRemovedAsync(finalTargetNoteName);
        }
    }
}