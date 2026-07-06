@approvals
@postapprovals
@linkedScenarios
Feature: AP_CoC_02_LearningWithdrawnEvent

Commitments receives LearningWithdrawnEvent from Learning domain for variety of reasons. This test validates that event is processed correctly and the apprentice record is updated (stopped) with correct reason code and stop date 

Data Requirment:
	- Employer Account: <LevyUser>			<--- please refer to user secrets file for the actual value
	- Provider Account: <ProviderConfig>	<--- please refer to user secrets file for the actual value
	- FirstName:DoNotUse_TestData
	- LastName: ChangeStatusApprentice
	- ULN: any
	- StartDate: -6 months in the past
	- EndDate: +6 months in the future
	- Training Course: Any apprenticeship course, ideally without any course option


@regression
Scenario: AP_CoC_02_Verify Learning Withdrawal Event marks the apprenticeship as Stopped
    Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "ChangeStatusApprentice"
	When LearningWithdrawnEvent is received for the apprentice
	Then Commitments db is updated with the correct reason code and stop date
    And provider verifies that record is set as "Stopped" in Provider portal
    And employer verifies that record has been "Stopped" in Employer portal
	When LearningWithdrawnEvent is received with different stop date and reason code for the same apprentice
	Then Commitments db is updated with the new stop date and reason code


##//emails validation for employer and apprentice to be implemented here in future after APPMAN-2733 is ready
#@regression
#Scenario Outline: AP_E2E_LE_EUA_01b emails validation
#	Given previous test has been completed successfully
#	Then Verify the "<Recipient>" receive "<NotificationType>" email
#
#Examples:
#		| Recipient		| NotificationType								|
#		| Employer      | ??? 			                                |
#		| Apprentice	| ???                                           |
