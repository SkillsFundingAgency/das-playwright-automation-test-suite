@apprenticeapp
Feature: Sign in

    Scenario: Successful login with valid credentials
        When the user signs in to the Apprentice app
        Then the user should be redirected to the dashboard