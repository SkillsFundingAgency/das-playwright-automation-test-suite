Feature: Fin_OA_04_getAccountUsersByUserId

@api
@employerfinanceapi
@regression
@outerapi

Scenario: Fin_OA_04 getAccountUsersByUserId_linkedAccounts

	Given get the user with linked accounts
	When endpoint request GET /AccountUsers/{{UserRef}}/accounts is called
	Then account details should retrun for given user