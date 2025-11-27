Feature: Fin_OA_getTransferAccCounts

@api
@employeraccountsapi
@regression
@innerapi
Scenario: getTransferAccCounts

	Then endpoint /Transfers/{accountId}/counts can be accessed