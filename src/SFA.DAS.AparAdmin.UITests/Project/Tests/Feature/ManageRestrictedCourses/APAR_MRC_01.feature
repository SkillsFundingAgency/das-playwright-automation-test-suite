Feature: APAR_MRC_01

@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01_Search and verify restricted courses
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user searches for "Leadership"
    Then the user is able to verify the restricted course results


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01_Select a single filter and verify restricted courses
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user selects the "Apprenticeship" training type filter
    And the user applies the filter
    Then the user is able to verify the "Apprenticeship" filter is selected
    And the user is able to verify the restricted course results
    When the user clears the selected filter
    Then the user is able to verify that no filters are selected


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01_Select multiple filters and verify restricted courses and pagination
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
    And the user is able to verify the restricted course results
    When the user clears all selected filters
    Then the user is able to verify that no filters are selected
    And the user verifies pagination links are working as expected


@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01_Navigate to manage providers and filter results
    Given the provider logs into old apar admin portal
    When the user navigates to restricted courses
    And the user selects Manage providers on course with LARS Code "272"
    And the user navigates to providers of course LARS Code "272"
    And the user seaches for provider with UKPRN "10001143"
    Then the user is able to verify the provider results
    When the user clears all selected filters
    Then the user selects the filter "Open to new starts"
    And the user is able to verify the provider results