Feature: EMPACC_API_01_ApiGetLevyAccounts

@api
@employeraccountsapi
@regression
@innerapi
Scenario: EMPACC_API_01_ApiGetLevyAccounts
	Then endpoint api/accounts/{hashedAccountId}/levy  can be accessed
