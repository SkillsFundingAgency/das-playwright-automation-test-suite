Feature: Certificate Details (Standard and Framework)
 
Scenario: View Standard Certificate details
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
 
Scenario: View Framework Certificate details
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
 
Scenario: Multiple Certificates (Standard)
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct multiple Standard learner certificate details

Scenario: Multiple Certificates (Framework)
  Given   The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct multiple Framework learner certificate details