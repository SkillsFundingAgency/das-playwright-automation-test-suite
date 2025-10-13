@approvals
@linkedScenarios
Feature: AP_NL_E2E_EUA_01a_ExistingUserAccount

@regression
@e2escenarios
Scenario: AP_NL_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
 
	Given Provider successfully submits 2 ILR record containing a learner record for a "NonLevy" Employer
	And SLD push its data into AS
	When Provider logs into Provider-Portal
	And creates reservations for each learner
	And sends an apprentice request (cohort) to the employer by selecting apprentices from ILR list and reservations
	And the Employer approves the apprentice request (cohort)
	Then the Employer can access live apprentice records under Manager Your Apprentices section
	#And Apprentice records are available on Apprenticeships endpoint for SLD


@regression
@e2escenarios
Scenario Outline: AP_NL_E2E_EUA_01b emails validation
	Given previous test has been completed successfully
	Then Verify the "<Recipient>" receive "<NotificationType>" email

Examples:
		| Recipient		| NotificationType								|
		| Employer      | Reservation made on your behalf 				|
		| Employer      | Apprentice details ready to approve 			|
		| Apprentice	| Confirm apprenticeship details                |