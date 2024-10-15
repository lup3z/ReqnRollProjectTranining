Feature: responseValidation

A short summary of the feature

@tag1
  Scenario: Validar que todas las preguntas tienen 3 respuestas incorrectas y extraer la respuesta correcta
    Given el usuario envía una solicitud GET a "https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple"
    Then todas las preguntas deberían tener exactamente 3 respuestas incorrectas
    And extraer y mostrar la "correct_answer" para la pregunta "Which car manufacturer won the 2017 24 Hours of Le Mans?"

