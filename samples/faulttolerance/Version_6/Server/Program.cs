using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{

    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.FaultTolerance.Server");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.SendFailedMessagesTo("error");

        // To disable second level retries(SLR), uncomment the following line. SLR is enabled by default.
        // busConfiguration.DisableFeature<SecondLevelRetries>();

        using (IBus bus = await Bus.Create(busConfiguration).StartAsync())
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
