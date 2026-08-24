Feature: Certificate Sharing(Standard and Framework)

@digicerts
Scenario: Create a sharing link for a Standard Certificate 
  Given The StandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When StandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And the user clicks the sharing link and verifies its details
  And the user opens the sharing link in a private browser and verifies the Standard certificate details
  And the user shares the certificate via email successfully

@digicerts
Scenario: Create a sharing link for a Framework Certificate
  Given The FrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When FrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And the user clicks the sharing link and verifies its details
  And the user opens the sharing link in a private browser and verifies the Framework certificate details
  And the user shares the certificate via email successfully