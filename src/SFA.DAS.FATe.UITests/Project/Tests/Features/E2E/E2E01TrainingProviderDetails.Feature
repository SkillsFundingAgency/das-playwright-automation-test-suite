Feature: E2E Training Provider

  @fate
  @regression
  Scenario: Verify Training Provider Courses Offered and Feedback Functionality
    Given the user navigates to the training provider details page
    When the user accesses the training provider courses page
    And the user enters a location and performs a search
    And the user removes or updates the location
    And the user accesses the feedback link for the last five years
    And the user changes the feedback view to table view
    And the user changes the feedback view to graph view
    And the user adds the provider to the shortlist
    And the user accesses the training provider course page from shortlisted training providers
    And the user removes the provider from shortlist
    And the user accesses training providers for this course
    Then the user navigates back to the training provider details page
    And the user accesses training courses page
