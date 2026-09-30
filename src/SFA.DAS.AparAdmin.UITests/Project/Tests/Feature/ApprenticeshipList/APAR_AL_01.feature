Feature: APAR_AL_01

@aparalc01
@rpadnp01
@apar
@regression
Scenario: APAR_AL_01_Manage restricted coures for a provider
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
    When the user clicks on manage restricted courses
    #Then the user verifies pagination links are working as expected
    Then the user uses the search and filter functionality and results are displayed as expected
    