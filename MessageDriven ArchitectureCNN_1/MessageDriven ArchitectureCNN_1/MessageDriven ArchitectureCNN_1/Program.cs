
using System.Diagnostics;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MessageDriven_ArchitectureCNN_1.Task1;
using MessageDriven_ArchitectureCNN_1.Task2;

Console.OutputEncoding = System.Text.Encoding.UTF8;



Notifications notification = new Notifications();


notification.NotificationsConsole(); // отправка сообщений



