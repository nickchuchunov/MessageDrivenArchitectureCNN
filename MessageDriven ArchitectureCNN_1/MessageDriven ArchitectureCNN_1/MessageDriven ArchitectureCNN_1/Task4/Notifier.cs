using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal class Notifier : IConsumer<ITableBoked>
    {
        private readonly IBus bus;

        List<TableBoked> tableBokedList = new List<TableBoked>();


       internal Notifier()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });

                cfg.ReceiveEndpoint("Restaurant", e => { e.Consumer<Restaraunt>(); });
                cfg.UseMessageScheduler(new Uri("RestaurantBookingSagа"));
            });
       
        }



        internal void Notify(Guid orderId) 
        {

            bool SearhList(TableBoked table) // функция поиска в _tables
            {
                return table.OrderId == orderId;
            }

            Predicate<TableBoked> searhList = SearhList; // создание делегата необходимого для метода FindIndex

            int indexTableBokedList = tableBokedList.FindIndex(searhList);

            TableBoked tableBoked = tableBokedList[indexTableBokedList];


            if (tableBoked.StateId != 0) { Console.WriteLine($"Столки {tableBoked.StateId} для клиента {tableBoked.ClientId} с заказам {tableBoked.OrderId}  забронирован   "); }
            else { Console.WriteLine($"Столки {tableBoked.StateId} для клиента {tableBoked.ClientId} не удалось забронировать   "); }


        }




        public async Task Consume(ConsumeContext<ITableBoked> context)
        {

            TableBoked tableBoked = new TableBoked();
            tableBoked.OrderId = context.Message.OrderId;
            tableBoked.ClientId = context.Message.ClientId;
            tableBoked.StateId = context.Message.StateId;
            tableBoked.KitchenStatus = context.Message.KitchenStatus;
            tableBoked.State = context.Message.State;
            tableBoked.TableIndex = context.Message.TableIndex;
            tableBoked.StatesCount = context.Message.StatesCount;

            tableBokedList.Add(tableBoked);


        }







    }

}
