@approvals
Feature: AP_E2E_EUA_01_ExistingUserAccount

A short summary of the feature

@regression
@e2escenarios
@selectstandardwithmultipleoptions
Scenario: AP_E2E_EUA_01 Provider creates cohort from ILR data Employer approves it
  
#Given Provider submit ILR successfully for 2 new apprentices
When Provider logs into Provider-Portal
And creates an apprentice request (cohort) by selecting same apprentices
Then Provider can send it to the Employer for approval
And apprentice request (cohort) is available under 'Apprentice requests > With employers'
When Employer approves the cohort
Then apprentice request (cohort) is no longer available under any tab in 'Apprentice requests' section
