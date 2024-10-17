Feature: Enviar y recibir mensajes en RabbitMQ

@rabbitmq
 Scenario: Verificar que el mensaje Json se envía correctamente
    Given un contenedor RabbitMQ está en funcionamiento
    When envío un mensaje "{"type": "simple", "content": "Hello, World"}" a la cola
    Then el mensaje "{"type": "simple", "content": "Hello, World"}" debería ser recibido correctamente

@rabbitmq
Scenario: Verificar que el mensaje Json + objeto se envía correctamente
    Given un contenedor RabbitMQ está en funcionamiento
    When envío un mensaje "{"type": "complex", "content": {"text": "Hello", "number": 123}}" a la cola
    Then el mensaje "{"type": "complex", "content": {"text": "Hello", "number": 123}}" debería ser recibido correctamente

@rabbitmq
Scenario: Verificar que el mensaje Json + Array se envía correctamente
    Given un contenedor RabbitMQ está en funcionamiento
    When envío un mensaje "{"type": "list", "content": ["item1", "item2", "item3"]}" a la cola
    Then el mensaje "{"type": "list", "content": ["item1", "item2", "item3"]}" debería ser recibido correctamente