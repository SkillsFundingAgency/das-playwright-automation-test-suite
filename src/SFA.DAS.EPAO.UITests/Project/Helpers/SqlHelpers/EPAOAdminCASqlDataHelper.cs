using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Helpers.SqlHelpers;

public partial class EPAOAdminCASqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AssessorDbConnectionString)
{
    public async Task DeleteCertificate(string uln, string standardcode)
    {
        if (string.IsNullOrEmpty(uln)) await Task.CompletedTask;

        await ExecuteSqlCommand($"DELETE from certificatelogs where certificateid = (select id from certificates where uln = {uln} and StandardCode = {standardcode});" +
        $"DELETE from certificates where uln = {uln} and StandardCode = {standardcode}");

        EPAOCAInUseUlns.RemoveInUseUln(uln);
    }

    public async Task<List<string>> GetStaticTestData((string familyName, string uln) data)
    {
        var query = $"select top 1 uln, StdCode, title, GivenNames, FamilyName from [Ilrs] join standards on larscode = stdcode and uln = {data.uln} and FamilyName = '{data.familyName}'";

        var testdata = await GetData(query);

        return GetTestData(testdata);
    }

    public async Task<List<string>> GetCATestData(string email, LearnerCriteria learnerCriteria)
    {
        var testdata = await GetTestData(email, learnerCriteria);

        return GetTestData(testdata);
    }

    private static List<string> GetTestData(List<string> testdata)
    {
        List<string> data = [];

        int i = 0;

        while (i <= 2)
        {
            data = testdata;

            var uln = data[0];

            if (!string.IsNullOrEmpty(uln) && EPAOCAInUseUlns.IsNotInUseUln(uln))
            {
                return data;
            }
            else i++;
        }

        return data;
    }

    private async Task<List<string>> GetTestData(string email, LearnerCriteria learnerCriteria)
    {
        string query = FileHelper.GetSql(GetLearnersDataSqlFileName(learnerCriteria));

        Dictionary<string, string> sqlParameters = new()
        {
            { "@endPointAssessorEmail", email }
        };

        query = GetTestData(query, learnerCriteria.IsActiveStandard, learnerCriteria.HasMultipleVersions, learnerCriteria.WithOptions, learnerCriteria.VersionConfirmed, learnerCriteria.OptionIsSet);

        return await GetData(query, sqlParameters);
    }

    private static string GetTestData(string sqlQueryFromFile, bool isActiveStandard, bool hasMultipleVersions, bool withOptions, bool versionConfirmed, bool optionSet)
    {
        sqlQueryFromFile = IsActiveStandardRegex().Replace(sqlQueryFromFile, isActiveStandard ? $"{1}" : $"{0}");
        sqlQueryFromFile = HasVersionsRegex().Replace(sqlQueryFromFile, hasMultipleVersions ? $"{1}" : $"{0}");
        sqlQueryFromFile = HasOptionsRegex().Replace(sqlQueryFromFile, withOptions ? $"{1}" : $"{0}");
        sqlQueryFromFile = InUseUlnRegex().Replace(sqlQueryFromFile, EPAOCAInUseUlns.GetInUseUln());

        sqlQueryFromFile = VersionConfirmedRegex().Replace(sqlQueryFromFile, versionConfirmed ? $"{1}" : $"{0}");
        sqlQueryFromFile = OptionSetRegex().Replace(sqlQueryFromFile, optionSet ? $"{1}" : $"{0}");


        return sqlQueryFromFile;
    }

    private static string GetLearnersDataSqlFileName(LearnerCriteria learnerCriteria) => learnerCriteria.HasMultiStandards ? "GetMultiStandardLearnersData" : "GetSingleStandardLearnersData";

    [GeneratedRegex(@"__Isactivestandard__")]
    private static partial Regex IsActiveStandardRegex();
    [GeneratedRegex(@"__HasVersions__")]
    private static partial Regex HasVersionsRegex();
    [GeneratedRegex(@"__HasOptions__")]
    private static partial Regex HasOptionsRegex();
    [GeneratedRegex(@"__InUseUln__")]
    private static partial Regex InUseUlnRegex();
    [GeneratedRegex(@"__VersionConfirmed__")]
    private static partial Regex VersionConfirmedRegex();
    [GeneratedRegex(@"__OptionSet__")]
    private static partial Regex OptionSetRegex();
}
