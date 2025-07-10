@approvals
Feature: AP_G&S_P_01_ValidateAgeChecksOnFoundationLevelCourses_Provider

@regression
@e2escenarios
Scenario: AP_G&S_P_01_Verify Age Checks On Foundation Level Courses on Provider side
 
    Given Provider adds an apprentice aged 25 years using Foundation level standard 
    Then system does not allow to add apprentice details if their age is below 15 years and over 25 years
    And system allows to approve apprentice details with a warning if their age is in range of 15 - 24 years
	When Employer reviews the above cohort 
	Then display the warning message for foundation courses
