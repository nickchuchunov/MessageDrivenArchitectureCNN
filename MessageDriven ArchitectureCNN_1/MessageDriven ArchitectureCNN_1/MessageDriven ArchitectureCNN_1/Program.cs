using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.Configuration;
using MassTransit.Extensions.Hosting.RabbitMq;
using MassTransit.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MessageDriven_ArchitectureCNN_1.Task1;
using MessageDriven_ArchitectureCNN_1.Task3;
using MessageDriven_ArchitectureCNN_1;


   Console.OutputEncoding = System.Text.Encoding.UTF8;


    CreateHostBuild(args).Build().StartAsync();
     worker worker = new worker();



static   IHostBuilder CreateHostBuild(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices((services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<Kitchen>(); x.AddConsumer<Booking>(); x.AddConsumer<NotificationsMassTransit>(); x.UsingRabbitMq((context, ctg) => { ctg.ConfigureEndpoints(context); ctg.UseDelayedMessageScheduler(); });

    });



    services.AddOptions<MassTransitHostOptions>().Configure(options => { options.WaitUntilStarted = true; });
    services.AddOptions<IBusControl>().Configure(options => { options.ConnectConsumer<Kitchen>(); });

    services.AddSingleton<NotificationsMassTransit>();
    services.AddSingleton<Kitchen>();
    services.AddSingleton<worker>();
});