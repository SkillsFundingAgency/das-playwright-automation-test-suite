Feature: Fin_IA_PS_02_PutPaymentMetaDataStaging

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PS_02 Put payment metadata staging and validate DB data
	Given post new payments to PaymentStaging table via api
	When put payment metadata in PaymentMetaDataStaging table via api
	And find record in PaymentMetaDataStaging table
	Then Verify the record in PaymentMetaDataStaging table with the data posted via api
