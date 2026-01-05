Feature:Fin_OA_02_getAccAggrementSignedVersion

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_02 getAccAggrementSignedVersion

	Then endpoint /Accounts/{accountId}/minimum-signed-agreement-version can be accessed