Feature: MockUserManagement

A short summary of the feature

@tag1
  Scenario: User is retrieved successfully
    Given the user service returns a user with ID 1
    When I request the user with ID 1
    Then I should receive the user details with ID 1
