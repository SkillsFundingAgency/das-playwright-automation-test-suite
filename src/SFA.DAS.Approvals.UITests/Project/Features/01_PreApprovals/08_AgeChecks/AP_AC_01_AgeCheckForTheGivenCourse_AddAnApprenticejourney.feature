@approvals
Feature: AP_AC_01_AgeCheckForTheGivenCourse_AddAnApprenticejourney

Following courses are restricted to youth aged b/w 15-25 only:
	- Foundation type (G&S) standards	-	enforced from: 1st Aug 2025
	- Level-7 standards					-	enforced from: 1st Jan 2026


@regression
@e2escenarios
Scenario Outline: AP_AC_01_Validate Age-Checks for the restricted Courses_AddAnApprenticeJourney 
	Given Provider adds an apprentice aged 25 years using below "<CourseType>", "<CourseLevel>" and "<StartDate>"
    Then system does not allow to add apprentice details if their age is below 15 years and over 25 years
    And system allows to approve apprentice details with a warning if their age is in range of "<LowerAgeLimit>" - "<UpperAgeLimit>" years
	When Employer reviews the above cohort 
	Then display the warning message for foundation courses

Examples: 
	| CourseType				| CourseLevel | StartDate  | LowerAgeLimit | UpperAgeLimit |
	| FoundationApprenticeship	| N/A         | 2025-08-01 |            15 |            25 |
	| Apprenticeship			| Level-7     | 2026-01-01 |            15 |            25 |