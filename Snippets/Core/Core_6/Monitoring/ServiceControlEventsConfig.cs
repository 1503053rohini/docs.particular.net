﻿namespace Core6.Monitoring
{
    using NServiceBus;

    class ServiceControlEventsConfig
    {
        ServiceControlEventsConfig(EndpointConfiguration endpointConfiguration)
        {
            #region ServiceControlEventsConfig

            // required to talk to ServiceControl
            endpointConfiguration.UseSerialization<JsonSerializer>();
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningEventsAs(type =>
            {
                return typeof(IEvent).IsAssignableFrom(type) ||
                       // include ServiceControl events
                       type.Namespace != null &&
                       type.Namespace.StartsWith("ServiceControl.Contracts");
            });

            #endregion
        }
    }
}