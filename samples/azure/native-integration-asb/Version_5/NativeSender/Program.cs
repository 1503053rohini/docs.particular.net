﻿using System;
using System.IO;
using System.Text;
using Microsoft.ServiceBus.Messaging;

namespace NativeSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus.ConnectionString");
            var queueClient = QueueClient.CreateFromConnectionString(connectionString, "Samples.ASB.NativeIntegration");

            #region SerializedMessage

            var nativeMessage = @"{""Content"":""Hello from native sender"",""SendOnUtc"":""2015-10-27T20:47:27.4682716Z""}";

            #endregion

            var nativeMessageAsStream = new MemoryStream(Encoding.UTF8.GetBytes(nativeMessage));

            var message = new BrokeredMessage(nativeMessageAsStream)
            {
                MessageId = Guid.NewGuid().ToString()
            };

            #region NecessaryHeaders

            message.Properties["NServiceBus.EnclosedMessageTypes"] = "Shared.NativeMessage";
            message.Properties["NServiceBus.MessageIntent"] = "Send";

            #endregion

            queueClient.Send(message);

            Console.WriteLine("Native message sent");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
