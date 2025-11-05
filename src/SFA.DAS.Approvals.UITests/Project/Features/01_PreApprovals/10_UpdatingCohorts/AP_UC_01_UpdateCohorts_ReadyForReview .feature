Feature: AP_UC_01_UpdateCohorts_ReadyForReview 

A short summary of the feature

@tag1
Scenario: AP_UC_01_Update existing cohort - Ready for review 
	Given a cohort created via ILR exists in 'Ready for review' section
	When Provider resubmits ILR file with changes to apprentice details
	Then a banner is displayed on the cohort for provider to accept changes
	And provider cannot approve the cohort
	When provider accepts the changes
	Then provider can approve the cohort