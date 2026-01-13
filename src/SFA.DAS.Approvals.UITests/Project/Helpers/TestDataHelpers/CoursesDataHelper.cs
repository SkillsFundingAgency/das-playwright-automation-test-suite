using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
internal class CoursesDataHelper
{
    private static readonly List<Course> AllCourses = new()
    {
        // Standard Courses
        new() { StandardCode = 2, Title = "Software developer", MaxFunding = 18000, EffectiveFrom = new(2014, 08, 01), Level = 4, Version = "1.1" },
        new() { StandardCode = 101, Title = "Retailer", MaxFunding = 5000, EffectiveFrom = new(2015, 08, 01), Level = 5, Version = "1.2" },
        new() { StandardCode = 119, Title = "Adult care worker", MaxFunding = 4000, EffectiveFrom = new(2023, 06, 26), Level = 2, Version = "1.0" },
        new() { StandardCode = 274, Title = "Abattoir worker", MaxFunding = 6000, EffectiveFrom = new(2018, 05, 08), Level = 2, Version = "1.1" },
        new() { StandardCode = 297, Title = "Teaching assistant", MaxFunding = 7000, EffectiveFrom = new(2018, 06, 26), Level = 3, Version = "1.1" },
        new() { StandardCode = 409, Title = "Registered nurse degree", MaxFunding = 26000, EffectiveFrom = new(2019, 02, 13), Level = 6, Version = "1.1" },
        new() { StandardCode = 530, Title = "Fire safety inspector", MaxFunding = 11000, EffectiveFrom = new(2019, 11, 20), Level = 4, Version = "1.1" },
        new() { StandardCode = 567, Title = "Florist", MaxFunding = 18000, EffectiveFrom = new(2020, 06, 10), Level = 2, Version = "1.0" },
        new() { StandardCode = 204, Title = "Accountancy or taxation professional", MaxFunding = 21000, EffectiveFrom = new(2017, 11, 07), Level = 7, Version = "1.0" },
        new() { StandardCode = 586, Title = "Senior journalist", MaxFunding = 14000, EffectiveFrom = new(2020, 08, 07), Level = 7, Version = "1.0" },
        new() { StandardCode = 580, Title = "Health and care intelligence specialist", MaxFunding = 17000, EffectiveFrom = new(2020, 07, 10), Level = 7, Version = "1.0" },

        // Foundation Courses
        new() { StandardCode = 805, Title = "Building service engineering foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },
        new() { StandardCode = 806, Title = "Finishing trades foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },
        new() { StandardCode = 807, Title = "Onsite trades foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },
        new() { StandardCode = 808, Title = "Hardware, network and infrastructure foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },
        new() { StandardCode = 809, Title = "Software and data foundation apprenticeship", MaxFunding = 4000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },
        new() { StandardCode = 810, Title = "Engineering and manufacturing foundation apprenticeship", MaxFunding = 4500, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },
        new() { StandardCode = 811, Title = "Health and social care foundation apprenticeship", MaxFunding = 3000, EffectiveFrom = new(2025, 08, 01), ApprenticeshipType = "FoundationApprenticeship", Level = 2, Version = "1.0" },

        // Courses with Options      
        new() { StandardCode = 57, Title = "Gas network craftsperson", MaxFunding = 27000, EffectiveFrom = new(2015, 08, 01), Level = 3, Version = "1.3", Options = new() { "Emergency Response Craftsperson", "Network Pipelines Maintenance Craftsperson", "Network Maintenance Craftsperson (Pressure Management)", "Network Maintenance Craftsperson (Electrical & Instrumentation)" } },      
        new() { StandardCode = 87, Title = "Aviation ground operative", MaxFunding = 3000, EffectiveFrom = new(2015, 08, 01), Level = 2, Version = "1.0", Options = new() { "Aircraft Handling", "Aircraft Movement", "Fire Fighter", "Flight Operations", "Passenger Services" } },
        new() { StandardCode = 117, Title = "Professional accounting or taxation technician", MaxFunding = 12000, EffectiveFrom = new(2015, 08, 01), Level = 4, Version = "1.1", Options = new() { "Accounting", "Tax" } }
    };

    private static readonly Random Random = new();

    internal async Task<Course> GetCourse(int standardCode)
    {
        await Task.Delay(50);
        return AllCourses.FirstOrDefault(c => c.StandardCode == standardCode);             
    }

    internal async Task<Course> GetRandomStandardCourse()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == "Apprenticeship" && c.Level < 7 && c.Options == null)
            .OrderBy(_ => Random.Next())
            .First();
    }

    internal async Task<Course> GetRandomLevel7Course()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == "Apprenticeship" && c.Level == 7)
            .OrderBy(_ => Random.Next())
            .First();
    }

    internal async Task<Course> GetRandomFoundationCourse()
    {
        await Task.Delay(50);
        return AllCourses
            .Where(c => c.ApprenticeshipType == "FoundationApprenticeship")
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
    public int StandardCode { get; set; }
    public string Title { get; set; }    
    public int MaxFunding { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; } = null;
    public string ApprenticeshipType { get; set; } = "Apprenticeship";
    public int Level { get; set; }
    public string Version { get; set; } = null;
    public List<string> Options { get; set; } = null;
}