using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel
{
    internal interface ITrainingFactory
    {
        Task<Training> CreateTrainingAsync(EmployerType employerType, ApprenticeshipStatus? apprenticeshipStatus = null);
    }
}
