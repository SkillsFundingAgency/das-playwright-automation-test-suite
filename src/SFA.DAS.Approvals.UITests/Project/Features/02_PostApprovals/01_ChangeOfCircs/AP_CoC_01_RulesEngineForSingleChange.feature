Feature: AP_CoC_01_RulesEngineForSingleChange

A short summary of the feature

@postapprovals
Scenario: AP_CoC_01_Rules Engine For Single Change
	Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "CoCApprentice1"
	When Learning domain sends CoC request via approvals endpoint for below "<Category>": "<changeType>", "<old>", "<new>" and "<effFromDateDiff>"
	Then Commitments responds with "<responseCode>" and "<responseBody>"
	#And ApprovalRequest is created in commitments db with "<ApprovalRequestStatus>"
	#And ApprovalFieldRequest is created in commitments db with "<ApprovalFieldRequestStatus>"


Examples: 
	| Category          | changeType | old               | new                   | effFromDateDiff | responseCode | responseBody                                         | ApprovalRequestStatus | ApprovalFieldRequestStatus |
	| price increase    | TNP1       |              5000 |                  5500 |               0 |          201 | EmployerApprovalRequested                            |                     1 |                          3 |
	| price decrease    | TNP1       |              5000 |                  2500 |             -30 |          201 | EmployerApprovalRequested                            |                     1 |                          3 |
	| invalid TNP1      | TNP1       |              5000 |                       |             -30 |          400 | String could not be converted to an integer          |                     1 |                          3 |
	| price increase    | TNP2       |              1000 |                  1100 |             +30 |          201 | EmployerApprovalRequested                            |                     1 |                          3 |
	| price decrease    | TNP2       |              1000 |                  1100 |              -1 |          201 | EmployerApprovalRequested                            |                     1 |                          3 |
#   | invalid TNP2      | TNP2       |              1000 |                     0 |              -1 |          201 | AutoRejected                                         | null                  | null                       |
	| firstName         | Firstname  | DoNotUse_TestData | DoNotUse_TestData_New |             +10 |          201 | AutoApproved                                         |                     1 |                          3 |
#   | lastName          | Lastname   | CoCApprentice1    | CoCApprentice1_New    |             -50 |          201 | AutoApproved                                         |                     1 |                          3 |
	| invalid firstName | Firstname  | DoNotUse_TestData |                       |               0 |          400 | String value cannot be null or allow only whitespace |                     1 |                          3 |
	
