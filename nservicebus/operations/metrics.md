---
title: Metrics
summary: Measuring the performance and health of an endpoint.
reviewed: 2017-04-04
component: Metrics
related:
 - samples/metrics
---

`NServiceBus.Metrics` collects information to measure endpoint health and performance. When a system is broken down into multiple processes, each with its own queue, then bottlenecks may be identified by tracking:
 - the length of each queue
 - incoming rate of messages for each queue
 - processing rate for each queue.

## Enabling NServiceBus.Metrics

snippet: Metrics-Enable


## Reporting metrics data

Metrics can be reported to a number of different locations. Each location is updated on a separate interval. 

### To NServiceBus log

Metrics data can be written to the [NServiceBus Log](/nservicebus/logging/).

snippet: Metrics-Log

NOTE: By default metrics will be written to the log at the `DEBUG` log level. The API allows this parameter to be customized.

snippet: Metrics-Log-Info

### To trace log

Metrics data can be written to [System.Diagnostics.Trace](https://msdn.microsoft.com/en-us/library/system.diagnostics.trace.aspx).

snippet: Metrics-Tracing

### To custom function

Metrics data can be consumed by a custom function.

snippet: Metrics-Custom-Function

### To Windows Performance Counters

Some of the data captured by the NServiceBus.Metrics component can be forwarded to Windows Performance Counters. See [Performance Counters](./performance-counters.md) for more information.


## Metrics captured

NServiceBus and ServiceControl capture a number of different metrics about a running endpoint.

### Processing time

Processing time is the time it takes for an endpoint to process a single message.

### Critical time

Critical time is the time between when a message is sent and when it is fully processed. It is a combination of:
- Network send time: The time a message spends on the network before arriving in the destination queue
- Queue wait time: The time a message spends in the destination queue before being picked up and processed
- Processing time: The time it takes for the destination endpoint to process the message

### Messages received performance statistics

These statistics encompass a number of different metrics, including:

- Number of messages pulled from queue
- Number of message processing failures
- Number of messages successfully processed

### Queue length

This metric tracks estimated number of messages in the input queue of an endpoint.

A _link_ is a communication channel between a sender of the message and its receiver. Each link is uniquely identified by some combination of destination address, message assembly, and the [host identifier](/nservicebus/hosting/override-hostid.md#host-identifier) of the sender. The exact composition of link identifiers depends on the transport properties and type of message being sent.

Each sender maintains a monotonic counter of messages sent over each of its outgoing links and transmits the value of this counter to the receiver in a message header. The receiver tracks the counter value for the last message received over each link. This allows both communicating endpoints to track how many messages were sent and received over each link.

ServiceControl collects these metrics for all links and estimates the length of the input queue for each receiver based on how many messages were sent in total over all incoming links and how many of those messages have already been received.

#### Example

The system consists of two endpoints, Sales and Shipping. The Sales endpoint is scaled out and deployed to two machines, `1` and `2`. Consider the following values reported to ServiceControl:

| Link ID                        | Max sent counter | Max received counter | Messages in queue from this link |
|--------------------------------|:----------------:|:--------------------:|:--------------------------------:|
| `Sales@1-Shipping`             | 10               | 6                    | 4                                |
| `Sales@2-Shipping`             | 5                | 1                    | 4                                |
| `Shipping-Sales@1`             | 1                | 1                    | 0                                |
| `Shipping-Sales@2`             | 3                | 1                    | 2                                |

Based on the data above, ServiceControl can estimate the following values of queue length for `Sales` and `Shipping` endpoints:

| Endpoint | Queue length |
|----------|:------------:|
| Sales    | 8            |
| Shipping | 2            |
