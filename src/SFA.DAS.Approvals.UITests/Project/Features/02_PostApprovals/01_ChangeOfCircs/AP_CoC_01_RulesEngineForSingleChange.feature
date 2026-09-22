Feature: AP_CoC_01_RulesEngineForSingleChange

A short summary of the feature

@postapprovals
Scenario: AP_CoC_01_Rules Engine For Single Change
	Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "CoCApprentice1"
	When Learning domain sends CoC request via approvals endpoint for below "<Category>": "<changeType>", "<old>", "<new>" and "<effectivefromDate>"
	#Then Commitments responds with "<approvalStatus>"
	#And ApprovalRequest is created in commitments db with "<Status1>"
	#And ApprovalFieldRequest is created in commitments db with "<Status2>"


Examples: 
	| Category			| changeType	| old					| new						| effectivefromDate | approvalStatus | Status1	| Status2	|
	| price increase    | TNP1			| 5000					| 5500						| 0					|             25 | 1		| 3			|
	| price decrease    | TNP1			| 5000					| 2500						| -30				|             25 | 1		| 3			|
	| price increase    | TNP2			| 1000					| 1100						| +30				|             25 | 1		| 3			|
	| price decrease    | TNP2			| 1000					| 1100						| -1				|             25 | 1		| 3			|
	| invalid price		| TNP2			| 1000					| 0							| -1				|             25 | null		| null		|
	| firstName         | firstName		| DoNotUse_TestData		| DoNotUse_TestData_New		| +10				|            115 | 1		| 3			|
	| lastName          | lastName		| CoCApprentice1		| CoCApprentice1_New		| -50				|            115 | 1		| 3			|
	| invalid Name      | lastName		| CoCApprentice1		| ''						| 0					|            115 | 1		| 3			|
	| invalid date      | lastName		| CoCApprentice1		| CoCApprentice1_New		| -10000			|            115 | null		| null		|
