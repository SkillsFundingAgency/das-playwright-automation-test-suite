Feature: CA_EM_01_EmployerHub
  Verify navigation across all stepper cards and callout panels on the Employers landing page ("Hire an apprentice").

  @campaigns @employer @regression
  Scenario Outline: Verify card and panel navigation links
    Given the user navigates to the Hire An Apprentice page
    When the user clicks on the employer card "<CardName>"
    Then the links are not broken

    Examples:
      # Section 1: Considering hiring an apprentice?
      | CardName                                                 |
      | Find out if an apprentice is right for your business     |
      | Compare apprenticeship training types                    |
      | Explore funding options                                  |
      | Understand your responsibilities as an employer          |
      | Get £2,000 for hiring an apprentice                      |

      # Section 2: How to hire an apprentice
      | Find and choose training for your apprentice             |
      | Create an apprenticeship service account                 |
      | Recruit your apprentice                                  |

      # Section 3: During an apprenticeship
      | Complete an initial assessment                           |
      | Support your apprentice                                  |
      | Plan what's next for your apprentice                     |
      | Become an apprenticeship ambassador as an employer       |