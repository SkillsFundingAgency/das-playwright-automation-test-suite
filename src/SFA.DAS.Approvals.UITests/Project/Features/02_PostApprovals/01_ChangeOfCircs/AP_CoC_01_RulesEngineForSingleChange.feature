Feature: AP_CoC_01_RulesEngineForSingleChange

A short summary of the feature

#Note:	This test is only to validate the rules engine for single change of circumstance request.
#		db and UI validation are not in scope of this test. 

@api
@postapprovals
Scenario: AP_CoC_01_Rules Engine For Single Change
	Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "CoCApprentice1"
	When a CoC request is made with below payload
	Then the response is as per table below
		| changeType | old        | new        | responseCode | responseBody																			|
		| StartDate  | 2026-09-09 | 2027-09-09 |          200 | EmployerApprovalRequested																|
		| StartDate  | 2026-09-09 | 2026-08-08 |          200 | EmployerApprovalRequested																|
		| StartDate  | 2026-09-09 | 2026-09-09 |          400 | New and Old values cannot be the same													|
		| StartDate  | 2026-09-09 | 2028-08-01 |          400 | The start date must be no later than one year after the end of the current teaching		|	
