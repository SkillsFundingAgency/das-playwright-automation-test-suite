Feature: Fin_IA_PS_02_PostEnglishFractionsUpdateRequiredFalse

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PS_02 Post english fractions with update required false and validate DB data
	Given post english fractions via api with update required false
	When find record in EnglishFraction table
	Then Verify the record in EnglishFraction table with the data posted via api
