using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel
{
    internal class TrainingFactory : ITrainingFactory
    {
        private readonly Func<CoursesDataHelper, Task<Course>> courseSelector;
        private readonly DateTime startDate;
        private readonly int? courseDurationInDays = 450;

        public TrainingFactory(Func<CoursesDataHelper, Task<Course>> CourseSelector = null)
        {
            courseSelector = CourseSelector;
        }

        public TrainingFactory(DateTime StartDate, Func<CoursesDataHelper, Task<Course>> CourseSelector = null, int? duration = null)
        {
            startDate = StartDate;
            courseSelector = CourseSelector;
            courseDurationInDays = duration ?? 450;
        }

        public async Task<Training> CreateTrainingAsync(EmployerType employerType, ApprenticeshipStatus? apprenticeshipStatus = null, int? duration = null)
        {
            Training training = new Training();

            CoursesDataHelper coursesDataHelper = new CoursesDataHelper();
            var course = courseSelector == null ? await coursesDataHelper.GetRandomStandardCourse() : await courseSelector(coursesDataHelper);
            bool isShortCourse = course.ApprenticeshipType == LearningType.ShortCourses;

            if (startDate != DateTime.MinValue)
            {
                training.StartDate = startDate;
            }
            else if (employerType == EmployerType.Levy)
            {
                training.StartDate = await GetStartDate(apprenticeshipStatus);
            }
            else
            {
                training.StartDate = DateTime.Now;
            }

            training.EndDate = training.StartDate.AddDays((double)courseDurationInDays);
            training.AcademicYear = AcademicYearDatesHelper.GetCurrentAcademicYear();
            training.PercentageLearningToBeDelivered = 40;
            training.EpaoPrice = (isShortCourse) ? 0 : Convert.ToInt32(RandomDataGenerator.GenerateRandomNumber(3));
            training.TrainingPrice = (isShortCourse) ? course.MaxFunding : Convert.ToInt32("2" + RandomDataGenerator.GenerateRandomNumber(3));
            training.TotalPrice = training.EpaoPrice + training.TrainingPrice;
            training.IsFlexiJob = false;
            training.PlannedOTJTrainingHours = 1200;
            training.LarsCode = course.LarsCode;
            training.LearningType = (int)course.ApprenticeshipType;
            training.CourseTitle = course.Title;
            training.ConsumerReference = "CR123456";

            await Task.Delay(100);

            return training;
        }

        private async Task<DateTime> GetStartDate(ApprenticeshipStatus? apprenticeshipStatus = null)
        {
            var lowerDateRangeForStartDate = AcademicYearDatesHelper.GetCurrentAcademicYearStartDate();
            var academicYearEndDate = AcademicYearDatesHelper.GetCurrentAcademicYearEndDate();
            var todaysDate = DateTime.Now;
            var upperDateRangeForStartDate = academicYearEndDate > todaysDate ? todaysDate : academicYearEndDate;

            await Task.Delay(100);


            if (apprenticeshipStatus == ApprenticeshipStatus.WaitingToStart)
            {
                return RandomDataGenerator.GenerateRandomDate(DateTime.Now, DateTime.Now.AddMonths(2));
            }
            else
            {
                return RandomDataGenerator.GenerateRandomDate(lowerDateRangeForStartDate, upperDateRangeForStartDate);
            }

        }

    }
}
