Feature: CA_HM_01_HomeHub
  Verify navigation and ensure no broken links across all cards, panels, header, and footer links on the "Home" page.

  @campaigns @homepage @regression
  Scenario Outline: Verify all navigation links on the Home page - <CardName>
    Given the user navigates to the Home page
    When the user clicks on the homepage card "<CardName>"
    Then the links are not broken

    Examples:
      # Header Branding & Links
      | CardName                                         |
      | Home                                             |

      # Main Navigation Bar (Header)
      | Apprentices                                      |
      | Employers                                        |

      # Primary Hub Callouts & CTA
      | Become an apprentice                             |
      | Hire an apprentice                               |
      | Find an apprenticeship                           |

      # Lower Homepage Cards & Panels
      | What employers hire apprentices?                 |
      | Connect with apprentices and employers           |
      | Resources to inspire and help future apprentices |

      # Footer Links & Policies
      | Give us feedback                                 |
      | Sitemap                                          |
      | Cookies                                          |
      | Privacy                                          |
      | Accessibility                                    |
      | Department for Education                         |
      | Open Government Licence v3.0                     |
      | © Crown copyright                                |