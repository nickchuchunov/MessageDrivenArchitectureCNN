using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using RabbitMQ.Client.Events;
using MassTransit;

namespace MessageDriven_ArchitectureCNN_1.Task3
{
    internal class Booking: IConsumer<ITableBoked>
    {

       static  internal ConcurrentDictionary<Guid, ITableBoked> State { get; set; }
        private readonly IBus bus;


      public Booking()
        {
            State = new ConcurrentDictionary<Guid, ITableBoked>();
            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });

                cfg.ReceiveEndpoint("Booking", e => { e.Consumer<Kitchen>(); e.Consumer<NotificationsMassTransit>(); });

            });

        }

  



        /// <summary>
        /// Отпровляем сообщения от столика на кухню
        /// </summary>
        /// <param name="StateId"> номер столика</param>

      public  async void RegistrationKitchen(int StateId) 
        {

            TableBoked tableBoked = new TableBoked();
            tableBoked.OrderId = Guid.NewGuid();
            tableBoked.ClientId = Guid.NewGuid();
            tableBoked.StateId = StateId;

         await Task.Run(() => { bus.Publish(tableBoked); });

        }


        /// <summary>
        /// получаем сообщение из кухни и добавляем заказ в лист State 
        /// и отправляем сообщение в NitificationMassTransit
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public  Task Consume(ConsumeContext<ITableBoked> context)
        {
        TableBoked tableBoked = new TableBoked();
        tableBoked.OrderId = context.Message.OrderId;
        tableBoked.ClientId = context.Message.ClientId;
        tableBoked.StateId = context.Message.StateId;
        tableBoked.KitchenStatus = context.Message.KitchenStatus;
            if (tableBoked.KitchenStatus) { State[tableBoked.OrderId] = tableBoked; bus.Publish(tableBoked); }
                
            return Task.CompletedTask;

    }
    }
}
