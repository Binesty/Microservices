namespace Microservices.Analysis.Settings
{
    public class Secret
    {
        public Content? Data { get; set; }

        public record Content(string RabbiMQHostName,
                              string RabbitMQPassword,
                              int RabbitMQPort,
                              string RabbitMQQueueName,
                              string RabbitMQUserName);
    }
}