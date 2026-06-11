Feature: E2E Training Provider Verify App Unit

  @fate
  @regression
  @managingstandardsadddeleteefapprenticeshipunit
  @e2e03
Scenario: Add Update and Delete App Unit And Verify In FAT
	Given the provider logs into portal
    When the provider adds the Electrical Fitting course to the App Units list
    And the provider is listed on the FAT apprenticeship unit providers page
    When the provider updates the course to be available at learners and provider location
    And the provider is listed at learners and provider location on the FAT apprenticeship unit providers page
    And the provider deletes the Electrical Fitting course from the App Units list
    Then the provider is not listed or no provider found on the FAT apprenticeship unit providers page
	