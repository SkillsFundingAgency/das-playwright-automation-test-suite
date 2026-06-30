Feature:ShortList_02

@fate
@regression
Scenario Outline: Shortlist retains different location filters
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user applies a location filter <location>
    And the user adds a provider to the shortlist
    And the user navigates to the shortlist page
    Then the shortlisted provider should display location <location>

    Examples:
    | location |
    | London   |
    | Manchester |
    | Birmingham |