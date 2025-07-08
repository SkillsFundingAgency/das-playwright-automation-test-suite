using Dynamitey;
using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers
{
    internal class ApprenticeDataHelper(ScenarioContext context)
    {
        /*
        public async Task<List<Apprenticeship>> CreateApprenticeshipAsync(EmployerType employerType, int numberOfApprenticeships, string? ukprn)
        {
            List<Apprenticeship> apprenticeships = new List<Apprenticeship>();
            var employerDetails = await GetEmployerDetails(employerType);

            for (int i = 0; i < numberOfApprenticeships; i++)
            {
                // Create random apprentice, training, and RPL details
                Apprentice apprentice = await CreateNewApprenticeDetails();
                Training training = await CreateNewApprenticeshipTrainingDetails(employerType);
                RPL rpl = await CreateNewApprenticeshipRPLDetails();

                // Create apprenticeship object with the generated details
                Apprenticeship apprenticeship = new Apprenticeship(apprentice, training, rpl);           

                // Set employer details and UKPRN in the apprenticeship object
                apprenticeship.EmployerDetails = employerDetails;
                apprenticeship.UKPRN = (ukprn!=null) ? Convert.ToInt32(ukprn) : Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn);

                // Add to the list
                apprenticeships.Add(apprenticeship);
            }

            return apprenticeships;
        }
        */
        public async Task<List<Apprenticeship>> CreateApprenticeshipAsync(EmployerType EmployerType, int NumberOfApprenticeships, string? Ukprn, Apprentice? ApprenticeDetails = null, Training? Training = null, RPL? Rpl = null)
        {
            List<Apprenticeship> apprenticeships = new List<Apprenticeship>();
            var employerDetails = await GetEmployerDetails(EmployerType);

            for (int i = 0; i < NumberOfApprenticeships; i++)
            {
                // Create random apprentice, training, and RPL details
                var apprenticeDetails = (ApprenticeDetails == null ) ?await CreateNewApprenticeDetails() : ApprenticeDetails;
                var training = (Training == null) ? await CreateNewApprenticeshipTrainingDetails(EmployerType) : Training;
                var rpl = (Rpl == null) ? await CreateNewApprenticeshipRPLDetails() : Rpl;

                // Create apprenticeship object with the generated details
                Apprenticeship apprenticeship = new Apprenticeship(apprenticeDetails, training, rpl);

                // Set employer details and UKPRN in the apprenticeship object
                apprenticeship.EmployerDetails = employerDetails;
                apprenticeship.UKPRN = (Ukprn != null) ? Convert.ToInt32(Ukprn) : Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn);

                // Add to the list
                apprenticeships.Add(apprenticeship);
            }

            return apprenticeships;
        }

        private async Task<Employer> GetEmployerDetails(EmployerType employerType)
        {
            Employer employer = new Employer();

            EasAccountUser employerUser = employerType switch
            {
                EmployerType.NonLevy => context.GetUser<NonLevyUser>(),
                EmployerType.NonLevyUserAtMaxReservationLimit => context.GetUser<NonLevyUserAtMaxReservationLimit>(),
                _ => context.GetUser<LevyUser>()
            };

            employer.EmployerName = employerUser.OrganisationName;

            employer.AgreementId = await context.Get<AccountsDbSqlHelper>().GetAgreementId(employerUser.Username, employer.EmployerName[..3] + "%");

            employer.EmployerType = employerType;

            employer.Email = employerUser.Username;

            return employer;
        }

        private async Task<Apprentice> CreateNewApprenticeDetails()
        {
            Apprentice apprentice = new Apprentice();

            apprentice.ULN = RandomDataGenerator.GenerateRandomUln();
            apprentice.FirstName = RandomDataGenerator.GenerateRandomAlphabeticString(6);
            apprentice.LastName = RandomDataGenerator.GenerateRandomAlphabeticString(9);
            apprentice.Email = $"{apprentice.FirstName}.{apprentice.LastName}@l38cxwya.mailosaur.net";
            apprentice.DateOfBirth = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddYears(-30), DateTime.Now.AddYears(-16));

            await Task.Delay(100); 
            return apprentice;
        }

        private async Task<Training> CreateNewApprenticeshipTrainingDetails(EmployerType employerType, ApprenticeshipStatus? apprenticeshipStatus=null)
        {
            Training training = new Training();

            CoursesDataHelper coursesDataHelper = new CoursesDataHelper(context);
            var course = await coursesDataHelper.GetRandomCourse();

            training.StartDate = (employerType == EmployerType.Levy) ? await GetStartDate(apprenticeshipStatus) : DateTime.Now;
            training.EndDate = training.StartDate.AddMonths(15);
            training.AcademicYear = AcademicYearDatesHelper.GetCurrentAcademicYear();
            training.PercentageLearningToBeDelivered = 40;
            training.EpaoPrice = Convert.ToInt32(RandomDataGenerator.GenerateRandomNumber(3));
            training.TrainingPrice = Convert.ToInt32("2" + RandomDataGenerator.GenerateRandomNumber(3));
            training.TotalPrice = training.EpaoPrice + training.TrainingPrice;
            training.IsFlexiJob = false;
            training.PlannedOTJTrainingHours = 1200;
            training.StandardCode = course.StandardCode;
            training.ConsumerReference = "CR123456";

            await Task.Delay(100);

            return training;
        }

        private async Task<DateTime> GetStartDate(ApprenticeshipStatus? apprenticeshipStatus = null)
        {
            var lowerDateRangeForStartDate = AcademicYearDatesHelper.GetCurrentAcademicYearStartDate();
            var academicYearEndDate = AcademicYearDatesHelper.GetCurrentAcademicYearEndDate();
            var todaysDate = DateTime.Now;
            var upperDateRangeForStartDate = (academicYearEndDate > todaysDate) ? todaysDate : academicYearEndDate;

            await Task.Delay(100);


            if (apprenticeshipStatus == ApprenticeshipStatus.WaitingToStart)
            {
                return RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddMonths(2), DateTime.Now);
            }
            else
            {
                return RandomDataGenerator.GenerateRandomDate(lowerDateRangeForStartDate, upperDateRangeForStartDate);
            }

        }

        private async Task<RPL> CreateNewApprenticeshipRPLDetails()
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
