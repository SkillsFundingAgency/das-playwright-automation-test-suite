Feature: Certificate Framework User E2E
 
@digicerts
Scenario: Download Framework Certificate PDF
  Given The FrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When FrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And User is able to Download Framework Certificate in PDF format

@digicerts
@RemoveAuthentication
Scenario: View Framework Certificate details
  Given The FrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When FrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And the authorised FrameworkUser is successfully verified

@digicerts
Scenario: Create a sharing link for a Framework Certificate
  Given The FrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When FrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And the user clicks the sharing link and verifies its details
  And the user opens the sharing link in a private browser and verifies the Framework certificate details
  And the user shares the certificate via email successfully