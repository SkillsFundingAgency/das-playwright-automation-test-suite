Feature: APAR_AL_04

@providerrestrictedcourses
@rpadalup04
@apar
@regression
Scenario: APAR_AL_04_Change a provider from unrestricted to restricted
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
   Then the user changes the provider from unrestricted to restricted
    