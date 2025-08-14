namespace SFA.DAS.Apar.UITests.Project.Helpers.DataHelpers;

public class RoatpApplyCreateUserDataHelper : GovStubSignCreateUserDataHelper
{
    // This parameterless constructor is used to create instance from a specflow table
    public RoatpApplyCreateUserDataHelper() : base()
    {
        CreateAccountIdOrUserRef = Guid.NewGuid().ToString();
    }

    public string CreateAccountIdOrUserRef { get; private set; }

    public void UpdateData(RoatpApplyCreateUserDataHelper data)
    {
        GivenName = data.GivenName;

        FamilyName = GetFamilyName(data.FamilyName);

        CreateAccountEmail = $"{GivenName}{FamilyName}@digital.education.gov.uk";
    }
}
