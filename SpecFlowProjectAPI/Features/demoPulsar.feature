Feature: Enviar y recibir mensajes en Pulsar

@pulsar
 Scenario: Verificar que el mensaje Json se envía correctamente
    Given un contenedor Pulsar está en funcionamiento
    When envío un mensaje al producer "{"type": "simple", "content": "Hello, World"}" a la cola
    Then el mensaje recibido por el consumer "{"type": "simple", "content": "Hello, World"}" debería ser recibido correctamente

@pulsar
Scenario: Verificar que el mensaje Json + objeto se envía correctamente
    Given un contenedor Pulsar está en funcionamiento
    When envío un mensaje al producer "{"type": "complex", "content": {"text": "Hello", "number": 123}}" a la cola
    Then el mensaje recibido por el consumer "{"type": "complex", "content": {"text": "Hello", "number": 123}}" debería ser recibido correctamente

@pulsar
Scenario: Verificar que el mensaje Json + Array se envía correctamente
    Given un contenedor Pulsar está en funcionamiento
    When envío un mensaje al producer "{"type": "list", "content": ["item1", "item2", "item3"]}" a la cola
    Then el mensaje recibido por el consumer "{"type": "list", "content": ["item1", "item2", "item3"]}" debería ser recibido correctamente