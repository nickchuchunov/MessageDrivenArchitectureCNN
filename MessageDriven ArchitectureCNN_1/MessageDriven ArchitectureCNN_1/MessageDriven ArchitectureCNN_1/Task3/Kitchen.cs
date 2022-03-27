using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using System.Collections.Concurrent;
namespace MessageDriven_ArchitectureCNN_1
{
    internal class Kitchen : IConsumer<ITableBoked>
    {

       internal NotificationsMassTransit _notifier = new NotificationsMassTransit();
     

      

       public async Task Consume(ConsumeContext<ITableBoked> context)
        {

            TableBoked tableBoked = new TableBoked();
            tableBoked.OrderId = context.Message.OrderId;
            tableBoked.ClientId = context.Message.ClientId;
            tableBoked.StateId = context.Message.StateId;
            tableBoked.KitchenStatus = KitchenStatus(_notifier, context.Message.StateId).Current;

            await Task.Run(() => { _notifier.Accept(context.Message.OrderId, tableBoked); });
           
      
        }

        internal IEnumerator<bool>  KitchenStatus(NotificationsMassTransit notifier, int StateId)
        {
           int Count = 0;
            foreach (var Notifier in notifier.State) 
            {
                if (Notifier.Value.StateId == StateId) { Count++; }


            }
            if (Count >10){yield return false;}
            
        }


    }
}
