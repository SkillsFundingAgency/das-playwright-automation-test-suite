Feature: Certificate Details (Standard and Framework)
 
Scenario: View Standard Certificate details
  Given The StandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When StandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And the authorised StandardUser is successfully verified
 
Scenario: View Framework Certificate details
  Given The FrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When FrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And the authorised FrameworkUser is successfully verified
 
Scenario: Multiple Standard Certificates
  Given The MultiStandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When MultiStandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct multiple Standard learner certificate details
  And the authorised MultiStandardUser is successfully verified

Scenario: Multiple Framework Certificates
  Given The MultiFrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When MultiFrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct multiple Framework learner certificate details
  And the authorised MultiFrameworkUser is successfully verified