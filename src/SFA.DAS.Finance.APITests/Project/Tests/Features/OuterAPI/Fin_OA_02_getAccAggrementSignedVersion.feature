Feature:Fin_OA_02_getAccAggrementSignedVersion

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_02 getAccAggrementSignedVersion

	Given send an api request GET /Accounts/{{accountId}}/users/minimum-signed-agreement-version

    Then Verify the minimumSignedAgreementVersion api response with records fetch from DB
        | query |
        | GetSignedVersion.sql |