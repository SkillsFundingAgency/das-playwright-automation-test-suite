@approvals
@linkedScenarios
Feature: AP_E2E_EUA_01_ExistingUserAccount

A short summary of the feature

@regression
@e2escenarios
Scenario: AP_E2E_EUA_01a Provider creates cohort from ILR data Employer approves it
	Given Provider successfully submits 1 ILR record containing a learner record for a "Levy" Employer
	And SLD push its data into AS
	Then a record is created in LearnerData Db for each learner
	When Provider sends an apprentice request (cohort) to the employer by selecting same apprentices
	Then Commitments Db is updated with respective LearnerData Id
	When Employer approves the apprentice request (cohort)
	Then LearnerData Db is updated with respective Apprenticeship Id
	And Apprenticeship record is created in Learning Db
	Then Provider can access live apprentice records under Manager Your Apprentices section
	And apprentice/learner record is no longer available on SelectLearnerFromILR page
	#And apprentice/learner record is available on Learning endpoint for SLD (so they do not resubmit it)


@regression
@e2escenarios
Scenario Outline: AP_E2E_EUA_01b emails validation
	Given previous test has been completed successfully
	Then Verify the "<Recipient>" receive "<NotificationType>" email

Examples:
		| Recipient		| NotificationType								|
		| Employer      | Apprentice details ready to approve 			|
		| Apprentice	| Confirm apprenticeship details                |
