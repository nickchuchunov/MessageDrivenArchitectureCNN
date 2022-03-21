using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace MessageDriven_ArchitectureCNN_1.Task2
{
    internal class Producer
    {

     private   ConnectionFactory factory { get; set; }
        private string _queveName { get; set; } = "KeyRestaurant";
        private string _hostName { get; set; } = "localhost";

        internal Producer(string queveName, string hostName) 
        {
            _queveName = queveName;
            _hostName = hostName;
            ConnectionFactory _factory = new ConnectionFactory();
            factory = _factory;
        }


        public void Send(string message)
        {
            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.HostName = "localhost";
            factory.Port = 5672;
           
            factory.ClientProvidedName = "app: restaurant component: event-producer";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                IBasicProperties props = channel.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.DeliveryMode = 2;
                props.Expiration = "3600";

                channel.ExchangeDeclare(exchange: "direct_excenge", type: ExchangeType.Direct, false, false, null);
                channel.QueueDeclare(_queveName, false, false, false, null);
                channel.QueueBind(_queveName, exchange: "direct_excenge", routingKey: _queveName, null);
                byte[] body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "direct_excenge", routingKey: _queveName, basicProperties: props, body: body); // отправляем сообщегние



            }

        }

    }
}
