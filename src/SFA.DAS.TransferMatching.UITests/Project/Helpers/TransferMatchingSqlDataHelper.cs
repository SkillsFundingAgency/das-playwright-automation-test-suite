namespace SFA.DAS.TransferMatching.UITests.Project.Helpers;

public class TransferMatchingSqlDataHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.TMDbConnectionString)
{
    public async Task DeletePledge(List<Pledge> pledges)
    {
        foreach (var pledge in pledges)
        {
            // Match only the pledge created by this scenario; delete all applications, not just the last one.
            await ExecuteSqlCommand("""
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;
                DECLARE @pledgeId int;
                IF (SELECT COUNT(*) FROM Pledge WHERE EmployerAccountId = @EmployerAccountId
                    AND Amount = @Amount AND CreatedOn >= CONVERT(datetime, @CreatedOn, 126)) = 1
                BEGIN
                    SELECT @pledgeId = Id FROM Pledge WHERE EmployerAccountId = @EmployerAccountId
                        AND Amount = @Amount AND CreatedOn >= CONVERT(datetime, @CreatedOn, 126);
                    DELETE FROM ApplicationEmailAddress WHERE ApplicationId IN (SELECT Id FROM Application WHERE PledgeId = @pledgeId);
                    DELETE FROM ApplicationStatusHistory WHERE ApplicationId IN (SELECT Id FROM Application WHERE PledgeId = @pledgeId);
                    DELETE FROM ApplicationLocation WHERE ApplicationId IN (SELECT Id FROM Application WHERE PledgeId = @pledgeId);
                    DELETE FROM ApplicationCostProjection WHERE ApplicationId IN (SELECT Id FROM Application WHERE PledgeId = @pledgeId);
                    DELETE FROM Application WHERE PledgeId = @pledgeId;
                    DELETE FROM PledgeLocation WHERE PledgeId = @pledgeId;
                    DELETE FROM Pledge WHERE Id = @pledgeId;
                END
                COMMIT TRANSACTION;
                """, new Dictionary<string, string>()
                {
                    ["@EmployerAccountId"] = pledge.EmployerAccountId,
                    ["@Amount"] = pledge.Amount.ToString(),
                    ["@CreatedOn"] = pledge.CreatedOn.ToString("yyyy-MM-ddTHH:mm:ss")
                });
        }
    }

    public Task<int> UpdateCreatedDateForApplicationTo6WeeksAgo(string details) => UpdateCreatedDate(details, "DATEADD(WEEK, -6, GETDATE())");
    public Task<int> UpdateCreatedDateForApplicationTo5WeeksAgo(string details) => UpdateCreatedDate(details, "DATEADD(WEEK, -5, GETDATE())");
    public Task<int> UpdateCreatedDateForApplicationTo3MonthsAgo(string details) => UpdateCreatedDate(details, "DATEADD(DAY, -1, DATEADD(MONTH, -3, GETDATE()))");
    public Task<int> UpdateCreatedDateForApplicationToAWeekLessThan3MonthsAgo(string details) => UpdateCreatedDate(details, "DATEADD(WEEK, 1, DATEADD(MONTH, -3, GETDATE()))");
    private Task<int> UpdateCreatedDate(string details, string date) =>
        ExecuteSqlCommand($"UPDATE Application SET CreatedOn = {date} WHERE Details = @Details;", new Dictionary<string, string> { ["@Details"] = details });
    public Task<int> UpdateApprovedDateToBeSixWeeksAgo(string details) => UpdateHistory(details, 1, "DATEADD(WEEK, -6, GETDATE())");
    public Task<int> UpdateAcceptedDateToBe3MonthsAgo(string details) => UpdateHistory(details, 3, "DATEADD(DAY, -1, DATEADD(MONTH, -3, GETDATE()))");
    private Task<int> UpdateHistory(string details, int status, string date) =>
        ExecuteSqlCommand($"""
            UPDATE hist SET CreatedOn = {date} FROM ApplicationStatusHistory hist
            JOIN Application app ON app.Id = hist.ApplicationId
            WHERE app.Details = @Details AND app.Status = @Status AND hist.Status = @Status;
            """, new Dictionary<string, string> { ["@Details"] = details, ["@Status"] = status.ToString() });
    public async Task<(int status, int remainingAmount)> GetPledgeDetails(string details)
    {
        var data = await GetData("""
            SELECT TOP (1) pl.Status, pl.RemainingAmount FROM Application app
            JOIN Pledge pl ON pl.Id = app.PledgeId WHERE app.Details = @Details;
            """, new Dictionary<string, string> { ["@Details"] = details });
        return (int.Parse(data[0]), int.Parse(data[1]));
    }
    public async Task<int> GetPledgeApplicationStatusByDetails(string details) =>
        int.Parse((await GetData("SELECT TOP (1) Status FROM Application WHERE Details = @Details;",
            new Dictionary<string, string> { ["@Details"] = details }))[0]);
}
