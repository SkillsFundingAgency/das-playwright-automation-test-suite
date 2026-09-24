Feature: APAR_MRC_03_AddProvider

@restrictedcourses
@aparmrc03
@apar
@regression
Scenario: APAR_MRC_03_Add a provider to a restricted course
    Given the provider logs into old apar admin portal
    And the user navigates to restricted courses
    And the user selects Manage providers on course with LARS Code "272"
    When the user clicks to add a training provider
    And the user submits the UKPRN "10043565" to add
    Then the user is asked to confirm that they want to allow this provider to offer this course and they click cancel
    And it is confirmed that the provider with UKPRN "10043565" has not been added to the list
    When the user clicks to add a training provider
    And the user adds a UKPRN that does not exist "12345678"
    Then the user gets an errror message
    And the user submits the UKPRN "10043565" to add
    And the user is asked to confirm that they want to allow this provider to offer this course and they click confirm
    And it is confirmed that the provider with UKPRN "10043565" has been added to the list


    