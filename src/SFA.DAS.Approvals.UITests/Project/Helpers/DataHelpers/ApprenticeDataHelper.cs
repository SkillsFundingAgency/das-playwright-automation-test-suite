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
        public async Task<Apprenticeship> CreateNewApprenticeshipDetails(int ukprn, EmployerType employerType)
        {
            //create random apprentice, training and RPL details
            Apprentice apprentice = await CreateNewApprenticeDetails();
            Training training = await CreateNewApprenticeshipTrainingDetails();
            RPL rpl = await CreateNewApprenticeshipRPLDetails();

            //create apprenticeship object with the above details
            Apprenticeship apprenticeship = new Apprenticeship(apprentice, training, rpl);

            //get employer details based on the employer type
            var employerDetails = GetEmployerDetails(employerType);

            //set employer details and rest in the apprenticeship object
            apprenticeship.EmployerDetails = await employerDetails;
            apprenticeship.UKPRN = ukprn;

            return apprenticeship;
        }

        public async Task<List<Apprenticeship>> CreateNewApprenticeshipDetails(int ukprn, EmployerType employerType, int numberOfApprenticeships)
        {
            List<Apprenticeship> apprenticeships = new List<Apprenticeship>();
            var employerDetails = GetEmployerDetails(employerType);

            for (int i = 0; i < numberOfApprenticeships; i++)
            {
                // Create random apprentice, training, and RPL details
                Apprentice apprentice = await CreateNewApprenticeDetails();
                Training training = await CreateNewApprenticeshipTrainingDetails();
                RPL rpl = await CreateNewApprenticeshipRPLDetails();

                // Create apprenticeship object with the generated details
                Apprenticeship apprenticeship = new Apprenticeship(apprentice, training, rpl);           

                // Set employer details and UKPRN in the apprenticeship object
                apprenticeship.EmployerDetails = await employerDetails;
                apprenticeship.UKPRN = ukprn;

                // Add to the list
                apprenticeships.Add(apprenticeship);
            }

            return apprenticeships;
        }

        private async Task<Employer> GetEmployerDetails(EmployerType employerType)
        {
            Employer employer = new Employer();

            await Task.Delay(100);

            EasAccountUser employerUser = employerType switch
            {
                EmployerType.NonLevy => context.GetUser<NonLevyUser>(),
                EmployerType.NonLevyUserAtMaxReservationLimit => context.GetUser<NonLevyUserAtMaxReservationLimit>(),
                _ => context.GetUser<LevyUser>()
            };

            employer.EmployerName = employerUser.OrganisationName;

            employer.AgreementId = await context.Get<AccountsDbSqlHelper>().GetAgreementId(employerUser.Username, employer.EmployerName[..3] + "%");

            employer.EmployerType = employerType;

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

        private async Task<Training> CreateNewApprenticeshipTrainingDetails(ApprenticeshipStatus? apprenticeshipStatus=null)
        {
            var lowerDateRangeForStartDate = AcademicYearDatesHelper.GetCurrentAcademicYearStartDate();
            var academicYearEndDate = AcademicYearDatesHelper.GetCurrentAcademicYearEndDate();
            var todaysDate = DateTime.Now;
            var upperDateRangeForStartDate = (academicYearEndDate > todaysDate) ? todaysDate : academicYearEndDate;

            Training training = new Training();

            if (apprenticeshipStatus == ApprenticeshipStatus.WaitingToStart)
            {
                training.StartDate = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddYears(-1), DateTime.Now);
                training.EndDate = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddMonths(1), DateTime.Now.AddYears(1));
            }
            else
            {
                training.StartDate = RandomDataGenerator.GenerateRandomDate(lowerDateRangeForStartDate, upperDateRangeForStartDate);
                training.EndDate = training.StartDate.AddMonths(15);
            }

            training.AcademicYear = AcademicYearDatesHelper.GetCurrentAcademicYear();
            training.PercentageLearningToBeDelivered = 40;
            training.EpaoPrice = Convert.ToInt32(RandomDataGenerator.GenerateRandomNumber(3));
            training.TrainingPrice = Convert.ToInt32("2" + RandomDataGenerator.GenerateRandomNumber(3));
            training.TotalPrice = training.EpaoPrice + training.TrainingPrice;
            training.IsFlexiJob = false;
            training.PlannedOTJTrainingHours = 1200;
            training.StandardCode = 5; // Example standard code
            training.ConsumerReference = "CR123456";

            await Task.Delay(100);

            return training;
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
