Feature: SearchForTrainingCourse_04

@fate
@regression
Scenario: Search for courses with course name where no results found
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user searches for a course with no results
    Then no courses that match your search is displayed with filters set

