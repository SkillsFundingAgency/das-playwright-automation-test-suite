## IMPORTANT: TO BE IMPLEMENTED ONCE TRANSFERS TESTS ARE MIGRATED TO THIS SOLUTION ##
Feature: AP_UC_04_UpdateCohorts_WithTransferSender

Cohorts created via ILR are read only (except email and RPL fields)
Only way to update an unapproved cohort is to re-submit ILR file
If a cohort is with Transfer-Sender for final approval and a change arrive, then it should be returned back to the Provider

@tag1
Scenario: AP_UC_04_UpdateCohorts_WithTransferSender
	Given a cohort created via ILR exists in WithTransferSendingEmployers section
	When Provider resubmits ILR file with changes to apprentice details
	Then cohort is sent back to the provider
	And a banner is displayed on the cohort for provider to accept changes
