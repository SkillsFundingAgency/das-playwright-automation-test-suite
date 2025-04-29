Feature:TrainingProviders_01

  @fate
  @regression
Scenario: Verify training providers results count
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user selects a course and views training providers
    Then the training provider count should be displayed correctly
    
