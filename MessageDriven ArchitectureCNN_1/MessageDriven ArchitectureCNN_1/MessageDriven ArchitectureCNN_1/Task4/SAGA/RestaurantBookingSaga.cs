using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using MassTransit;

namespace MessageDriven_ArchitectureCNN_1.Task4.SAGA
{
    internal class RestaurantBookingSaga: MassTransitStateMachine<RasaraurantBookingSagaInstans>

    {

        public State AwaitingBookingApproved { get; private set; }
        public Event<IBookingRequest> BookingRequested { get; private set; }
        public Event<ITableBoked> TableBooked { get; private set;  }
        public Event<IKitchenReady> KitchenReady { get; private set; }
        public Schedule<RasaraurantBookingSagaInstans, IBookingExpire> BookingExpired { get; private set; }
        //public Schedule<BookingRequested, IBookingExpire> BookingExpired { get; private set; }

        public Event BookingApproved { get; private set; }

        [Obsolete]
        RestaurantBookingSaga() 
        {
            InstanceState(x => x.CurrentState);
            Event(() => BookingRequested,x=> x.CorrelateById(context => context.Message.OrderId).SelectId(context => context.Message.OrderId));
            Event(() => TableBooked, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => KitchenReady, x => x.CorrelateById(context => context.Message.OrderId));
            CompositeEvent(() => BookingApproved, x => x.ReadyEventStatus, KitchenReady, TableBooked);
            Schedule(() => BookingExpired, x => x.ExpirationId, x => { x.Delay = TimeSpan.FromSeconds(5); x.Received = e => e.CorrelateById(context => context.Message.OrderId); });

            Initially
                (
                When(BookingRequested).Then(context => 
                
                {
                    context.Instance.CorrelationId = context.Data.OrderId;
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.ClientId = context.Data.ClientId;
                   //context.Instance.KitchenStatus = context.Data.KitchenStatus;
                   //context.Instance.State = context.Data.State;
                   //context.Instance.TableIndex = context.Data.TableIndex;
                   //context.Instance.StatesCount = context.Data.StatesCount;
                }).Schedule(BookingExpired, context=> new BookingExpire(context.Instance)).TransitionTo(AwaitingBookingApproved));


            During(AwaitingBookingApproved, When(BookingApproved).Unschedule(BookingExpired).Publish(context => new Notifier().Notify (context.Instance.OrderId)).Finalize(),
                
               When (BookingExpired.Received).Then(context=>Console.WriteLine($"Отмена заказа {context.Instance.OrderId}")).Finalize());











        }





    }
}
