﻿
namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;

public class CommitmentsSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.CommitmentsDbConnectionString)
{

    public async Task<int> GetNumberOfCohortsReadyToReview(string employerAccountId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId }
        };

        string query = @"WITH CohortsFiltered AS (
								SELECT 
								[c].[Id]
							FROM [Commitment] AS [c]
							LEFT JOIN [Accounts] AS [a] ON [c].[TransferSenderId] = [a].[Id]
							INNER JOIN (
								SELECT [a0].[Id]
									,[a0].[Name]
									,[a0].[PublicHashedId]
								FROM [AccountLegalEntities] AS [a0]
								WHERE [a0].[Deleted] IS NULL
								) AS [t] ON [c].[AccountLegalEntityId] = [t].[Id]
							INNER JOIN [Providers] AS [p] ON [c].[ProviderId] = [p].[UkPrn]
							WHERE ([c].[IsDeleted] = CAST(0 AS BIT))
								AND [c].IsDraft = 0
							AND [c].WithParty = 1
								AND (
									(([c].[EmployerAccountId] = @EmployerAccountId))
									AND (
										([c].[EditStatus] <> CAST(0 AS SMALLINT))
										OR (
											([c].[TransferSenderId] IS NOT NULL)
											AND ([c].[TransferApprovalStatus] = CAST(0 AS TINYINT))
											)
										)
									)
							)
							SELECT COUNT(*) AS TotalCohorts
							FROM CohortsFiltered;
							";

        var result = await GetDataAsObject(query, sqlParameters);

        return (int)result;
    }

    public async Task<int> GetNumberOfTransferRequestToReview(string employerAccountId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId }
        };

        string query = @"WITH TransferRequestsFiltered AS (
                                SELECT [t].Id
                                FROM [TransferRequest] AS [t]
                                    INNER JOIN
                                    (
                                        SELECT [c].[Id],
                                               [c].[EmployerAccountId],
                                               [c].[Reference],
                                               [c].[TransferSenderId]
                                        FROM [Commitment] AS [c]
                                        WHERE [c].[IsDeleted] = CAST(0 AS bit)
                                    ) AS [t0]
                                        ON [t].[CommitmentId] = [t0].[Id]
                                WHERE [t0].[TransferSenderId] = @EmployerAccountId
                                      and [t].Status = 0
                                )
                                SELECT COUNT(*) AS TotalRequests
                                FROM TransferRequestsFiltered;
							";

        var result = await GetDataAsObject(query, sqlParameters);

        return (int)result;
    }

    public async Task<int> GetNumberOfApprenticesToReview(string employerAccountId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId }
        };

        string query = @"select COUNT(aup.Id)
                            from [dbo].[ApprenticeshipUpdate] aup
                                inner join [dbo].[Apprenticeship] app
                                    on app.Id = aup.ApprenticeshipId
                                inner join [dbo].[Commitment] comm
                                    on comm.Id = app.CommitmentId
                            where comm.EmployerAccountId = @EmployerAccountId
                                  AND aup.Status = 0
                                  AND aup.Originator = 1
							";

        var result = await GetDataAsObject(query, sqlParameters);

        return (int)result;
    }


    public async Task<int> UpdateEmailForApprenticeshipRecord(string email, long apprenticeshipid)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@Email", email },
            { "@Apprenticeshipid", apprenticeshipid.ToString() }
        };
        
        return await ExecuteSqlCommand($"UPDATE [Apprenticeship] SET Email = '@Email' WHERE Id = @Apprenticeshipid", sqlParameters);
    }

    public async Task<int> ResetEmailForApprenticeshipRecord(string email)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@Email", email }
        };

        return await ExecuteSqlCommand($"UPDATE [Apprenticeship] SET Email = NULL, EmailAddressConfirmed = NULL WHERE Email = '@Email'", sqlParameters);
    }
    public async Task<List<CohortsWithPledgeApplications>> GetPledgeApplicationIdsAndNumberOfDraftApprentices(string employerAccountId)
    {
        Dictionary<string, string> sqlParameters = new()
    {
        { "@EmployerAccountId", employerAccountId }
    };

        string query = @"
        SELECT
            [c].[PledgeApplicationId],
            COUNT([a1].[Id]) AS [NumberOfDraftApprentices]
        FROM
            [Commitment] AS [c]
        LEFT JOIN
            [Accounts] AS [a]
            ON [c].[TransferSenderId] = [a].[Id]
        INNER JOIN
            (
                SELECT
                    [a0].[Id],
                    [a0].[Name],
                    [a0].[PublicHashedId]
                FROM
                    [AccountLegalEntities] AS [a0]
                WHERE
                    [a0].[Deleted] IS NULL
            ) AS [t]
            ON [c].[AccountLegalEntityId] = [t].[Id]
        INNER JOIN
            [Providers] AS [p]
            ON [c].[ProviderId] = [p].[UkPrn]
        LEFT JOIN
            [Apprenticeship] AS [a1]
            ON [c].[Id] = [a1].[CommitmentId]
            AND [a1].[IsApproved] = CAST(0 AS bit)
        WHERE
            [c].[PledgeApplicationId] IS NOT NULL
            AND [c].[IsDeleted] = CAST(0 AS bit)
            AND [c].[EmployerAccountId] = @EmployerAccountId
            AND (
                [c].[EditStatus] <> CAST(0 AS smallint)
                OR (
                    [c].[TransferSenderId] IS NOT NULL
                    AND [c].[TransferApprovalStatus] = CAST(0 AS tinyint)
                )
            )
        GROUP BY
            [c].[PledgeApplicationId]";

        List<string[]> rawResults = await GetMultipleData(query, sqlParameters); // <-- await it here
        var results = new List<CohortsWithPledgeApplications>();

        if (rawResults == null || rawResults.Count == 0 || rawResults.All(row => row.Length < 2 || row.All(string.IsNullOrEmpty)))
        {
            return null;
        }

        foreach (var row in rawResults)
        {
            if (row.Length >= 2)
            {
                var cohort = new CohortsWithPledgeApplications
                {
                    PledgeApplicationId = int.Parse(row[0]),
                    NumberOfDraftApprentices = int.Parse(row[1])
                };
                results.Add(cohort);
            }
        }

        return results;
    }
}
