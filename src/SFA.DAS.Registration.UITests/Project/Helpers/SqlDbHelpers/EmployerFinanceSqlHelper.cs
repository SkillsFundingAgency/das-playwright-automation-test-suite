﻿namespace SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;

public class EmployerFinanceSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.FinanceDbConnectionString)
{
    public async Task<int> GetNumberOfPendingTransferConnections(string employerAccountId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId }
        };

        string query = @"WITH Invitations AS (
                            SELECT [t].[Id]
                            FROM [employer_financial].[TransferConnectionInvitation] AS [t]
                                INNER JOIN [employer_financial].[Account] AS [a]
                                    ON [t].[ReceiverAccountId] = [a].[Id]
                                INNER JOIN [employer_financial].[Account] AS [a0]
                                    ON [t].[SenderAccountId] = [a0].[Id]
                                LEFT JOIN
                                (
                                    SELECT [t1].[Id],
                                            [t1].[CreatedDate],
                                            [t1].[DeletedByReceiver],
                                            [t1].[DeletedBySender],
                                            [t1].[ReceiverAccountId],
                                            [t1].[SenderAccountId],
                                            [t1].[Status],
                                            [t1].[TransferConnectionInvitationId],
                                            [t1].[UserId],
                                            [u].[Id] AS [Id0],
                                            [u].[CorrelationId],
                                            [u].[Email],
                                            [u].[FirstName],
                                            [u].[LastName],
                                            [u].[UserRef]
                                    FROM [employer_financial].[TransferConnectionInvitationChange] AS [t1]
                                        INNER JOIN [employer_financial].[User] AS [u]
                                            ON [t1].[UserId] = [u].[Id]
                                ) AS [t0]
                                    ON [t].[Id] = [t0].[TransferConnectionInvitationId]
                            WHERE [a].[Id] = @EmployerAccountId
                                    AND [t].[Status] = 1
		                                )
                            SELECT COUNT(*) AS TotalInvitations
                            FROM Invitations;";

        var result = await GetDataAsObject(query, sqlParameters);

        return (int)result;
    }

}
