Feature: Fin_IA_FL_03_AccountGetEnglishFractionHistory

@api
@employerfinanceapi
@outerapi
@regression
Scenario: Fin_IA_FL_03 AccountGetEnglishFractionHistory
	Then endpoint api/accounts/{hashedAccountId}/levy/english-fraction-history can be accessed