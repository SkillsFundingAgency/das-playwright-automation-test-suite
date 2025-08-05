using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using System;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    public class CohortReferenceHelper
    {
        private readonly ObjectContext _objectContext;
        private readonly ApprenticeDataHelper _dataHelper;
        private readonly CommitmentsDbSqlHelper _commitmentsSqlDataHelper;

        public CohortReferenceHelper(ScenarioContext context)
        {
            _objectContext = context.Get<ObjectContext>();
            _dataHelper = context.Get<ApprenticeDataHelper>();
            _commitmentsSqlDataHelper = new CommitmentsDbSqlHelper(_objectContext, context.Get<DbConfig>());
        }

        public void SetCohortReference(string cohortReference) => _objectContext.SetCohortReference(cohortReference);

        public void UpdateCohortReference(string cohortReference) => _objectContext.UpdateCohortReference(cohortReference);

        //public void UpdateCohortReference()
        //{
        //    string ULN = Convert.ToString(_dataHelper.ApprenticeULN);

        //    var cohortRef = _commitmentsSqlDataHelper.GetNewcohortReference(ULN);
        //    UpdateCohortReference(cohortRef);
        //}
    }
}