Feature: Certificate Download (Standard and Framework)
 
Scenario: Download Standard Certificate PDF
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  And   User is able to Standard Download Certificate in PDF format

Scenario: Download Framework Certificate PDF
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And   User is able to Framework Download Certificate in PDF format