Feature: EMPACC_API_03_GetEnglishFractionHistory

@api
@employeraccountsapi
@outerapi
@regression
Scenario: EMPACC_API_03_GetEnglishFractionHistory
	Then endpoint /Accounts/{hashedAccountId}/levy/english-fraction-history can be accessed