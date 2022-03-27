using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MessageDriven_ArchitectureCNN_1
{
    internal class NotificationsMassTransit 
    {

      internal   ConcurrentDictionary<Guid, ITableBoked > State { get; set; }



      internal   NotificationsMassTransit() 
        {
           
            State = new ConcurrentDictionary<Guid, ITableBoked>();

        }


        public void Accept(Guid orderId, ITableBoked tableBoked)
        {
            State[orderId]= tableBoked;


        }

        internal void Notifi(Guid orderId)
        {
            var booking = State[orderId];
            if (booking.KitchenStatus == true)
            {
                Console.WriteLine($"Заказ {booking.OrderId} за столиком {booking.StateId}  Успешно забранирован для клиента {booking.ClientId}");

            }
            else { Console.WriteLine($"Мы не можем выполнить заказ {booking.OrderId} для клиента {booking.ClientId} "); }

        }


    }
}
