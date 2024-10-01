using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectAPI.Support
{
    public class RabbitMqProducer
    {
        private readonly IModel _channel;

        public RabbitMqProducer(IModel channel)
        {
            _channel = channel;
        }

        public void SendMessage(string queueName, string message)
        {
            // Enviar el mensaje a la cola
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            Console.WriteLine($"[x] Sent {message}");

        }
    }
}
