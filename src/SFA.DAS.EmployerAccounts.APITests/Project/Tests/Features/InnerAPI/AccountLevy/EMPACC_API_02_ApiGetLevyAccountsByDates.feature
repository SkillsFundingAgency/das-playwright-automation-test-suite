Feature: EMPACC_API_02_ApiGetLevyAccountsByDates.feature

@api
@employeraccountsapi
@regression
@innerapi
Scenario: EMPACC_API_02_ApiGetLevyAccountsByDates.feature
	Then endpoint api/accounts/{hashedAccountId}/levy/{year}/{month} can be accessed	
