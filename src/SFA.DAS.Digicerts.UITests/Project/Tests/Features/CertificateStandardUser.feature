Feature: Certificate Standard User E2E 

@digicerts
@RemoveAuthentication
Scenario: View Standard Certificate details
  Given The StandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When StandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And the authorised StandardUser is successfully verified

@digicerts 
Scenario: Download Standard Certificate PDF
  Given The StandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When StandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And  User is able to Download Standard Certificate in PDF format
 
@digicerts
Scenario: Create a sharing link for a Standard Certificate 
  Given The StandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When StandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And the user clicks the sharing link and verifies its details
  And the user opens the sharing link in a private browser and verifies the Standard certificate details
  And the user shares the certificate via email successfully

