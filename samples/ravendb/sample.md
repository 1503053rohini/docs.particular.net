---
title: RavenDB persistence Sample
summary: This sample shows how use RavenDB to store Sagas and Timeouts.
tags:
- Saga
- Timeouts
related:
- nservicebus/sagas
- nservicebus/ravendb
---


## Code walk-through

This sample shows a simple Client + Server scenario. 

* `Client` sends a `StartOrder` message to `Server`
* `Server` starts an `OrderSaga`. 
* `OrderSaga` requests a timeout with a `CompleteOrder` data.
* When the `CompleteOrder` timeout fires the `OrderSaga` publishes a `OrderCompleted` event.
* The Server then publishes a message that the client subscribes to.
* `Client` handles `OrderCompleted` event.


### In Process Raven Host

So that no running instance of RavenDB server is required.

<!-- import ravenhost -->


### Order Saga Data

<!-- import sagadata -->


### Order Saga

<!-- import thesaga -->


## The Data in RavenDB

The data in RavenDB is stored in three different collections.


### The Saga Data 

 * `IContainSagaData.Id` maps to the native RavenDB document `Id`
 * `IContainSagaData.Originator` and `IContainSagaData.OriginalMessageId` map to simple properties pairs.
 * Custom properties on the SagaData, in this case `OrderDescription` and `OrderId`, are also mapped to simple properties.

![](sagadata.png)


### The Timeouts 

  * The subscriber is stored in a `Destination` with the nested properties `Queue` and `Machine`.
  * The endpoint that initiated the timeout is stored in the `OwningTimeoutManager` property
  * The connected saga id is stored in a `SagaId` property.
  * The serialized data for the message is stored in a `State` property.
  * The scheduled timestamp for the timeout is stored in a `Time` property.
  * Any headers associated with the timeout are stored in an array of key value pairs.  

![](timeouts.png)


### The Subscriptions

Note that the message type maps to multiple subscriber endpoints.

 * The Subscription message type and version are stored under the `MessageType` property.
 * The list of subscribers is stored in a array of objects each containing `Queue` and `MachineName` properties. 

![](subscriptions.png)