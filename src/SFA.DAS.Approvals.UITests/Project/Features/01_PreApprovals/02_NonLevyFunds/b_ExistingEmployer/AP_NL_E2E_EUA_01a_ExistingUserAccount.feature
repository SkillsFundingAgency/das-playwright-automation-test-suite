@approvals
Feature: AP_NL_E2E_EUA_01a_ExistingUserAccount

@regression
@e2escenarios
Scenario: AP_NL_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
 
	Given Provider successfully submits 2 ILR record containing a learner record for a "NonLevy" Employer
	And SLD push its data into AS
	When Provider logs into Provider-Portal
	And creates reservations for each learner
	And creates an apprentice request (cohort) by selecting apprentices from ILR list via reservations
	Then Provider can send it to the Employer for approval
	When Employer approves the apprentice request (cohort)
	#And apprentice record is available on Apprenticeships endpoint for SLD
