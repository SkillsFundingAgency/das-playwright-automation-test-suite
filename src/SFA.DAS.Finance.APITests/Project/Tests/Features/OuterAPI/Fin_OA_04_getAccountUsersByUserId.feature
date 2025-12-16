Feature: Fin_OA_04_getAccountUsersByUserId

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_04 getAccountUsersByUserId

	Given send an api request GET /AccountUsers/{{UserRef}}/accounts?email={{email}}

	Then Verify the getAccountUsersByUserId api response with records fetch from DB
		| query |
		| getAccountUsersByUserId.sql |