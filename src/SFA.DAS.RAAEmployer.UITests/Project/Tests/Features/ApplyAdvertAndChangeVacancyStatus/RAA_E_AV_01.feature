Feature: RAA_E_AV_01

@regression
@raaemployer
@raaapiemployer
Scenario Outline: RAA_E_AV_01 - Create anonymous advert with National Minimum Wage using API, Approve, Apply and close the advert

	When the user sends POST request to vacancy with payload <Payload>
	Then a <ResponseStatus> response is received
	Given the Employer navigates to 'Recruit' Page
	When the Employer verify 'National Minimum Wage' the wage option selected in the Preview page
	When the Reviewer Approves the vacancy
	Then the Applicant can apply for a Vacancy in FAA

	Examples: 
	| ResponseStatus | Payload                         |
	| Created        | singleLocationAnonymousUI.json             |