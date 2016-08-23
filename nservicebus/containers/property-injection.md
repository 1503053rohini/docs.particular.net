---
title: Property injection
reviewed: 2016-08-23
component: Core
tags:
 - Dependency Injection
redirects:
 - nservicebus/property-injection-in-handlers
---

NServiceBus will automatically enable property injection known types across the supported [Containers](/nservicebus/containers). Use the `Func` overload of `.ConfigureComponent` to get full control over the injected properties if needed.

A common use case it to set primitive properties on message handlers. Given then below handler:

snippet: PropertyInjectionWithHandler

Setting the properties is done as follows:

snippet: ConfigurePropertyInjectionForHandler


partial: handler
