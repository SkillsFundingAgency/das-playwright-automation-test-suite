﻿using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Threading;

namespace SFA.DAS.Framework;

public class InsertTprDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.TPRDbConnectionString)
{
    private static readonly Lock _object = new();

    public string InsertSingleOrgTprData(string aornValue, string payescheme)
        => InsertTprData(aornValue, payescheme, "SingleOrg");

    public string InsertTprData(string aornValue, string payescheme, string orgType)
    {
        var queryToExecute = $"DECLARE @tprUniqueId bigint, @vartprid varchar(256), @organisationName varchar(256), @orgSK bigint; {InsertQuery(orgType, aornValue, payescheme)}";

        if (orgType == "MultiOrg") queryToExecute += InsertQuery(orgType, aornValue, payescheme);

        lock (_object)
        {
            var result = GetListOfData(queryToExecute).Result;

            var x = result[0][0];

            return Convert.ToString(x);
        }
    }

    private static string InsertQuery(string orgType, string aornValue, string payescheme)
    {
        var datetime = DateTime.Now;

        return $"SELECT @tprUniqueId = (MAX([TPRUniqueId]) +1) FROM [Tpr].[Organisation]; if (@tprUniqueId is null) set @tprUniqueId = 1;" +
                $"SET @vartprid = @tprUniqueId;" +
                $"SET @organisationName = 'AutomationTestFor{orgType}Aorn' + @vartprid;" +
                "INSERT INTO [Tpr].[Organisation] ([TPRUniqueId],[OrganisationName],[AORN],[DistrictNumber],[Reference],[AODistrict],[AOTaxType],[AOCheckChar],[AOReference],[RecordCreatedDate]) " +
                $"VALUES (@tprUniqueId, @organisationName, '{aornValue}', '1', '1', 1, '1', '1', '1', GETDATE());" +
                "SELECT @orgSK = [OrgSK] FROM [Tpr].[Organisation] where [TPRUniqueId] = @tprUniqueId;" +
                "INSERT INTO [Tpr].[OrganisationAddress] ([OrgSK],[TPRUniqueID],[OrganisationFullAddress],[AddressLine1],[AddressLine2],[AddressLine3],[PostCode],[RecordCreatedDate]) " +
                "VALUES (@orgSK, @tprUniqueId, @organisationName + 'Address', @tprUniqueId, 'Test Street', 'Coventry', 'CV1 2WT', GETDATE());" +
                "INSERT INTO [Tpr].[OrganisationPAYEScheme] ([OrgSK],[TPRUniqueID],[PAYEScheme],[SchemeStartDate],[RecordCreatedDate], [SchemeEndDateCodeDesc]) " +
                $"VALUES (@orgSK, @tprUniqueId, '{payescheme}', '{datetime.Year}-{datetime.Month}-{datetime.Day}', GETDATE(), 'Not Closed'); " +
                "SELECT @organisationName;";

    }
}
