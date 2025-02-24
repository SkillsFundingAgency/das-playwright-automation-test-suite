Feature: Verify the login for DfE user

     @dfe_aodp
     Scenario Outline: Verify the AODP login for DfE <role>

          Given a user with <role> role dfe should able to login
          Then access the dfe Dashboard as as an <role>

          Examples:
               | role     |
               | dfe_user |