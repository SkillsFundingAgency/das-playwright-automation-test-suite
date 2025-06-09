@approvals
Feature: AP_E2E_EUA_01_ExistingUserAccount

A short summary of the feature

@regression
@e2escenarios
@selectstandardwithmultipleoptions
Scenario: AP_E2E_EUA_01 Provider creates cohort from ILR data Employer approves it
 
	Given Provider successfully submits 1 ILR record containing a learner record for a "Levy" Employer
	And SLD push its data into AS
	When Provider logs into Provider-Portal
	And creates an apprentice request (cohort) by selecting same apprentices
	Then Provider can send it to the Employer for approval
	When Employer approves the cohort
	#And apprentice record is available on Apprenticeships endpoint for SLD
