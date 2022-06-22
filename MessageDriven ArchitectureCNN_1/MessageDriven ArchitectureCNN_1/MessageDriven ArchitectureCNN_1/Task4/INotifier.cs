using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal interface INotifier
    {

        public Guid OrderId { get; set; }
        internal void Notify(Guid orderId);



    }
}
