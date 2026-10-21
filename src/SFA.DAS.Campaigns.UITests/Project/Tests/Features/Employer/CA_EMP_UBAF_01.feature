Feature: CA_EMP_UBAF_01
  Verify funding calculation for estimating benefit funding via the Explore funding options card on the Employers page.

  @campaigns @employer @regression
  Scenario Outline: Verify funding calculation for annual payroll options
    Given the user navigates to the Hire An Apprentice page
    When the user clicks on the employer card "Explore funding options"
    And the employer calculates funding selecting "<PayrollOption>" and standard "<Standard>"
    Then the estimated funding result should be calculated successfully

    Examples:
      | PayrollOption    | Standard                        |
      | Under £3 million | Abattoir worker (Level 2)       |
      | Over £3 million  | Academic professional (Level 7) |