using Dynamitey;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace SFA.DAS.Approvals.UITests.Helpers.DataHelpers
{
    internal class ApprenticeDataHelper
    {
        public Apprenticeship CreateNewApprenticeshipDetails(int ukprn, string agreementId)
        {
            Apprentice apprentice = CreateNewApprenticeDetails();
            Training training = CreateNewApprenticeshipTrainingDetails(null);
            RPL rpl = CreateNewApprenticeshipRPLDetails();
            
            Apprenticeship apprenticeship = new Apprenticeship(apprentice, training, rpl);

            apprenticeship.UKPRN = ukprn;
            apprenticeship.AgreementId = agreementId;


            return apprenticeship;
        }

        private Apprentice CreateNewApprenticeDetails()
        {
            Apprentice apprentice = new Apprentice();

            apprentice.ULN = RandomDataGenerator.GenerateRandomUln();
            apprentice.FirstName = RandomDataGenerator.GenerateRandomAlphabeticString(6);
            apprentice.LastName = RandomDataGenerator.GenerateRandomAlphabeticString(9);
            apprentice.Email = $"{apprentice.FirstName}.{apprentice.LastName}@l38cxwya.mailosaur.net";
            apprentice.DateOfBirth = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddYears(-30), DateTime.Now.AddYears(-16));

            return apprentice;
        }

        private Training CreateNewApprenticeshipTrainingDetails(ApprenticeshipStatus? apprenticeshipStatus)
        {
            Training training = new Training();

            if (apprenticeshipStatus == ApprenticeshipStatus.WaitingToStart)
            {
                training.StartDate = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddYears(-1), DateTime.Now);
                training.EndDate = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddMonths(1), DateTime.Now.AddYears(1));
            }
            else
            {
                training.StartDate = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddYears(-1), DateTime.Now);
                training.EndDate = RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddMonths(1), DateTime.Now.AddYears(1));
            }


            training.PercentageLearningToBeDelivered = 40; 
            training.EpaoPrice = Convert.ToInt32(RandomDataGenerator.GenerateRandomNumber(3));
            training.TrainingPrice = Convert.ToInt32("2" + RandomDataGenerator.GenerateRandomNumber(3));
            training.TotalPrice = training.EpaoPrice + training.TrainingPrice;
            training.IsFlexiJob = false;
            training.PlannedOTJTrainingHours = 1200; 
            training.StandardCode = 123; // Example standard code
            training.ConsumerReference = "CR123456"; 

            return training;
        }

        private RPL CreateNewApprenticeshipRPLDetails()
        {
            RPL rpl = new RPL();

            rpl.RecognisePriorLearning = "Yes"; 
            rpl.TrainingTotalHours = 1500; 
            rpl.TrainingHoursReduction = 300; 
            rpl.IsDurationReducedByRPL = true; 
            rpl.DurationReducedBy = 3; 
            rpl.PriceReducedBy = 500; 

            return rpl;
        }



    }
}
