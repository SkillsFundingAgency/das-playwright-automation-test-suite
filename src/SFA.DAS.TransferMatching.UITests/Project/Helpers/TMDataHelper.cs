namespace SFA.DAS.TransferMatching.UITests.Project.Helpers;

public class TMDataHelper
{
    internal TMDataHelper(SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.Course course)
    {
        Course = course.Title;
        Cost = course.MaxFunding;
    }

    public static string ApprenticeshipMoreDetails => RandomDataGenerator.GenerateRandomAlphabeticString(25);
    public static int GenerateRandomNumberMoreThanMaxAmount(int max) => Random.Shared.Next(max + 1, checked(max * 2));
    public static int GenerateRandomNumberLessThanMaxAmount(int max) => Random.Shared.Next(1, max);
    public int GenerateRandomNumberMoreThanMinAmount(int max) => Random.Shared.Next(MinAmount, max);
    public static string GetRandomLocation() => RandomDataGenerator.GetRandomElementFromListOfElements(
        new List<string> { "Coventry, West Midlands", "Manchester, Greater Manchester", "Sheffield, South Yorkshire" });
    public string Course { get; }
    public int Cost { get; set; }
    public int NoOfApprentice { get; set; } = 1;
    public int Duration { get; set; } = 24;
    public int MinAmount => Cost * NoOfApprentice;
    public int MinimalPledgeAmount => Cost * (NoOfApprentice + 1);
    public DateTime CourseStartDate { get; set; } = DateTime.Now;
    public string GetEstimatedCostOfTrainingForApplicationDetail() =>
        (Duration <= 12 ? Cost * NoOfApprentice : Cost * NoOfApprentice * 0.8 / Duration * 12).ToString("N0");
}
