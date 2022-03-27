using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MessageDriven_ArchitectureCNN_1
{
    internal class OrderSubmittedEventConsumer : IConsumer<ITableBoked>
    {
        
       
       
        

        public  async Task Consume(ConsumeContext<ITableBoked> context)
        {


            await Task.Run(() => { return context; });
        }
    }
}