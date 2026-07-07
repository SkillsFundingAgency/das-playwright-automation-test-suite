Feature: RAA_P_E2E_02F

@raa
@raaprovider
@raae2e
@raaprovidere2e
@regression
Scenario: RAA_P_E2E_02F - Create a foundation vacancy with registered name, Approve, Apply, receive email notifications and make Application Successful
	Given the Provider creates a foundation vacancy by using a registered name
	And the Reviewer Approves the vacancy
	When the Applicant can apply for a foundation vacancy in FAA
	Then the 'provider' receive 'new application' email notification
	And the 'applicant' receive 'new application' email notification
	And Provider can make the application successful
	And the status of the Application is shown as 'successful' in FAA
	And the 'applicant' receive 'successful application' email notification