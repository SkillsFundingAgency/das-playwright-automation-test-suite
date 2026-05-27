Feature: RAA_E_E2E_05

@regression
@raaemployer
@raaapiemployer
Scenario Outline: RAA_E_E2E_05 - Create anonymous advert using API, Approve, Apply, close the advert, mark application unsuccessful and archive the advert

	When the user sends POST request to vacancy with payload <Payload>
	Then a <ResponseStatus> response is received
	When the Reviewer Approves the vacancy
	Then the Applicant can apply for a Vacancy in FAA
	Then the Employer can close the vacancy
	And Employer can make the application unsuccessful and archive the advert

	Examples: 
	| ResponseStatus | Payload                         |
	| Created        | singleLocationAnonymousUI.json  |