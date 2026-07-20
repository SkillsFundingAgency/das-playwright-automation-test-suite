Feature: CA_AP_03_CheckAboutApprenticeshipsPage

Check navigation and links on the "About apprenticeships" page.

@campaigns
@apprentice
@regression
Scenario: CA_AP_03_CheckAboutApprenticeshipsPage
	Given the user navigates to About Apprenticeships Page
	Then the links are not broken