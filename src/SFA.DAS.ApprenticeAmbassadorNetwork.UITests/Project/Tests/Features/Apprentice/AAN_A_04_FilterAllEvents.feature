﻿Feature: AAN_A_04_FilterAllEvents

@aan
@aanaprenticeevents
@aanaprentice
@regression
Scenario: AAN_A_04_Apprentice filter all events
    Given an onboarded apprentice logs into the AAN portal
    Then the user should be able to successfully filter events by date
    And the user should be able to successfully filter events by event format
    And the user should be able to successfully filter events by event type
    And the user should be able to successfully filter events by multiple combination of filters

@aan
@aanaprenticeevents
@aanaprentice
@aanapprentice04b
@regression
@ignore
Scenario: AAN_A_04b_Apprentice user filters events by location
    Given the following events have been created:
    | Event Title                             | Location                                                                    |
    | Location Filter Apprentice Test Event 1 | The Maids Head, King's Lynn, PE32 1NG                                       |
    | Location Filter Apprentice Test Event 2 | Eagles Golf Club, 37-39 School Road, King's Lynn, PE34 4RS                  |
    | Location Filter Apprentice Test Event 3 | Spalding United Football Club, Sir Halley Stewart Field, Spalding, PE11 1DA |
    When an onboarded apprentice logs into the AAN portal
    And the user filters events within 10 miles of "PE30 5HF"
    Then the following events can be found within the search results:
    | Event Title                             |
    | Location Filter Apprentice Test Event 1 |
    | Location Filter Apprentice Test Event 2 |
    And the following events can not be found within the search results:
    | Event Title                             |
    | Location Filter Apprentice Test Event 3 |
    When the user filters events Across England centered on "PE30 5HF"
    And the user orders the results by Closest
    Then the following events can be found within the search results in the given order:
    | Event Title                             | Order |
    | Location Filter Apprentice Test Event 1 | 1     |
    | Location Filter Apprentice Test Event 2 | 2     |
    | Location Filter Apprentice Test Event 3 | 3     |

@aan
@aanaprenticeevents
@aanaprentice
@aanapprentice04c
@regression
Scenario:AAN_A_04c apprentice user filters events by a location that does not exist
    Given an onboarded apprentice logs into the AAN portal
    When the user filters events within 10 miles of "Lilliput"
    Then the heading text "We cannot find the location you entered" is displayed
    And the text "We do not recognise Lilliput" is displayed

@aan
@aanaprenticeevents
@aanaprentice
@aanapprentice04d
@regression
Scenario:AAN_A_04d apprentice user filters events and finds no matching results
    Given an onboarded apprentice logs into the AAN portal
    When the user navigates to Network Events
    And the user filters events so that there are no results
    Then the heading text "No events currently match your filters" is displayed
