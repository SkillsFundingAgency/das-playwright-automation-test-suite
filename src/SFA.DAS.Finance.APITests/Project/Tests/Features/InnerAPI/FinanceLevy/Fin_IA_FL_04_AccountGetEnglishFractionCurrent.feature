Feature: Fin_IA_FL_04_AccountGetEnglishFractionCurrent

@api
@employerfinanceapi
@outerapi
@regression
Scenario: Fin_IA_FL_04 AccountGetEnglishFractionCurrent
	Then endpoint api/accounts/{hashedAccountId}/levy/english-fraction-current can be accessed