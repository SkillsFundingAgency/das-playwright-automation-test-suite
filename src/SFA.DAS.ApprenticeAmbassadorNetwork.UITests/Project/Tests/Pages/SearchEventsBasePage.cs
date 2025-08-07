using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages
{
    public abstract class SearchEventsBasePage(ScenarioContext context) : AanBasePage(context)
    {
        private static string Published => "Published";
        private static string New => "New";
        private static string Cancelled => "Cancelled";
        private static string Active => "Active";
        private static string TrainingEvent => "Training event";
        private static string InPerson => "In person";
        private static string Online => "Online";
        private static string Hybrid => "Hybrid";
        private static string London => "London";
        private static string Apprentice => "Apprentice";
        private static string Employer => "Employer";
        private static string Regionalchair => "Regional chair";

        private static string EventTitle => (".das-search-results__heading");
        private static string FromDateField => ("fromdate");
        private static string ToDateField => ("todate");

        private static readonly string SearchResultsHeading = (".das-search-results__heading");

        private static readonly string BodyText = (".govuk-body");

        private async Task VerifySelectedFilter(string x) => await Assertions.Expect(page.Locator("#events-filter, #directory-filter")).ToContainTextAsync(x);

        public async Task FilterEventFromTomorrow() => await FilterEventByDate(null);

        public async Task FilterEventByOneMonth() => await FilterEventByDate(DateTime.Now.AddDays(30));

        public async Task SelectOrderByClosest()
        {
            await page.GetByLabel("Sort by").SelectOptionAsync(new[] { "closest" });
        }

        public async Task FilterEventsByLocation(string location, int radius)
        {
            await EnterLocation(location);
            await EnterRadius(radius);
            await ApplyFilter();
        }

        public async Task EnterKeywordFilter(string keyword)
        {
            await EnterKeyword(keyword);
        }

        protected async Task FilterEventBy(DateTime startDate, DateTime endDate, string type, string region)
        {
            await EnterDate(startDate, FromDateField);

            await EnterDate(endDate, ToDateField);

            await page.GetByRole(AriaRole.Checkbox, new() { Name = type }).CheckAsync();

            await page.GetByRole(AriaRole.Checkbox, new() { Name = region }).CheckAsync();

            await ApplyFilter();
        }

        protected async Task FilterEventByEventStatus_Published() => await ApplyFilter(Published);
        protected async Task FilterAmbassadorByStatus_New() => await ApplyFilter(New);

        protected async Task FilterEventByEventStatus_Cancelled() => await ApplyFilter(Cancelled);
        protected async Task FilterEventByAmbassadorStatus_Active() => await ApplyFilter(Active);

        protected async Task FilterEventByEventType_TrainingEvent() => await ApplyFilter(TrainingEvent);

        protected async Task FilterEventByEventFormat_InPerson() => await ApplyFilter(InPerson);

        protected async Task FilterEventByEventFormat_Online() => await ApplyFilter(Online);

        protected async Task FilterEventByEventFormat_Hybrid() => await ApplyFilter(Hybrid);

        protected async Task FilterEventByEventRegion_London() => await ApplyFilter(London);

        protected async Task FilterByRole_Apprentice() => await ApplyFilter(Apprentice);

        protected async Task FilterByRole_Employer() => await ApplyFilter(Employer);

        protected async Task FilterByRole_Regionalchair() => await ApplyFilter(Regionalchair);

        protected async Task ClearAllFilters() => await page.GetByRole(AriaRole.Link, new() { Name = "Clear  the selected filters" }).ClickAsync();

        protected async Task VerifyEventStatus_Published_Filter() => await VerifySelectedFilter(Published);
        protected async Task VerifyEventStatus_Cancelled_Filter() => await VerifySelectedFilter(Cancelled);
        protected async Task VerifyAMbassadorStatus_Published_New() => await VerifySelectedFilter(New);
        protected async Task VerifyAMbassadorStatus_Published_Active() => await VerifySelectedFilter(Active);

        protected async Task VerifyEventType_TrainingEvent_Filter() => await VerifySelectedFilter(TrainingEvent);

        protected async Task VerifyEventFormat_Inperson_Filter() => await VerifySelectedFilter(InPerson);
        protected async Task VerifyEventFormat_Online_Filter() => await VerifySelectedFilter(Online);
        protected async Task VerifyEventFormat_Hybrid_Filter() => await VerifySelectedFilter(Hybrid);

        protected async Task VerifyEventRegion_London_Filter() => await VerifySelectedFilter(London);
        protected async Task VerifyRole_Apprentice_Filter() => await VerifySelectedFilter(Apprentice);
        protected async Task VerifyRole_Employer_Filter() => await VerifySelectedFilter(Employer);

        protected async Task VerifyRole_Regionalchair_Filter() => await VerifySelectedFilter(Regionalchair);

        private async Task FilterEventByDate(DateTime? endDate)
        {
            await EnterDate(DateTime.Now.AddDays(1), FromDateField);

            if (endDate != null) await EnterDate(endDate, ToDateField);

            await ApplyFilter();
        }

        private async Task EnterDate(DateTime? date, string datestring)
        {
            string formattedDate = date?.ToString(DateFormat);

            if (datestring == FromDateField)
            {
                await page.GetByRole(AriaRole.Textbox, new() { Name = "From date" }).FillAsync(formattedDate);
            }

            if (datestring == ToDateField)
            {
                await page.GetByRole(AriaRole.Textbox, new() { Name = "To date" }).FillAsync(formattedDate);
            }
        }

        private async Task EnterKeyword(string keyword)
        {
            await page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter events or keywords" }).ClearAsync();

            await page.GetByRole(AriaRole.Searchbox, new() { Name = "Enter events or keywords" }).FillAsync(keyword);
        }

        private async Task EnterLocation(string location)
        {
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Enter a city, town or postcode" }).ClearAsync();

            await page.GetByRole(AriaRole.Combobox, new() { Name = "Enter a city, town or postcode" }).FillAsync(location);

            await page.GetByRole(AriaRole.Option, new() { Name = location }).ClickAsync();
        }

        private async Task EnterRadius(int radius)
        {
            await page.Locator("#Radius").SelectOptionAsync([$"{radius}"]);
        }

        private async Task ApplyFilter(string x)
        {
            await page.GetByRole(AriaRole.Checkbox, new() { Name = x }).CheckAsync();

            await ApplyFilter();
        }

        public async Task ApplyFilter() => await page.Locator("#filters-submit").ClickAsync();

        public async Task<List<NetworkEventSearchResult>> GetSearchResults()
        {
            var index = 0;
            var list = await page.Locator(EventTitle).AllTextContentsAsync();

            return list
                .Select(x => new NetworkEventSearchResult
                {
                    EventTitle = x,
                    Index = index++
                })
                .ToList();
        }

        protected async Task<bool> HasNextPageLink()
        {
            return await page.GetByRole(AriaRole.Link, new() { Name = "Next »" }).IsVisibleAsync();
        }

        protected async Task ClickNextPageLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Next »" }).ClickAsync();
        }

        public async Task ClickNextPage()
        {
            await ClickNextPageLink();
        }

        public async Task<bool> HasNextPage()
        {
            return await HasNextPageLink();
        }

        public async Task VerifyHeadingText(string expectedText)
        {
            await Assertions.Expect(page.Locator(SearchResultsHeading).First).ToContainTextAsync(expectedText);
        }

        public async Task VerifyBodyText(string expectedText)
        {
            await Assertions.Expect(page.Locator(BodyText).First).ToContainTextAsync(expectedText);
        }
    }
}
