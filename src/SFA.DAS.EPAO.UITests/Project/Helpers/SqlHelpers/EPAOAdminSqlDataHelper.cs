using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Helpers.SqlHelpers;

public class EPAOAdminSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AssessorDbConnectionString)
{
    public async Task<string> GetEPAOId(string email) => await GetDataAsString($"SELECT EndPointAssessorOrganisationId from Organisations where id = (select OrganisationId from Contacts where Email = '{email}')");

    public async Task DeleteOrganisation(string ukprn) => await ExecuteSqlCommand($"DELETE FROM Organisations WHERE EndPointAssessorUkprn = '{ukprn}'");

    public async Task DeleteContact(string email) => await ExecuteSqlCommand($"DELETE CONTACTS WHERE EMAIL = '{email}'");

    public async Task DeleteOrganisationStandard(string standardcode, string epaoid) => await ExecuteSqlCommand(
        $"DELETE FROM OrganisationStandardDeliveryArea WHERE OrganisationStandardId IN " +
        $"(select ID FROM OrganisationStandard WHERE StandardCode = '{standardcode}' AND EndPointAssessorOrganisationId = '{epaoid}'); " +
        $"DELETE FROM OrganisationStandard WHERE StandardCode = '{standardcode}' AND EndPointAssessorOrganisationId = '{epaoid}'");

    public async Task DeleteOrganisationStandardDeliveryArea(string emailid) => await ExecuteSqlCommand(
            $"DELETE from OrganisationStandardDeliveryArea where OrganisationStandardId IN " +
            $"(select id from OrganisationStandard where EndPointAssessorOrganisationId IN " +
            $"(select EndPointAssessorOrganisationId from Contacts where Email = '{emailid}'))");

    public async Task DeleteOrganisationStanard(string emailid) => await ExecuteSqlCommand(
        $"DELETE from OrganisationStandard where EndPointAssessorOrganisationId IN " +
        $"(select EndPointAssessorOrganisationId from Contacts where Email = '{emailid}')");


    public async Task DeleteEPAOOrganisation(string emailid) => await ExecuteSqlCommand(
        $" DELETE from Organisations where EndPointAssessorOrganisationId IN" +
            $" (select EndPointAssessorOrganisationId from Contacts where Email = '{emailid}')");

    public async Task UpdateOrgStatusToNew(string epaoid) => await UpdateOrgStatus("New", epaoid);

    public async Task UpdateOrgStatusToLive(string epaoid) => await UpdateOrgStatus("Live", epaoid);

    private async Task UpdateOrgStatus(string status, string epaoid) => await ExecuteSqlCommand($"Update Organisations Set Status = '{status}' Where EndPointAssessorOrganisationId = '{epaoid}'");

    public async Task UpdateCertificateToPrinted(string learnerUln) => await ExecuteSqlCommand($"UPDATE [Certificates] SET Status = 'Printed' WHERE Uln = {learnerUln}");
}