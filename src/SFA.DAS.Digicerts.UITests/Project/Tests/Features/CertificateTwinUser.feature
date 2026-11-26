Feature: Twin Certificate User E2E

@digicerts
Scenario: Twin Multi Certificates
  Given The TwinCertUser is logged into Apprenticeship Certificate Service after valid authentication
  When TwinCertUser answers the correct questions related to apprenticeship
  Then User is able to view the correct Twin Learner certificate details
  And the authorised TwinCertUser is successfully verified