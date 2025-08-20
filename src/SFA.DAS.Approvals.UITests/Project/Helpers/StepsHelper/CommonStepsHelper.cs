using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    internal class CommonStepsHelper
    {
        private readonly ScenarioContext context;
        private readonly ObjectContext objectContext;

        public CommonStepsHelper(ScenarioContext _context)
        {
            context = _context;
            objectContext = context.Get<ObjectContext>();
        }

        internal async Task WaitForStatusUpdateAsync(Func<Task<string>> getStatusFunc, string expectedStatus, TimeSpan maxWaitTime)
        {
            var timeoutAt = DateTime.UtcNow + maxWaitTime;
            var pollInterval = TimeSpan.FromMinutes(1);


            while (DateTime.UtcNow < timeoutAt)
            {
                var currentStatus = await getStatusFunc();

                if (currentStatus == expectedStatus)
                {
                    objectContext.SetDebugInformation($"✅ Status updated to '{expectedStatus}'.");
                    return;
                }

                objectContext.SetDebugInformation($"⏳ Status is '{currentStatus}'. Retrying in {pollInterval.TotalSeconds} seconds...");
                await Task.Delay(pollInterval);
            }

            throw new TimeoutException($"Status did not update to '{expectedStatus}' within {maxWaitTime.TotalMinutes} minutes.");
        }

        internal async Task<bool> VerifyText(ILocator locator, string expected)
        {
            var actual = (await locator.TextContentAsync()).Trim();

            if(actual.Contains(expected.Trim(), StringComparison.OrdinalIgnoreCase)) { return true; }

            throw new Exception(MessageHelper.GetExceptionMessage("Text", expected, actual));
        }
    }
}
