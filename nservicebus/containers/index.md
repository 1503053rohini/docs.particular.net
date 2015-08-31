---
title: Containers
summary: NServiceBus automatically registers components, user-implemented handlers, and sagas.
tags: 
- Dependency Injection
- IOC
redirects:
- nservicebus/containers
related:
- samples/containers
---

NServiceBus relies heavily on Containers and Dependency Injection to manage services and state.

NServiceBus automatically registers all its components as well as user-implemented handlers and sagas so that all instancing modes and wiring are done correctly by default and without errors.

NServiceBus has a built-in container (currently an ILMerged version of Autofac) but it can be replaced by any other container.


## Supported Containers

Support for other containers is provided via custom integrations.

- [Autofac](autofac.md)
- [Ninject](ninject.md)
- [CastleWindsor](castlewindsor.md)
- [StructureMap](structuremap.md)
- [Spring](spring.md)
- [Unity](unity.md)


## Using an existing container

The above pages all have examples of how to pass in an instance of an existing container. This is useful when you want to make use of the full features of your container and share the DI behavior with NServiceBus.


### IBus resolution

Note that the instance of `IBus` is scoped for the lifetime of the container. Hence if you resolve `IBus` and then dispose of it the endpoint will stop processing messages.


## Plugging in your own container

If you have your own container that is not already supported by a NuGet package, you can create a plugin centering around the `IContainer` abstraction. Once this is created and registered, NServiceBus will use your custom container to look up its own dependencies.

<!-- import CustomContainers -->
