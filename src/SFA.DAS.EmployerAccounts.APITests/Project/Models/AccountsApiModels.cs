namespace SFA.DAS.EmployerAccounts.APITests.Project.Models;

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

public class EmployerAccountsApiConfig
{
    public string HashCharacters { get; set; }

    public string HashString { get; set; }
}

