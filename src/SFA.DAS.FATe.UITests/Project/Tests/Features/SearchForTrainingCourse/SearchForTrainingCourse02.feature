Feature: SearchForTrainingCourse_02

@fate
@regression
Scenario: Search for courses without specifying a location but specifying partial course name
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course without location
    Then the relevant training courses are displayed with filters set

