Feature: APAR_AL_05

@providerrestrictedcourses
@rpadalup05
@apar
@regression
Scenario: APAR_AL_05_Add apprenticeship and apprenticeship units to a restricted provider allow list
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
    When the user navigates to manage approved courses
    Then the user can add apprenticeships that the provider can deliver
    When the user navigates to manage approved units
    Then the user can add apprenticeship units that the provider can deliver
