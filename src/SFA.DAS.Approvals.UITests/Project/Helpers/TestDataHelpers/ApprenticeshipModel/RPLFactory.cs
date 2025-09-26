using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel
{
    internal class RPLFactory : IRPLFactory
    {
        public async Task<RPL> CreateRPLAsync()
        {
            RPL rpl = new RPL();

            rpl.RecognisePriorLearning = "Yes";
            rpl.TrainingTotalHours = 1500;
            rpl.TrainingHoursReduction = 300;
            rpl.IsDurationReducedByRPL = true;
            rpl.DurationReducedBy = 3;
            rpl.PriceReducedBy = 500;

            await Task.Delay(100);

            return rpl;
        }


    }
}
