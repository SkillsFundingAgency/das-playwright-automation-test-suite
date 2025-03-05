Feature: VerifyFilters02


@fate
@regression
Scenario: Verify functionality of the filters and results
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course without location and course name
    Then the user is able to apply filters and verify results



