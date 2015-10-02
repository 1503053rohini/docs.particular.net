---
title: Sending a Message
summary: Describes how to send a message
tags: []
redirects:
- nservicebus/how-do-i-send-a-message
---

To send a message, use the `Send` method on the `IBus` interface, passing as the argument the instance of the message to deliver:


<!-- import BasicSend -->

 Or instantiate and send all messages at once:

<!-- import BasicSendInterface -->

## Immediate Dispatch

While its usually best to let NServiceBus [handle exceptions for you](/nservicebus/errors) there are some scenarios where you want to send messages out even though the incoming message is rolled back. One example would be sending a reply notifying that there was an issue processing the message. 

In order to request a immediate dispatch you can use the following syntax.

<!-- import RequestImmediateDispatch -->

NOTE: By immediate dispatch outgoing message are not [batched](/nservicebus/messaging/batched-dispatch.md) or enlisted in the current receive transaction even if the transport has support for it.

### Suppressing the transaction scope

Version 6 and below allows you to suppress the ambient transaction in order to have the outgoing operation being sent immediately.

<!-- import RequestImmediateDispatchUsingScope -->

The issue with this approach is that it only works for transports that enlists the receive operation in a transaction scope. Currently this would be MSMQ and SqlServer in DTC mode. Should you use any other transport or disable the DTC this no longer works and the outgoing message might be rolled back together with the incoming message. 

For this reason we've decided to deprecate this method and recommend users to switch to the explicit API mentioned above.



