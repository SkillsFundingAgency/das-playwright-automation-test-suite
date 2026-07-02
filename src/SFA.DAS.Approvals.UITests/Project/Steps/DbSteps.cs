using Dynamitey;
using Polly;
using Polly.Retry;
using Reqnroll.CommonModels;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;

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
        private readonly ApprenticeDataHelper apprenticeDataHelper;
        private List<Apprenticeship> listOfApprenticeship;


        public DbSteps(ScenarioContext context)
        {
            this.context = context;
            objectContext = context.Get<ObjectContext>();
            accountsDbSqlHelper = context.Get<AccountsDbSqlHelper>();
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
            learningDbSqlHelper = context.Get<LearningDbSqlHelper>();
            learnerDataDbSqlHelper = context.Get<LearnerDataDbSqlHelper>();
            accountsDbSqlHelper = context.Get<AccountsDbSqlHelper>();
            apprenticeDataHelper = new ApprenticeDataHelper(context);
        }

        [Then("^a record is created in LearnerData Db for each learner$")]
        public async Task ThenARecordIsCreatedInLearnerDataDbForEachLearner()
        {
            listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                var learnerDataId = await DbRetryPolicy(getValue: async () => await learnerDataDbSqlHelper.GetLearnerDataId(uln) , 0, "LearnerData db");
                Assert.IsNotEmpty(learnerDataId, $"No record found in LearnerData db for ULN: {uln}");
                apprenticeship.ApprenticeDetails.LearnerDataId = Convert.ToInt32(learnerDataId);
                await Task.Delay(100);
                context.Set(apprenticeship, "Apprenticeship");
                objectContext.SetDebugInformation($"[{learnerDataId} set as learnerDataId for ULN: {uln}]");
            }

        }

        [Then("^Commitments Db is updated with respective LearnerData Id$")]
        public async Task ThenCommitmentsDbIsUpdatedWithRespectiveLearnerDataId()
        {
            listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

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

        [Then("^LearnerData Db is updated with respective Apprenticeship Id$")]
        public async Task ThenLearnerDataDbIsUpdatedWithRespectiveApprenticeshipId()
        {
            listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);            

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                var learnerDataId = apprenticeship.ApprenticeDetails.LearnerDataId;
                var apprenticeshipIdExpected = apprenticeship.ApprenticeDetails.ApprenticeshipId;
                var apprenticeshipIdActual = await DbRetryPolicy(getValue: async () => await learnerDataDbSqlHelper.GetApprenticeshipIdLinkedWithLearnerData(learnerDataId), apprenticeshipIdExpected, "LearnerData db");
                Assert.AreEqual(apprenticeshipIdExpected.ToString(), apprenticeshipIdActual, $"[Id] from LearnerData db ({apprenticeshipIdActual}) does not match with [LearnerDataId] in Apprenticeship > Commitments db ({apprenticeshipIdExpected})");
            }

        }

        [Then("^Apprenticeship record is created in Learning Db$")]
        public async Task ThenApprenticeshipRecordIsCreatedInLearningDb()
        {
            listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN;
                var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
                var learningType = apprenticeship.TrainingDetails.LearningType;
                string result = string.Empty;
                
                if (learningType == (int)LearningType.ShortCourses)
                    result = await DbRetryPolicy(getValue: async () => await learningDbSqlHelper.CheckIfShortCourseLearnerRecordUpdatedInLearningDb(apprenticeshipId, uln), 0, "Learning db");
                else
                    result = await DbRetryPolicy(getValue: async () => await learningDbSqlHelper.CheckIfApprenticeshipRecordCreatedInLearningDb(apprenticeshipId, uln), 0, "Learning db");

                Assert.IsNotEmpty(result, $"Apprenticeship record not found in Learning Db for ApprenticeshipId: {apprenticeshipId}");
                apprenticeship.ApprenticeDetails.LearningIdKey = result;
                await Task.Delay(100);
                context.Set(apprenticeship, "Apprenticeship");
                objectContext.SetDebugInformation($"[{result} set as LearningIdKey for ULN: {uln}]");
            }
        }

        [Given(@"^A live apprentice record exists for an apprentice with ""(.*)"", ""(.*)"" and ""(.*)""$")]
        public async Task GivenALiveApprenticeRecordExistsForAnApprenticeWithAnd(string courseType, string courseLevel, string startDate)
        {
            listOfApprenticeship = new List<Apprenticeship>();
            string additionalWhereFilter = $@"AND c.CreatedOn > DATEADD(month, -12, GETDATE())
                                            AND c.IsDeleted = 0
                                            And c.Approvals = 3
                                            AND c.ChangeOfPartyRequestId is null             
                                            AND c.PledgeApplicationId is null
                                            AND a.PaymentStatus = 1                                            
                                            AND a.PendingUpdateOriginator is null
                                            AND a.CloneOf is null
                                            AND a.ContinuationOfId is null
                                            AND a.DeliveryModel = 0
                                            AND a.StartDate > '{startDate}'";

            if (courseType == "FoundationApprenticeship")
            {
                additionalWhereFilter +=   @"AND a.HasHadDataLockSuccess = 0
                                             AND a.TrainingCode IN('803','804','805','806','807','808','809', '810', '811')";
            }
            else if (courseType == "ShortCourses")
            {
                additionalWhereFilter += "AND a.TrainingCode Like 'ZSC%'";
            }
            else
            {
                additionalWhereFilter +=   @"AND a.HasHadDataLockSuccess = 0
                                            AND TrainingName like '%, Level: 7'";
            }

            await FindApprenticeFromDbAndSaveItInTheContext(EmployerType.Levy, additionalWhereFilter);
        }

        [Given(@"^a live apprentice record exists with startdate of <(.*)> months and endDate of <\+(.*)> months from current date$")]
        public async Task GivenALiveApprenticeRecordExistsWithStartdateOfMonthsAndEndDateOfMonthsFromCurrentDate(int startDateFromNow, int endDateFromNow)
        {
            listOfApprenticeship = new List<Apprenticeship>();

            var additionalWhereFilter = @$"AND c.CreatedOn > DATEADD(month, -12, GETDATE())
                                            AND c.IsDeleted = 0
                                            And c.Approvals = 3
                                            AND c.ChangeOfPartyRequestId is null             
                                            AND c.PledgeApplicationId is null
                                            AND a.PaymentStatus = 1
                                            AND a.HasHadDataLockSuccess = 0
                                            AND a.PendingUpdateOriginator is null
                                            AND a.CloneOf is null
                                            AND a.ContinuationOfId is null
                                            AND a.DeliveryModel = 0
                                            AND a.StartDate < DATEADD(month, {startDateFromNow}, GETDATE()) 
                                            AND a.EndDate > DATEADD(month, {endDateFromNow}, GETDATE())
                                            AND a.TrainingCode < 800";

            await FindApprenticeFromDbAndSaveItInTheContext(EmployerType.Levy, additionalWhereFilter);
        }

        [Given(@"^a Live apprenticeship record exists for learner with Firstname: ""(.*)"" and LastName: ""(.*)""")]
        public async Task GivenALiveApprenticeshipRecordExistsForLearnerWithFirstnameAndLastName(string firstname, string lastname)
        {
            listOfApprenticeship = new List<Apprenticeship>();
            var additionalWhereFilter = $"AND a.FirstName = '{firstname}' AND a.LastName = '{lastname}'";
            await FindApprenticeFromDbAndSaveItInTheContext(EmployerType.Levy, additionalWhereFilter);

            //reset the payment status to 1 (Live):
            await commitmentsDbSqlHelper.ResetPaymentStatus(listOfApprenticeship.FirstOrDefault().ApprenticeDetails.ApprenticeshipId);            
        }

        [Then(@"Commitments db is updated with the correct reason code and stop date")]
        public async Task ThenCommitmentsDbIsUpdatedWithTheCorrectReasonCodeAndStopDate()
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault(); 
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var uln = apprenticeship.ApprenticeDetails.ULN;
            var expectedStopDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Date;     // commitments always normalises stop date to the first day of the month
            List<string> result = new List<string>();

            var actualPaymentStatus = await DbRetryPolicy(
                getValue: async () =>
                {
                    result = await commitmentsDbSqlHelper.GetValuesFromApprenticeshipTable("paymentstatus, stopdate, WithdrawnReasonCode, MadeRedundant", apprenticeshipId);  

                    return (result as IEnumerable<string[]>)?.FirstOrDefault()[0];
                },
                expectedValue: 3,
                dbName: "CommitmentsDb"
            );

            Assert.That(result[0], Is.EqualTo("3"), $"Expected payment status '3' but found '{actualPaymentStatus}'");
            Assert.That(DateTime.Parse(result[1]).Date, Is.EqualTo(expectedStopDate), $"Expected stop date '{expectedStopDate}' but found '{DateTime.Parse(result[1]).Date}'");
            Assert.That(result[2], Is.EqualTo("29"), $"Expected WithdrawnReasonCode '29' but found '{result[2]}'");
            Assert.That(result[3], Is.EqualTo("True"), $"Expected MadeRedundant 'True' but found '{result[3]}'");
        }

        internal async Task FindAvailableLearner()
        {
            listOfApprenticeship = new List<Apprenticeship>();
            var providerConfig = context.GetProviderConfig<ProviderConfig>();
            Apprenticeship apprenticeship = await apprenticeDataHelper.CreateEmptyCohortObject(EmployerType.Levy, providerConfig);
            apprenticeship = await learnerDataDbSqlHelper.GetEditableApprenticeDetails(apprenticeship);
            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
        }

        internal async Task<Apprenticeship> FindUnapprovedCohortReference(Apprenticeship apprenticeship, ApprenticeRequests status)
        {
            (int withParty, int isDraft, int approvals) = status switch
            {
                ApprenticeRequests.ReadyForReview => (2, 0, 0),
                ApprenticeRequests.WithEmployers => (1, 0, 2),
                ApprenticeRequests.Drafts => (2, 1, 0),
                ApprenticeRequests.WithTransferSendingEmployers => (4, 0, 0),
                _ => (1, 0, 0)
            };

            var details = await commitmentsDbSqlHelper.GetCohortRefAndLearnerDataIdFromCommitmentsDb(
                apprenticeship.ProviderDetails.Ukprn,
                apprenticeship.EmployerDetails.AccountLegalEntityId,
                withParty,
                isDraft,
                approvals);

            //if no matching cohort found in the database, return as is
            if (details == null || details[0] == "")
                return apprenticeship;

            apprenticeship.Cohort.Reference = details[0].ToString();
            apprenticeship.ApprenticeDetails.LearnerDataId = Convert.ToInt32(details[1]);
            apprenticeship.ApprenticeDetails.FirstName = details[2].ToString();
            apprenticeship.ApprenticeDetails.LastName = details[3].ToString();
            
            apprenticeship = await learnerDataDbSqlHelper.GetLearnerDetailsFromLearnerDataId(apprenticeship);
            return apprenticeship;
        }

        private async Task FindApprenticeFromDbAndSaveItInTheContext(EmployerType employerType, string additionalWhereFilter, string ukprn = null)
        {
            var providerConfig = context.GetProviderConfig<ProviderConfig>();
            Apprenticeship apprenticeship = await apprenticeDataHelper.CreateEmptyCohortObject(employerType, providerConfig);
            apprenticeship = await commitmentsDbSqlHelper.GetApprenticeDetailsFromCommitmentsDb(apprenticeship, additionalWhereFilter);
            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
        }

        private async Task<string> DbRetryPolicy(Func<Task<string>> getValue, int expectedValue, string dbName)
        {
            var policy = Policy<string>
                .Handle<Exception>()
                .OrResult(result => (expectedValue > 0) ? result != expectedValue.ToString() : string.IsNullOrEmpty(result))
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(1),
                    onRetry: (result, timeSpan, retryCount, context) =>
                    {
                        objectContext.SetDebugInformation(
                            $"Retry {retryCount} - Expected '{expectedValue}' but got '{result.Result}' from {dbName}. " +
                            $"Waiting {timeSpan.TotalSeconds}s before next attempt.");
                    });

            return await policy.ExecuteAsync(getValue);
        }

    }
}
