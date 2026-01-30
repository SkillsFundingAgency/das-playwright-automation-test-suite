Feature: EMPACC_API_01_ApiGetTransferConnections

@api
@employeraccountsapi
@regression
@innerapi
Scenario: EMPACC_API_01_ApiGetTransferConnections
	Then endpoint api/accounts/{hashedAccountId}/transfers/connections can be accessed