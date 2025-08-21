﻿namespace SFA.DAS.Apar.UITests.Project.Helpers.UkprnDataHelpers;

public class AparFullUkprnDataHelpers : AparUkprnBaseDataHelpers
{
    public AparFullUkprnDataHelpers() : base() => AddE2EDatahelpers();

    public (string email, string providername, string ukprn) GetRoatpE2EData(string key) => GetData(key, emailkey, providernamekey, ukprnkey);

    private void AddE2EDatahelpers()
    {
        _data.Add("rpendtoend01apply",
        [
                new(emailkey, "sudhakar.chinoor+NewDemo@digital.education.gov.uk"),
            new(providernamekey, "SIMPLY CREATING CHANGE LTD"),
            new(ukprnkey, "10082167"),
        ]);
        _data.Add("rpip01",
        [
                new(emailkey, "sudhakar.chinoor+InProgressApplication@digital.education.gov.uk"),
            new(providernamekey, "THE PAROCHIAL CHURCH COUNCIL OF THE ECCLESIASTICAL PARISH OF THE GOOD SHEPHERD"),
            new(ukprnkey, "10065943"),
        ]);
        _data.Add("rpendtoend02apply",
        [
                new(emailkey, "sudhakar.chinoor+E2E02@digital.education.gov.uk"),
            new(providernamekey, "METROPOLITAN TABERNACLE"),
            new(ukprnkey, "10068436"),
        ]);
        _data.Add("rpexistingprovider01",
           [
                new(emailkey, "sudhakar.chinoor+roatp2@digital.education.gov.uk"),
               new(providernamekey, "CHRYSALIS NOT FOR PROFIT LIMITED"),
               new(ukprnkey, "10047121"),
           ]);
        _data.Add("rpexistingprovider02",
           [
                new(emailkey, "sudhakar.chinoor+employer@digital.education.gov.uk"),
               new(providernamekey, "GATESHEAD VISIBLE ETHNIC MINORITIES SUPPORT GROUP"),
               new(ukprnkey, "10061310"),
           ]);
        _data.Add("rpexistingprovider03",
         [
                new(emailkey, "sudhakar.chinoor+supporting@digital.education.gov.uk"),
             new(providernamekey, "COLEMAN TRAINING & CONSULTANCY"),
             new(ukprnkey, "10083833"),
         ]);
        _data.Add("rpexistingprovider04",
          [
                new(emailkey, "sudhakar.chinoor+mainCC@digital.education.gov.uk"),
              new(providernamekey, "SIGTA LIMITED"),
              new(ukprnkey, "10005839"),
          ]);
        _data.Add("rpexistingprovider05",
         [
                new(emailkey, "umakanth.gangaraju+FHAExempt@digital.education.gov.uk"),
             new(providernamekey, "TALENTINO LIMITED"),
             new(ukprnkey, "10046095"),
         ]);
        _data.Add("rpexistingprovider06",
        [
                new(emailkey, "sudhakar.chinoor+GOVT@digital.education.gov.uk"),
            new(providernamekey, "WILLIAMSTON PRIMARY SCHOOL"),
            new(ukprnkey, "10052113"),
        ]);
    }
}
