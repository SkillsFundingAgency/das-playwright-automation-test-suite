@approvals
Feature: AP_UC_01_UpdateCohorts_ReadyForReview 

Cohorts created via ILR are read only (except email and RPL fields)
Only way to update an unapproved cohort is to re-submit ILR file
Below test verify the same journey whilst cohort is "Ready for review" with Training Provider

@regression
@e2escenarios
Scenario: AP_UC_01_Update existing cohort - Ready for review 
	Given a cohort created via ILR exists in 'Ready for review' section
	When Provider resubmits ILR file with changes to apprentice details
	Then a banner is displayed on the cohort for provider to accept changes
	And Provider cannot approve the cohort
	When Provider reviews and accepts the changes
	Then Provider can approve the cohort