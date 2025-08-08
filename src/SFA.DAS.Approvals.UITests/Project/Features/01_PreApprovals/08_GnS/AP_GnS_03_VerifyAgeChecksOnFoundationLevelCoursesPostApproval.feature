@approvals
Feature: AP_GnS_03_VerifyAgeChecksOnFoundationLevelCoursesPostApproval

A short summary of the feature

@regression
@e2escenarios
Scenario Outline: AP_GnS_03_Verify Age Checks On Foundation Level Courses post approval
 
	Given A live apprentice record exists for an apprentice on Foundation level course
	When Employer tries to edit live apprentice record by setting age old than 24 years
	Then the employer is stopped with an error message
	When Provider tries to edit live apprentice record by setting age old than 24 years
	Then the provider is stopped with an error message