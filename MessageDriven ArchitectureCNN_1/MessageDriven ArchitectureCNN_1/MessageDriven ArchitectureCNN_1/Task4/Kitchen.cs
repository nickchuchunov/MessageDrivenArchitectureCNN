using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using System.Collections.Concurrent;


namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal class Kitchen : IConsumer<ITableBoked>
    {
        private readonly IBus bus;
       
      public   Kitchen() 
        {

            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });

                cfg.ReceiveEndpoint("Restaurant", e => { e.Consumer<Restaraunt>(); });
                cfg.UseMessageScheduler(new Uri("RestaurantBookingSagа"));
            });


        }


        /// <summary>
        /// принимаем ссобщение из кухни и если номер столика четный
        /// то мы не можем принять заказ  в связи с тем что этот столик обслуживает другая кухня
        /// присваеваем KitchenStatus false или true и отправляем в booking 
        /// </summary>
        /// <param name="context"> ссобщение от кухни</param>
        /// <returns></returns>

        public async Task Consume(ConsumeContext<ITableBoked> context)
        {
            TableBoked tableBoked = new TableBoked();
            tableBoked.OrderId = context.Message.OrderId;
            tableBoked.ClientId = context.Message.ClientId;
            tableBoked.StateId = context.Message.StateId;
            if (context.Message.StateId % 2 == 0)
            {
                tableBoked.KitchenStatus = true;

            }
            else { tableBoked.KitchenStatus = false; };

            await Task.Run(() => { bus.Publish(tableBoked); });   
      
        }

    }
}
