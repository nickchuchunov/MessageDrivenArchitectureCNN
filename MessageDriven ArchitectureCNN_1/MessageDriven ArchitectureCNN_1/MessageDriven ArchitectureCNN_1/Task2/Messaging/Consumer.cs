using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using MessageDriven_ArchitectureCNN_1.Task1;
using RabbitMQ.Client.Events;


namespace MessageDriven_ArchitectureCNN_1.Task2
{
    internal class Consumer
    {
        delegate EventHandler<BasicDeliverEventArgs> MessageHandler(Restaurant sender, BasicDeliverEventArgs e);
        private string _queveName { get; set; } = "KeyRestaurant";
        private string _hostName { get; set; }  = "localhost";
        private IConnection _connection { get; set; }
       private IModel  _channel { get; set; }
        public string Messeg { get; set; }

        public Consumer(string queveName, string hostName)
        {
            _queveName = queveName; 
            _hostName = hostName;    
         var factory = new ConnectionFactory(); // создаем подключение.
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.HostName = "localhost";
            factory.Port = 5672;
            factory.ClientProvidedName = "app: restaurant component: event-consumer";
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
           
            
            _channel.BasicPublish(exchange: "direct_excenge", routingKey: "KeyRestaurant");
        }

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallback)
        {

            var consumer = new EventingBasicConsumer(_channel); // создаем consumer для канала
            consumer.Received += receiveCallback; // добавляем обработчик события приема сообщения
            _channel.BasicConsume(_queveName, true, consumer);
            

        }

    }
}
