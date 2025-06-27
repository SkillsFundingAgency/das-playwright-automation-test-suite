using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Helpers.SqlHelpers;

public partial class EPAOApplySqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AssessorDbConnectionString)
{
    public async Task DeleteCertificate(string uln)
    {
        await ExecuteSqlCommand($"DELETE FROM [CertificateLogs] WHERE CertificateId IN (SELECT Id FROM [Certificates] WHERE ULN = '{uln}')");
        await ExecuteSqlCommand($"DELETE FROM [Certificates] WHERE ULN = '{uln}'");
    }

    public async Task DeleteOrganisationStandardVersion()
    {
        await ExecuteSqlCommand($"DELETE FROM [OrganisationStandardVersion] where OrganisationStandardId " +
            $"in(select Id from OrganisationStandard where StandardCode = 128 And EndPointAssessorOrganisationId = 'EPA0008') And Version in (1.1)");
    }

    public async Task ResetApplyUserOrganisationId(string applyUserEmail)
    {
        var organisationId = GetDataAsString($"SELECT OrganisationId from Contacts where Email = '{applyUserEmail}'");
        if (organisationId.Equals("")) return;
        await ExecuteSqlCommand($"UPDATE Contacts SET OrganisationID = null WHERE Email = '{applyUserEmail}'");
        await ExecuteSqlCommand($"DELETE from Apply where OrganisationId = '{organisationId}'");
    }

    public async Task DeleteAnyOtherOrganisationId(string applyUserEmail)
    {
        await ExecuteSqlCommand($"DELETE from Contacts where OrganisationId = " +
            $"(select id from Organisations where EndPointAssessorOrganisationId = " +
            $"(select EndPointAssessorOrganisationId from Contacts where Email = '{applyUserEmail}') " +
            $"and Email != '{applyUserEmail}')");
    }

    public async Task ResetApplyUserEPAOId(string applyUserEmail) => await ExecuteSqlCommand($"update Contacts set EndPointAssessorOrganisationId = null, [Status] = 'New' where Email = '{applyUserEmail}'");

    public async Task DeleteStandardApplicication(string standardcode, string organisationId, string userid) => await ExecuteSqlCommand($"DELETE from [Apply] where OrganisationId = (select Id from Organisations WHERE EndPointAssessorOrganisationId = '{organisationId}') and CreatedBy = (select Id from Contacts where Email = '{userid}') and StandardCode = {standardcode}");

    public async Task<bool> HasWithdrawals(string email)
    {
        var sqlQueryFromFile = EmailRegex().Replace(FileHelper.GetSql("HasWithdrawals"), email);

        var data = await GetData(sqlQueryFromFile, connectionString);

        return data.Count != 0 && data.First() == "1";
    }

    public async Task ResetStandardWithdrawals(string email)
    {
        var sqlQueryFromFile = EmailRegex().Replace(FileHelper.GetSql("ResetStandardWithdrawals"), email);
        await ExecuteSqlCommand(sqlQueryFromFile, connectionString);
    }

    public async Task ResetRegisterWithdrawals(string email)
    {
        var sqlQueryFromFile = EmailRegex().Replace(FileHelper.GetSql("ResetRegisterWithdrawals"), email);
        await ExecuteSqlCommand(sqlQueryFromFile, connectionString);
    }

    [GeneratedRegex(@"__email__")]
    private static partial Regex EmailRegex();
}