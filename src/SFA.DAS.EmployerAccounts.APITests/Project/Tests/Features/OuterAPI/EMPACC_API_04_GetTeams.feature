Feature: EMPACC_API_04_GetTeams

@api
@employeraccountsapi
@outerapi
@regression
Scenario: EMPACC_API_04_GetTeams
	Then endpoint /Accounts/{accountId}/teams can be accessed