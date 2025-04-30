Feature:TrainingProviders_03

  @fate
  @regression
Scenario: Verify training providers sort results order
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user selects a course and views training providers
    Then verify default sort order results with no location