using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.Pulsar;

namespace SpecFlowProjectAPI.Support.Pulsar
{
    public class PulsarContainerManager
    {
        private PulsarContainer _pulsarContainer;

        [SetUp]
        public async Task StartContainerAsync()
        {
            _pulsarContainer = new PulsarBuilder()
                .WithImage("apachepulsar/pulsar:latest")
                .WithCleanUp(true)
                .WithExposedPort(6650)  // Aseguramos que el puerto 6650 está expuesto
            .Build();

            await _pulsarContainer.StartAsync();
        }

        public async Task StopContainerAsync()
        {
            await _pulsarContainer.StopAsync();
        }

        public string GetPulsarBrokerUrl()
        {
            // Usar la propiedad ExposedPorts para obtener el puerto mapeado
            var mappedPort = _pulsarContainer.GetMappedPublicPort(6650);
            return $"pulsar://localhost:{mappedPort}";
        }

        //[TearDown]
        //public async Task TearDown()
        //{
        //    await _pulsarContainer.StopAsync();
        //}

    }
}
