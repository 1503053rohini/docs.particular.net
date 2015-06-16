﻿using System;
using NServiceBus;
using NServiceBus.Installation.Environments;

class Program
{
    static void Main()
    {
        Configure.Serialization.Json();
        #region ConfigureRabbit
        Configure configure = Configure.With();
        configure.Log4Net();
        configure.DefineEndpointName("Samples.RabbitMQ.Simple");
        configure.DefaultBuilder();
        configure.UseTransport<NServiceBus.RabbitMQ>(() => "host=localhost");
        #endregion
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        using (IStartableBus startableBus = configure.UnicastBus().CreateBus())
        {
            IBus bus = startableBus.Start(() => configure.ForInstallationOn<Windows>().Install());
            bus.SendLocal(new MyMessage());

            Console.WriteLine("\r\nPress any key to stop program\r\n");
            Console.ReadKey();
        }

    }

}