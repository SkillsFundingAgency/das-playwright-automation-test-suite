Feature: SearchForTrainingCourse_03

@fate
@regression
Scenario: Search for courses without specifying a location and course name
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course without location and course name
    Then all the courses are displayed without filters set

