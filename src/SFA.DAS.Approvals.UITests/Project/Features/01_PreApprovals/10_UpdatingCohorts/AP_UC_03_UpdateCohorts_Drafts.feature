@approvals
Feature: AP_UC_03_UpdateCohorts_Drafts

Cohorts created via ILR are read only (except email and RPL fields)
Only way to update an unapproved cohort is to re-submit ILR file
Below test verify the same journey whilst cohort is "Draft"

@regression
@e2escenarios
Scenario: AP_UC_03_UpdateCohorts_Drafts
	Given a cohort created via ILR exists in Drafts section
	When Provider resubmits ILR file with changes to apprentice details
	Then a banner is displayed on the cohort for provider to accept changes
	And Provider cannot approve the cohort
	When Provider reviews and accepts the changes
	Then Provider can approve the cohort
