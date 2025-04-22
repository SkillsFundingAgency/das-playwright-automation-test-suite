

using System;
using SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.DfE;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class FormBuilderPage : DfeAdminStartPage

    {
        public FromsPage _forms;
        public EditFromsPage _editForms;
        public EditFromsPage _commonSteps;
        public ILocator FormBuilderPageTitle => page.Locator("//h1[.=\"View Forms\"]");

        public FormBuilderPage(ScenarioContext context) : base(context)
        {
            _forms = new(context);
            _editForms = new(context);
            _editForms = new(context);
        }

        private ILocator CreateNewFormBtn => page.Locator("//*[.=\"Create New Form\"]");

        // public override async Task VerifyPage() => await Task.CompletedTask;

        public override async Task VerifyPage() => await Assertions.Expect(FormBuilderPageTitle).ToBeVisibleAsync();

        public async Task CreateAndSaveNewForm(string formName)
        {

            string xpath = $"(//*[.=\"{formName}\"])[1]";
            bool isExist = await page.IsVisibleAsync(xpath);

            if (isExist == true)
            {
                Console.WriteLine($">>>>>>> Form {formName} found. ");
            }
            else
            {
                Console.WriteLine($">>>>>> Form {formName} NOT found and creating. ");
                await CreateNewFormBtn.ClickAsync();
                await _forms.EnterFormName(formName);
                await _forms.EnterFormDescription(formName + " Description");
                await _forms.SaveForm();
                await _forms.NavigateBack();
            }
        }
       
        // public async Task NavigateToFormBuilder() => await FormBuilderPage.ClickAsync();


        // public async Task VerifyFormStatus(string formName, string status)
        // {
        //     string locator = $"(//*[.=\"{formName}\"])[1]/following-sibling::td[1]";
        //     await Assertions.Expect(page.Locator(locator)).ToContainTextAsync(status);
        // }

        // public async Task VerifyFormButtonLabel(string formName, string order, string label)
        // {
        //     string button = $"(//*[.=\"{formName}\"])[1]/following-sibling::td[2]/*[contains(@class, \"govuk-button\")][{order}]";
        //     await Assertions.Expect(page.Locator(button)).ToContainTextAsync(label);
        // }

        // public async Task<FormBuilderPage> ClickFormButtonByOrder(string formName, string order)
        // {
        //     string button = $"(//*[.=\"{formName}\"])[1]/following-sibling::td[2]/*[contains(@class, \"govuk-button\")][{order}]";

        //     await page.Locator(button).ClickAsync();
        //     return this;
        // }

        // public async Task<FormBuilderPage> CheckLocatorText(string locator, string text)
        // {
        //     await Assertions.Expect(page.Locator($"#{locator}")).ToContainTextAsync(text);
        //     return this;
        // }

    }
}
