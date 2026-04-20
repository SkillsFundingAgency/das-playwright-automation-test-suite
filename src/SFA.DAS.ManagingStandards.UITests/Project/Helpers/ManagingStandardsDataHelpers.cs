namespace SFA.DAS.ManagingStandards.UITests.Project.Helpers;

public record StandardsTestData
{
    public string LarsCode;
    public string LarsCode_Delete;
    public string StandardName;
    public string Venue;
    public string ApprenticeshipUnitAILarsCode;
    public string ApprenticeshipUnitEVLarsCode;
}

public class ManagingStandardsDataHelpers
{
    public const string LocationName = "Test Demo Automation Venue";

    public const string UpdatedLocationName = "Test Demo Automation Venue";
    public string UpdatedVenueName { get; init; } = UpdatedLocationName;
    public string EmailAddress { get; init; } = "ManagingStandardstest.demo@l38cxwya.mailosaur.net";
    public string UpdatedEmailAddress { get; init; } = "UpdatedManagingStandardstest.demo@l38cxwya.mailosaur.net";
    public string NewlyUpdatedEmailAddress { get; init; } = "NewlyUpdatedManagingStandardstest.demo@l38cxwya.mailosaur.net";
    public string VenueName { get; init; } = LocationName;
    public string Standard_ActuaryLevel7 { get; init; } = "Actuary (level 7)";
    public string Apprenticeshipunit_AIleadership{ get; init; } = "AI leadership";
    public string Apprenticeshipunit_AILarsCode { get; init; } = "ZSC00003";
    public string Apprenticeshipunit_Electricvehicle { get; init; } = "Electric vehicle (EV)";
    public string Apprenticeshipunit_EVLarsCode { get; init; } = "ZSC00004";
    public string Apprenticeshipunit_ElectricalFitting { get; init; } = "Electrical fitting and";
    public string Apprenticeshipunit_EFLarsCode { get; init; } = "ZSC00005";
    public string Standard_CraftPlastererlevel { get; init; } = "Craft plasterer (level 3)";
    public string PostCode { get; init; } = "Tw14 9py";
    public string FullAddressDetails { get; init; } = "160 Hatton Road, Feltham, TW14 9PY";
    public string Website { get; init; } = "www.company.co.uk";
    public string UpdatedWebsite { get; init; } = "www.123company.co.uk";
    public string ContactWebsite { get; init; } = "www.companycontact.co.uk";
    public string ContactNumber { get; init; } = "12345678910";
    public string UpdatedContactNumber { get; init; } = "0999999399333";
    public string NewlyUpdatedContactNumber { get; init; } = "98989843434334";
    public static StandardsTestData StandardsTestData => new() { LarsCode = "281", StandardName = "Podiatrist (level 6)", Venue = LocationName ,
        LarsCode_Delete = "255", ApprenticeshipUnitAILarsCode = "ZSC00003", ApprenticeshipUnitEVLarsCode = "ZSC00006", };
    public string UpdateProviderDescriptionText { get; init; } = RandomDataGenerator.GenerateRandomAlphanumericString(20);
   
}

