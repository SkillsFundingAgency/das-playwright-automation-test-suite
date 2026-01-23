using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using System;


namespace SFA.DAS.Approvals.UITests.Project.Hooks
{
    [Binding]
    public class AfterScenarioHooks
    {
        private readonly ScenarioContext _context;
        private readonly ObjectContext _objectContext;
        private readonly FeatureContext _featureContext;

        public AfterScenarioHooks(ScenarioContext context, FeatureContext featureContext)
        {
            _context = context;
            _objectContext = context.Get<ObjectContext>();
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

        [AfterScenario(Order = 32)]
        [Scope(Tag = "providerpermissions")]
        public async Task ClearProviderRelatins()
        {
            await new DeleteProviderRelationinDbHelper(_context).DeleteProviderRelation();
        }

        [AfterScenario("cleanup-db-pymt-completion-status")]
        public async Task ClearCompletionStatus()
        { 
            CommitmentsDbSqlHelper commitmentsDbSqlHelper = _context.Get<CommitmentsDbSqlHelper>();
            var apprenticeships = _context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeships.ApprenticeDetails.ApprenticeshipId;
            await commitmentsDbSqlHelper.SetPaymentStatus(apprenticeshipId, 1);
            _objectContext.SetDebugInformation($"AfterScenario: Reset PaymentStatus to 1 for apprenticeshipId: {apprenticeshipId}");
        }


    }
}
