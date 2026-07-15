Feature: Fin_IA_PS_01_PostEnglishFractions

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PS_01 Post english fractions and validate DB data
	Given post english fractions via api
	When find record in EnglishFraction table
	Then Verify the record in EnglishFraction table with the data posted via api
