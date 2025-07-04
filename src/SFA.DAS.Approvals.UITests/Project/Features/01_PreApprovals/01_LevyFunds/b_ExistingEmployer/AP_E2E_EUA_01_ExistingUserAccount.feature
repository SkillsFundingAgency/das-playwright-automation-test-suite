@approvals
@linkedScenarios
Feature: AP_E2E_EUA_01_ExistingUserAccount

A short summary of the feature

@regression
@e2escenarios
Scenario: AP_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
 
	Given Provider successfully submits 2 ILR record containing a learner record for a "Levy" Employer
	And SLD push its data into AS
	When Provider logs into Provider-Portal
	And creates an apprentice request (cohort) by selecting same apprentices
	Then Provider can send it to the Employer for approval
	When Employer approves the apprentice request (cohort)
	#And apprentice record is available on Apprenticeships endpoint for SLD


@wip
@e2escenarios
Scenario: AP_E2E_EUA_01b Provider creates cohort from ILR data Employer approves it - Email Verification

	Given previous test has been completed successfully
	Then Verify following email notifications
 
		| Recipient		| Subject											| EmailText										| 
		| Employer		| Apprentice details ready to approve				| {cohortRef}									| 
		| Provider		| Apprenticeship service cohort ready for approval	| {cohortRef}									| 
		| Apprentice	| You need to confirm your apprenticeship details	| Congratulations on becoming an apprentice		| 
