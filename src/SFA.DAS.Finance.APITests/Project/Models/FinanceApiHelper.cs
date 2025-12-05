namespace SFA.DAS.Finance.APITests.Project.Models;

public class TransactionSummary
{
    public int Year { get; set; }
    public int Month { get; set; }
}

public class LevyDeclaration
{
    public string PayrollYear { get; set; }
    public short? PayrollMonth { get; set; }
}