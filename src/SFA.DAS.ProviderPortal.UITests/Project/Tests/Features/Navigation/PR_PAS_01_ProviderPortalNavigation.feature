Feature: PR_PAS_01_ProviderPortalNavigation

@provider
@regression
@pasproviderrole
Scenario: PR_PAS_01a_ProviderPortalNavigation - Viewer
	Given the provider logs in as a Viewer
	Then user can access Notification Settings page
	And user can access Orgs And Agreements page
	And user can access Help page
	And user can access Feedback page
	And user can access Privacy Statement page
	And user can access Cookies page
	And user can access Terms Of Use page
	And user can signout from their account


@provider
@regression
@pasproviderrole
Scenario: PR_PAS_01b_ProviderPortalNavigation - Contributor
	Given the provider logs in as a Contributor
	Then user can access Notification Settings page
	And user can access Orgs And Agreements page
	And user can access Help page
	And user can access Feedback page
	And user can access Privacy Statement page
	And user can access Cookies page
	And user can access Terms Of Use page
	And user can signout from their account

@provider
@regression
@pasproviderrole
Scenario: PR_PAS_01c_ProviderPortalNavigation - ContributorWithApproval
	Given the provider logs in as a ContributorWithApproval
	Then user can access Notification Settings page
	And user can access Orgs And Agreements page
	And user can access Help page
	And user can access Feedback page
	And user can access Privacy Statement page
	And user can access Cookies page
	And user can access Terms Of Use page
	And user can signout from their account

@provider
@regression
@pasproviderrole
Scenario: PR_PAS_01d_ProviderPortalNavigation - AccountOwner
	Given the provider logs in as a AccountOwner
	Then user can access Notification Settings page
	And user can access Orgs And Agreements page
	And user can access Help page
	And user can access Feedback page
	And user can access Privacy Statement page
	And user can access Cookies page
	And user can access Terms Of Use page
	And user can signout from their account
