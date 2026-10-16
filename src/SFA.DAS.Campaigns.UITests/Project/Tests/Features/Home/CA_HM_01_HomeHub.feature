Feature: CA_HM_01_HomeHub
  Verify navigation and ensure no broken links across all cards, panels, header, and footer links on the "Home" page.

  @campaigns @homepage @regression
  Scenario Outline: Verify all navigation links on the Home page - <CardName>
    Given the user navigates to the Home page
    When the user clicks on the homepage card "<CardName>"
    Then the links are not broken

    Examples:
      # Header Branding & Navigation
      | CardName                                                 |
      | Home                                                     |
      | Apprentices                                              |
      | Employers                                                |

      # Primary Callouts & Hero Cards
      | Become an apprentice                                     |
      | Hire an apprentice                                       |
      | Find an apprenticeship                                   |

      # Lower Homepage Cards & Content Links
      | Is an apprenticeship right for you?                      |
      | Join the Apprenticeship Ambassador Network (AAN)         |
      | Find out if an apprentice is right for your business     |

      # Regional Links (Outside England)
      | Scotland                                                 |
      | Northern Ireland                                         |
      | Wales                                                    |

      # Footer Links & Policies
      | Give us feedback                                         |
      | Sitemap                                                  |
      | Cookies                                                  |
      | Privacy                                                  |
      | Accessibility                                            |
      | Department for Education                                 |
      | Open Government Licence v3.0                             |
      | © Crown copyright                                        |