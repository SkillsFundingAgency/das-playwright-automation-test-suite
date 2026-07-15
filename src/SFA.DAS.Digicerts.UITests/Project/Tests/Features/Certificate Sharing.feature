Feature: Certificate Sharing(Standard and Framework)

Scenario: Create a sharing link for a Standard Certificate
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct Standard learner certificate details
  When User creates a sharing link
  Then the Create Sharing Link page is displayed
  When User Copied and opens the sharing link in a private browser
  Then the certificate details are displayed correctly
  When User shares the certificate via email
  Then the sharing email is sent successfully

Scenario: Create a sharing link for a Framework Certificate
  Given The Apprentice is logged into Apprenticeship Certificate Service after valid authentication
  When  User answers the correct questions related to apprenticeship
  Then  User is able to view the correct Framework learner certificate details
  And   User is able to Framework Download Certificate in PDF format
  When User creates a sharing link
  Then the Create Sharing Link page is displayed
  When User Copied and opens the sharing link in a private browser
  Then the certificate details are displayed correctly
  When User shares the certificate via email
  Then the sharing email is sent successfully