﻿namespace SFA.DAS.EPAO.UITests.Project.Helpers.DataHelpers;

public class EPAOApplyStandardDataHelper : EPAODataHelper
{
    public EPAOApplyStandardDataHelper() : base() { }

    public static string ApplyStandardName => "Solicitor";

    public static string ApplyStandardCode => "43";

    public static string StandardAssessorOrganisationEpaoId => "EPA0002";

    public static string GenerateRandomAlphanumericString(int length) => RandomDataGenerator.GenerateRandomAlphanumericString(length);

    public static string GenerateRandomWholeNumber(int length) => RandomDataGenerator.GenerateRandomWholeNumber(length);
}
