@cmad
@approvals
@linkedScenarios
@regression
@e2escenarios
Feature: CMAD_OnboardApprentice_E2E

A comprehensive end-to-end onboarding journey utilizing the Approvals pipeline 
to seed structural learner rows, followed by verifying active record visibility 
within the Apprentice CMAD portal application view.

Scenario: AP_E2E_CMAD_01a Onboard an apprentice via Approvals and verify profile within CMAD
    # -----------------------------------------------------------------------------------------
    # From SFA.DAS.Approvals.UITests Project Assemblies
    # -----------------------------------------------------------------------------------------
    Given Provider successfully submits 1 ILR record containing a learner record for a "Levy" Employer
    Then a record is created in LearnerData Db for each learner
    When Provider sends an apprentice request (cohort) to the employer by selecting same apprentices
    Then Commitments Db is updated with respective LearnerData Id
    When the Employer approves the apprentice request (cohort)
    Then LearnerData Db is updated with respective Apprenticeship Id
    And Apprenticeship record is created in Learning Db
    # -----------------------------------------------------------------------------------------
    When the apprentice logs into the CMAD application via the developer stub page
    Then the apprentice is taken to the CMAD home view dashboard
    And the apprentice should see their approved employment details correctly populated