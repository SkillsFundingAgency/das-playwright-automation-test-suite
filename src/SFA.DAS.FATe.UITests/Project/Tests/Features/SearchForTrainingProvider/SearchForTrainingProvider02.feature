Feature: SearchForTrainingProvider_02

@fate
@regression

Scenario: SearchForTrainingProvider Search with Invalid UKPRN
    Given the user navigates to the Search for a training provider page
    When the user should not be able to search without a UKPRN
    And the user should not be able to search with an invalid UKPRN