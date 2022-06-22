using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ;
using RabbitMQ.Client.Events;
using MessageDriven_ArchitectureCNN_1.Task2;
using MessageDriven_ArchitectureCNN_1.Task1;

namespace MessageDriven_ArchitectureCNN_1.Task4
{
   public class worker
    {
       internal Booking booking;



   internal  worker()
        {
           
            work();
        }

         

      public  static void work() 
     {
            Booking booking = new Booking();
            while (true)
        {
                Console.WriteLine("Нужно разместить заказ ? сообщите вашн омер столика");
                 int.TryParse(Console.ReadLine(), out int  StateId);
                booking.RegistrationKitchen(StateId);
                Console.WriteLine("Ожидайте регистрации заказа, или повторите попытку");
                Console.ReadLine();
                continue;
        }
    
    
     }

    }
}
