Feature: APAR_MRC_03_AddProvider


@aparmrc02
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

    