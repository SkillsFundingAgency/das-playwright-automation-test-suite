using System;
using System.IO;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel
{
    internal class FileUploadHelper
    {
        private readonly ScenarioContext context;

        public FileUploadHelper(ScenarioContext _context) => context = _context;

        internal string CsvFileLocation() => Path.GetFullPath(@"..\..\..\") + $"{context.ScenarioInfo.Title[..8]}_BulkUpload.csv";


    }
}
