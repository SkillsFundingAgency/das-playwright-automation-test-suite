Feature: APAR_AL_05

@providerrestrictedcourses
@rpadalup05
@rpadalup05a
@apar
@regression
Scenario: APAR_AL_05A_Add apprenticeship and apprenticeship units to a restricted provider allow list
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
    When the user navigates to manage approved courses
    Then the user can add apprenticeships that the provider can deliver
    When the user navigates to manage approved units
    Then the user can add apprenticeship units that the provider can deliver

@rpadalup05
@providerrestrictedcourses
@allowedCourseClearEndDate
@apar
@regression
Scenario: APAR_AL_05B_Add and edit dates for allowlisted apprenticeships and apprenticeship units for a restricted provider
    Given the provider logs into old apar admin portal
    And the user navigates to training providers page
    When the user navigates to manage approved courses
    Then the user can edit dates for allowlisted apprenticeships that the provider can deliver
    When the user navigates to manage approved units
    Then the user can edit dates for allowlisted apprenticeship units that the provider can deliver