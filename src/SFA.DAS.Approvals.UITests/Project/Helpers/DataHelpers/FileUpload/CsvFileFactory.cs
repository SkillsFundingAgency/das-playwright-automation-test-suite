using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.IO;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel
{
    internal class CsvFileFactory : ICsvFileFactory
    {
        private string CsvHeader = "CohortRef,AgreementID,ULN,FamilyName,GivenNames,DateOfBirth,EmailAddress,StdCode,StartDate,EndDate,TotalPrice,EPAOrgID,ProviderRef,RecognisePriorLearning,TrainingTotalHours,TrainingHoursReduction,IsDurationReducedByRPL,DurationReducedBy,PriceReducedBy";
       
        public async Task CreateCsvFile(List<Apprenticeship> listOfApprenticeship, string CsvFileLocation)
        {
            var csvLines = new List<string> { CsvHeader };
            foreach (var apprentice in listOfApprenticeship)
            {
                var line = $"{apprentice.CohortReference},{apprentice.EmployerDetails.AgreementId},{apprentice.ApprenticeDetails.ULN},{apprentice.ApprenticeDetails.LastName}," +
                           $"{apprentice.ApprenticeDetails.FirstName},{apprentice.ApprenticeDetails.DateOfBirth.ToString("yyyy-MM-dd")},{apprentice.ApprenticeDetails.Email}," +
                           $"{apprentice.TrainingDetails.StandardCode},{apprentice.TrainingDetails.StartDate.ToString("yyyy-MM-dd")},{apprentice.TrainingDetails.EndDate.ToString("yyyy-MM")}," +
                           $"{apprentice.TrainingDetails.TotalPrice},EPA0001,{RandomDataGenerator.GenerateRandomAlphabeticString(6)}," +
                           $"{apprentice.RPLDetails.IsDurationReducedByRPL}, {apprentice.RPLDetails.TrainingTotalHours}, {apprentice.RPLDetails.TrainingHoursReduction}," +
                           $"{apprentice.RPLDetails.IsDurationReducedByRPL}, {apprentice.RPLDetails.DurationReducedBy}, {apprentice.RPLDetails.PriceReducedBy}";
                csvLines.Add(line);
            }
            await File.WriteAllLinesAsync(CsvFileLocation, csvLines);
        }

    }
}
