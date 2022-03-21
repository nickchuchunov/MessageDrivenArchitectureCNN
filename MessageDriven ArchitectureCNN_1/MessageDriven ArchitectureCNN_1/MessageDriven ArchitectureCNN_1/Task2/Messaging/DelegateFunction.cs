using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageDriven_ArchitectureCNN_1.Task2
{
    public class DelegateFunction
    {

        public DelegateFunction() { }


        public  void MessageHandlerFunction(object sender, BasicDeliverEventArgs e) //Restaurant sender,
        {

            Console.WriteLine(" Сообщение с заголовком {0}\n с текстлом {1} \n с тегом потребителя {2} \n с мкеткой доставки {3} \n ключем маршрутизации {4} \n  обмен сообщениями первоночально опубликован в {5}    ", e.BasicProperties, Encoding.UTF8.GetString(e.Body.ToArray()), e.ConsumerTag, e.DeliveryTag, e.RoutingKey, e.Exchange);
            string messeg = $"Сообщение с заголовком {e.BasicProperties}\n с текстлом {Encoding.UTF8.GetString(e.Body.ToArray())} \n с тегом потребителя {e.ConsumerTag} \n с мкеткой доставки {e.DeliveryTag} \n ключем маршрутизации {e.RoutingKey} \n  обмен сообщениями первоночально опубликован в {e.Exchange}  ";

        }

    }
}
