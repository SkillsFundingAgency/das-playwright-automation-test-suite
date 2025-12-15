using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Helpers
{
    internal class SharedTestContext
    {
        public List<Apprenticeship> ListOfApprenticeships { get; set; }
    }

    public static class ScenarioKeys
    {
        public const string ListOfApprenticeship = "listOfApprenticeship";
        public const string ListOfUpdatedApprenticeship = "listOfUpdatedApprenticeship";
    }

}
