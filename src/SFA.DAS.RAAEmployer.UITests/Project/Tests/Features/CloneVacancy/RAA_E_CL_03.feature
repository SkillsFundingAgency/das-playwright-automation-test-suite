Feature: RAA_E_CL_03

@raa
@raaemployer
@clonevacancy
@regression
Scenario: RAA_E_CL_03 - Clone, Reject an advert and verify rejection email notification
	Given the Employer clones and creates an advert
	And the Reviewer Refer the vacancy
	Then the 'employer' receives 'rejected advert' email notification
