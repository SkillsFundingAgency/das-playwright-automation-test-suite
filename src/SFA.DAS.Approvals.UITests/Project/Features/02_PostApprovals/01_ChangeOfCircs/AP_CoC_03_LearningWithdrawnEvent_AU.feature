@postapprovals
@linkedScenarios
Feature: AP_CoC_03_LearningWithdrawnEvent_AU

Commitments receives LearningWithdrawnEvent from Learning domain for variety of reasons. This test validates that event is processed correctly and the apprentice record is updated (stopped) with correct reason code and stop date 

Data Requirements:
	- Employer Account: <LevyUser>			<--- please refer to user secrets file for the actual value
	- Provider Account: <ProviderConfig>	<--- please refer to user secrets file for the actual value
	- FirstName:DoNotUse_TestData
	- LastName: ChangeStatusAuLearner
	- ULN: any
	- StartDate: -6 months in the past
	- EndDate: +6 months in the future
	- Training Course: Any GSO/short-course/AU


@regression
Scenario: AP_CoC_03_Verify Learning Withdrawal Event marks the AU learner as Stopped
	Given a Live AU learner record exists with Firstname: "DoNotUse_TestData" and LastName: "ChangeStatusAuLearner"
	When LearningWithdrawnEvent is received for the apprentice
	Then Commitments db is updated with the correct reason code and stop date
    And provider verifies that record is set as "Stopped" in Provider portal
    And employer verifies that record has been "Stopped" in Employer portal
	When LearningWithdrawnEvent is received with different stop date and reason code for the same apprentice
	Then Commitments db is updated with the new stop date and reason code


##//emails validation for employer and apprentice to be implemented here in future after APPMAN-2733 is ready
#@regression
#Scenario Outline: AP_E2E_LE_EUA_03 emails validation
#	Given previous test has been completed successfully
#	Then Verify the "<Recipient>" receive "<NotificationType>" email
#
#Examples:
#		| Recipient		| NotificationType								|
#		| Employer      | ??? 			                                |
#		| Apprentice	| ???                                           |
