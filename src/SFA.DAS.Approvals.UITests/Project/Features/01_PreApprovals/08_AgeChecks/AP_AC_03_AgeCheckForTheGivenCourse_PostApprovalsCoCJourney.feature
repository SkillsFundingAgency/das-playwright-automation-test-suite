@approvals
Feature: AP_AC_03_AgeCheckForTheGivenCourse_PostApprovalsCoCJourney

Following courses are restricted to youth aged b/w 15-25 only:
	- Foundation type (G&S) standards	-	enforced from: 1st Aug 2025
	- Level-7 standards					-	enforced from: 1st Jan 2026

@regression
@e2escenarios
Scenario Outline: AP_AC_03_Validate age checks for the restricted Courses_FileUploadJourney - post approval
	Given A live apprentice record exists for an apprentice with "<CourseType>", "<CourseLevel>" and "<StartDate>"
	When Employer tries to edit live apprentice record by setting age old than 24 years
	Then the employer is stopped with an error message
	When Provider tries to edit live apprentice record by setting age old than 24 years
	Then the provider is stopped with an error message

Examples: 
	| CourseType				| CourseLevel | StartDate  | 
	| FoundationApprenticeship	| N/A         | 2025-08-01 | 
	| Apprenticeship			| Level-7     | 2026-01-01 |