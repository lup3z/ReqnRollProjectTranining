using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.RabbitMq;

namespace SpecFlowProjectAPI.Support.RabbitMQ
{
    public class RabbitMqContainerManager
    {
        private RabbitMqContainer _rabbitMqContainer;
        private IConnection _connection;
        private IModel _channel;

        public void StartContainer()
        {
            // Iniciar el contenedor RabbitMQ utilizando TestContainers
            _rabbitMqContainer = new RabbitMqBuilder()
                .WithImage("rabbitmq:3.13-management")
                .WithPortBinding(5672, 5672)
                .WithUsername("testuser")
                .WithPassword("testpassword")
                .Build();

            // Iniciar el contenedor RabbitMQ
            _rabbitMqContainer.StartAsync().Wait();

            // Conectar a RabbitMQ en localhost, usando el puerto predeterminado 5672
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "testuser",
                Password = "testpassword"
            };

            // Crear la conexión y el canal de comunicación
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declarar la cola
            _channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public IModel GetChannel() => _channel;

        public void StopContainer()
        {
            _channel?.Close();
            _connection?.Close();
            _rabbitMqContainer?.StopAsync().Wait();
        }
    }

}
