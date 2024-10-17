Feature: Validar la respuesta correcta en una API pública

  Scenario: Validar y extraer preguntas con "correct_answer" igual a "True"
    Given el usuario envía una solicitud GET a "https://opentdb.com/api.php?amount=10&category=9&difficulty=easy&type=boolean"
    Then la respuesta debería contener al menos una "correct_answer" con valor "True"
    And mostrar las preguntas donde "correct_answer" es "True"

  Scenario: Validar que todas las preguntas tienen 3 respuestas incorrectas y extraer la respuesta correctaa
    Given el usuario envía una solicitud GET a "https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple"
    Then todas las preguntas deberían tener exactamente 3 respuestas incorrectas
    And extraer y mostrar la "correct_answer" para la pregunta "Which car manufacturer won the 2017 24 Hours of Le Mans?"