using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
internal class CoursesDataHelper
{
    private static readonly List<Course> AllCourses = new()
    {
        // Standard Courses
        new() { LarsCode = "2", Title = "Software developer", MaxFunding = 18000, EffectiveFrom = new(2014, 08, 01), Level = 4, Version = "1.1" },
        new() { LarsCode = "101", Title = "Retailer", MaxFunding = 5000, EffectiveFrom = new(2015, 08, 01), Level = 5, Version = "1.2" },
        new() { LarsCode = "119", Title = "Adult care worker", MaxFunding = 4000, EffectiveFrom = new(2023, 06, 26), Level = 2, Version = "1.0" },
        new() { LarsCode = "274", Title = "Abattoir worker", MaxFunding = 6000, EffectiveFrom = new(2018, 05, 08), Level = 2, Version = "1.1" },
        new() { LarsCode = "297", Title = "Teaching assistant", MaxFunding = 7000, EffectiveFrom = new(2018, 06, 26), Level = 3, Version = "1.1" },
        new() { LarsCode = "409", Title = "Registered nurse degree", MaxFunding = 26000, EffectiveFrom = new(2019, 02, 13), Level = 6, Version = "1.1" },
        new() { LarsCode = "530", Title = "Fire safety inspector", MaxFunding = 11000, EffectiveFrom = new(2019, 11, 20), Level = 4, Version = "1.1" },
        new() { LarsCode = "567", Title = "Florist", MaxFunding = 18000, EffectiveFrom = new(2020, 06, 10), Level = 2, Version = "1.0" },
        new() { LarsCode = "204", Title = "Accountancy or taxation professional", MaxFunding = 21000, EffectiveFrom = new(2017, 11, 07), Level = 7, Version = "1.0" },
        new() { LarsCode = "586", Title = "Senior journalist", MaxFunding = 14000, EffectiveFrom = new(2020, 08, 07), Level = 7, Version = "1.0" },
        new() { LarsCode = "580", Title = "Health and care intelligence specialist", MaxFunding = 17000, EffectiveFrom = new(2020, 07, 10), Level = 7, Version = "1.0" },

        // Foundation Courses
        new() { LarsCode = "805", Title = "Building service engineering foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },
        new() { LarsCode = "806", Title = "Finishing trades foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },
        new() { LarsCode = "807", Title = "Onsite trades foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },
        new() { LarsCode = "808", Title = "Hardware, network and infrastructure foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },
        new() { LarsCode = "809", Title = "Software and data foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },
        new() { LarsCode = "810", Title = "Engineering and manufacturing foundation apprenticeship", MaxFunding = 4500, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },
        new() { LarsCode = "811", Title = "Health and social care foundation apprenticeship", MaxFunding = 3000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = LearningType.FoundationApprenticeship, Level = 2, Version = "1.0" },

        // Short Courses
        new() { LarsCode = "ZSC00002", Title = "AI leadership – Developing AI strategy – Apprenticeship unit", MaxFunding = 1000, EffectiveFrom = new(2026, 03, 17), ApprenticeshipType = LearningType.ShortCourses, Level = 2, Version = "1.0" },
        new() { LarsCode = "ZSC00004", Title = "Welding (mechanised) – Apprenticeship unit", MaxFunding = 1000, EffectiveFrom = new(2026, 03, 17), ApprenticeshipType = LearningType.ShortCourses, Level = 2, Version = "1.0" },
        new() { LarsCode = "ZSC00006", Title = "Electric vehicle (EV) charging point installation and maintenance – Apprenticeship unit", MaxFunding = 1000, EffectiveFrom = new(2026, 03, 17), ApprenticeshipType = LearningType.ShortCourses, Level = 3, Version = "1.0" },
        new() { LarsCode = "ZSC00007", Title = "Solar PV installation and maintenance – Apprenticeship unit", MaxFunding = 1000, EffectiveFrom = new(2026, 03, 17), ApprenticeshipType = LearningType.ShortCourses, Level = 3, Version = "1.0" },
        

        // Courses with Options      
        new() { LarsCode = "57", Title = "Gas network craftsperson", MaxFunding = 27000, EffectiveFrom = new(2015, 08, 01), Level = 3, Version = "1.3", Options = new() { "Emergency Response Craftsperson", "Network Pipelines Maintenance Craftsperson", "Network Maintenance Craftsperson (Pressure Management)", "Network Maintenance Craftsperson (Electrical & Instrumentation)" } },      
        new() { LarsCode = "87", Title = "Aviation ground operative", MaxFunding = 3000, EffectiveFrom = new(2015, 08, 01), Level = 2, Version = "1.0", Options = new() { "Aircraft Handling", "Aircraft Movement", "Fire Fighter", "Flight Operations", "Passenger Services" } },
        new() { LarsCode = "117", Title = "Professional accounting or taxation technician", MaxFunding = 12000, EffectiveFrom = new(2015, 08, 01), Level = 4, Version = "1.1", Options = new() { "Accounting", "Tax" } }
    };

    private static readonly Random Random = new();

    internal async Task<Course> GetCourse(string larsCode)
    {
        await Task.Delay(50);
        return AllCourses.FirstOrDefault(c => c.LarsCode == larsCode);             
    }

    internal async Task<Course> GetRandomStandardCourse()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == LearningType.Apprenticeship && c.Level < 7 && c.Options == null)
            .OrderBy(_ => Random.Next())
            .First();
    }

    internal async Task<Course> GetRandomLevel7Course()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == LearningType.Apprenticeship && c.Level == 7)
            .OrderBy(_ => Random.Next())
            .First();
    }

    internal async Task<Course> GetRandomFoundationCourse()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == LearningType.FoundationApprenticeship)
            .OrderBy(_ => Random.Next())
            .First();
    }

    internal async Task<Course> GetRandomShortCourse()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == LearningType.ShortCourses)
            .OrderBy(_ => Random.Next())
            .First();
    }

    internal async Task<Course> GetRandomTestCourseWithOptions()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.Options is { Count: > 0 })
            .OrderBy(_ => Random.Next())
            .First();
    }

}

internal class Course
{
    public string LarsCode { get; set; }    
    public string Title { get; set; }    
    public int MaxFunding { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; } = null;
    public LearningType ApprenticeshipType { get; set; } = LearningType.Apprenticeship;
    public int Level { get; set; }
    public string Version { get; set; } = null;
    public List<string> Options { get; set; } = null;
}