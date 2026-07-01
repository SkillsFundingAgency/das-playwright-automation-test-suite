using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Common
{
    internal class ChangeHistoryPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private readonly string learnerName;

        internal ChangeHistoryPage(ScenarioContext context, string learnerName) : base(context)
        {
            this.context = context;
            this.learnerName = learnerName;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync($"Change history for {learnerName}");
        }

        public async Task AssertChangeHistoryRow(DateTime expectedDate, string expectedChangeMade, string expectedDecision)
        {
            var expectedDateString = expectedDate.ToString("d MMM yyyy");
                
            // Locate the row by matching all three expected values
            var row = page.Locator("tr", new() { HasTextString = expectedDateString });
            
            var dateCell = row.Locator("td[data-label='Date of changes']");
            await Assertions.Expect(dateCell).ToHaveTextAsync(expectedDateString);
           
            var changeCell = row.Locator("td[data-label='Change made ']");
            await Assertions.Expect(changeCell).ToContainTextAsync(expectedChangeMade);
            
            var decisionCell = row.Locator("td[data-label='Decision'] strong");
            await Assertions.Expect(decisionCell).ToHaveTextAsync(expectedDecision);
        }


    }
}
