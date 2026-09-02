Feature: RAA_Transferred_01

@raa
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_1 - Transfer a Live vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option set as Yes
	When the Provider submits a vacancy to the DfE for review
	And the Reviewer Approves the vacancy
	When the Applicant can apply for a Vacancy in FAA
	When the Employer revokes permission to the provider to create advert
	Then the transferred advert is saved as a closed

@raa
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_2 - Transfer a closed vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option set as Yes
	When the Provider submits a vacancy to the DfE for review
	And the Reviewer Approves the vacancy
	Then the Provider can close the vacancy
	When the Employer revokes permission to the provider to create advert
	Then the transferred advert is saved as a closed

@raa
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_3 - Transfer a rejected vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option set as Yes
	When the Provider submits a vacancy to the DfE for review
	And the Reviewer Refer the vacancy
	And the Employer revokes permission to the provider to create advert
	Then the transferred advert is saved as a rejected

@raa
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_4 - Transfer a pending DfE review vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option set as Yes
	When the Provider submits a vacancy to the DfE for review
	And the Employer revokes permission to the provider to create advert
	Then the advert is saved as a draft

@raa
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_5 - Transfer an archived vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option set as Yes
	When the Provider submits a vacancy to the DfE for review
	And the Reviewer Approves the vacancy
	Then the Provider can close the vacancy
	And the Provider can archive the vacancy
	When the Employer revokes permission to the provider to create advert
	Then the transferred advert is saved as a archived

@raa
@raa-epc
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_6 - Transfer a pending employer review vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option
	When the Provider submits a vacancy to the employer for review
	And the Employer revokes permission to the provider to create advert
	Then the advert is saved as a draft

@raa
@raa-epc
@raatransfer
@regression
@raaprovider
@raaemployer
Scenario: RAA_Transferred_01_7 - Transfer a draft vacancy from provider to employer
	Given the Employer grants permission to the provider to create advert with review option
	When Provider cancels after saving the title of the advert
	And the Employer revokes permission to the provider to create advert
	Then the advert is saved as a draft