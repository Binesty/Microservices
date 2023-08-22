using Microservices.Analysis.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Microservices.Analysis.Services
{
    public class RabbitMQServices
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IBasicProperties _properties;
        public readonly IModel Channel;
        private readonly string _queueName;

        public RabbitMQServices()
        {
            if (SecretsExtensions.Loaded is null ||
                SecretsExtensions.Loaded.Data is null)
                throw new Exception("Not found secrets");

            _queueName = SecretsExtensions.Loaded.Data.RabbitMQQueueName;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = SecretsExtensions.Loaded.Data.RabbiMQHostName,
                Port = SecretsExtensions.Loaded.Data.RabbitMQPort,
                UserName = SecretsExtensions.Loaded.Data.RabbitMQUserName,
                Password = SecretsExtensions.Loaded.Data.RabbitMQPassword
            };

            Channel = _connectionFactory.CreateConnection()
                                        .CreateModel();

            Channel.QueueDeclareNoWait(_queueName, true, false, false, null);
            _properties = Channel.CreateBasicProperties();
            _properties.Persistent = true;
        }

        public void StartGetMessages(ILogger<Worker> logger)
        {
            string message = string.Empty;

            Channel.BasicQos(prefetchSize: 0, prefetchCount: 10000, global: false);

            var eventingConsumer = new EventingBasicConsumer(Channel);
            eventingConsumer.Received += (model, content) =>
            {
                message = Encoding.UTF8.GetString(content.Body.ToArray());

                logger.LogInformation($"Message received: {message} at: {DateTimeOffset.Now}");

                Channel.BasicAck(deliveryTag: content.DeliveryTag, multiple: false);
                //Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            };

            Channel.BasicConsume(queue: _queueName, autoAck: false, consumer: eventingConsumer);
        }
    }
}
