Feature: testContainerExample

A short summary of the feature

@tag1
Scenario: Retrieve two customers from the service
    Given the PostgreSQL container is running
    And I have created two customers
    When I retrieve the customers
    Then there should be 2 customers