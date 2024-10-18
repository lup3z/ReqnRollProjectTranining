[![CI - Status de pruebas](https://github.com/lup3z/SpecFlowProjectAPI/actions/workflows/dotnet-desktop.yml/badge.svg?branch=master)](https://github.com/lup3z/SpecFlowProjectAPI/actions/workflows/dotnet-desktop.yml)

Proyect C# y Reqnroll

Assignment Task 1
Pre-requisites
    - Install moq (dotnet add package Moq)
    - Install JSON Reader (dotnet add package Newtonsoft.Json)
Tasks
    - Go to https://restful-booker.herokuapp.com/apidoc/index.html
    - Create a test script that uses the Post request: Auth - CreateToken endpoint and validate the response
    - Create a test script that uses the Get request to Booking - GetBookingIds endpoint and validate the response body
    - Create a test script that uses moq to mock the response (a fixed response different from the real one)
    - Execute the tests
    - Push the changes to the GitHub Repo

Assignment Task 2
Pre-Requisites
    - Install Test Container (dotnet add package TestContainers)
    - Install MySql (dotnet add package MySql.data)
Tasks
    - Go to https://www.saucedemo.com/
    - Create a DB and store the credentials mentioned in https://www.saucedemo.com/ based on the following feature
        Scenario: Database Container
        Given I have a running MySQL Container
        Then the database should be accessible
    - Include this test in the HTML test report
    - Push the changes to the GitHub Repo

Assignment Task 3
Pre-Requisites
    - Install RabbitMQ Cliente (dotnet add package RabbitMQ.Client)
    - Install RabbitMQ for TestContainer ( dotnet add package Testcontainers.RabbitMq)
Tasks
    - Using TestContainers and RabbitMQ create a producer and a consumer given the following scenarios:
    - Produce and consume a simple message
    - Message '{"type: "simple", "content": "Hello, World" }'
    - Validate the message is received and with the same structure and data
    - Produce and consume a message with multiple fields
    - Message '{"type": "complex", "content": {"text": "Hello" "number": 123}}
    - Validate the structure and the values of the received message
    - Produce and consume a message with a list
    - Message '{"type": "list", "content": [ "item1", "item2", "item3"]}
    - Validate that the message received contains the expected elements
    - Develop the test (features) according to the validation needed 
    - Create a new tag for this test as @rabbitmq_test
    - Include this test in the HTML report
    - Modify the GitHub Actions pipeline to execute this tag in a schedule of once a day
    - Push the changes to the repo
    - Execute and share the report by email

Assignment Task 4
Pre-Requisites
    - Install Pulsar Cliente (dotnet add package Dotpulsar)
    - Install Pulsar for TestContainer ( dotnet add package Testcontainers.Pulsar)
Tasks
    - Using TestContainers and Pulsar create a producer and a consumer given the following scenarios:
    - Produce and consume a simple message
    - Message '{"type: "simple", "content": "Hello, Pulsar World" }'
    - Validate the message is received and with the same structure and data
    - Produce and consume a message with multiple fields
    - Message '{"type": "complex", "content": {"text": "Hello Pulsar" "number": 123}}
    - Validate the structure and the values of the received message
    - Produce and consume a message with a list
    - Message '{"type": "list", "content": [ "item1", "item2", "item3"]}
    - Validate that the message received contains the expected elements
    - Develop the test (features) according to the validation needed 
    - Create a new tag for this test as @Pulsar_test
    - Include this test in the HTML report
    - Modify the GitHub Actions pipeline to execute this tag in a schedule of once a day
    - Push the changes to the repo
    - Execute and share the report by email

Assignment Task 5
Pre-Requisites
    - Install ReqNroll package (dotnet add package ReqNroll)
Tasks
    - Migrate the current test from Spectflow to ReqNroll (you could use the ReqNroll compatibility nugget or overwrite the current scripts)
    - Design 2 new tests using ReqNroll and make them run in parallel
        - Using the following API https://opentdb.com/api.php?amount=10&category=9&difficulty=easy&type=boolean
            - Design and write the test cases for the following scenarios in Gherkin format
                - Validate that "correct_answer": "True" exists
                - Extract and print as evidence the values of the questions where "correct_answer": "True"
    - Using the following Request https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple
        - Design and write the test cases for the following scenarios in Ghernkins format
            - Validate that all questions have 3 incorrect answers
            - Extract and print as evidence the "correct_answer": for  "question": "Which car manufacturer won the 2017 24 Hours of Le Mans?
