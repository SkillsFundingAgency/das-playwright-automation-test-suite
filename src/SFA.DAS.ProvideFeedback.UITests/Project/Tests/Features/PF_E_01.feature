Feature: PF_E_01

@providefeedback
@regression
@employerfeedback
Scenario: PF_E_01A Employer provides feedback for a provider
	Given the Employer logins into Employer Portal
	And the user completes the feedback journey for a training provider

@providefeedback
@regression
@employerfeedback
Scenario: PF_E_01B Employer View only User provides feedback for a provider
	Given the Second Employer View only User logins into Employer Portal
	And the viewUser completes the feedback journey for a training provider
