@apprenticeapp
@regression
Feature: AA_03_TaskAddEditDelete

Apprentice adds a task, edits and then deletes

@tag1
Scenario: AA_03a_Apprentice adds, edits and deletes a Todo task
    Given the apprentice has logged into the app
    When the apprentice skips the onboarding tour if present
    And the apprentice adds, edits, and then deletes a to do task
    Then the task is completely removed from the list

Scenario: AA_03b_Apprentice adds, edits and deletes a Done task
    Given the apprentice has logged into the app
    When the apprentice skips the onboarding tour if present
    And the apprentice has clicked on the done tasks tab
    And the apprentice adds, edits, and then deletes a done task
    Then the task is completely removed from the list