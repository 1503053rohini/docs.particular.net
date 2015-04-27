using System;
using System.Web;
using NServiceBus;

namespace WebApplication
{
    public class Global : HttpApplication
    {
        public static IBus Bus;

        protected void Application_Start(object sender, EventArgs e)
        {
            #region ApplicationStart

            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Samples.AsyncPages.WebApplication");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            Bus = NServiceBus.Bus.Create(busConfiguration).Start();

            #endregion
        }

    }
}