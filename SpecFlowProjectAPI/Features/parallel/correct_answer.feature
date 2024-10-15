
Feature: Validar la respuesta correcta en una API pública
@tag1
  Scenario: Validar y extraer preguntas con "correct_answer" igual a "True"
    Given el usuario envía una solicitud GET a "https://opentdb.com/api.php?amount=10&category=9&difficulty=easy&type=boolean"
    Then la respuesta debería contener al menos una "correct_answer" con valor "True"
    And mostrar las preguntas donde "correct_answer" es "True"
