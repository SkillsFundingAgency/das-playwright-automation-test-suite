using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Approvals.UITests.Project.Hooks
{
    [Binding]
    public class AfterScenarioHooks
    {
        private readonly ScenarioContext _context;
        private readonly FeatureContext _featureContext;

        public AfterScenarioHooks(ScenarioContext context, FeatureContext featureContext)
        {
            _context = context;
            _featureContext = featureContext;
        }

        [AfterScenario(Order = 31)]
        public void SaveScenarioContextInFeatureContext()
        {
            if (_featureContext.FeatureInfo.Tags.Contains("linkedScenarios"))
            {
                _featureContext["ResultOfPreviousScenario"] = _context.ScenarioExecutionStatus;

                if (_featureContext.ContainsKey("ScenarioContextofPreviousScenario"))
                    _featureContext["ScenarioContextofPreviousScenario"] = _context;
                else
                    _featureContext.Add("ScenarioContextofPreviousScenario", _context);
            }
        }
    }
}
