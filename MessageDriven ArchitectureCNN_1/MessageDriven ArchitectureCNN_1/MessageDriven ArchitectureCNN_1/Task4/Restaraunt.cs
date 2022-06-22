using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;


namespace MessageDriven_ArchitectureCNN_1.Task4
{
    internal class Restaraunt : IConsumer<ITableBoked>
    {

        
        private readonly IBus bus;
        private TableBoked tableBoked { get; set; }

        private static List<TableBoked> _tables = new List<TableBoked>();

        public Restaraunt()
        {

            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });
                cfg.UseMessageScheduler(new Uri("RestaurantBookingSagа"));
                cfg.ReceiveEndpoint("RestaurantBookingSagа", e => { e.Consumer<RestaurantBookingSagа>(); });

            });



            for (int i = 0; i <= 10; i++)
            {
                _tables.Add(new TableBoked());
            }
        }




        public Task Consume(ConsumeContext<ITableBoked> context)
        {
            tableBoked = new TableBoked();

            tableBoked.OrderId = context.Message.OrderId;
            tableBoked.ClientId = context.Message.ClientId;
            tableBoked.StateId = context.Message.StateId;
            tableBoked.KitchenStatus = context.Message.KitchenStatus;
            tableBoked.State = context.Message.State;
            tableBoked.TableIndex = context.Message.TableIndex;
            tableBoked.StatesCount = context.Message.StatesCount;


            return Task.CompletedTask;


        }


        /// <summary>
        /// Бронирование столика
        /// </summary>
        /// <param name="countOfPersons"></param>

        public void BookFreeTable(int countOfPersons)
        {
            bool SearhList(TableBoked table) // функция поиска в _tables
            {
                return table.State == State.Free && table.StatesCount >= countOfPersons;
            }

            Predicate<TableBoked> searhList = SearhList; // создание делегата необходимого для метода FindIndex

            // Console.WriteLine("Добрый день! Подождите секунду я подберу  столик  и подтвержу вашу бронь, оставайтесь на линии");
            int table = _tables.FindIndex(searhList);



            _tables[table].State = State.Booked;

            System.Timers.Timer timer = new System.Timers.Timer(1800);
            timer.Elapsed += async (sender, e) => await Task.Run(() => { _tables[table].State = State.Free; });  //Автоматическое снятие брони

        }





        /// <summary>
        /// Разблокировка стола - Асинхронно
        /// </summary>
        /// <param name="id"></param>

        public void RemovingReservationAsync(int TableIndex)
        {
            Console.WriteLine($"Вы уходите ! Хорошего дня !"); ;

            bool SearhList(TableBoked table) // функция поиска в _tables
            {
                return table.TableIndex == TableIndex;
            }
            Predicate<TableBoked> searhList = SearhList; // создание делегата необходимого для метода FindIndex

            Task.Run(async () =>
            {
                int table = _tables.FindIndex(searhList);
                await Task.Delay(1000 * 5);
                _tables[table].State = State.Free;


            });


        }

    }
}

