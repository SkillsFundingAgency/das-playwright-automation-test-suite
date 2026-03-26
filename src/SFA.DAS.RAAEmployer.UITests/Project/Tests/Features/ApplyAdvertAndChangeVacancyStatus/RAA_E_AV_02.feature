Feature: RAA_E_AV_02

@regression
Scenario: RAA_E_AV_02 - Create advert with different work location, Approve, Apply and edit the advert
	Given the Employer creates an advert by selecting different work location
	When the Reviewer Approves the vacancy
	Then the Applicant can apply for a Vacancy in FAA
	Then the Employer can edit the vacancy