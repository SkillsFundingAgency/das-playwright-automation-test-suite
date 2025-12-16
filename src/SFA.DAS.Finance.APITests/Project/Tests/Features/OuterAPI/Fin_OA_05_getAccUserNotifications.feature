
Feature: Fin_OA_05_getAccUserNotifications

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_05 getAccUserNotifications

    Given send an api request GET /Accounts/{{accountId}}/users/which-receive-notifications

    Then Verify the getUserNotifications api response with records fetch from DB
        | query |
        | GetUsersWhichReceiveNotifications.sql |
