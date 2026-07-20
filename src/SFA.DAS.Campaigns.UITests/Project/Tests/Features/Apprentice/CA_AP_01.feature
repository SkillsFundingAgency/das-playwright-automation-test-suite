Feature: CA_AP_01

@campaigns
@apprentice
@regression
Scenario: CA_AP_01_Check Browse Apprenticeship Page And Search For An Apprenticeship
	Given the user navigates to the browse apprenticeship page
	Then the links are not broken
	And the user is taken to the external find an apprenticeship page