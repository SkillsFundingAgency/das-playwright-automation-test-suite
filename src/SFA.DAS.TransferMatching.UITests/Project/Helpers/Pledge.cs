namespace SFA.DAS.TransferMatching.UITests.Project.Helpers;

public class Pledge
{
    public string PledgeId { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EmployerAccountId { get; set; }
    public string SenderHashedAccountId { get; set; }
    public List<string> Applications { get; set; } = [];
}
