Feature: SearchForTrainingCourse_01

@fate
@regression
Scenario: Search for courses without specifying a course but specifying a location
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course with an apprenticeship location only
    Then the relevant training courses are displayed