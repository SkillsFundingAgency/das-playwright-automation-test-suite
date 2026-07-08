@postapprovals
@linkedScenarios
Feature: AP_CoC_05_EmployerDrivenPause_AU

Employer can pause/freeze provider payments via UI

Data Requirements:
	- Employer Account: <LevyUser>			<--- please refer to user secrets file for the actual value
	- Provider Account: <ProviderConfig>	<--- please refer to user secrets file for the actual value
	- FirstName:DoNotUse_TestData
	- LastName: EmployerDrivenPauseAuLearner
	- ULN: any
	- StartDate: -6 months in the past
	- EndDate: +6 months in the future
	- Training Course: Any GSO/short-course/AU


@regression
Scenario: AP_CoC_05_Verify employer can pause/freeze provider payments for AU learner via UI    
	Given a Live AU learner record exists with Firstname: "DoNotUse_TestData" and LastName: "EmployerDrivenPauseAuLearner"
	When employer "pause" payments status for the apprenticeship record
	Then Commitments db is updated with the correct Freeze Payments Reason and Date for "Paused" status
	When employer "unpause" payments status for the apprenticeship record
	Then Commitments db is updated with the correct Freeze Payments Reason and Date for "Active" status
