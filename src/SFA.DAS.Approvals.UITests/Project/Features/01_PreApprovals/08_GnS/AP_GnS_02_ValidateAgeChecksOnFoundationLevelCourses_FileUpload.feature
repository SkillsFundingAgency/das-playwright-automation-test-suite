@approvals
Feature: AP_GnS_02_ValidateAgeChecksOnFoundationLevelCourses_FileUpload


@regression
@e2escenarios
Scenario: AP_GnS_02_Validate Age Checks On Foundation Level Courses During CSV File Upload

	Given Provider have few apprentices to add using CSV file upload
	And one of the apprentice on Foundation course is above 25 years 
	When Provider uploads the csv file
	Then system does not allow to upload the file and displays an error message
