Feature: CA_SU_EMP_01
  Sign up to emails as an Employer

  @campaigns @signup @regression
  Scenario Outline: Sign up to emails as an Employer with <CompanySize> employees
    Given the employer navigates to Sign Up Page
    When the employer fills the Your Details section
    And selects company size "<CompanySize>"
    Then an employer registers interest

    Examples:
      | CompanySize                            |
      | Less than 10 employees                 |
      | Between 10 and 49 employees            |
      | Between 50 and 249 employees           |
      | Over 250 employees                     |