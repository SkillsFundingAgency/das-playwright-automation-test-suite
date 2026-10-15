Feature: RAA_E_AC_01

@raa
@raaemployer
@accessibility
Scenario: RAA_E_AC_01 - Employer navigates key Recruit an apprentice pages, including a validation error
Given the Employer navigates to 'Recruit' Page
When the employer selects the 'Manage your emails' link
Then the employer sets the email preferences
And the Employer sees a validation error when leaving the vacancy title blank
