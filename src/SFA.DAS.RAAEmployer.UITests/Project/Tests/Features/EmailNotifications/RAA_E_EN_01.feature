Feature: RAA_E_EN_01

@raa
@raaemployer
@raae2e
@raaemployere2e
@regression
@raaapiemployer

Scenario Outline: RAA_E_EN_01 - Create an advert with registered name, reject the advert and verify email notification
	When the user sends POST request to vacancy with payload <Payload>
	Then a <ResponseStatus> response is received
	And the Reviewer Refer the vacancy
	Then the 'employer' receives 'rejected advert' email notification

	Examples: 
	| Payload              | ResponseStatus |
	| singleLocation1.json | Created        |