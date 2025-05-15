

using System;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE;

namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.DfE
{
    [Binding]
    public class DfeFormBuilderSteps
    {
        private readonly FormBuilderPage _formBuilder;
        private readonly DfeAdminHomePage _homePage;

        public DfeFormBuilderSteps(ScenarioContext context)
        {
            _formBuilder = context.Get<FormBuilderPage>();
            _homePage = context.Get<DfeAdminHomePage>();
        }

        // [When(@"navigate to form the form management")]
        // public async Task NavigateToFromBuilder() => await _formBuilder.NavigateToFormBuilder();

        [When(@"create a new form (.*) and save the changes")]
        public async Task CreateNewForm(string formName) => await _formBuilder.CreateAndSaveNewForm(formName);

        // [Then(@"the newly created form (.*) should display with (.*) status")]
        // public async Task VerifyTheFormName(string formName, string status) => await _formBuilder.VerifyFormStatus(formName, status);

        // [Then(@"the form (.*) button (.*) label should contain (.*)")]
        // public async Task VerifyTheFormName(string formName, string order, string label) => await _formBuilder.VerifyFormButtonLabel(formName, order, label);

        // [When(@"click button (.*) for the form (.*)")]
        // public async Task clickFormButton(string order, string formName) => await _formBuilder.ClickFormButtonByOrder(formName, order);

        // [When(@"verify the form name (.*) in EditForm page")]
        // public async Task VerifyEditFormName(string formName) => await _formBuilder._editForms.VerifyFormNameShouldContain(formName);

    }
}
