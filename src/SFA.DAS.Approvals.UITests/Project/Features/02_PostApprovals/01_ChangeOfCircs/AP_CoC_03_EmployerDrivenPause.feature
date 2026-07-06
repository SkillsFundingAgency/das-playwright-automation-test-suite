@approvals
@postapprovals
@linkedScenarios
Feature: AP_CoC_03_EmployerDrivenPause

Employer can pause/freeze provider payments via UI

@regression
Scenario: AP_CoC_03_Verify employer can pause/freeze provider payments via UI
    Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "EmployerDrivenPauseApprentice"
	When employer "pause" payments status for the apprenticeship record
	Then Commitments db is updated with the correct Freeze Payments Reason and Date for "Paused" status
	When employer "unpause" payments status for the apprenticeship record
	Then Commitments db is updated with the correct Freeze Payments Reason and Date for "Active" status
