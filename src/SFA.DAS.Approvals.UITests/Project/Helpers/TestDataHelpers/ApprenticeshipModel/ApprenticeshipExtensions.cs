using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.ApprenticeshipModel
{
    internal static class ApprenticeshipExtensions
    {
        public static List<Apprenticeship> CloneApprenticeships(this List<Apprenticeship> listOfApprenticeship)
        {
            return listOfApprenticeship.Select(a => new Apprenticeship
            {
                ReservationID = a.ReservationID,
                Cohort = new Cohort
                {
                    Reference = a.Cohort.Reference,
                    Status_Provider = a.Cohort.Status_Provider,
                    Status_Employer = a.Cohort.Status_Employer
                },
                EmployerDetails = new Employer
                {
                    AgreementId = a.EmployerDetails.AgreementId,
                    AccountLegalEntityId = a.EmployerDetails.AccountLegalEntityId,
                    EmployerName = a.EmployerDetails.EmployerName,
                    EmployerType = a.EmployerDetails.EmployerType,
                    Email = a.EmployerDetails.Email
                },
                ProviderDetails = new Provider
                {
                    ProviderName = a.ProviderDetails.ProviderName,
                    Ukprn = a.ProviderDetails.Ukprn,
                    Email = a.ProviderDetails.Email
                },
                ApprenticeDetails = new Apprentice
                {
                    ULN = a.ApprenticeDetails.ULN,
                    FirstName = a.ApprenticeDetails.FirstName,
                    LastName = a.ApprenticeDetails.LastName,
                    Email = a.ApprenticeDetails.Email,
                    DateOfBirth = a.ApprenticeDetails.DateOfBirth,
                    ApprenticeshipId = a.ApprenticeDetails.ApprenticeshipId,
                    LearningIdKey = a.ApprenticeDetails.LearningIdKey,
                    LearnerDataId = a.ApprenticeDetails.LearnerDataId
                },
                TrainingDetails = new Training
                {
                    StartDate = a.TrainingDetails.StartDate,
                    EndDate = a.TrainingDetails.EndDate,
                    AcademicYear = a.TrainingDetails.AcademicYear,
                    PercentageLearningToBeDelivered = a.TrainingDetails.PercentageLearningToBeDelivered,
                    EpaoPrice = a.TrainingDetails.EpaoPrice,
                    TrainingPrice = a.TrainingDetails.TrainingPrice,
                    TotalPrice = a.TrainingDetails.TotalPrice,
                    IsFlexiJob = a.TrainingDetails.IsFlexiJob,
                    PlannedOTJTrainingHours = a.TrainingDetails.PlannedOTJTrainingHours,
                    StandardCode = a.TrainingDetails.StandardCode,
                    CourseTitle = a.TrainingDetails.CourseTitle,
                    ConsumerReference = a.TrainingDetails.ConsumerReference
                },
                RPLDetails = new RPL
                {
                    RecognisePriorLearning = a.RPLDetails.RecognisePriorLearning,
                    TrainingTotalHours = a.RPLDetails.TrainingTotalHours,
                    TrainingHoursReduction = a.RPLDetails.TrainingHoursReduction,
                    IsDurationReducedByRPL = a.RPLDetails.IsDurationReducedByRPL,
                    DurationReducedBy = a.RPLDetails.DurationReducedBy,
                    PriceReducedBy = a.RPLDetails.PriceReducedBy
                }
            }).ToList();
        }
    }
}
