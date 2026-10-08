@postapprovals
@linkedScenarios
Feature: AP_CoC_06_LearningPausedEventAndResume

1. Commitments receives LearningPausedEvent from Learning domain for variety of reasons. This test validates that event is processed correctly and the apprentice record is updated (paused) with correct pause date 
2. Commitments receives LearningResumedEvent from Learning domain for variety of reasons. This test validates that event is processed correctly and the apprentice record is updated (resumed) and staus changed to Live 

Data Requirements:
	- Employer Account: <LevyUser>			<--- please refer to user secrets file for the actual value
	- Provider Account: <ProviderConfig>	<--- please refer to user secrets file for the actual value
	- FirstName:DoNotUse_TestData
	- LastName: ChangeApprenticeStatus_Paused
	- ULN: any
	- StartDate: -6 months in the past
	- EndDate: +6 months in the future
	- Training Course: Any apprenticeship course, ideally without any course option
	- PauseDate  condition - StratDate < pauseDate < EndDate
	- ResumeDate condition - StratDate < resumeDate >=pauseDate  < EndDate


@regression
Scenario: AP_CoC_06a_Verify Learning Paused Event marks the apprenticeship as Paused
    Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "ChangeApprenticeStatus_Paused"
	When  LearningPausedEvent is received for the apprentice
    Then  provider verifies that record is set as "Paused" in Provider portal
    And  employer verifies that record has been "Paused" in Employer portal

@regression
Scenario: AP_CoC_06b_Verify Learning Resumed Event marks the apprenticeship as Live
	Given previous test has been completed successfully
	When  LearningResumedEvent is received for the apprentice
    Then  provider verifies that record is set as "Live" in Provider portal
    And   employer verifies that record has been "Live" in Employer portal

@regression
Scenario Outline: AP_CoC_06c_LearningPausedEvent and LearningResumedEvent emails validation
	Given previous test has been completed successfully
	Then  Verify the "<Recipient>" receive "<NotificationType>" email

Examples:
		| Recipient		| NotificationType					|
		| Employer      | Learner record has been paused	|
		| Employer      | Learner record has been resumed	|
