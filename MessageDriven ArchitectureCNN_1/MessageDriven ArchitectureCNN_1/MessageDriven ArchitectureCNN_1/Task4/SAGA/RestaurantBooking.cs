using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal class BookingRequested: SagaStateMachineInstance, IConsumer<TableBoked>
    {
       
        public Guid CorrelationId { get; set; }
        private readonly IBus bus;


        BookingRequested()
        {

            
            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });

                //cfg.ReceiveEndpoint("RestaurantBookingSagа", e => { e.Consumer<RestaurantBookingSagа>(); });
                cfg.UseMessageScheduler(new Uri("RestaurantBookingSagа"));


            });


        }

        public Task Consume(ConsumeContext<TableBoked> context)
        {
            TableBoked tableBoked = new TableBoked();
            tableBoked.OrderId = context.Message.OrderId;
            tableBoked.ClientId = context.Message.ClientId;
            tableBoked.StateId = context.Message.StateId;
            tableBoked.KitchenStatus = context.Message.KitchenStatus;
            tableBoked.State = context.Message.State;
            tableBoked.TableIndex = context.Message.TableIndex;
            tableBoked.StatesCount = context.Message.StatesCount;
            CorrelationId = context.Message.OrderId;

            return Task.Run(() => { bus.Publish(tableBoked); });

        }








    }
}
