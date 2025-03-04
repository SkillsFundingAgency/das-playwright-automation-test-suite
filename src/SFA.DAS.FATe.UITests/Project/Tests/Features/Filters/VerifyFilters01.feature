Feature: VerifyFilters01


@fate
@regression
Scenario: Verify functionality of the filters 
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course without location and course name
    Then the user verifies filters functionality

