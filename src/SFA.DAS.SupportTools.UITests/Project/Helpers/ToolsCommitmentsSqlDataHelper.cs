namespace SFA.DAS.SupportTools.UITests.Project.Helpers;

public class ToolsCommitmentsSqlDataHelper(ObjectContext objectContext, DbConfig dBConfig) : SqlDbHelper(objectContext, dBConfig.CommitmentsDbConnectionString)
{
    public async Task<int> UpdateApprenticeshipStatus(string sqlQueryToSetDataLockSuccessStatus)
    {
        return await ExecuteSqlCommand(sqlQueryToSetDataLockSuccessStatus);
    }

    public static string GetUpdateApprenticeshipStatus(string uln, int status)
    {
        string stopDate, pauseDate, completionDate;

        if (uln.Equals(null))
            throw new Exception("ULN is not set");

        if (status < 1 || status > 4)
            throw new Exception("Invalid Status!");


        pauseDate = status == 2 ? DateTime.Now.ToString("yyyy-MM-dd") : null;
        stopDate = status == 3 ? DateTime.Now.ToString("yyyy-MM-dd") : null;
        completionDate = status == 4 ? DateTime.Now.ToString("yyyy-MM-dd") : null;

        return $"UPDATE Apprenticeship " +
            $"SET PaymentStatus = {status}, StopDate = '{stopDate}', PauseDate = '{pauseDate}', CompletionDate = '{completionDate}' " +
            $"WHERE ULN = '{uln}';";
    }
}
