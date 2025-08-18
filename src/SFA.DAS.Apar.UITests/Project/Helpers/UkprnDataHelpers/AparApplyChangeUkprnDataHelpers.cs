namespace SFA.DAS.Apar.UITests.Project.Helpers.UkprnDataHelpers;

public class AparApplyChangeUkprnDataHelpers : AparUkprnBaseDataHelpers
{
    public AparApplyChangeUkprnDataHelpers() : base() => AddApplyDatahelpers();

    public (string email, string ukprn, string newukprn) GetRoatpChangeUkprnAppplyData(string key) => GetData(key, emailkey, ukprnkey, newukprnkey);

    private void AddApplyDatahelpers()
    {
        _data.Add("rpchangeukprn01",
            [
                new(emailkey, "sudhakar.chinoor+ChangeUKPRNJourney@digital.education.gov.uk"),
                new(ukprnkey, "10084176"),
                new(newukprnkey, "10084177")
            ]);
    }
}
