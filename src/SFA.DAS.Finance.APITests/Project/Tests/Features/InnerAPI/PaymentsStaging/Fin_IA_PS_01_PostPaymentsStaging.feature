Feature: Fin_IA_PS_01_PostPaymentsStaging

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PS_01 Post payments staging and validate DB data
	Given post new payments to PaymentStaging table via api
	When find record in PaymentStaging table
	Then Verify the record in PaymentStaging table with the data posted via api
