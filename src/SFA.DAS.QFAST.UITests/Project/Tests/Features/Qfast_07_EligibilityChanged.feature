Feature: Qfast_07

@regression
@qfast
Scenario: Eligibility changed - MVS1 - New Qualifications
     Given the admin user log in to the portal
     When I select the Create a form option
	 When I publish the form for eligibility update scenario
	 And I Sign out from the portal

    # AO user creates a funding application
	 Given the ao user2 user log in to the portal	
	 When I submit the application for eligibility update
     And I Sign out from the portal

    # Approving the applicaiton does not change the status of the MVS1 qulifications
     Given the admin user log in to the portal
     When I select the Review applications for funding option
     Then I select the RAD Level 2 Award in Solo Performance in Dance: Grade 5 application
     And I set the funding stream and approve the application	 
    
    # Dfe user checks the MVS1 qualifications status has not changed
     Then I click on Dashboard link
     When I select the Review newly regulated qualifications option
     Then I validate QAN 60146527 has status as Decision Required