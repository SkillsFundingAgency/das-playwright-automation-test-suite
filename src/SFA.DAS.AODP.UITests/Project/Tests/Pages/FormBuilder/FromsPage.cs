

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class FromsPage(ScenarioContext context) : DfeAdminHomePage(context)
    {
        public ILocator Name => page.Locator("#Name");
        public ILocator Description => page.Locator("#Description");

        public override async Task VerifyPage() => await Task.CompletedTask;

        public async Task SaveForm() => await ClickSaveButton();
        public async Task EnterFormName(string name) => await Name.FillAsync(name);
        public async Task EnterFormDescription(string description) => await Description.FillAsync(description);

    }
}
