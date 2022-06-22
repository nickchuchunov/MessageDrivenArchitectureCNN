using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using MassTransit;


namespace MessageDriven_ArchitectureCNN_1.Task4
{
    public interface ITableBoked //: SagaStateMachineInstance
    {
        //public  SagaStateMachineInstance CorrelationId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public int StateId { get; set; }
        public bool KitchenStatus { get; set; }
        public State State { get; set; }
        public int TableIndex { get; set; }
        public int StatesCount { get; set; }
    }
}
