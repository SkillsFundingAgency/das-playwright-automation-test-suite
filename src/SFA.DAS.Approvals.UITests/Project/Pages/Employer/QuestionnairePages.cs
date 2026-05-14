using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Text.RegularExpressions;


namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Are you setting up an apprenticeship for an existing employee?");


        internal async Task<AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage> SetApprenticeshipForAnExistingEmployee()
        {
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(context));
        }

        internal async Task<SetUpAnApprenticeshipForNewEmployeePage> SetApprenticeshipForNewEmployee()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetUpAnApprenticeshipForNewEmployeePage(context));
        }

    }

    internal class DoYouKnowWhichTrainingCourseYourLearnerWillTakePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Do you know which training course your learner will take?");


        internal async Task<WhenWillTheTrainingStartPage> Yes()
        {
            await page.Locator("[value= 'true']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WhenWillTheTrainingStartPage(context));
        }

        internal async Task<WhenWillTheTrainingStartPage> ReserveFundsAsync(string courseName)
        {
            await page.Locator("[value= 'true']").ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).FillAsync(courseName.Substring(0, 3));
            await page.GetByRole(AriaRole.Option, new() { Name = courseName }).First.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WhenWillTheTrainingStartPage(context));
        }
    }

    internal class DoYouKnowWhichCourseYourApprenticeWillTakePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Do you know which course your apprentice will take?");


        public async Task<HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage> IKnowWhichCourseMyApprenticeWillTake()
        {

            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage(context));
        }


        public async Task<SetupAnApprenticeshipDynamicHomepage> IDontKnowWhichCourseMyApprenticeWillTake()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipDynamicHomepage(context));
        }
    }

    internal class DoYouNeedToCreateAdvertForThisApprenticeship(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Do you need to create an advert for this apprenticeship?");


        public async Task<DoYouNeedToCreateAdvertForThisApprenticeship> CreateAdvertForThisApprenticeship()
        {
            await page.Locator("[value='True']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouNeedToCreateAdvertForThisApprenticeship(context));
        }


        public async Task<AddApprenticePage> DoNotCreateAdvertForThisApprenticeship()
        {
            await page.Locator("[value='False']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AddApprenticePage(context));
        }
    }

    internal class HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Have you chosen a training provider to deliver the apprenticeship training?");


        internal async Task<WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage> IChooseTrainingProvider()
        {
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage(context));
        }

        internal async Task<SetupAnApprenticeshipDynamicHomepage> IDontKnowMyTrainingProvider()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipDynamicHomepage(context));
        }
    }

    internal class WhenWillTheTrainingStartPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("When will the training start?");

        private ILocator reservationStartDate(int offset) => page.Locator($"#StartDate-{DateTime.Now.AddMonths(offset):yyyy-MM}");


        internal async Task<CheckDetailsAndReserveFundingPage> SelectReservationDate(int offset)
        {
            await reservationStartDate(offset).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new CheckDetailsAndReserveFundingPage(context));
        }

        internal async Task VerifyPreiousMonthIsNotAvailableToSelect() => await Assertions.Expect(reservationStartDate(-1)).Not.ToBeVisibleAsync();


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

        internal async Task<EmployerHomePage> SelectOptionGoToHomePage()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Go to homepage" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new EmployerHomePage(context));
        }

        internal async Task<AddApprenticePage> SelectOptionAddApprenticeDetails()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Add apprentice's details" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AddApprenticePage(context));
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