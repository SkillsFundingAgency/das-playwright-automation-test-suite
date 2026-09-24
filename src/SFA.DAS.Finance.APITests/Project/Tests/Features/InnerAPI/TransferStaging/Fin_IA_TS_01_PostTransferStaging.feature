Feature: Fin_IA_TS_01_PostTransferStaging

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_TS_01 Post transfer staging and validate DB data
	Given post new transfers to TransferStaging table via api
	When find record in TransferStaging table
	Then Verify the record in TransferStaging table with the data posted via api
