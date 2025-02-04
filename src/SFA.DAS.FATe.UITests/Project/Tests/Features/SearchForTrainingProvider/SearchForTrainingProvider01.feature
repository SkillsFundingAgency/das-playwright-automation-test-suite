Feature: SearchForTrainingProvider_01

@fate
@regression
Scenario: SearchForTrainingProvider Verify Search Functionality
    Given the user navigates to the Search for a training provider page
    When the user searches with a valid ukprn
    Then the results page is displayed with training providers