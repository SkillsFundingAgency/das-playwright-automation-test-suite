Feature: AP_UC_02_UpdateCohorts_WithEmployer

A short summary of the feature

@tag1
Scenario: AP_UC_02_Update existing cohort - With Employer
	Given a cohort created via ILR exists in 'With Employer' section
	When Provider resubmits ILR file with changes to apprentice details
	Then cohort is sent back to the provider
	And a banner is displayed on the cohort for provider to accept changes