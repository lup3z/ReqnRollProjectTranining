using DotPulsar.Abstractions;
using DotPulsar;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotPulsar.Extensions;

namespace SpecFlowProjectAPI.Support.Pulsar
{
    public class ConsumerAndProducer
    {
        
        private PulsarContainerManager _contConfig = new PulsarContainerManager();
        private IPulsarClient _pulsarClient;
        private IProducer<ReadOnlySequence<byte>> _producer;
        private IConsumer<ReadOnlySequence<byte>> _consumer;
        private readonly string _topic = $"persistent://public/default/mytopic";


        public async Task StartPulsarCon()
        {
            await _contConfig.StartContainerAsync();
        }

        public async Task CreateProdCons()
        {

            Console.WriteLine(_contConfig.GetPulsarBrokerUrl());
            _pulsarClient = PulsarClient.Builder()
                    .ServiceUrl(new Uri(_contConfig.GetPulsarBrokerUrl()))
                    .Build();

            // Crear productor y consumidor 
            _producer = _pulsarClient.NewProducer()
                                     .Topic(_topic)
                                     .Create();

            _consumer = _pulsarClient.NewConsumer()
                                     .Topic(_topic)                     // El tema del cual consumir mensajes
                                     .SubscriptionName("my-subscription")   // La suscripción a usar
                                     .SubscriptionType(SubscriptionType.Exclusive) // El tipo de suscripción (en este caso, exclusiva)
                                     .Create();

            await Task.Delay(5000);


        }

        public async Task SendMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var messageSequence = new ReadOnlySequence<byte>(messageBytes);

            await _producer.Send(messageSequence);

        }

        public async Task<string> ReceiveMessage()
        {
            var message = await _consumer.Receive();
            var messageBytes = message.Data.ToArray();
            var receivedMessage = Encoding.UTF8.GetString(messageBytes);

            await _consumer.Acknowledge(message);
            return receivedMessage;

        }

        public void CloseAsyncConsumer()
        {
            _consumer.DisposeAsync();
        }

        public void CloseAsyncProducer()
        {
            _producer.DisposeAsync();
        }

        public void CloseAsyncPulsarContainerManager()
        {
            _contConfig.StopContainerAsync();
        }

    }

}
