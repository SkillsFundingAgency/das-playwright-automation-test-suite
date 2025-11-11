@approvals
Feature: AP_UC_02_UpdateCohorts_WithEmployer

Cohorts created via ILR are read only (except email and RPL fields)
Only way to update an unapproved cohort is to re-submit ILR file
If a cohort is with employer and change arrive, then it should be returned back to the Provider

@regression
@e2escenarios
Scenario: AP_UC_02_Update existing cohort - With Employer
	Given a cohort created via ILR exists in 'With Employer' section
	When Provider resubmits ILR file with changes to apprentice details
	Then cohort is sent back to the provider
	And a banner is displayed on the cohort for provider to accept changes