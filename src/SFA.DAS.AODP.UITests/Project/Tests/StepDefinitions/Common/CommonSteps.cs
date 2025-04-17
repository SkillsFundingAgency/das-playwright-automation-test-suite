

using System;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.AO;

namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.Common
{
    [Binding]
    public class CommonSteps
    {
        private readonly AodpHomePage _aodpHomePage;
        private readonly AodpLoginPage _loginPage;
        private readonly AoHomePage _aoHomePage;
        private readonly AoStartPage _aoBasePage;
        private readonly DfeAdminStartPage _dfeBasePage;
        private readonly DfeAdminHomePage _dfeHomePage;
        private readonly FormBuilderPage _formBuilder;
        private readonly DfeReviewStartPage _dfeReviewStartPage;
        private readonly NewQualificationsHomePage _newQualPage;
        private readonly ChangedQualificationsHomePage _changedQualPage;
        private readonly OutputFilePage _outputFilePage;
        public CommonSteps(ScenarioContext context)
        {
            _aodpHomePage = new(context);
            _loginPage = new(context);
            _dfeBasePage = new(context);
            _dfeHomePage = new(context);
            _aoBasePage = new(context);
            _aoHomePage = new(context);
            _formBuilder = new(context);
            _dfeReviewStartPage = new(context);
            _newQualPage = new(context);
            _changedQualPage = new(context);
            _outputFilePage = new(context);
            context.Set<AodpHomePage>(_aodpHomePage);
            context.Set<AodpLoginPage>(_loginPage);
            context.Set<DfeAdminStartPage>(_dfeBasePage);
            context.Set<DfeAdminHomePage>(_dfeHomePage);
            context.Set<AoStartPage>(_aoBasePage);
            context.Set<AoHomePage>(_aoHomePage);
            context.Set<FormBuilderPage>(_formBuilder);
            context.Set<DfeReviewStartPage>(_dfeReviewStartPage);
            context.Set<NewQualificationsHomePage>(_newQualPage);
            context.Set<ChangedQualificationsHomePage>(_changedQualPage);
            context.Set<OutputFilePage>(_outputFilePage);
        }

        [When(@"Click on (.*) in the (.*)")]
        public async Task ClickBtn(string webElement, string page) => await ClickBtnOnPage(webElement, page);

        [When(@"Enter text (.*) in (.*) field on (.*)")]
        public async Task EnterText(string text, string webElement, string page) => await EnterTextOnPage(text, webElement, page);

        [When(@"Click the button (.*) following text (.*)")]
        public async Task ClickBtnFollowingText(string btnText, string followingText) => await ClickBtnOnPageFollowingText(btnText, followingText);

        [Then(@"User should able to see (.*)")]
        public async Task VerifyTheUserCanSeeThePage(string page) => await VerifyPage(page);



        [Then(@"Verify the (.*) exists in the (.*)")]
        public async Task VerifyTheFieldExists(string webElement, string page) => await VerifyTheWebElementExistsOnPage(page, webElement);

        [Then(@"Verify the (.*) exists in (.*) containing (.*)")]
        public async Task VerifyTheFieldExistsContainText(string webElement, string page, string text) => await VerifyTheWebElementExistsOnPageContainText(page, webElement, text);


        [Then(@"Verify the (.*) in (.*) it should contain (.*)")]
        public async Task VerifyTheStartPageVisibility(string webElement, string page, string text) => await VerifyTheWebElementTextOnPage(page, webElement, text);

        [Then(@"Verify the text (.*) following to (.*)")]
        public async Task VerifyTextBtnFollowingText(string targetText, string followingText) => await VeifyOnPageFollowingText(targetText, followingText);

        private async Task ClickBtnOnPage(string webElementName, string pageName)
        {
            ILocator locator = await GetElement(pageName, webElementName);
            await locator.ClickAsync();
        }

        private async Task EnterTextOnPage(string text, string webElementName, string pageName)
        {
            ILocator locator = await GetElement(pageName, webElementName);
            await locator.FillAsync(text);
        }

        private async Task ClickBtnOnPageFollowingText(string btnText, string followingText)
        {
            string xpath = $"(//*[.=\"{followingText}\"]/following::*[contains(@class, 'button') and .=\"{btnText}\"])[1]";
            ILocator locator = await _aodpHomePage.GetLocator(xpath);

            await locator.ClickAsync();
        }
        private async Task VeifyOnPageFollowingText(string targetText, string followingText)
        {
            string xpath = $"(//*[.=\"{followingText}\"]/following::*)[1]";
            ILocator locator = await _aodpHomePage.GetLocator(xpath);

            await Assertions.Expect(locator).ToContainTextAsync(targetText);
        }

        private async Task VerifyTheWebElementTextOnPage(string pageName, string webElementName, string text)
        {
            ILocator locator = await GetElement(pageName, webElementName);
            await Assertions.Expect(locator).ToContainTextAsync(text);
            Console.WriteLine($"{pageName} found locator {locator.TextContentAsync()}");
        }

        private async Task<bool> VerifyTheWebElementExistsOnPage(string pageName, string webElementName)
        {

            ILocator locator = await GetElement(pageName, webElementName);
            await Assertions.Expect(locator).ToBeVisibleAsync();
            Console.WriteLine($"{webElementName} not found in the {pageName}");
            return await locator.IsVisibleAsync();
        }

        private async Task<bool> VerifyTheWebElementExistsOnPageContainText(string pageName, string webElementName, string text)
        {

            string xpath = $"(//*[.=\"{text}\"])[1]";
            ILocator locator = await _aodpHomePage.GetLocator(xpath);
            await Assertions.Expect(locator).ToContainTextAsync(text);

            Console.WriteLine($"Locator not found {webElementName} in the page {pageName} with text {text}");
            return await locator.IsVisibleAsync();
        }

        private async Task VerifyPage(string pageName)
        {
            AodpLandingPage page = await GetPage(pageName);
            await page.VerifyPage();
        }


        private async Task<ILocator> GetElement(string pageName, string webElementName)
        {
            ILocator webElement = null;

            AodpLandingPage page = await GetPage(pageName);

            if (page is FormBuilderPage)
            {
                webElement = webElementName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
                {
                    "name" => _formBuilder._forms.Name,
                    _ => throw new ArgumentException($"Web Element '{webElementName}' not found in the {pageName}"),
                };
            }
            else if (page is EditFromsPage)
            {
                webElement = webElementName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
                {
                    "first button" => _formBuilder._editForms.firstBtn,
                    "back to forms button" => _formBuilder._editForms.BackButton,
                    _ => throw new ArgumentException($"Web Element '{webElementName}' not found in the {pageName}"),
                };
            }
            else if (page is DfeAdminHomePage)
            {
                webElement = webElementName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
                {
                    "form builder button" => _formBuilder.FormBuilderButton,
                    _ => throw new ArgumentException($"Web Element '{webElementName}' not found in the {pageName}"),
                };
            }
            else if (page is NewQualificationsHomePage)
            {
                webElement = webElementName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
                {
                    "qualification name" => _newQualPage.QualificationName,
                    "message for no matching qualication" => _newQualPage.NoQualificationsError,
                    "search button" => _newQualPage.SearchBtn,
                    _ => throw new ArgumentException($"Web Element '{webElementName}' not found in the {pageName}"),
                };
            }
            else if (page is ChangedQualificationsHomePage)
            {
                webElement = webElementName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
                {
                    "qualification name" => _changedQualPage.QualificationName,
                    "message for no matching qualication" => _changedQualPage.NoQualificationsError,
                    "search button" => _changedQualPage.SearchBtn,
                    _ => throw new ArgumentException($"Web Element '{webElementName}' not found in the {pageName}"),
                };
            }
            else if (page is AodpHomePage)
            {
                webElement = webElementName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
                {
                    "review ao applications option" => _aodpHomePage.ApplicationsReviewBtn,
                    "review new qualifications option" => _aodpHomePage.NewQualificationsBtn,
                    "review changed qualifications option" => _aodpHomePage.ChangedQualificationsBtn,
                    "import data option" => _aodpHomePage.ImportDataBtn,
                    "form management option" => _aodpHomePage.FormsManagementBtn,
                    "output file option" => _aodpHomePage.OutputFileBtn,
                    "continue button" => _aodpHomePage.ContinueBtn,
                    _ => throw new ArgumentException($"Web Element '{webElementName}' not found in the {pageName}"),
                };
            }

            return webElement;
        }


#pragma warning disable CS1998
        private async Task<AodpLandingPage> GetPage(string pageName)
        {
            return pageName.ToLower() switch  // Convert to lowercase to handle case-insensitive matching
            {
                "aodp home page" => _aodpHomePage,
                "dfe base page" => _dfeBasePage,
                "dfe home page" => _dfeHomePage,
                "forms page" => _formBuilder._forms,
                "edit forms page" => _formBuilder._editForms,
                "ao base page" => _aoBasePage,
                "ao home page" => _aoHomePage,
                "dfe reviewer start page" => _dfeReviewStartPage,
                "dfe new qualifications page" => _newQualPage,
                "form builder page" => _formBuilder,
                "dfe changed qualifications page" => _changedQualPage,
                "output file page" => _outputFilePage,
                _ => throw new ArgumentException($"Page '{pageName}' not found"),
            };
        }
#pragma warning restore CS1998

    }
}
