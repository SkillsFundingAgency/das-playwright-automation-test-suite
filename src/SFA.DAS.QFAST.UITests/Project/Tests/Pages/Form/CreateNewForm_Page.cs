using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form
{
    public class CreateNewForm_Page(ScenarioContext context) : BasePage(context)
    {
        protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();
        protected readonly EditForm_Page editForm_Page = new(context);
        protected readonly CreateSection_Page createSection_Page = new(context);
        protected readonly EditSection_Page editSection_Page = new(context);
        protected readonly CreatePage_Page createPage_Page = new(context);
        protected readonly EditPage_Page editPage_Page = new(context);  
        protected readonly CreateQuestion_Page createQuestion_Page = new(context);
        protected readonly EditQuestion_Page editQuestion_Page = new(context);  

        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create new form" })).ToBeVisibleAsync();
        
        public async Task CreateForm()
        {
            await VerifyErrorMessageForEmptyFormNameAndDescription();
            await EnterFormAndDescripitonDetail(); 
            await editForm_Page.ValidateStatusAsDraftAndClickOnCreateNewSection();
            await createSection_Page.EnterSectionName();
            await editSection_Page.CreateNewPage();
            await createPage_Page.EnterPageDetails();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterFirstQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterSecondQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterThirdQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterFourthQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterFifthQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterSixthQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.CreateNewQuestion();
            await createQuestion_Page.EnterSeventhQuestionDetails();
            await editQuestion_Page.SaveQuestion();
            await editPage_Page.GoToSection();
            await editSection_Page.GoToForm();
            await editForm_Page.PublishForm();
            await editForm_Page.GoToForms();
        }

        public async Task<CreateNewForm_Page> VerifyErrorMessageForEmptyFormNameAndDescription()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            var formError = page.Locator("a[href='#Name']");
            var descriptionError = page.Locator("a[href='#Description']");
            await Assertions.Expect(formError).ToContainTextAsync("The Name field is required.");
            await Assertions.Expect(descriptionError).ToContainTextAsync("The Description field is required.");
            return await VerifyPageAsync(() => new CreateNewForm_Page(context));
        }

        public async Task<EditForm_Page> EnterFormAndDescripitonDetail()
        {
            await page.Locator("#Name").FillAsync(_qfastDataHelpers.FormName);
            await page.Locator("#Description").FillAsync(_qfastDataHelpers.FormDescription);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            return await VerifyPageAsync(() => new EditForm_Page(context));
        }



    }
}
