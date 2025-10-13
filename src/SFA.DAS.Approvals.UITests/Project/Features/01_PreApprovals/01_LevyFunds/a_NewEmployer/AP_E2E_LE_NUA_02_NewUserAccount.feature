@approvals
Feature: AP_E2E_LE_NUA_02_NewUserAccount

1. A new levy employer creates an employer account and send an empty cohort to the training provider.
2. Provider follows 'Cohort > Add another apprentice > Select apprentices from ILR' route to add apprentice details
3. Provider send cohort back to employer to review
4. Employer approves the cohort
5. Provider does the final approval.


@regression
@e2escenarios
@addlevyfunds
Scenario: AP_E2E_LE_NUA_02 Create Employer Provider sends cohort to employer for review then employer approves then provider approves
	Given The User creates "Levy" Employer account and sign an agreement
	And the employer has 1 apprentice ready to start training
	When the employer create and send an empty cohort to the training provider to add learner details
	And the provider adds apprentices along with RPL details and sends to employer to review
	Then the Employer can approve the cohort
	And the provider approves the cohorts