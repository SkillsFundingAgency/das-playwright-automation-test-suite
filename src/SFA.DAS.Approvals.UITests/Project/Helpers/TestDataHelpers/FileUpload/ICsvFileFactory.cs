using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel
{
    internal interface ICsvFileFactory
    {
        Task CreateCsvFile(List<Apprenticeship> listOfApprenticeship, string CsvFileLocation);
    }
}
