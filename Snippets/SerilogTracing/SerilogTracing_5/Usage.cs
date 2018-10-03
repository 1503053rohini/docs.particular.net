﻿using NServiceBus;
using NServiceBus.Serilog.Tracing;
using Serilog;

class Usage
{
    Usage()
    {
        #region SerilogTracingLogger

        var tracingLog = new LoggerConfiguration()
            .WriteTo.ColoredConsole()
            .MinimumLevel.Information()
            .CreateLogger();

        #endregion

        #region SerilogTracingPassLoggerToFeature

        var endpointConfiguration = new EndpointConfiguration("EndpointName");
        var serilogTracing = endpointConfiguration.EnableSerilogTracing(tracingLog);
        serilogTracing.EnableSagaTracing();

        #endregion
    }

    void Seq()
    {
        #region SerilogTracingSeq

        var tracingLog = new LoggerConfiguration()
            .WriteTo.Seq("http://localhost:5341")
            .MinimumLevel.Information()
            .CreateLogger();

        #endregion
    }
}