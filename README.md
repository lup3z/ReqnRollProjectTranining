
[![CI - Status de pruebas](https://github.com/lup3z/SpecFlowProjectAPI/actions/workflows/dotnet-desktop.yml/badge.svg?branch=master)](https://github.com/lup3z/SpecFlowProjectAPI/actions/workflows/dotnet-desktop.yml)

## Proyect C# y Reqnroll

### Assignment Task 1

#### Pre-requisites:
- Install moq (`dotnet add package Moq`)
- Install JSON Reader (`dotnet add package Newtonsoft.Json`)

#### Tasks:
- Go to [API Docs](https://restful-booker.herokuapp.com/apidoc/index.html)
- Create a test script that uses the Post request: `Auth - CreateToken` endpoint and validate the response
- Create a test script that uses the Get request: `Booking - GetBookingIds` endpoint and validate the response body
- Create a test script that uses Moq to mock the response (a fixed response different from the real one)
- Execute the tests
- Push the changes to the GitHub Repo

---

### Assignment Task 2

#### Pre-requisites:
- Install Test Container (`dotnet add package TestContainers`)
- Install MySql (`dotnet add package MySql.Data`)

#### Tasks:
- Go to [Sauce Demo](https://www.saucedemo.com/)
- Create a DB and store the credentials mentioned in Sauce Demo based on the following feature:

  ```gherkin
  Scenario: Database Container
    Given I have a running MySQL Container
    Then the database should be accessible
    ```
- Include this test in the HTML test report
- Push the changes to the GitHub Repo

---

### Assignment Task 3

#### Pre-requisites:
- Install RabbitMQ Client (`dotnet add package RabbitMQ.Client`)
- Install RabbitMQ for TestContainers (`dotnet add package Testcontainers.RabbitMq`)

#### Tasks:
- Using TestContainers and RabbitMQ, create a producer and a consumer given the following scenarios:
  - **Produce and consume a simple message**:
    ```json
    {"type": "simple", "content": "Hello, World"}
    ```
    Validate the message is received and has the same structure and data.
  
  - **Produce and consume a message with multiple fields**:
    ```json
    {"type": "complex", "content": {"text": "Hello", "number": 123}}
    ```
    Validate the structure and values of the received message.

  - **Produce and consume a message with a list**:
    ```json
    {"type": "list", "content": ["item1", "item2", "item3"]}
    ```
    Validate that the message received contains the expected elements.

- Develop the test (features) according to the validation needed.
- Create a new tag for this test as `@rabbitmq_test`.
- Include this test in the HTML report.
- Modify the GitHub Actions pipeline to execute this tag on a daily schedule.
- Push the changes to the repo.
- Execute and share the report by email.

---

### Assignment Task 4

#### Pre-requisites:
- Install Pulsar Client (`dotnet add package DotPulsar`)
- Install Pulsar for TestContainers (`dotnet add package Testcontainers.Pulsar`)

#### Tasks:
- Using TestContainers and Pulsar, create a producer and a consumer given the following scenarios:
  - **Produce and consume a simple message**:
    ```json
    {"type": "simple", "content": "Hello, Pulsar World"}
    ```
    Validate the message is received and has the same structure and data.
  
  - **Produce and consume a message with multiple fields**:
    ```json
    {"type": "complex", "content": {"text": "Hello Pulsar", "number": 123}}
    ```
    Validate the structure and values of the received message.

  - **Produce and consume a message with a list**:
    ```json
    {"type": "list", "content": ["item1", "item2", "item3"]}
    ```
    Validate that the message received contains the expected elements.

- Develop the test (features) according to the validation needed.
- Create a new tag for this test as `@Pulsar_test`.
- Include this test in the HTML report.
- Modify the GitHub Actions pipeline to execute this tag on a daily schedule.
- Push the changes to the repo.
- Execute and share the report by email.

---

### Assignment Task 5

#### Pre-requisites:
- Install ReqNroll package (`dotnet add package ReqNroll`)

#### Tasks:
- Migrate the current tests from SpecFlow to ReqNroll (you can use the ReqNroll compatibility nugget or overwrite the current scripts).
- Design 2 new tests using ReqNroll and make them run in parallel:
  - **Test 1**: Using the following API: [Open Trivia DB](https://opentdb.com/api.php?amount=10&category=9&difficulty=easy&type=boolean)
    - Design and write the test cases for the following scenarios in Gherkin format:
      - Validate that `"correct_answer": "True"` exists.
      - Extract and print as evidence the values of the questions where `"correct_answer": "True"` appears.

  - **Test 2**: Using the following API: [Open Trivia DB](https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple)
    - Design and write the test cases for the following scenarios in Gherkin format:
      - Validate that all questions have 3 incorrect answers.
      - Extract and print as evidence the `"correct_answer"` for the question: `"Which car manufacturer won the 2017 24 Hours of Le Mans?"`.

---

