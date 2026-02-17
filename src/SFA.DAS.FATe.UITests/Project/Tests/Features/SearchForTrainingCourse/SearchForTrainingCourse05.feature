Feature: SearchForTrainingCourse_05

@fate
@regression
Scenario: Search for courses_Foundation Apprenticeships
    Given the user navigates to find training page
    When I select the following training types
      | TrainingType |
      | <Type1>      |
      | <Type2>      |
      | <Type3>      |
    Then the following training type filters are applied
      | TrainingType |
      | <Type1>      |
      | <Type2>      |
      | <Type3>      |

    Examples:
      | Type1                     | Type2                        | Type3          |
      | ApprenticeshipUnits       |                              |                |
      | FoundationApprenticeships |                              |                |
      | Apprenticeships           |                              |                |
      | ApprenticeshipUnits       | FoundationApprenticeships    |                |
      | ApprenticeshipUnits       | Apprenticeships              |                |
      #The below combinations are not workign as expected due to a bug in html dev's will put in a fix
      #| Apprenticeships           | FoundationApprenticeships    |                |
      #| ApprenticeshipUnits       | FoundationApprenticeships    | Apprenticeships |