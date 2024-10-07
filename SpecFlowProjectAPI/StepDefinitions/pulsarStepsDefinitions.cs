using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecFlowProjectAPI.Support.Pulsar;
using SpecFlowProjectAPI.Support;

namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    public class pulsarStepsDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        ConsumerAndProducer _PulsarContA = new ConsumerAndProducer();
        private PulsarContainerManager _containerManager;

        // Inyectar AfterHooks a través del constructor
        public pulsarStepsDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"un contenedor Pulsar está en funcionamiento")]
        public async Task GivenUnContenedorRabbitMqEstaEnFuncionamiento()
        {
            await _PulsarContA.StartPulsarCon();
            await _PulsarContA.CreateProdCons();

        }

        [When(@"envío un mensaje al producer ""(.*)"" a la cola")]
        public async Task WhenEnvioUnMensajeALaCola(string message)
        {
            await _PulsarContA.SendMessage(message);
        }

        [Then(@"el mensaje recibido por el consumer ""(.*)"" debería ser recibido correctamente")]
        public async Task ThenElMensajeDeberiaSerRecibidoCorrectamente(string expectedMessage)
        {
            var receivedMessage = await _PulsarContA.ReceiveMessage();

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

            Assert.AreEqual(expectedMessage, receivedMessage);
        }

        
        [AfterScenario]
        public async Task FinalizarContenedores()
        {
            if (_scenarioContext.ScenarioInfo.Tags.Contains("pulsar"))
            {
                _PulsarContA?.CloseAsyncConsumer();
                _PulsarContA?.CloseAsyncProducer();
                _PulsarContA?.CloseAsyncPulsarContainerManager();
            }
        }
    }

}
