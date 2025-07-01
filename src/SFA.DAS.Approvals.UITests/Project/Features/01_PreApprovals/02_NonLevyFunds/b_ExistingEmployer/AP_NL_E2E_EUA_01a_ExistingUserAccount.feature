@approvals
Feature: AP_NL_E2E_EUA_01a_ExistingUserAccount

@regression
@e2escenarios
Scenario: AP_NL_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
 
	Given Provider successfully submits 2 ILR record containing a learner record for a "NonLevy" Employer
	And SLD push its data into AS
	When Provider logs into Provider-Portal
	And creates reservations for each learner
	And sends an apprentice request (cohort) to the employer by selecting apprentices from ILR list and reservations
	And Employer approves the apprentice request (cohort)
	#Then Apprentice records are available under Manager Your Apprentices section
	#And Apprentice records are available on Apprenticeships endpoint for SLD