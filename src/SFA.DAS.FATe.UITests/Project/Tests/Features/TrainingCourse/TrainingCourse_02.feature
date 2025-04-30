Feature: TrainingCourse_02

  @fate
  @regression
  Scenario: Verify search filters and persistence on Apprenticeship training course page
    Given the user navigates to the Apprenticeship training course page
    When the user enters and selects a location and verifies the selected location is displayed correctly
    And verifies the location is stored in the filters on the results page
    And updates the apprentice travel distance
    And verifies the travel distance is updated correctly
    And removes the location
    And searches for a new location
    Then verifies the new location and travel distance are updated correctly
