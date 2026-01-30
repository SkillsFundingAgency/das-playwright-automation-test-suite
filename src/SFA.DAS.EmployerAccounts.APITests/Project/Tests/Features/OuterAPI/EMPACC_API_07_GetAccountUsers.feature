Feature: EMPACC_API_07_GetAccountUsers

@api
@employeraccountsapiF
@outerapi
@regression
Scenario: EMPACC_API_07_GetAccountUsers
	Then endpoint /AccountUsers/{userId}/accounts can be accessed