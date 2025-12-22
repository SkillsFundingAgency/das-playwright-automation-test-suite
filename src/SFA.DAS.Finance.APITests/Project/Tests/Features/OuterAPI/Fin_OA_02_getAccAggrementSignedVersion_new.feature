Feature:Fin_OA_02_getAccAggrementSignedVersion_new

@api
@employerfinanceapi
@regression
@outerapi

Scenario: Fin_OA_02 getAccAggrementSignedVersion_New

  Given an employer account with signed version
  When endpoint /Accounts/{accountId}/users/minimum-signed-agreement-version is called
  Then the response body should contain valid account details signed aggrement details 
