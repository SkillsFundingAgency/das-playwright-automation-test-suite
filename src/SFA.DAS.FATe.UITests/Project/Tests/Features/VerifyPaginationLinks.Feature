Feature: Verify Pagination

@fate
@regression
Scenario: Verify Pagination Links 01
     Given the user navigates to the Search for apprenticeship training courses and training providers page
     When the user verifies pagination links are working as expected
     And the user navigates to Training providers page 
     When the user verifies pagination links are working as expected