using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using System.Collections.Concurrent;


namespace MessageDriven_ArchitectureCNN_1
{
    internal class NotificationsMassTransit : IConsumer<ITableBoked>
    {

      internal   ConcurrentDictionary<Guid, ITableBoked > State { get; set; }



     public   NotificationsMassTransit() 
        {
           
            State = new ConcurrentDictionary<Guid, ITableBoked>();

        }

`
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public int StateId { get; set; }
        public bool KitchenStatus { get; set; }

        /// <summary>
        /// публикуем полученное сообщение от booking
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public async Task Consume(ConsumeContext<ITableBoked> context)
        {
           await Task.Run(() => { Console.WriteLine($"Заказ от столика {context.Message.StateId}, с индификатором {context.Message.StateId} от клиенат {context.Message.ClientId} в  статусе {context.Message.KitchenStatus} "); });
            //return Task.CompletedTask;
        }
   


    }
}
