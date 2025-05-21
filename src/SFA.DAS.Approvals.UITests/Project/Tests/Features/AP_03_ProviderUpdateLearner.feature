Feature: AP_03_ProviderUpdateLearner

@approvals
@regression
Scenario: AP_03_ProviderUpdateLearner
	When the user sends PUT request to /provider/10005760/academicyears/2425/learners with payload uln1.json
	Then api Accepted response is received
	Given the provider logs into portal
	
	