Feature: RAA_E_E2E_01

@raa
@raaemployer
@raae2e
@raaemployere2e
@regression
Scenario: RAA_E_E2E_01 - Create an advert with registered name, disability confident, Approve, Apply, receive email notifications, make Application Successful and close the advert
	Given the Employer creates an advert by using a registered name
	When the Employer verify 'Fixed Wage Type' the wage option selected in the Preview page
	When the Reviewer verifies disability confident and approves the vacancy
	# And the Reviewer Approves the vacancy
	Then the 'provider' receives 'employer listed you as training provider' email notification
	When the Applicant can apply for a Vacancy in FAA
	Then the 'employer' receives 'new application' email notification
	And the 'applicant' receives 'new application' email notification
	Then Employer can make the application successful
	And the status of the Application is shown as 'successful' in FAA 
	And the 'applicant' receives 'successful application' email notification
	Then the Employer can close the vacancy