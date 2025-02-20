

using SFA.DAS.Framework;
using SFA.DAS.MongoDb.DataGenerator;
using SFA.DAS.UI.FrameworkHelpers;

namespace SFA.DAS.Registration.UITests.Project.Helpers;

public class TprSqlDataHelper(DbConfig dbConfig, ObjectContext objectContext, AornDataHelper aornDataHelper)
{
    public async Task<(string paye, string aornNumber, string orgName)> CreateAornData(bool isSingleOrg) => isSingleOrg ? await CreateSingleOrgAornData() : await CreateMultiOrgAORNData();

    public async Task<(string paye, string aornNumber, string orgName)> CreateSingleOrgAornData() => await CreateAornData("SingleOrg");

    public async Task<(string paye, string aornNumber, string orgName)> CreateMultiOrgAORNData() => await CreateAornData("MultiOrg");

    private async Task<(string paye, string aornNumber, string orgName)> CreateAornData(string orgType)
    {
        var aornNumber = aornDataHelper.AornNumber;

        var paye = objectContext.GetGatewayPaye(0);

        var organisationName = await new InsertTprDataHelper(objectContext, dbConfig).InsertTprData(aornNumber, paye, orgType);

        objectContext.SetOrganisationName(organisationName);

        objectContext.UpdateAornNumber(aornNumber, 0);

        return (paye, aornNumber, organisationName);
    }
}
