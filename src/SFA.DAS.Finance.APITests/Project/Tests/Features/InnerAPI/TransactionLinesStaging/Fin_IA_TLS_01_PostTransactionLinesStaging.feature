Feature: Fin_IA_TLS_01_PostTransactionLinesStaging

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_TLS_01 Post transaction lines staging and validate DB data
	Given post new transaction lines to TransactionLineStaging table via api
	When find record in TransactionLineStaging table
	Then Verify the record in TransactionLineStaging table with the data posted via api
