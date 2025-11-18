using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class TransformationSteps(ScenarioContext _)
    {
        
        /*
        [StepArgumentTransformation]
        public ApprenticeRequests TransformToApprenticeRequests(string input)
        {
            //context.GetValue<ObjectContext>().SetDebugInformation(input);
            if (Enum.TryParse<ApprenticeRequests>(input, ignoreCase: true, out var result))
                return result;

            throw new ArgumentException($"Invalid ApprenticeRequests value: '{input}'");
        }
        */
    }
}
