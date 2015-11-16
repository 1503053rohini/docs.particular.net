---
title: Message routing
summary: How NServiceBus routes messages between the endpoints
tags:
- routing
- message
- route
- send
- publish
- reply
---

One of the things NServiceBus takes care of is the routing of messages. Usually the only thing a developer has to do is call `Send`, `Publish` or `Reply` and the actual message destination is calculated by the framework.

## Endpoint and endpoint instances

Endpoint is a logical concept that relates to a program that uses NServiceBus to communicate with other similar programs. Each endpoint has a name e.g. `Sales` or `OrderProcessing`.

During deployment each endpoint might be materialized in form of one or many instances. Each instance is an identical copy of binaries resulting from building the endpoint program code base. Usually each endpoint instance is placed in separate directory or on separate machine. Each instance has its unique name that consists of the name of the endpoint (e.g. `Sales`) and up to two *discriminator* values:
 * User-provided discriminator is configured via `BusConfiguration` APIs
 * Transport-provided discriminator is wired by the transport if a particular transport supports it

Both discriminators are optional and if they are absent, the resulting instance name is based only on the endpoint name. This means that in the absence of discriminators, there can be only one instance of an endpoint because there is one possible instance name value. In order to scale out an endpoint and deploy multiple instances, either a user-provided or transport-provided discriminator has to be used.

## Send, Publish and Reply

NServiceBus offers three top-level API calls for sending messages, namely `Send`, `Publish` and `Reply`. Each of these calls can have different behavior in terms of message routing. 

## Types of routing

There are two types of routing recognized by NServiceBus, unicast routing and multicast routing.

### Unicast routing

Unicast routing uses queues which are inherently point-to-point channels. Each endpoint instance has an input queue. When sending a message to multiple destinations, a message is copied in-memory at sender endpoint and a copy of the message is dispatched to each destination's input queue.

### Multicast routing

Multicast routing uses topics. Multiple receivers can subscribe to a single topic and each message published to the topic is copied by the messaging infrastructure and delivered to each interested receiver.

### Transport

The transport selected by the user via

<!-- import Routing-ConfigureTransport -->

decides, for each of the API calls (`Send`, `Publish`, `Reply`), which routing type is used. For example, MSMQ transport uses the unicast routing for all the APIs while RabbitMQ transport uses the unicast routing for `Send` and `Reply` but multicast routing for `Publish`. 

## Unicast routing

Unicast routing in NServiceBus uses a lax layered model. The task of mapping a message type to a collection of transport addresses is split into three layers:
 * type mapping
 * endpoint mapping
 * instance mapping

### Type mapping layer

In an ideal world the topmost layer of unicast routing would decide, given a message type, which endpoints should receive the message. In practice such an ideal model is not possible, mostly due to wire-compatibility problems it would cause. A more practical but less strict approach is used where a number of user- and system- defined rules is applied to generate a collection of destinations for a given message type. Each rule operates independently and the result is a logical sum of results of individual rules. Destinations can be expressed either via endpoint name, endpoint instance name or directly via transport address. In last two cases the second and/or third mapping layer is skipped.

#### Static routes

Static routes in NServiceBus are similar to static routes in the IP routing protocols. The `UnicastRoutingTable` class provides a number of convenience methods that let you register a static route which is defined by a pair of `Type` and `DirectRoutingDestination`.

<!-- import Routing-StaticRoutes -->

#### Dynamic routes

Dynamic routes are meant to provide a convenient extensibility point that can be used both by users and NServiceBus add-ons. To add a dynamic route you need to pass a function that takes a `Type` and a `ContextBag` containing the context of current message processing and returns a collection of destinations. The function is called each time any message is being send so you need to take performance into account (e.g. use caching if the destinations are to be fetched from a database).

<!-- import Routing-DynamicRoutes -->

Following example shows how you could implement a shared-store based routing where destinations of messages are managed e.g. in a database.

<!-- import Routing-CustomRoutingStore -->

NOTE: The function passed to the `AddDynamic` call is executed **each time** a message is sent so it is essential to make sure it is performant (e.g. by caching the results if getting the result requires crossing a process boundary).

#### Send via

*This is implemented in a separate pull*

The type mapping layer allows to configure the itinerary a given message is suppose to be routed. By default, a message is dispatched to its destination calculated as a result of a routing process. If a *send via* is configured, a message is physically dispatched to the destination specified by *send via* and a `NServiceBus.UltimateDestination` header is attached. Should there be more intermediate hops in the route, `NServiceBus.SendVia.N` headers are attached where `N` is the 1-based index of hop. 

```
string proxy = "ProxyQueueName";
busConfig.Routing().UnicastRoutingTable.AddStatic(typeof(OrderAccepted), new EndpointName("Sales"), proxy);
```

### Endpoint mapping

The middle layer is responsible for mapping the endpoint name into the list of endpoint instance names. This mapping is done in a two-step process:
 * Based on the endpoint name, find out what are all its instances
 * Based on the message type and the list of instances, find out which instances should receive the message
The default behavior of the second step is to select a single instance in a round-robin manner. A user can override it by registering a different `DistributionStrategy` (e.g. send to all instances if a cache synchronization protocol is required).

#### Static mapping

The `KnownEndpoints` class provides a number of convenience methods that let you register a static mapping.

<!-- import Routing-StaticEndpointMapping -->

You can also pass only the transport discriminators

<!-- import Routing-StaticEndpointMappingWithDiscriminators -->

#### Dynamic mapping

Dynamic mapping is meant to provide a convenient extension point for both users and NServiceBus add-ons. To add a dynamic mapping rule you need to pass a function that takes an endpoint name and returns a collection of endpoint instance names. 

<!-- import Routing-DynamicEndpointMapping -->

In this example the rule returns two instances passing both user-provided (1, 2) and transport-provided (A, B) discriminators.

### Instance mapping

The bottom layer is responsible for mapping between the instance name and a transport address. Usually it does not require intervention from the user as the selected transport automatically registers its translation rule. The only times where user is required to configure the instance mapping is when the default translation violates some naming rules implied by the transport infrastructure (e.g. the generated transport address is too long for a queue name in MSMQ). 

#### Special cases

Sometimes there is a need to override the address translation for the single endpoint instance e.g. because the auto-generated address violates a constraint imposed by the transport. Such special case mappings have precedence over other mappings. In order to register an exception you need to use following API:

<!-- import Routing-SpecialCaseTransportAddress -->

#### Rules

User-provided rules override the transport defaults. In order to register a rule you need to use following API:

<!-- import Routing-TransportAddressRule -->

A rule is a function that takes the instance name as the input and return a transport address or null if it does not provide translation for that particular instance name. All rules registered via this API have equal importance. It is expected that the user is responsible for providing a set of mutually exclusive rules so that for each endpoint instance name there is only one not-null translation result. In case there is more than one, an exception is raised.
