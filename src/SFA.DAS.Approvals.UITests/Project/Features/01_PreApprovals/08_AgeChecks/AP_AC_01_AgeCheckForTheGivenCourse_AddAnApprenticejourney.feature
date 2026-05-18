@approvals
Feature: AP_AC_01_AgeCheckForTheGivenCourse_AddAnApprenticejourney

Following courses / levels have age restrictions:
	- Foundation type (G&S) standards			-	enforced from: 1st Aug 2025
	- Level-7 standards							-	enforced from: 1st Jan 2026
	- GSO / ApprenticeshipUnits / ShortCourses	-	enforced from: 28 April 2026


@regression
@e2escenarios
Scenario Outline: AP_AC_01_Validate Age-Checks for the restricted Courses_AddAnApprenticeJourney 
	Given Provider adds an apprentice aged <UpperAgeLimit> years using below "<CourseType>", "<CourseLevel>" and "<StartDate>"
	Then system stop user to add that apprentice with an error message for "<UpperAgeLimit>"
	When Provider resubmits ILR with apprentice aged below "<LowerAgeLimit>"
	Then system stop user to add that apprentice with an error message for "<LowerAgeLimit>"	
	And system allows to approve apprentice details with a "<WarningMessage>" if their age is in range of <LowerAgeLimit> - <UpperAgeLimit> years
	When Employer reviews the above cohort 
	Then display the "<WarningMessage>" as per table below

Examples: 
	| CourseType               | CourseLevel | StartDate  | LowerAgeLimit | UpperAgeLimit | WarningMessage |
	| FoundationApprenticeship | N/A         | 2025-08-01 |            15 |            25 | true           |
	| Apprenticeship           | Level-7     | 2026-01-01 |            15 |            25 | true           |
	| ShortCourses             | N/A         | 2026-04-28 |            19 |           115 | false          |