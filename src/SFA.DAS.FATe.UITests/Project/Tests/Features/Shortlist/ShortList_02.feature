Feature:ShortList_02

@fate
@regression
Scenario: Shortlist retains different location filters
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    And the user shortlist a provider for a course
    When the user shorlist the same provider using a location filter
    Then the shortlisted provider should display location 
