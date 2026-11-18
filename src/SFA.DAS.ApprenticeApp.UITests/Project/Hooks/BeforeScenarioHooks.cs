using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Hooks
{
    [Binding]
    public class BeforeScenarioHooks(ScenarioContext context)
    {

        [BeforeScenario(Order = 10)]
        public async Task AppSetupHelpers() => await context.SetApprenticeAccountsPortalUser([ context.Get<ConfigSection>().GetConfigSection<ApprenticeAppUser>()]);

        [AfterScenario(Order = 10)]
        public async Task CleanUpTestData()
        {
            try
            {
                var notesBasePage = new NotesBasePage(context);
                var appStepsHelper = new AppStepsHelper(context);

                await appStepsHelper.NavigateToNotesPageAsync();

                string noteToDelete = null;

                if (context.ContainsKey("UpdatedNoteName"))
                    noteToDelete = context["UpdatedNoteName"].ToString();
                else if (context.ContainsKey("CurrentNoteName"))
                    noteToDelete = context["CurrentNoteName"].ToString();

                if (!string.IsNullOrEmpty(noteToDelete))
                {
                    Console.WriteLine($"[CleanUp] Targeting main scenario note: '{noteToDelete}'");
                    await notesBasePage.CleanupNoteByTitleAsync(noteToDelete);
                }

                await notesBasePage.SweepOrphanedNotesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CleanUp Warning] Automated sweeper encountered an issue: {ex.Message}");
            }
        }
    }
}