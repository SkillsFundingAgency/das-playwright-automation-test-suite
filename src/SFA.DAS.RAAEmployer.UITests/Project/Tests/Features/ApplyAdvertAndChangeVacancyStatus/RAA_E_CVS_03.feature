Feature: RAA_E_CVS_03

@raa		
@raaemployer
@regression
@raaapiemployer

Scenario Outline: RAA_E_CVS_03 - Create, Approve and Close the advert before applying
	When the user sends POST request to vacancy with payload <Payload>
	Then a <ResponseStatus> response is received
	And the Reviewer Approves the vacancy
	Then the Employer can close the vacancy

	Examples: 
	| Payload              | ResponseStatus |
	| singleLocation1.json | Created        |

