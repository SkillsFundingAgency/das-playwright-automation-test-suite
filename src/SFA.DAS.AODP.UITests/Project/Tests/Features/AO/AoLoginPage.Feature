Feature: Verify the login for AO user

     @ao_aodp
     Scenario Outline: Verify the AODP login for AO <role>

          Given a user with <role> role ao should able to login
          Then access the ao Dashboard as <role>

          Examples:
               | role |
               | User |
