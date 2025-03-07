Feature: VerifyFilters01

@fate
@regression
Scenario: Verify functionality of the filters 
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course without location and course name
    Then the user is able to select single clear single filters
    And the user is able to add multiple filters and clear all 
    And the user is able to verify results as per the filters set

