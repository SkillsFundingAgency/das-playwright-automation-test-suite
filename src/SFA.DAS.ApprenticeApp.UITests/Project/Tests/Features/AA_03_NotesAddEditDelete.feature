@apprenticeapp
@regression
Feature: AA_03_NotesAddEditDelete

Apprentice adds a note, edits and then deletes it

@tag1
Scenario: AA_03_Apprentice adds, edits and deletes a note
    Given the apprentice has logged into the app
    When the apprentice skips the onboarding tour if present
    And the apprentice adds, edits, and then deletes a note
    Then the note is completely removed from the list