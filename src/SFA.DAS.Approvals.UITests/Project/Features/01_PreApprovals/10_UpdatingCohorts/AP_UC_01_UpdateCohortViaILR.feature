Feature: AP_UC_01_UpdateCohortViaILR

A short summary of the feature

@tag1
Scenario: AP_UC_01_Update cohort via ILR
	Given a cohort created via ILR exists in 'Drafts' section
	When Provider resubmits ILR file with changes to apprentice details
	Then a banner is displayed on the cohort for provider to accept changes
	And provider cannot approve the cohort
