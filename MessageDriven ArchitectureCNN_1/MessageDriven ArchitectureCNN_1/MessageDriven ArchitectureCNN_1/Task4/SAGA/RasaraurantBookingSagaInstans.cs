using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MessageDriven_ArchitectureCNN_1.Task4
{
    public class RasaraurantBookingSagaInstans : SagaStateMachineInstance
    {
        public Guid CorrelationId { get ; set ; } // индификатор для соотнесения всех сообщений  друг с другом

        public int CurrentState { get; set; } // текущее состояние саги

        public Guid OrderId { get; set; } // индификатор заказа

        public Guid ClientId { get; set; }// индификатор клиента
        
        public int ReadyEventStatus { get; set; } // пометка о том, что наша заявка просрочена.

        public Guid? ExpirationId { get; set; } 


    }
}
