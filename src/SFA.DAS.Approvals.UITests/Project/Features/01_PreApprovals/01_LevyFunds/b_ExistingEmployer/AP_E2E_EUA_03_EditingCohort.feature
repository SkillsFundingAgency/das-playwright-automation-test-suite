@approvals
Feature: AP_E2E_EUA_03_EditingCohort

It is to verify a cohort can be edited by re-submitting ILR file

@regression
@e2escenarios
Scenario: AP_E2E_EUA_03_Editing a cohort via ILR
	Given Provider sends an apprentice request (cohort) to an employer
	When Provider resubmits ILR file with changes to apprentice details
	Then cohort should automatically be sent back to provider to accept changes
	#And the 'Provider' receives 'cohort ready for review' email notification
	When Provider reviews and accepts the changes
	And resend it to the employer for approval
	Then Employer sees the updated details in the cohort
	And can approve the cohort

