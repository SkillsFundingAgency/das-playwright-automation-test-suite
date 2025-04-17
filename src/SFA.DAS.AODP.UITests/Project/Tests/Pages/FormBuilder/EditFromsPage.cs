

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class EditFromsPage(ScenarioContext context) : DfeAdminHomePage(context)
    {
        public ILocator BackButton => page.Locator(".govuk-back-link");
        public ILocator Name => page.Locator("#Title");
        public ILocator Description => page.Locator("#Description");
        public ILocator Version => page.Locator("#Version");
        public ILocator Status => page.Locator("#Status");
        public ILocator CreateNewSectionBtn => page.GetByText("Create new section");
        public ILocator ViewRoutesBtn => page.GetByText("View routes");
        public ILocator PublishFormBtn => page.GetByText("Publish form");
        public ILocator firstBtn => page.Locator("(//button[@type=\"submit\"])[1]");

        public override async Task VerifyPage() => await Task.CompletedTask;

        public async Task SaveForm() => await ClickSaveButton();
        public async Task CancelForm() => await ClickCancelButton();
        public async Task EnterFormName(string name) => await Name.FillAsync(name);
        public async Task EnterFormDescription(string description) => await Description.FillAsync(description);
        public async Task CreateSection() => await CreateNewSectionBtn.ClickAsync();
        public async Task ViewRoutes() => await ViewRoutesBtn.ClickAsync();
        public async Task ClickOnPublish() => await PublishFormBtn.ClickAsync();
        public async Task EnterSectionName(string name) => await Name.FillAsync(name);
        public async Task VerifyFormNameShouldContain(string name)
        {
            await Assertions.Expect(Name).ToHaveValueAsync(name);
        }

    }
}
