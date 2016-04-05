---
title: Using MEF or Reflection to customize NServiceBus
summary: Using MEF or Reflection to add custom configuration, startup, and shutdown logic to NServiceBus.
reviewed: 2016-04-05
related:
- nservicebus/lifecycle
- nservicebus/pipeline
---


## Shared

The Shared project defines all the messages used in the sample.

It also contains the custom extension point definitions that both the endpoints and the implementations use to communicate. For example the extension point for running code after the endpoint has started is:

snippet: IRunAfterEndpointStart

The complete list of extension points in this solution are

 * `ICustomizeConfiguration`
 * `IRunBeforeEndpointStart`
 * `IRunAfterEndpointStart`
 * `IRunBeforeEndpointStop`
 * `IRunAfterEndpointStop`


## Approaches to executing extension points

Both approaches have a similar parts.

 * An endpoint project that starts the endpoint and loads + executes the specific extension points in the Shared project.
 * An extensions project that contains the implementations for the extension points in the Shared project.


## Custom Reflection

This approach uses directory scanning and Reflection to discover and execute the extension points.


### Endpoint Startup

snippet: CustomStartup


### Helpers

Some common scanning and reflection helpers.

snippet: Resolver


### Example extension implementations

snippet: CustomCustomizeConfiguration

snippet: CustomRunAfterEndpointStart

snippet: CustomSendMessageAfterEndpointStart


## [Managed Extensibility Framework (MEF)](https://msdn.microsoft.com/en-us/library/dd460648.aspx)

This approach uses [MEF](https://msdn.microsoft.com/en-us/library/dd460648.aspx) to discover and execute the extension points.


### Endpoint Startup

snippet: MefStartup


### Helpers

Some common MEF helpers.

snippet: MefExtensions


### Example extension implementations

snippet: MefCustomizeConfiguration

snippet: MefRunAfterEndpointStart

snippet: MefSendMessageAfterEndpointStart


## Other notes


### Using an [IOC container](/nservicebus/containers/)

Both the above approaches could be made more extensible and versatile by leveraging a [container](/nservicebus/containers/). In the case of MEF most containers have custom integrations, for example [Autofac.Mef](http://docs.autofac.org/en/latest/integration/mef.html). For the custom reflection approach the standard reflection based APIs of a given container would be used.


### Adding more extensions points

More extension points could be defined based on requirements. The extensions could be plugged into part of the NServiceBus [lifecycle](/nservicebus/lifecycle/) or the [pipeline](/nservicebus/pipeline/). Also more context could be passed, as parameters, to any give extension point.