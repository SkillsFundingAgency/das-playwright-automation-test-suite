Feature: Certificate Download (Standard and Framework)
 
Scenario: Download Standard Certificate PDF
  Given The StandardUser is logged into Apprenticeship Certificate Service after valid authentication
  When StandardUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And  User is able to Download Standard Certificate in PDF format

Scenario: Download Framework Certificate PDF
  Given The FrameworkUser is logged into Apprenticeship Certificate Service after valid authentication
  When FrameworkUser answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And User is able to Download Framework Certificate in PDF format