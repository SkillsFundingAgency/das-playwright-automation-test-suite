@approvals
@linkedScenarios
Feature: AP_GnS_01_ValidateAgeChecksOnFoundationLevelCourses

This test is a part of Growth & Skills feature where Foundation courses are limited to young learners b/w the age range of 15-25

@regression
@e2escenarios
Scenario: AP_GnS_01a_Verify Age Checks On Foundation Level Courses
 
    Given Provider adds an apprentice aged 25 years using Foundation level standard 
    Then system does not allow to add apprentice details if their age is below 15 years and over 25 years
    And system allows to approve apprentice details with a warning if their age is in range of 15 - 24 years
	When Employer reviews the above cohort 
	Then display the warning message for foundation courses


@regression
@e2escenarios
Scenario: AP_GnS_01b_Verify Age Checks On Foundation Level Courses post approval
 
    Given previous test has been completed successfully
	When Employer tries to edit live apprentice record by setting age old than 24 years
	Then the employer is stopped with an error message
	When Provider tries to edit live apprentice record by setting age old than 24 years
	Then the provider is stopped with an error message


