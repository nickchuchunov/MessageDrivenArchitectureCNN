using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDriven_ArchitectureCNN_1
{
   public class TableBoked : ITableBoked
    {
       

        public TableBoked() { }

        public Guid OrderId { get ; set ; }
        public Guid ClientId { get ; set ; }
        public int StateId { get ; set; }
        public bool KitchenStatus { get ; set ; }
    }
}
