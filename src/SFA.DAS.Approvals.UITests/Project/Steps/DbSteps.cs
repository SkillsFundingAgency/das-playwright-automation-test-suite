using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class DbSteps
    {
        protected readonly ScenarioContext context;
        protected readonly ObjectContext objectContext; 
        private readonly AccountsDbSqlHelper accountsDbSqlHelper;
        private readonly CommitmentsDbSqlHelper commitmentsDbSqlHelper;
        private readonly LearningDbSqlHelper learningDbSqlHelper;
        private readonly LearnerDataDbSqlHelper learnerDataDbSqlHelper;
        private List<Apprenticeship> listOfApprenticeship;


        public DbSteps(ScenarioContext context)
        {
            this.context = context;
            objectContext = context.Get<ObjectContext>();
            accountsDbSqlHelper = context.Get<AccountsDbSqlHelper>();
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
            learningDbSqlHelper = context.Get<LearningDbSqlHelper>();
            learnerDataDbSqlHelper = context.Get<LearnerDataDbSqlHelper>();
        }

        [Given("A live apprentice record exists for an apprentice on Foundation level course")]
        public async Task GivenALiveApprenticeRecordExistsForAnApprenticeOnFoundationLevelCourse()
        {
            listOfApprenticeship = new List<Apprenticeship>();

            //GET Apprenticeship details from the database
            var ukprn = Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn);
            EasAccountUser employerUser = context.GetUser<LevyUser>();

            var accountLegalEntityId = Convert.ToInt32(await context.Get<AccountsDbSqlHelper>().GetAccountLegalEntityId(employerUser.Username, employerUser.OrganisationName[..3] + "%"));
            var details = await commitmentsDbSqlHelper.GetEditableApprenticeDetails(ukprn, accountLegalEntityId);

            var uln = details[0].ToString();
            var firstName = details[1].ToString();
            var LastName = details[2].ToString();
            var dob = Convert.ToDateTime(details[3].ToString());

            //SET Apprenticeship details in the context
            Apprenticeship apprenticeship = new Apprenticeship();

            apprenticeship.UKPRN = ukprn;
            apprenticeship.EmployerDetails.EmployerType = EmployerType.Levy;
            apprenticeship.EmployerDetails.Email = employerUser.Username;
            apprenticeship.EmployerDetails.EmployerName = employerUser.OrganisationName;
            apprenticeship.ApprenticeDetails.ULN = uln;
            apprenticeship.ApprenticeDetails.FirstName = firstName;
            apprenticeship.ApprenticeDetails.LastName = LastName;
            apprenticeship.ApprenticeDetails.DateOfBirth = dob;

            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship);

        }

        [Then("a record is created in LearnerData Db for each learner")]
        public async Task ThenARecordIsCreatedInLearnerDataDbForEachLearner()
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>();
            
            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                await Task.Delay(1000);
                var learnerDataId = await learnerDataDbSqlHelper.GetLearnerDataId(uln);
                objectContext.SetDebugInformation($"[{learnerDataId} found in learnerData db for ULN: {uln}]");
                Assert.IsNotNull(learnerDataId, $"No record found in LearnerData db for ULN: {uln}");
                apprenticeship.ApprenticeDetails.LearnerDataId = Convert.ToInt32(learnerDataId);
                await Task.Delay(100);
                context.Set(apprenticeship, "Apprenticeship");
                objectContext.SetDebugInformation($"[{learnerDataId} set as learnerDataId for ULN: {uln}]");
            }

        }


        [Then("Commitments Db is updated with respective LearnerData Id")]
        public async Task ThenCommitmentsDbIsUpdatedWithRespectiveLearnerDataId()
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                var learnerDataIdExpected = apprenticeship.ApprenticeDetails.LearnerDataId;
                var learnerDataIdActual = await commitmentsDbSqlHelper.GetValueFromApprenticeshipTable("LearnerDataId", uln);
                Assert.AreEqual(learnerDataIdExpected.ToString(), learnerDataIdActual, $"[LearnerDataId] from Commitments db ({learnerDataIdActual}) does not match with [Id] in LearnerData db ({learnerDataIdExpected})");
                var apprenticehipId = await commitmentsDbSqlHelper.GetValueFromApprenticeshipTable("Id", uln);
                apprenticeship.ApprenticeDetails.ApprenticeshipId = Convert.ToInt32(apprenticehipId);
                await Task.Delay(100);
                context.Set(apprenticeship, "Apprenticeship");
                objectContext.SetDebugInformation($"[{apprenticehipId} set as AprenticeshipID for ULN: {uln}]");
            }
            
        }

        [Then("LearnerData Db is updated with respective Apprenticeship Id")]
        public async Task ThenLearnerDataDbIsUpdatedWithRespectiveApprenticeshipId()
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                var learnerDataId = apprenticeship.ApprenticeDetails.LearnerDataId;
                var apprenticeshipIdExpected = apprenticeship.ApprenticeDetails.ApprenticeshipId;
                var apprenticeshipIdActual = await learnerDataDbSqlHelper.GetApprenticeshipIdLinkedWithLearnerData(learnerDataId);
                Assert.AreEqual(apprenticeshipIdExpected.ToString(), apprenticeshipIdActual, $"[Id] from LearnerData db ({apprenticeshipIdActual}) does not match with [LearnerDataId] in Apprenticeship > Commitments db ({apprenticeshipIdExpected})");
            }

        }

        [Then("Learning Db is updated with respective Apprenticeship Id")]
        public async Task ThenLearningDbIsUpdatedWithRespectiveApprenticeshipId()
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
                var result = await learningDbSqlHelper.CheckIfApprenticeshipRecordCreatedInLearningDb(apprenticeshipId, uln);
                Assert.IsNotNull(result, $"Apprenticeship record not found in Learning Db for ApprenticeshipId: {apprenticeshipId}");
                apprenticeship.ApprenticeDetails.LearningIdKey = result;
                await Task.Delay(100);
                context.Set(apprenticeship, "Apprenticeship");
                objectContext.SetDebugInformation($"[{result} set as LearningIdKey for ULN: {uln}]");
            }
        }

    }
}
