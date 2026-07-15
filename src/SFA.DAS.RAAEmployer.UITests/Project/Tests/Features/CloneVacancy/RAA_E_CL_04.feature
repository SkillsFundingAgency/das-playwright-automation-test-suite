Feature: RAA_E_CL_04

@raa
@raaemployer
@clonevacancy
@regression
Scenario: RAA_E_CL_04 - Clone an archived advert, Approve, Apply and make Application unsuccessful
	Given the Employer clones an archived advert and creates an advert
	And the Reviewer Approves the vacancy
	When the Applicant can apply for a Vacancy in FAA
	Then Employer can make the application successful
	And the status of the Application is shown as 'successful' in FAA