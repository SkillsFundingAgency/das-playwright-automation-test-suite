Feature: Fin_IA_PS_03_PostEnglishFractionCalculationDate

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PS_03 Post english fraction calculation date and validate DB data
	Given post english fraction calculation date via api
	When find records in EnglishFractionCalculationDate table
	Then Verify the records in EnglishFractionCalculationDate table contains the data posted via api
