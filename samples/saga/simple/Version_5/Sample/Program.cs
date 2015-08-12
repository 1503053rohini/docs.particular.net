﻿using System;
using NServiceBus;

class Program
{

    static void Main()
    {
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.ComplexSagaFindingLogic");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();

        using (IBus bus = Bus.Create(busConfiguration).Start())
        {
            bus.SendLocal(new StartOrder
                          {
                              OrderId = "123"
                          });
            bus.SendLocal(new StartOrder
                          {
                              OrderId = "456"
                          });
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
