@approvals
@linkedScenarios
Feature: AP_E2E_EUA_02_ReturnCohortBackToProviderAfter2Weeks

This test verify that a cohort sent to the employer 14 days ago, should automatically be returned back to the Provider

@regression
@e2escenarios
Scenario: AP_E2E_EUA_02_Return cohort back to provider after 2 weeks 
    Given Provider sends an apprentice request (cohort) to an employer
    When Employer does not take any action on that cohort for more than 2 weeks
	Then return the cohort back to the Provider
    And the 'Provider' receives 'cohort ready for review' email notification

