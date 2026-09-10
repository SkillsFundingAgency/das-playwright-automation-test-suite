Feature: Certificate Multi Standard User E2E

@digicerts
@RemoveAuthentication
Scenario: Multiple Standard Certificates
  Given The MultiStandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When MultiStandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct multiple Standard learner certificate details
  And the authorised MultiStandardUser is successfully verified
