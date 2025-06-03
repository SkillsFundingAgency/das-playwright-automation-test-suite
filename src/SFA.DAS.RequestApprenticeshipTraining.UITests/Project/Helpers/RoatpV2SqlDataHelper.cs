namespace SFA.DAS.RequestApprenticeshipTraining.UITests.Project.Helpers;

public class RoatpV2SqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ManagingStandardsDbConnectionString)
{
    public async Task<string> GetTitlethatProviderDeosNotOffer(string ukprn) => await GetDataAsString($"SELECT top 1 Title FROM [dbo].[Standard] WHERE LarsCode NOT IN ({ProviderCourseQuery(ukprn)}) order by NEWID();");


    private static string ProviderCourseQuery(string ukprn) => $"SELECT LarsCode FROM [dbo].[ProviderCourse] WHERE ProviderId = (SELECT id FROM [Provider] WHERE ukprn = {ukprn})";
}
