using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectAPI.Support.RabbitMQ
{
    public class RabbitMqConsumer
    {
        private readonly IModel _channel;
        private string _receivedMessage;

        public RabbitMqConsumer(IModel channel)
        {
            _channel = channel;
        }

        public string ConsumeMessage(string queueName)
        {
            // Configurar el consumidor para recibir el mensaje
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                _receivedMessage = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received {_receivedMessage}");
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            Thread.Sleep(1000); // Espera para recibir el mensaje
            return _receivedMessage;

        }
    }
}
