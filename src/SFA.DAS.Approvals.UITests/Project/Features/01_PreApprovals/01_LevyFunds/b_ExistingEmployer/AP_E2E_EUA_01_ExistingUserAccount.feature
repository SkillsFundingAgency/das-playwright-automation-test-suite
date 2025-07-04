@approvals
@linkedScenarios
Feature: AP_E2E_EUA_01_ExistingUserAccount

A short summary of the feature

@regression
@e2escenarios
Scenario: AP_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
	Given Provider successfully submits 2 ILR record containing a learner record for a "Levy" Employer
	And SLD push its data into AS
	When Provider sends an apprentice request (cohort) to the employer by selecting same apprentices
	And Employer approves the apprentice request (cohort)
	Then Provider can access live apprentice records under Manager Your Apprentices section
	#And Apprentice records are available on Apprenticeships endpoint for SLD


@regression
@e2escenarios
Scenario Outline: AP_E2E_EUA_01b emails validation
	Given previous test has been completed successfully
	Then Verify the "<Recipient>" receive "<NotificationType>" email

Examples:
		| Recipient		| NotificationType								|
		| Employer      | Apprentice details ready to approve 			|
		| Apprentice	| Confirm apprenticeship details                |
