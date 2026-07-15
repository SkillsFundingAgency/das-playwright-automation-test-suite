Feature: RAA_E_AV_03

@raa
@raaemployer
@regression
Scenario: RAA_E_AV_03 - Create advert with multiple work location and Set As Competitive wage type, Approve, Apply
	Given the Employer creates a advert with "multiple" work location and 'Set As Competitive' wage type
	When the Employer verify 'Set As Competitive' the wage option selected in the Preview page
	When the Reviewer Approves the vacancy
	Then the Applicant can apply for a Vacancy with multiple locations in FAA
