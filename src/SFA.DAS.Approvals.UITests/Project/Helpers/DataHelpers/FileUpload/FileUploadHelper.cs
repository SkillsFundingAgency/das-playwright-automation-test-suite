using System;
using System.IO;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel
{
    internal class FileUploadHelper
    {
        private readonly ScenarioContext context;

        public FileUploadHelper(ScenarioContext _context) => context = _context;

        //internal string CsvFileLocation2 => Path.GetFullPath(@"..\..\..\") + @"Project\CsvFiles\" + $"{context.ScenarioInfo.Title[..8]}_BulkUpload.csv";

        internal string CsvFileLocation()
        {
            //var approvalsConfig = context.GetApprovalsConfig<ApprovalsConfig>();
            //return Path.GetFullPath(@"..\..\..\") + approvalsConfig.BulkUploadFileLocation;

            return Path.GetFullPath(@"..\..\..\") + $"{context.ScenarioInfo.Title[..8]}_BulkUpload.csv";

            //var fileName = $"{context.ScenarioInfo.Title[..8]}_BulkUpload.csv";
            //return Path.Combine(AppContext.BaseDirectory, "Project", "CsvFiles", fileName);

        }

    }
}
