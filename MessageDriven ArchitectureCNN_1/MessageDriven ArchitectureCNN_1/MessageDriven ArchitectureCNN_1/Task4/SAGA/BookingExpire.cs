using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal class BookingExpire : RasaraurantBookingSagaInstans
    {

        internal Guid CorrelationId { get; set; } // индификатор для соотнесения всех сообщений  друг с другом

        internal int CurrentState { get; set; } // текущее состояние саги

        internal Guid OrderId { get; set; } // индификатор заказа

        internal Guid ClientId { get; set; }// индификатор клиента

        internal int ReadyEventStatus { get; set; } // пометка о том, что наша заявка просрочена.

        internal Guid? ExpirationId { get; set; }


      internal  BookingExpire(RasaraurantBookingSagaInstans rasaraurantBookingSagaInstans) 
        {
            CorrelationId = rasaraurantBookingSagaInstans.CorrelationId;
            CurrentState = rasaraurantBookingSagaInstans.CurrentState;  
            OrderId = rasaraurantBookingSagaInstans.OrderId;    
            ClientId = rasaraurantBookingSagaInstans.ClientId;
            ReadyEventStatus = rasaraurantBookingSagaInstans.ReadyEventStatus;
            ExpirationId = rasaraurantBookingSagaInstans.ExpirationId;

        }

    }
}
