Feature: RAA_E_AV_01

@regression
@raaemployer
Scenario: RAA_E_AV_01 - Create anonymous advert with National Minimum Wage, Approve, Apply and close the advert
	Given the Employer creates an anonymous advert
	When the Employer verify 'National Minimum Wage' the wage option selected in the Preview page
	When the Reviewer Approves the vacancy
	Then the 'employer' receives 'approved advert' email notification
	Then the Applicant can apply for a Vacancy in FAA
	Then the Employer can close the vacancy