Feature: Fin_OA_getAccAggrementSignedVersion

@api
@employeraccountsapi
@regression
@innerapi
Scenario: getAccAggrementSignedVersion

	Then endpoint /Accounts/{accountId}/minimum-signed-agreement-version can be accessed