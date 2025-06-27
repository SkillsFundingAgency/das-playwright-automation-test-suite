﻿using SFA.DAS.ConfigurationBuilder;
using System;

namespace SFA.DAS.EPAO.UITests.Project.Helpers.DataHelpers;

public class EPAOAdminDataHelper : EPAODataHelper
{
    public EPAOAdminDataHelper() : base()
    {
        GivenNames = GetRandomAlphabeticString(6);
        FamilyName = GetRandomAlphabeticString(6);
        Email = $"TestContact_{RandomEmail}";
        NewOrganisationName = $"New Org {GetRandomAlphabeticString(10)}";
        NewOrganisationLegalName = $"{NewOrganisationName} Legal Name";
        NewOrganisationUkprn = $"99{GetRandomNumber(6)}";
        CompanyNumber = $"76{GetRandomNumber(6)}";
        CharityNumber = $"9{GetRandomNumber(4)}-{GetRandomNumber(2)}";
        FinancialAssesmentDueDate = DateTime.Today.AddDays(100);
        LearnerUln = "7278214419";
        LearnerUlnForExistingCertificate = "4972280890";
        StandardCode = "100";
        StandardsName = "Transport planning technician";
    }

    public string LoginEmailAddress { get; set; }

    public string StandardCode { get; set; }

    public string StandardsName { get; set; }

    public static string OrganisationName => "City and Guilds";

    public static string OrganisationEpaoId => "EPA0008";

    public static string MakeLiveOrganisationEpaoId => EnvironmentConfig.IsPPEnvironment ? "EPA0337" : "EPA0001";

    public static string OrganisationUkprn => "10009931";

    public static string BatchSearch => EnvironmentConfig.IsPPEnvironment ? "298" : "142";

    public string LearnerUln { get; set; }

    public string LearnerUlnForExistingCertificate { get; set; }

    public string GivenNames { get; set; }

    public string FamilyName { get; set; }

    public string FullName => $"{GivenNames} {FamilyName}";

    public string Email { get; }

    public static string PhoneNumber => $"0844455{GetRandomNumber(4)}";

    public static DateTime StandardsEffectiveFrom => new(2015, 08, 01);

    public static DateTime OrgStandardsEffectiveFrom => StandardsEffectiveFrom.AddDays(35);

    public string NewOrganisationName { get; }

    public string NewOrganisationLegalName { get; }

    public string NewOrganisationUkprn { get; }

    public string CompanyNumber { get; }

    public string CharityNumber { get; }

    public static string StreetAddress1 => "5 Quninton Road";

    public static string StreetAddress2 => "Cheylsmore Avuene";

    public static string StreetAddress3 => "Warkwickshire";

    public DateTime FinancialAssesmentDueDate { get; }
}
