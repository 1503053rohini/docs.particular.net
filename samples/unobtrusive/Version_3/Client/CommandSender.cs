using System;
using Commands;
using Messages;
using NServiceBus;

public class CommandSender
{

    public static void Start(IBus bus)
    {
        Console.WriteLine("Press 'C' to send a command");
        Console.WriteLine("Press 'R' to send a request");
        Console.WriteLine("Press 'E' to send a message that is marked as Express");
        Console.WriteLine("Press 'D' to send a large message that is marked to be sent using Data Bus");
        Console.WriteLine("Press 'X' to send a message that is marked with expiration time.");
        Console.WriteLine("Press any key to exit");


        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.C:
                    SendCommand(bus);
                    continue;
                case ConsoleKey.R:
                    SendRequest(bus);
                    continue;
                case ConsoleKey.E:
                    Express(bus);
                    continue;
                case ConsoleKey.D:
                    Data(bus);
                    continue;
                case ConsoleKey.X:
                    Expiration(bus);
                    continue;
            }
            return;

        }
    }


    // Shut down server before sending this message, after 30 seconds, the message will be moved to Transactional dead-letter messages queue.
    static void Expiration(IBus bus)
    {
        MessageThatExpires messageThatExpires = new MessageThatExpires
        {
            RequestId = new Guid()
        };
        bus.Send("Samples.Unobtrusive.Server", messageThatExpires);
        Console.WriteLine("message with expiration was sent");
    }

    static void Data(IBus bus)
    {
        Guid requestId = Guid.NewGuid();

        LargeMessage largeMessage = new LargeMessage
        {
            RequestId = requestId,
            LargeDataBus = new byte[1024*1024*5]
        };
        bus.Send("Samples.Unobtrusive.Server", largeMessage);

        Console.WriteLine("Request sent id: " + requestId);
    }

    static void Express(IBus bus)
    {
        Guid requestId = Guid.NewGuid();

        RequestExpress requestExpress = new RequestExpress
        {
            RequestId = requestId
        };
        bus.Send("Samples.Unobtrusive.Server", requestExpress);

        Console.WriteLine("Request sent id: " + requestId);
    }

    static void SendRequest(IBus bus)
    {
        Guid requestId = Guid.NewGuid();

        Request request = new Request
        {
            RequestId = requestId
        };
        bus.Send("Samples.Unobtrusive.Server", request);

        Console.WriteLine("Request sent id: " + requestId);
    }

    static void SendCommand(IBus bus)
    {
        Guid commandId = Guid.NewGuid();

        MyCommand command = new MyCommand 
        {
            CommandId = commandId,
            EncryptedString = "Some sensitive information"
        };
        bus.Send("Samples.Unobtrusive.Server", command)
            .Register<CommandStatus>(outcome => Console.WriteLine("Server returned status: " + outcome));

        Console.WriteLine("Command sent id: " + commandId);

    }


}