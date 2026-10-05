Feature: APAR_AL_02

@providerrestrictedcourses
@rpadalup02
@apar
@regression
Scenario: APAR_AL_02_Restrict a course for a provider
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
    When the user clicks on manage restricted courses
    Then the user restricts a course that is not available in the drop down
    Then the user restricts for the provider course "Bricklayer" which they currently do not provide
    Then the user restricts for the provider course "Advanced golf greenkeeper" which they currently provide
