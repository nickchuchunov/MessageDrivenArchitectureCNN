using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using RabbitMQ.Client.Events;
using MassTransit;

namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal class Booking : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        static  internal ConcurrentDictionary<Guid, ITableBoked> State { get; set; }
        private readonly IBus bus;
       
        

        public Booking()
        {
            State = new ConcurrentDictionary<Guid, ITableBoked>();
            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });

                cfg.ReceiveEndpoint("Kitchen", e => { e.Consumer<Kitchen>(); });
                cfg.UseMessageScheduler(new Uri("RestaurantBookingSagа"));


            });
           
    }
   
      public  async void RegistrationKitchen() 
        {
        TableBoked tableBoked = new TableBoked();
            tableBoked.OrderId = Guid.NewGuid();
            tableBoked.ClientId = Guid.NewGuid();
            CorrelationId = tableBoked.OrderId;
            
          await Task.Run(() => { bus.Publish(tableBoked);  });

        }


    
    }
}
