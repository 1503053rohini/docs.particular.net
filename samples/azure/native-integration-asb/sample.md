---
title: Native Integration with Azure Service Bus Transport
summary: Shows how to consume messages published by non NServiceBus endpoints
tags:
related:
- nservicebus/azure/azure-servicebus-transport
---

## Prerequisites 

An environment variable named `AzureServiceBus.ConnectionString` that contains the connection string for the Azure Service Bus namespace.


## Azure Service Bus Transport

This sample utilizes the [Azure Service Bus Transport](/nservicebus/azure/azure-servicebus-transport.md).


## Code walk-through

This sample shows how to send a message from non-NServicebus code using the Azure Service Bus API and process it with an NServiceBus endpoint using the Azure Service Bus transport.

The sample contains two executable projects:

- NativeSender - sends a native `BrokeredMessage` messages to a queue
- Receiver - NServiceBus endpoint that processes messages sent by NativeSender

## Sending messages with native Azure Service Bus API

To integrate native Azure Service Bus sender with NServiceBus endpoints, you need to configure the native sender to send messages to the queue used by the receiving endpoint. By default, the input queue for an NServiceBus endpoint is its endpoint name.

<!-- import EndpointAndSingleQueue -->

The native sender is using queue client to send a `BrokeredMessage`.

## Message serialization

The Azure Service Bus transport is using the JSON serializer by default. Therefore, the message sent by a native sender needs to be valid JSON.

<!-- import SerializedMessage -->

To generate a serialized message, the `MessageGenerator` project can be used with the unit test named `Generate` under the `SerializedMessageGenerator` test fixture.

## BrokeredMessage body format

The Azure Service Bus API allows you to construct a `BrokeredMessage` body from a stream or an object that will get serialized by the internals of `BrokeredMessage`. 

NOTE: Both sender (native or NServiceBus) and receiver must agree on the convention used for sending the message body.

## Required headers

For a native message to be processed, NServiceBus endpoints using  the Azure Service Bus transport require two headers:

1. Message type
2. Message intent

These headers need to be stored as `BrokeredMessage` properties.

<!-- import NecessaryHeaders -->

NOTE: The `NServiceBus.EnclosedMessageTypes` property must contain the message type expected by the NServiceBus endpoint.

The message itself is defined as an `IMessage` under the `Shared` project.

<!-- import NativeMessage -->

## NServiceBus receiving endpoint

The receiver is defining how to get the Azure Service Bus transport message body by specifying a strategy using `BrokeredMessageBodyConversion`.

<!-- import BrokeredMessageConvention -->

NOTE: Both the sender (native or NServiceBus) and receiver have to agree on the convention used for sending the message body.

## Handling messages from native sender in an NServiceBus endpoint

Once the message is received by the NServiceBus endpoint, its contents will be presented.

<!-- import NativeMessageHandler -->

Things to note:

 * The use of the `AzureServiceBus.ConnectionString` environment variable mentioned above.
 * The use of `UseSingleBrokerQueue` prevents the Azure Service Bus transport individualizing queue names by appending the machine name.  
 * Execute `Receiver` first to create destination queue `NativeSender` will need to send native messages.
