Feature: Fin_OA_09_getTransferAccCounts

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_09 getTransferAccCounts

	Then endpoint /Transfers/{accountId}/counts can be accessed