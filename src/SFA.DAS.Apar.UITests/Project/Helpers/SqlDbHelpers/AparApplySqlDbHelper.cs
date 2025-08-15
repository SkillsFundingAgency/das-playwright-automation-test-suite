namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class AparApplySqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ApplyDatabaseConnectionString)
{
    public async Task GateWayClearDownDataFromApply(string ukprn)
    {
        var applicationId = GetDataAsString($"SELECT ApplicationId from dbo.Apply where ukprn = {ukprn}");

        var GateWayResetQuery = $"{GetApplicationId(ukprn)} DELETE FROM dbo.gatewayanswer WHERE ApplicationId = @ApplicationID; " +
        $" DELETE FROM dbo.AssessorPageReviewOutcome WHERE ApplicationId =  @ApplicationID; " +
        $" DELETE FROM dbo.ModeratorPageReviewOutcome WHERE ApplicationId =  @ApplicationID; " +
        $" DELETE FROM [dbo].[AppealFile] where ApplicationId = @ApplicationID;" +
        $" DELETE FROM Appeal where ApplicationId = @ApplicationID;" +
        $" DELETE FROM [dbo].[OversightReview] where ApplicationId = @ApplicationID;" +
        $" DELETE FROM dbo.Audit WHERE UpdatedState like '%{applicationId}%'; " +
        $" UPDATE Apply set GatewayReviewStatus = 'New' , Applicationstatus = 'Submitted' WHERE [UKPRN] = {ukprn} " +
        $" DELETE FROM [dbo].[FinancialReviewClarificationFile] where ApplicationId = @ApplicationID; " +
        $" DELETE FROM Financialreview where ApplicationId = @ApplicationID; " +
        $" Update dbo.Apply set  [Assessor1UserId] = null, [Assessor2UserId] = null, [Assessor1Name] = null, [Assessor2Name] = null," +
        $" [Assessor1ReviewStatus] = null, [Assessor2ReviewStatus] = null, [ModerationStatus] = 'New', " +
        $" [GatewayUserId] = null, [GatewayUserName] = null, " +
        $" [AssessorReviewStatus] = 'New', [ApplicationDeterminedDate] = null where ApplicationId = @ApplicationID";


        await ExecuteSqlCommand(GateWayResetQuery);
    }

    public async Task FHAClearDownDataFromApply(string ukprn)
    {
        var FhaResetQuery = $"{GetApplicationId(ukprn)} UPDATE Apply set GatewayReviewStatus = 'Pass' , Applicationstatus = 'GatewayAssessed'," +
        $" [Assessor1UserId] = null, [Assessor2UserId] = null, [Assessor1Name] = null, [Assessor2Name] = null," +
        $" [Assessor1ReviewStatus] = null, [Assessor2ReviewStatus] = null, [ModerationStatus] = 'New', " +
        $" [AssessorReviewStatus] = 'New', [ApplicationDeterminedDate] = null  WHERE ApplicationId =  @ApplicationID; " +
        $" DELETE FROM [dbo].[AppealFile] where ApplicationId = @ApplicationID;" +
        $" DELETE FROM Appeal where ApplicationId = @ApplicationID;" +
        $" DELETE FROM [dbo].[FinancialReviewClarificationFile] where ApplicationId = @ApplicationID; " +
        $" DELETE FROM Financialreview where ApplicationId = @ApplicationID; " +
        $" DELETE FROM [dbo].[OversightReview] where ApplicationId = @ApplicationID;" +
        $" DELETE FROM dbo.AssessorPageReviewOutcome WHERE   ApplicationId =  @ApplicationID; " +
        $" DELETE FROM dbo.ModeratorPageReviewOutcome WHERE  ApplicationId =  @ApplicationID";

        await ExecuteSqlCommand(FhaResetQuery);
    }

    public async Task AssessorClearDownDataFromApply(string ukprn)
    {
        var AssessorResetQuery = $"{GetApplicationId(ukprn)} DELETE FROM dbo.AssessorPageReviewOutcome WHERE ApplicationId = @ApplicationID; " +
        $" DELETE FROM dbo.ModeratorPageReviewOutcome WHERE ApplicationId =  @ApplicationID; " +
        $" DELETE FROM dbo.OversightReview WHERE ApplicationId =  @ApplicationID; " +
        $" DELETE FROM [dbo].[AppealFile] where ApplicationId = @ApplicationID;" +
        $" DELETE FROM Appeal where ApplicationId = @ApplicationID;" +
        $" Update dbo.Apply set  [Assessor1UserId] = null, [Assessor2UserId] = null, [Assessor1Name] = null, [Assessor2Name] = null," +
        $" [Assessor1ReviewStatus] = null, [Assessor2ReviewStatus] = null, [ModerationStatus] = 'New', Applicationstatus = 'GatewayAssessed', " +
        $" [AssessorReviewStatus] = 'New', [ApplicationDeterminedDate] = null where ApplicationId = @ApplicationID";

        await ExecuteSqlCommand(AssessorResetQuery);
    }

    public async Task ModeratorClearDownDataFromApply(string ukprn)
    {
        var ModeratorResetQuery = $"{GetApplicationId(ukprn)} " +
        $" Update dbo.ModeratorPageReviewOutcome set [ModeratorUserId] = null, [ModeratorUserName] = null, [ModeratorReviewStatus] = null, " +
        $" [UpdatedAt] = null, [UpdatedBy] = null, [ModeratorReviewComment] = null, [ClarificationUserId] = null,[ClarificationUserName] = null, " +
        $" [ClarificationStatus] = null, [ClarificationComment]= null, [ClarificationResponse]= null, [ClarificationFile]= null ," +
        $" [ClarificationUpdatedAt]= null WHERE ApplicationId =  @ApplicationID; " +
        $" DELETE FROM [dbo].[AppealFile] where ApplicationId = @ApplicationID;" +
        $" DELETE FROM Appeal where ApplicationId = @ApplicationID;" +
        $" DELETE FROM [dbo].[OversightReview] where ApplicationId = @ApplicationID;" +
        $" Update dbo.Apply set [ModerationStatus] = 'New', Applicationstatus = 'GatewayAssessed', " +
        $" [AssessorReviewStatus] = 'New', [ApplicationDeterminedDate] = null where ApplicationId = @ApplicationID";

        await ExecuteSqlCommand(ModeratorResetQuery);
    }

    public async Task ClarificationClearDownFromApply(string ukprn)
    {
        var ClarificationResetQuery = $"{GetApplicationId(ukprn)}  DELETE FROM[dbo].[AppealFile] where ApplicationId = @ApplicationID; " +
            $"DELETE FROM Appeal where ApplicationId = @ApplicationID;" +
            $"DELETE FROM [dbo].[OversightReview] where ApplicationId = @ApplicationID;" +
            $"UPDATE Apply set[ModerationStatus] = 'Clarification Sent', Applicationstatus = 'GatewayAssessed', [AssessorReviewStatus] = 'New',[ApplicationDeterminedDate] = NULL  WHERE ApplicationId = @ApplicationID;" +
            $"UPDATE ModeratorPageReviewOutcome set ClarificationUserId = NULL, ClarificationUserName = NULL, ClarificationStatus = NULL, ClarificationComment = NULL, ClarificationFile = NULL, " +
            $"ClarificationResponse = NULL,  ClarificationUpdatedAt = NULL WHERE ApplicationId = @ApplicationID ";

        await ExecuteSqlCommand(ClarificationResetQuery);
    }

    public async Task OversightReviewClearDownFromApply(string ukprn) => await ExecuteSqlCommand($"{GetApplicationId(ukprn)}" +
        $"DELETE FROM [dbo].[AppealFile] where ApplicationId = @ApplicationID;" +
        $"DELETE FROM Appeal where ApplicationId = @ApplicationID;" +
        $"DELETE FROM [dbo].[OversightReview] where ApplicationId = @ApplicationID;" +
        $"UPDATE Apply set Applicationstatus = 'GatewayAssessed' WHERE ApplicationId = @ApplicationID");

    public async Task OversightReviewClearDownFromApply_GatewayReject(string ukprn) => await ExecuteSqlCommand($"{GeRejectedApplication_ApplicationId(ukprn)}" +
       $"UPDATE Apply set [ApplyData] = JSON_MODIFY(JSON_MODIFY(JSON_Modify(ApplyData, '$.ApplyDetails.RequestToReapplyMade', null), '$.ApplyDetails.RequestToReapplyBy', null), '$.ApplyDetails.RequestToReapplyOn', null) " +
        $"Where ApplicationId =  @ApplicationID");

    private static string GetApplicationId(string ukprn) => $"DECLARE @ApplicationID UNIQUEIDENTIFIER; SELECT @ApplicationID = ApplicationId FROM dbo.apply WHERE [UKPRN] = {ukprn};";

    private static string GeRejectedApplication_ApplicationId(string ukprn) => $"DECLARE @ApplicationID UNIQUEIDENTIFIER; SELECT @ApplicationID = applicationid from apply where ukprn = {ukprn} and ApplicationStatus = 'Rejected';";

    public async Task Delete_AllowListProviders(string ukprn) => await ExecuteSqlCommand($"DELETE FROM AllowedProviders WHERE [UKPRN] = {ukprn}");
}
