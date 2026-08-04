
using NUnit.Framework.Internal;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ProvideFeedback.UITests.Project.Helpers;


public class AssessorSqlHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.AssessorDbConnectionString)
{

    public async Task<(string Uln, string StandardName, string AchievementDate, string ProviderName)> SingleCertificateAuthorisationdetailsfromuser(string firstname, string lastname)
    {
        var data = await GetData(
            $"SELECT TOP (1) " +
            $"uln, " +
            $"StandardName, " +
            $"YEAR(achievementDate) AS AchievementYear, " +
            $"ProviderName " +
            $"FROM [dbo].[Certificates] " +
            $"WHERE LearnerGivenNames = '{firstname}' " +
            $"AND LearnerFamilyName = '{lastname}'");

        return (
            data[0],
            data[1],
            data[2],
            data[3]
        );
    }


    public async Task<(string Uln, string FrameworkName, string CertificationYear, string ProviderName)>SingleFrameworkAuthorisationdetailsfromuser(string firstname, string lastname)
    {
        var data = await GetData(
            $"SELECT TOP (1) " +
            $"ApprenticeULN, " +
            $"FrameworkName, " +
            $"CertificationYear, " +
            $"ProviderName " +
            $"FROM [dbo].[frameworkLearner] " +
            $"WHERE CertificateGivenNames = '{firstname}' " +
            $"AND CertificateFamilyName = '{lastname}'");

        return (
            data[0],
            data[1],
            data[2],
            data[3]
        );
    }

    public async Task<(string Uln, string StandardName, string AchievementDate, string ProviderName)>
     MultiCertificateAuthorisationdetailsfromuser(string firstname, string lastname)
    {
        var data = await GetData(
            $"SELECT TOP (1) " +
            $"uln, " +
            $"StandardName, " +
            $"YEAR(achievementDate) AS AchievementYear, " +
            $"ProviderName " +
            $"FROM [dbo].[Certificates] " +
            $"WHERE LearnerGivenNames = '{firstname}' " +
            $"AND LearnerFamilyName = '{lastname}' " +
            $"ORDER BY CertificateReference");

        return (
            data[0],
            data[1],
            data[2],
            data[3]
        );
    }


}
   