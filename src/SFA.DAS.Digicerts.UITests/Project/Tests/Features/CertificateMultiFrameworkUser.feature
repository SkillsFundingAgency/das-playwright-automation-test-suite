Feature: Certificate Multi Framwork User E2E


@digicerts
@RemoveAuthentication
Scenario: Multiple Framework Certificates
  Given The MultiFrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When MultiFrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct multiple Framework learner certificate details
  And the authorised MultiFrameworkUser is successfully verified