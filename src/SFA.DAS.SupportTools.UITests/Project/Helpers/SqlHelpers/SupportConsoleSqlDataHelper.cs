namespace SFA.DAS.SupportTools.UITests.Project.Helpers.SqlHelpers;

public class SupportConsoleSqlDataHelper(AccountsSqlDataHelper accountsSqlDataHelper, CommitmentsSqlDataHelper commitmentsSqlDataHelper)
{
    public async Task<SupportConsoleConfig> GetUpdatedConfig(SupportConsoleConfig supportConsoleConfig)
    {
        string publicAccountId = supportConsoleConfig.PublicAccountId;

        var (name, createdDate, hashedId, email, fName, lName, payeref) = await accountsSqlDataHelper.GetAccountDetails(publicAccountId);

        var comtData = await commitmentsSqlDataHelper.GetCommtDetails(publicAccountId);

        var result = new SupportConsoleConfig
        {
            Name = $"{fName} {lName}",
            EmailAddress = email,
            PublicAccountId = publicAccountId,
            HashedAccountId = hashedId,
            AccountName = name,
            PayeScheme = payeref,
            CurrentLevyBalance = supportConsoleConfig.CurrentLevyBalance,
            AccountDetails = $"Account ID {publicAccountId}, created {createdDate:dd/MM/yyyy}",
            CohortDetails = new CohortDetails(comtData[0]),
            CohortNotAssociatedToAccount = new CohortDetails(comtData[1]),
            CohortWithPendingChanges = new CohortDetails(comtData[2]),
            CohortWithTrainingProviderHistory = new CohortDetails(comtData[3]),
        };

        return result;
    }

}
