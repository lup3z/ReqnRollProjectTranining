using Newtonsoft.Json;
using NUnit.Framework;
using RabbitMQ.Client;
using SpecFlowProjectAPI.Support;
using SpecFlowProjectAPI.Support.RabbitMQ;
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
        private RabbitMqContainerManager _rabbitMqManager;
        private RabbitMqProducer _producer;
        private RabbitMqConsumer _consumer;

        [Given(@"un contenedor RabbitMQ está en funcionamiento")]
        public void GivenUnContenedorRabbitMqEstaEnFuncionamiento()
        {
            // Inicializar y arrancar el contenedor RabbitMQ
            _rabbitMqManager = new RabbitMqContainerManager();
            _rabbitMqManager.StartContainer();

            // Inicializar el productor y el consumidor utilizando el canal proporcionado por RabbitMqManager
            var channel = _rabbitMqManager.GetChannel();

            // Inicializar el productor y el consumidor
            _producer = new RabbitMqProducer(channel);
            _consumer = new RabbitMqConsumer(channel);

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
            // Detener el contenedor y cerrar conexiones
            _rabbitMqManager.StopContainer();
        }
    }

}
