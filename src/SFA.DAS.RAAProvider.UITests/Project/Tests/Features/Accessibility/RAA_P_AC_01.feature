Feature: RAA_P_AC_01

@raa	
@raaprovider
@accessibility
Scenario: RAA_P_AC_01_1 - Create a vacancy navigating through all location types and wage types pages
When the Provider creates a vacancy with "all location types" work locations and "all wage types" wage type
Then the Provider can navigate to Manage your recruitment emails page
And the Provider views the recruitment API key

@raa	
@raaprovider
@accessibility
Scenario: RAA_P_AC_01_2 - Validation errors on create vacancy journey
Given the Provider sees validation errors in create vacancy journey