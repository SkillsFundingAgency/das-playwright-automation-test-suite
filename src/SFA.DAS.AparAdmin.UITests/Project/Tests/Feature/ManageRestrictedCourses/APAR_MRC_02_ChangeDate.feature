Feature: APAR_MRC_02_ChangeDate


@aparmrc02
@apar
@regression
Scenario: APAR_MRC_02_Select a training provider and change the last start date
    Given the provider logs into old apar admin portal
    And the user navigates to restricted courses
    And the user selects Manage providers on course with LARS Code "272"
    And the user clicks to change the details of provider with UKPRN "10061102"
    When the user enters a date in the future "20-05-2027"
    Then the last date for new starts is updated for "10061102" to "20 May 2027" and success banner is displayed
    And the banner for UKPRN "10061102" displays "Last start date added"
    When the user clicks to change the details of provider with UKPRN "10061102"
    And the user confirms they want to "Change last start date"
    And the user enters a date before Sept 2014 "01-01-1980"
    Then the user sees the message "The last start date must be on or after 1 September 2014"
    When the user enters a date after Sept 2014 "30-08-2020"
    Then the last date for new starts is updated for "10061102" to "30 Aug 2020" and success banner is displayed
    And the banner for UKPRN "10061102" displays "Closed to new starts"
    When the user clicks to change the details of provider with UKPRN "10061102"
    And the user confirms they want to "Remove last start date"
    Then the banner for UKPRN "10061102" displays "Open to new starts"