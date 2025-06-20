@approvals
Feature: AP_NL_E2E_EUA_01a_ExistingUserAccount

A short summary of the feature

@regression
@e2escenarios
Scenario: AP_NL_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
 
	Given Provider successfully submits 1 ILR record containing a learner record for a "NonLevy" Employer
	#And SLD push its data into AS
	When Provider logs into Provider-Portal
	And creates 1 reservations
	And used these reservations to add apprentices to a cohort
	And creates an apprentice request (cohort) by selecting same apprentices
	Then Provider can send it to the Employer for approval
	When Employer approves the apprentice request (cohort)
	#And apprentice record is available on Apprenticeships endpoint for SLD
