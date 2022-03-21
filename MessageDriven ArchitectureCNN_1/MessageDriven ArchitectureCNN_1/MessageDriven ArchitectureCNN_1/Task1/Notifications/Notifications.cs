using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MessageDriven_ArchitectureCNN_1.Task2;

namespace MessageDriven_ArchitectureCNN_1.Task1
{

    internal class Notifications
    {


        Restaurant rest { get; set; }

        Consumer cons { get; set; }

        EventHandler<BasicDeliverEventArgs> receiveCallback;

        internal Notifications()
        {
            rest = new Restaurant();
            cons = new Consumer("KeyRestaurant", "localhost");
            DelegateFunction delegateFunction = new DelegateFunction();
            receiveCallback = delegateFunction.MessageHandlerFunction;
        }



    public void NotificationsConsole() //Restaurant rest, Consumer cons, EventHandler<BasicDeliverEventArgs> receiveCallback
        {



            while (true)
            {
                Console.WriteLine(
               "Привет! Желаете забронировать столик?\n1 " +
               "- мы уведомим вас по смс (асинхронно)" +
               "\n2 - подождите на линии , мы Вас оповестим (синхронно)" +
               "\n3 - Вы уходите? Вам нужно сообщить номер столика для снятия брони (Синхронно) " +
               "\n4 - Вы уходите? Вам нужно сообщить номер столика для снятия брони (Асинхронно)"
               );

                var choice = Console.ReadLine();

                if (int.TryParse(choice, out int dialogue) && dialogue <= 4 && dialogue >= 1) 
                {
                Console.WriteLine("Ваше сообщение принято в обработку");    
                }        

                else
                {
                    Console.WriteLine("Введите, пожалуйста 1 -4 "); // защита от невалидного ввода
                    continue;

                }

                var stopWatch = new Stopwatch();
                stopWatch.Start();
                switch (dialogue)
                {
                    case 1:
                        {
                            Console.WriteLine("Подскажите количество человек от 1 до 5");
                            int numberPeopl = int.Parse(Console.ReadLine());
                            rest.BookFreeTableAsync(numberPeopl);
                            Task.Run(() => { cons.Receive(receiveCallback); });  
                            
                        };
                        break;

                    case 2:
                        {
                            Console.WriteLine("Подскажите количество человек от 1 до 5");
                            int numberPeopl = int.Parse(Console.ReadLine());
                            rest.BookFreeTable(numberPeopl);
                            Task.Run(() => { cons.Receive(receiveCallback);  });
                        };
                        break;

                    case 3:
                        {
                            Console.WriteLine("Подскажите номер вашего  столика");
                            int tableNumber = int.Parse(Console.ReadLine());
                            rest.RemovingReservation(tableNumber);
                            Task.Run(() => {  cons.Receive(receiveCallback);  });
                        };
                        break;
                        
                    case 4:
                        {
                            Console.WriteLine("Подскажите номер вашего  столика");
                            int tableNumber = int.Parse(Console.ReadLine());
                            rest.RemovingReservationAsync(tableNumber);
                            Task.Run(() => { cons.Receive(receiveCallback); });
                        };
                        break; 

                }

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds:80}: {ts.Milliseconds:00}"); //выведем потраченное нами время 
                continue;
            }


        }


    }
}
