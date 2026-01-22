using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.Text.RegularExpressions;


namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Are you setting up an apprenticeship for an existing employee?");


        internal async Task<AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage> Yes()
        {
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(context));
        }

        internal async Task<SetUpAnApprenticeshipForNewEmployeePage> No()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetUpAnApprenticeshipForNewEmployeePage(context));
        }

    }

    internal class DoYouKnowWhichApprenticeshipTrainingYourApprenticeWillTakePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Do you know which apprenticeship training your apprentice will take?");


        internal async Task<WhenWillTheApprenticeStartTheirApprenticeTraining> Yes()
        {
            await page.Locator("[value= 'true']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WhenWillTheApprenticeStartTheirApprenticeTraining(context));
        }

        internal async Task<WhenWillTheApprenticeStartTheirApprenticeTraining> ReserveFundsAsync(string courseName)
        {
            await page.Locator("[value= 'true']").ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).FillAsync(courseName.Substring(0, 3));
            await page.GetByRole(AriaRole.Option, new() { Name = courseName }).First.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WhenWillTheApprenticeStartTheirApprenticeTraining(context));
        }
    }

    internal class DoYouKnowWhichCourseYourApprenticeWillTakePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Do you know which course your apprentice will take?");


        public async Task<HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage> Yes()
        {

            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage(context));
        }


        public async Task<SetupAnApprenticeshipDynamicHomepage> No()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipDynamicHomepage(context));
        }
    }

    internal class DoYouNeedToCreateAdvertForThisApprenticeship(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Do you need to create an advert for this apprenticeship?");


        public async Task<DoYouNeedToCreateAdvertForThisApprenticeship> Yes()
        {
            await page.Locator("[value='True']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouNeedToCreateAdvertForThisApprenticeship(context));
        }


        public async Task<AddApprenticePage> No()
        {
            await page.Locator("[value='False']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AddApprenticePage(context));
        }
    }

    internal class HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Have you chosen a training provider to deliver the apprenticeship training?");


        internal async Task<WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage> Yes()
        {
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage(context));
        }

        internal async Task<SetupAnApprenticeshipDynamicHomepage> No()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipDynamicHomepage(context));
        }
    }

    internal class WhenWillTheApprenticeStartTheirApprenticeTraining(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("When will the apprentice start their apprenticeship training?");

        private ILocator alreadyStarted => page.Locator("#StartDate-alreadyStarted");

        internal async Task<ConfirmYourReservationPage> SelectAlreadyStartedDate()
        {
            await alreadyStarted.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ConfirmYourReservationPage(context));
        }

    }

    internal class WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Will the apprenticeship training start in the next 6 months?");


        internal async Task<AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage> StartInSixMonths()
        {

            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(context));
        }

        internal async Task<SetupAnApprenticeshipDynamicHomepage> DontStartInSixMonths()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipDynamicHomepage(context));
        }

        internal async Task<SetupAnApprenticeshipDynamicHomepage> DontKnowWhenItStarts()
        {
            await page.Locator("[value= 'Unknown']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipDynamicHomepage(context));
        }
    }

    internal class YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-panel__title")).ToContainTextAsync("You have successfully reserved funding for apprenticeship training");
        }

        internal async Task<EmployerHomePage> SelectGoToHomePageAndContinue()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Go to homepage" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new EmployerHomePage(context));

        }


        internal async Task GetReservationIdFromUrl(Apprenticeship apprenticeship)
        {
            var url = this.page.Url;
            var match = Regex.Match(url, @"reservations/(?<guid>[a-fA-F0-9\-]{36})");
            string reservationsId = match.Success ? match.Groups["guid"].Value : null;

            apprenticeship.ReservationID = reservationsId;

            await Task.Delay(100);
            context.Set(apprenticeship, "Apprenticeship");
        }
    }
}