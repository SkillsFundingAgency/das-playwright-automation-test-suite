Feature:ShortList_01

@fate
@regression
Scenario: Verify ShortList Functionality
    Given the user navigates to the Search for apprenticeship training courses and training providers page
    When the user selects a course and views training providers
    Then verify add remove count shortlist functionality