@approvals
Feature: AP_G&S_P_01_ValidateAgeChecksOnFoundationLevelCourses_Provider

@regression
@e2escenarios
Scenario: AP_G&S_P_01_Verify Age Checks On Foundation Level Courses on Provider side
 
    Given Provider adds an apprentice using Foundation level standard 
    Then system does not allow to add apprentice details if their age is below 15 years and over 25 years
    And system allows to save apprentice details with a warning if their age is in range of 22 - 24 years
    And above warning disappears if apprentice age is in range of 15 - 22 years 
