---
title: Castle Windsor
summary: Details on how to Configure NServiceBus to use Castle Windsor for dependency injection. Includes usage examples as well as lifecycle mappings.
component: Castle
reviewed: 2018-10-29
tags:
 - Dependency Injection
related:
 - samples/dependency-injection/castle
redirects:
 - nservicebus/containers/castle
---


NServiceBus can be configured to use [Castle Windsor](https://github.com/castleproject/Windsor) for dependency injection.


### Default Usage

snippet: CastleWindsor


### Existing Instance

snippet: CastleWindsor_Existing


### DependencyLifecycle Mapping

The [DependencyLifecycle](/nservicebus/dependency-injection/#dependency-lifecycle) map to [Castle LifestyleType](https://github.com/castleproject/Windsor/blob/master/docs/lifestyles.md) in the following way.


| DependencyLifecycle                                                                                             | Castle LifestyleType                                                                           |
|-----------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------|
| [InstancePerCall](/nservicebus/dependency-injection/#dependency-lifecycle-instance-per-call) | [Transient](https://github.com/castleproject/Windsor/blob/master/docs/lifestyles.md#transient) |
| [InstancePerUnitOfWork](/nservicebus/dependency-injection/#dependency-lifecycle-instance-per-unit-of-work)                    | [Scoped](https://github.com/castleproject/Windsor/blob/master/docs/lifestyles.md#scoped)       |
| [SingleInstance](/nservicebus/dependency-injection/#dependency-lifecycle-single-instance)                                  | [Singleton](https://github.com/castleproject/Windsor/blob/master/docs/lifestyles.md#singleton) |
