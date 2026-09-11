Feature: APAR_MRC_01

@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01_Search and verify restricted courses
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user searches for "leadership"
    Then the user is able to verify the restricted course results contain "leadership"


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01A_Select a single filter and verify restricted courses
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user selects the "Apprenticeship" training type filter
    And the user applies the filter
    Then the user is able to verify the "Apprenticeship" filter is selected
    And the user is able to verify the restricted course results contain "Apprenticeship"
    When the user clears the selected filter
    Then the user is able to verify that no filters are selected


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01B_Select multiple filters and verify restricted courses and pagination
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user selects the following training type filters:
        | Training Type       |
        | Apprenticeship      |
        | Apprenticeship unit |
    And the user applies the filter
    Then the user is able to verify the following filters are selected:
        | Training Type       |
        | Apprenticeship      |
        | Apprenticeship unit |
    When the user clears all selected filters
    Then the user is able to verify that no filters are selected
    And the user verifies pagination links are working as expected


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01C_Navigate to manage providers and filter results
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user selects Manage providers on course with LARS Code "272"
    And the user seaches for provider with UKPRN "10001143"
    Then the user is able to verify the "10001143" filter is selected
    And the user is able to verify the provider results contain "10001143"
    When the user clears all selected filters
    Then the user selects the filter "Open to new starts"
    And the user applies the filter
    And the user is able to verify the "Open to new starts" filter is selected
    And the user is able to verify the provider results contain "Open to new starts"


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01D_Select a training provider and change the last start date
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
    Then the last date for new starts is updated for "10061102" to "20 Aug 2020" and success banner is displayed
    And the banner for UKPRN "10061102" displays "Closed to new starts"
    When the user clicks to change the details of provider with UKPRN "10061102"
    And the user confirms they want to "Remove last start date"
    Then the banner for UKPRN "10061102" displays "Open to new starts"