using Newtonsoft.Json;
using NUnit.Framework;
using RabbitMQ.Client;
using SpecFlowProjectAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.RabbitMq;

namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    public class RabbitMqSteps
    {
        private RabbitMqContainer _rabbitMqContainer;
        private IConnection _connection;
        private IModel _channel;
        private RabbitMqProducer _producer;
        private RabbitMqConsumer _consumer;

        [Given(@"un contenedor RabbitMQ está en funcionamiento")]
        public void GivenUnContenedorRabbitMqEstaEnFuncionamiento()
        {
            // Iniciar un contenedor RabbitMQ utilizando TestContainers
            _rabbitMqContainer = new RabbitMqBuilder()
                .WithImage("rabbitmq:3.13-management")
                .WithPortBinding(5672, 5672)  // Mapeo del puerto RabbitMQ
                .WithUsername("testuser")
                .WithPassword("testpassword")
                .Build();

            // Iniciar el contenedor RabbitMQ
            _rabbitMqContainer.StartAsync().Wait();

            // Conectar a RabbitMQ en localhost, usando el puerto predeterminado 5672
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",  // Se conecta a RabbitMQ en el host
                Port = 5672, // Puerto predeterminado de RabbitMQ
                UserName = "testuser",
                Password = "testpassword"
            };

            // Crear la conexión y el canal de comunicación
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declarar la cola
            _channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Inicializar el productor y el consumidor
            _producer = new RabbitMqProducer(_channel);
            _consumer = new RabbitMqConsumer(_channel);

        }

        [When(@"envío un mensaje ""(.*)"" a la cola")]
        public void WhenEnvioUnMensajeALaCola(string message)
        {
            _producer.SendMessage("hello", message);
        }

        [Then(@"el mensaje ""(.*)"" debería ser recibido correctamente")]
        public void ThenElMensajeDeberiaSerRecibidoCorrectamente(string expectedMessage)
        {
            var receivedMessage = _consumer.ConsumeMessage("hello");

            var receivedMessageObject = JsonConvert.DeserializeObject<SimpleMessage>(receivedMessage);
            var receivedMessageObjectFromTest = JsonConvert.DeserializeObject<SimpleMessage>(expectedMessage);

            // Verificar que el mensaje recibido es el esperado
            Assert.AreEqual(expectedMessage, receivedMessage);

            if (receivedMessageObject.Type == "complex")
            {
                // Deserializar el contenido como un objeto de tipo ComplexContent
                var complexContent = JsonConvert.DeserializeObject<ComplexContent>(receivedMessageObject.Content.ToString());
                var complexContentTest = JsonConvert.DeserializeObject<ComplexContent>(receivedMessageObjectFromTest.Content.ToString());

                Assert.AreEqual(complexContent.Text, complexContentTest.Text, "El número de elementos en 'Content' no es el esperado.");

            }
            else if (receivedMessageObject.Type == "list")
            {
                // Deserializar el contenido como una lista de strings
                var listContent = JsonConvert.DeserializeObject<List<string>>(receivedMessageObject.Content.ToString());

                // Deserializar el expectedMessage.Content como una lista de strings
                var expectedListContent = JsonConvert.DeserializeObject<List<string>>(receivedMessageObjectFromTest.Content.ToString());

                // Verificar que ambas listas tengan el mismo número de elementos
                Assert.AreEqual(expectedListContent.Count, listContent.Count, "El número de elementos en 'Content' no es el esperado.");

                // Comparar cada elemento de la lista
                for (int i = 0; i < listContent.Count; i++)
                {
                    Assert.AreEqual(expectedListContent[i], listContent[i], $"El elemento en la posición {i} no es el esperado.");
                }
            }
            else if (receivedMessageObject.Content is string stringContent)
            {
                // Manejar el caso en que el contenido es un string
                Assert.AreEqual("simple", receivedMessageObject.Type, "El valor de 'type' no es el esperado.");
                Assert.AreEqual("Hello, World", receivedMessageObject.Content, "El valor de 'content' no es el esperado.");

            }
        }

        [AfterScenario]
        public void TearDown()
        {
            // Cerrar las conexiones y detener el contenedor
            _channel?.Close();
            _connection?.Close();
            _rabbitMqContainer?.StopAsync().Wait();
        }
    }

}
