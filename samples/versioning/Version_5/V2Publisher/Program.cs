﻿using System;
using NServiceBus;
using NServiceBus.Persistence;
using NServiceBus.Persistence.Legacy;

class Program
{
    static void Main()
    {
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.Versioning.V2Publisher");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.UsePersistence<MsmqPersistence, StorageType.Subscriptions>();
        busConfiguration.EnableInstallers();

        using (IBus bus = Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press enter to publish a message");
            Console.WriteLine("Press any key to exit");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                bus.Publish<V2.Messages.ISomethingHappened>(sh =>
                {
                    sh.SomeData = 1;
                    sh.MoreInfo = "It's a secret.";
                });

                Console.WriteLine("Published event.");
            }
        }
     
    }
}