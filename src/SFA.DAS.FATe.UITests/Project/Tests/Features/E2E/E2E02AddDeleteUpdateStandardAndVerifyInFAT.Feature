Feature: E2E Training Provider Verify Standards

  @fate
  @regression
  @e2e02
Scenario: Add Delete Standard And Verify In FAT
	Given the provider logs into portal
    When the provider adds the Craft Plasterer course to the standards list
    And the provider is listed on the FAT training providers page
    And the provider deletes the Craft Plasterer course from the standards list
    And the provider is not listed on the FAT training providers page
	