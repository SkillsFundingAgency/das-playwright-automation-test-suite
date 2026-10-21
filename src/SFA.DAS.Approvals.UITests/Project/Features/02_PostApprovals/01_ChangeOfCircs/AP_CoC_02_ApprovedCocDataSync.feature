Feature: AP_CoC_02_ApprovedCocDataSync

A short summary of the feature

@postapprovals
@api
@nameChange
Scenario: AP_CoC_02_Approved CoCs Data Sync
	Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "CoCApprentice2"
	When Learning domain sends ApprovedLearningUpdatedEvent for below "<Category>": "<changeType>", "<old>" and "<new>"
	Then "<changeType>" field is updated in Commitments db with the "<new>" value

	Examples: 
	| Category		| changeType		| old					| new                   | 
	| First Name	| Firstname			| DoNotUse_TestData		| DoNotUse_TestData_New | 
	| Last Name		| Surname			| CoCApprentice2		| CoCApprentice2_New    | 
	| Date of Birth	| DOB				| 2008-01-02			| 2005-02-02			| 
	| Start	Date	| PlannedStartDate	| 2026-06-15			| 2026-10-05			| 
	| End Date		| PlannedEndDate	| 2027-06-15			| 2027-12-31			| 
	| Email			| Email				| john.doe@example.com	| jane.doe@example.com	| 
#   | Course		| Lastname			| CoCApprentice1		| CoCApprentice1_New    | 
