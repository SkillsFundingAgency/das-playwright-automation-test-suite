using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Approvals.UITests.Project.Hooks
{
    [Binding]
    public class AfterScenarioHooks(ScenarioContext context, FeatureContext featureContext)
    {

        private readonly ObjectContext _objectcontext = context.Get<ObjectContext>();

        [AfterScenario(Order = 31)]
        public void SaveScenarioContextInFeatureContext()
        {
            if (featureContext.FeatureInfo.Tags.Contains("linkedScenarios"))
            {
                featureContext["ResultOfPreviousScenario"] = context.ScenarioExecutionStatus;

                featureContext.TryAdd("ScenarioContextofPreviousScenario", context);
            }
            
        }


    }
}
