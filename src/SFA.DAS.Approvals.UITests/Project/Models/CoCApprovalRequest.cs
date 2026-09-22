namespace SFA.DAS.Approvals.UITests.Project.Models
{
    internal class CoCApprovalRequest
    {
        public string learningKey { get; set; }
        public int apprenticeshipId { get; set; }
        public string learningType { get; set; }
        public string ukprn { get; set; }
        public string uln { get; set; }
        public string approvedUri { get; set; }
        public CoCApprovalFieldRequest changes { get; set; }
    }

    internal class CoCApprovalFieldRequest
    {
        public string changeType { get; set; }
        public CoCData data { get; set; }       
    }

    internal class CoCData
    {
        public string old { get; set; }
        public string @new { get; set; }
        public string effectiveFromDate { get; set; }
    }
}
