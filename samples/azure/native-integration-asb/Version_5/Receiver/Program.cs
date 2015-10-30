﻿using System;
using System.IO;
using NServiceBus;
using NServiceBus.Azure.Transports.WindowsAzureServiceBus;

class Program
{
    private static void Main()
    {
        var busConfiguration = new BusConfiguration();

        #region EndpointAndSingleQueue

        busConfiguration.EndpointName("Samples.ASB.NativeIntegration");
        busConfiguration.ScaleOut()
            .UseSingleBrokerQueue();

        #endregion

        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UseTransport<AzureServiceBusTransport>()
            .ConnectionString(Environment.GetEnvironmentVariable("AzureServiceBus.ConnectionString"));

        #region BrokeredMessageConvention

        BrokeredMessageBodyConversion.ExtractBody = brokeredMessage =>
        {
            using (var stream = new MemoryStream())
            using (var body = brokeredMessage.GetBody<Stream>())
            {
                body.CopyTo(stream);
                return stream.ToArray();
            }
        };

        #endregion

        using (Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
