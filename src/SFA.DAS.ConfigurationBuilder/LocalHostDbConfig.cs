﻿using SFA.DAS.FrameworkHelpers;

namespace SFA.DAS.ConfigurationBuilder
{
    public class LocalHostDbConfig(DbDevConfig dbDevConfig, bool useSqlLogin)
    {
        public DbConfig GetLocalHostDbConfig()
        {
            return new DbConfig
            {
                DatamartDbConnectionString = GetConnectionString(dbDevConfig.DatamartDbName),
                AccountsDbConnectionString = GetConnectionString(dbDevConfig.AccountsDbName),
                FinanceDbConnectionString = GetConnectionString(dbDevConfig.FinanceDbName),
                FcastDbConnectionString = GetConnectionString(dbDevConfig.FcastDbName),
                CommitmentsDbConnectionString = GetConnectionString(dbDevConfig.CommitmentsDbName),
                ApprenticeCommitmentDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeCommitmentDbName),
                ApprenticeCommitmentLoginDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeCommitmentLoginDbName),
                ApprenticeCommitmentAccountsDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeCommitmentAccountsDbName),
                ApplyDatabaseConnectionString = GetConnectionString(dbDevConfig.ApplyDatabaseName),
                LoginDatabaseConnectionString = GetConnectionString(dbDevConfig.LoginDatabaseName),
                QnaDatabaseConnectionString = GetConnectionString(dbDevConfig.QnaDatabaseName),
                RoatpDatabaseConnectionString = GetConnectionString(dbDevConfig.RoatpDatabaseName),
                EmployerFeedbackDbConnectionString = GetConnectionString(dbDevConfig.EmployerFeedbackDbName),
                ApprenticeFeedbackDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeFeedbackDbName),
                AssessorDbConnectionString = GetConnectionString(dbDevConfig.AssessorDbName),
                IncentivesDbConnectionString = GetConnectionString(dbDevConfig.EmployerIncentivesDbName),
                ReservationsDbConnectionString = GetConnectionString(dbDevConfig.ReservationsDbName),
                PermissionsDbConnectionString = GetConnectionString(dbDevConfig.PermissionsDbName),
                PublicSectorReportingConnectionString = GetConnectionString(dbDevConfig.PublicSectorReportingDbName),
                TPRDbConnectionString = GetConnectionString(dbDevConfig.TPRDbName),
                UsersDbConnectionString = GetConnectionString(dbDevConfig.UsersDbName),
                TMDbConnectionString = GetConnectionString(dbDevConfig.TMDbName),
                CRSDbConnectionString = GetConnectionString(dbDevConfig.CrsDbName),
                EmploymentCheckDbConnectionString = GetConnectionString(dbDevConfig.EmploymentCheckDbName),
                ManagingStandardsDbConnectionString = GetConnectionString(dbDevConfig.ManagingStandardsDbName),
                EarningsDbConnectionString = GetConnectionString(dbDevConfig.EarningsDbName),
                ApprenticeshipsDbConnectionString = GetConnectionString(dbDevConfig.ApprenticeshipsDbName),
                RofjaaDbConnectionString = GetConnectionString(dbDevConfig.RofjaaDbName),
                AANDbConnectionString = GetConnectionString(dbDevConfig.AANDbName),
                CanAccDbConnectionString = GetConnectionString(dbDevConfig.CanAccDbName),
                RatDbConnectionString = GetConnectionString(dbDevConfig.RatDbName),
                EarlyConnectConnectionString = GetConnectionString(dbDevConfig.EarlyConnectDbName)
            };
        }

        private string GetConnectionString(string dbName)
        {
            var x = $"Server={dbDevConfig.Server};{SqlDbConfigHelper.GetDbNameKey(useSqlLogin)}={dbName};{dbDevConfig.ConnectionDetails};TENANTID={dbDevConfig.TenantId}";

            return EnvironmentConfig.ReplaceEnvironmentName(x);
        }
    }
}