using SFA.DAS.FrameworkHelpers;

namespace SFA.DAS.ConfigurationBuilder;

public class LocalHostDbConfig(DbDevConfig dbDevConfig, bool useSqlLogin)
{
    public DbConfig GetLocalHostDbConfig()
    {
        return new DbConfig
        {
            AANDbConnectionString = GetConnectionString(dbDevConfig.AANDbName),
            AccountsDbConnectionString = GetConnectionString(dbDevConfig.AccountsDbName),
            ApprenticeCommitmentAccountsDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeCommitmentAccountsDbName),
            ApprenticeCommitmentDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeCommitmentDbName),
            ApprenticeCommitmentLoginDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeCommitmentLoginDbName),
            ApprenticeFeedbackDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeFeedbackDbName),
            ApprenticeshipsDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeshipsDbName),
            ApplyDatabaseConnectionString = GetConnectionString(dbDevConfig.ApplyDatabaseName),
            AssessorDbConnectionString = GetConnectionString(dbDevConfig.AssessorDbName),
            CanAccDbConnectionString = GetConnectionString(dbDevConfig.CanAccDbName),
            CommitmentsDbConnectionString = GetConnectionString(dbDevConfig.CommitmentsDbName),
            CRSDbConnectionString = GetConnectionString(dbDevConfig.CrsDbName),
            DatamartDbConnectionString = GetConnectionString(dbDevConfig.DatamartDbName),
            EarlyConnectConnectionString = GetConnectionString(dbDevConfig.EarlyConnectDbName),
            EarningsDbConnectionString = GetConnectionString(dbDevConfig.EarningsDbName),
            EmploymentCheckDbConnectionString = GetConnectionString(dbDevConfig.EmploymentCheckDbName),
            EmployerFeedbackDbConnectionString = GetConnectionString(dbDevConfig.EmployerFeedbackDbName),
            FcastDbConnectionString = GetConnectionString(dbDevConfig.FcastDbName),
            FinanceDbConnectionString = GetConnectionString(dbDevConfig.FinanceDbName),
            IncentivesDbConnectionString = GetConnectionString(dbDevConfig.EmployerIncentivesDbName),                
            LearningDbConnectionString = GetConnectionString(dbDevConfig.LearningDbName),
            LearnerDataDbConnectionString = GetConnectionString(dbDevConfig.LearnerDataDbName),
            LoginDatabaseConnectionString = GetConnectionString(dbDevConfig.LoginDatabaseName),
            ManagingStandardsDbConnectionString = GetConnectionString(dbDevConfig.ManagingStandardsDbName),
            PermissionsDbConnectionString = GetConnectionString(dbDevConfig.PermissionsDbName),
            PublicSectorReportingConnectionString = GetConnectionString(dbDevConfig.PublicSectorReportingDbName),
            QnaDatabaseConnectionString = GetConnectionString(dbDevConfig.QnaDatabaseName),
            RatDbConnectionString = GetConnectionString(dbDevConfig.RatDbName),
            ReservationsDbConnectionString = GetConnectionString(dbDevConfig.ReservationsDbName),
            RoatpDatabaseConnectionString = GetConnectionString(dbDevConfig.RoatpDatabaseName),
            RofjaaDbConnectionString = GetConnectionString(dbDevConfig.RofjaaDbName),
            TMDbConnectionString = GetConnectionString(dbDevConfig.TMDbName),
            TPRDbConnectionString = GetConnectionString(dbDevConfig.TPRDbName),
            UsersDbConnectionString = GetConnectionString(dbDevConfig.UsersDbName),
            QfastDbConnectionString = GetConnectionString(dbDevConfig.QfastDbName)
        };
    }

    private string GetConnectionString(string dbName)
    {
        var x = $"Server={dbDevConfig.Server};{SqlDbConfigHelper.GetDbNameKey(useSqlLogin)}={dbName};{dbDevConfig.ConnectionDetails};TENANTID={dbDevConfig.TenantId}";

        return EnvironmentConfig.ReplaceEnvironmentName(x);
    }
}