@approvals
@linkedScenarios
Feature: AP_E2E_EUA_02_ReturnCohortBackToProviderAfter2Weeks


@regression
@e2escenarios
Scenario: AP_E2E_EUA_02_Return cohort back to provider after 2 weeks
 
    Given Provider sends an apprentice request (cohort) to an employer
    When Employer does not take any action on that cohort for more than 2 weeks
	Then return the cohort back to the Provider
