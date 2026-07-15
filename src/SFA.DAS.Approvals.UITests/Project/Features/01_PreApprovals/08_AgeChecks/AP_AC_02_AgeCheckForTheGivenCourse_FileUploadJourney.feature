@approvals
Feature: AP_AC_02_AgeCheckForTheGivenCourse_FileUploadJourney

Following courses / levels have age restrictions:
	- Foundation type (G&S) standards			-	enforced from: 1st Aug 2025
	- Level-7 standards							-	enforced from: 1st Jan 2026
	- GSO / ApprenticeshipUnits / ShortCourses	-	enforced from: 28 April 2026	<-- cannot be added via csv upload route

@regression
@e2escenarios
Scenario: AP_AC_02_Validate Age-Checks for the restricted Courses_FileUploadJourney
	Given Provider have few apprentices to add using CSV file upload
	And one of the apprentice on Level-7 course is above 25 years  
	And one of the apprentice on Foundation course is above 25 years 
	And one of the apprentice on Short GSO course
	When Provider uploads the csv file
	Then system does not allow to upload the file and displays an error message