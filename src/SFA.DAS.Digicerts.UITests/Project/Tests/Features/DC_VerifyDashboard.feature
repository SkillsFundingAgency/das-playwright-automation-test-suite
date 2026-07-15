Feature: Certificate verification journey

  Scenario: User verifies apprenticeship certificates
       Given the user navigates to the start page
       When the user clicks the Start button
       And the user enters authentication details
       And the user uploads the authentication file
       And the user authenticates the session
       And the user continues to the verification page
       Then the user views available certificate categories