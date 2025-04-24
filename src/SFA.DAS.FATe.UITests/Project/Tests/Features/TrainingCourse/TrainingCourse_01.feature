Feature:TrainingCourse_01

  @fate
  @regression
  Scenario: Verify links and search functionality for providers
    Given the user navigates to the Apprenticeship training course page
    When the user searches for a provider without entering a location
    And the user searches for a provider with a location
    Then the user verifies that all links on the training course page are working as expected
