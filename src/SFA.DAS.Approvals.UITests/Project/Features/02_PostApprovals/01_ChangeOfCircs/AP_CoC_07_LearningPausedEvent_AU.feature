#@postapprovals
@linkedScenarios
Feature: AP_CoC_07_LearningPausedEvent_AU

Commitments receives LearningPausedEvent from Learning domain for an AU learner. This test validates that event is discarded and the learner record is NOT updated (paused) 

Data Requirements:
	- Employer Account: <LevyUser>			<--- please refer to user secrets file for the actual value
	- Provider Account: <ProviderConfig>	<--- please refer to user secrets file for the actual value
	- FirstName:DoNotUse_TestData
	- LastName: ChangeAuLearnerStatus_Paused
	- ULN: any
	- StartDate: -6 months in the past
	- EndDate: +6 months in the future
	- Training Course: Any GSO/short-course/AU

#@regression
Scenario: AP_CoC_07_Verify Learning Paused Event marks the apprenticeship as Paused
	Given a Live AU learner record exists with Firstname: "DoNotUse_TestData" and LastName: "ChangeAuLearnerStatus_Paused"
	When LearningPausedEvent is received for the apprentice
	Then it does not change the status of AU record to paused
	And Provider verifies that recrod status stays as "Live"
	And Employer verifies that recrod status stays as "Live"
	
