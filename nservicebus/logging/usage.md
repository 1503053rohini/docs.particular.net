---
title: Writing a log entry
summary: How to write to logging
tags: 
- Logging
redirects:
- nservicebus/logging-writing
---

Writing to logging from your code is straightforward. Set up a single static field to a `ILog` in your classes, and then use it in all your methods, like this:

<!-- import UsingLogging -->
 

NOTE: When writing to a logger ensure the log level is of an equivalent value that will result in that log entry being written. So for example when calling `.Debug(..)` ensure that the log level is set to DEBUG. See [Change settings via configuration](logging.md#changing-settings-via-configuration).

NOTE: Since `LogManager.GetLogger(..);` is an expensive call it is important that the field is `static` so that the call only happens once per class and have the best possible performance.