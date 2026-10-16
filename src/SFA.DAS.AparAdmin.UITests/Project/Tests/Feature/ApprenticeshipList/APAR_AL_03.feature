Feature: APAR_AL_03

@providerrestrictedcourses
@rpadalup02
@rpadalup03a
@apar
@regression
Scenario: APAR_AL_03_Edit and remove last date on a restricted course for a provider
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
    When the user clicks on manage restricted courses
    Then the user "Edits" a date for restricted course with larsCode "828" to a date in the "future" "01-02-2030"
    Then the user "Edits" a date for restricted course with larsCode "828" to a date in the "past" "01-02-2026"
    Then the user "Removes" restrictions for the provider on the course with lars code "828"