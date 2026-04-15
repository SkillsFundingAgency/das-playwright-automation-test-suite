Feature: RAA_E_AV_04

@raa
@raaemployer
@regression
Scenario: RAA_E_AV_04 - Create advert with nationwide locations and National minimum wage for apprentices, Approve, Apply and withdraw application, close the vacancy
	Given the Employer creates a advert with "national" work location and 'National Minimum Wage For Apprentices' wage type
	When the Employer verify 'National Minimum Wage For Apprentices' the wage option selected in the Preview page
	When the Reviewer Approves the vacancy
	Then the Applicant can apply for a Vacancy in FAA
	And the Applicant can withdraw the application
	And Employer can see the withdrawn application
	And the 'applicant' receives 'withdrawn application' email notification
