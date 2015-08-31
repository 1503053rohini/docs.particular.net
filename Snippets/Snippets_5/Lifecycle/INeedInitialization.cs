﻿namespace Snippets5.Lifecycle
{
    using NServiceBus;

    #region lifecycle-ineedinitialization

    class NeedsInitialization : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            // Perform initialization
            // This is after type scanning. Do not call the following here: 
            // * configuration.AssembliesToScan();
            // * configuration.ScanAssembliesInDirectory();
            // * configuration.TypesToScan();
        }
    }

    #endregion
}
