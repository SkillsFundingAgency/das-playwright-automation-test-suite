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
Scenario: APAR_MRC_01_Select multiple filters and verify restricted courses
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