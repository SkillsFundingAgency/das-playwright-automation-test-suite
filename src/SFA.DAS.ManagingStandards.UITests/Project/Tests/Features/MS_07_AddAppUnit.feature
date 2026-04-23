Feature: MS_07_AddEditApprenticeshipUnit

  @managingstandards
  @managingstandardsadddeleteevapprenticeshipunit
  @regression
  Scenario: MS_07A_Add_Edit_ApprenticeshipUnit_Region
    Given the provider logs into portal
    Then the provider is able to add a new application unit with regions
    And the provider is able to edit the new application unit via confirmation page
    And the provider is able to view the new application unit
    And the provider is able to delete the new application unit

  @managingstandards
  @managingstandardsadddeleteevapprenticeshipunit
  @regression
  Scenario: MS_07B_Add_ApprenticeshipUnit_ExistingContact
    Given the provider logs into portal
    Then the provider is able to add a new application unit with different contact
    And the provider can add delivery forecast
