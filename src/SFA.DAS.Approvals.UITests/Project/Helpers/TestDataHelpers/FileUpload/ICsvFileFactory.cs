using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel
{
    internal interface ICsvFileFactory
    {
        Task CreateCsvFile(List<Apprenticeship> listOfApprenticeship, string CsvFileLocation);
    }
}
