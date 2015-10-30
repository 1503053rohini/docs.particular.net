﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;
using NServiceBus.Serialization;
using NServiceBus.Transports;
using NServiceBus.Unicast.Messages;

#region deserialize-behavior
using ToContext = NServiceBus.Pipeline.Contexts.LogicalMessageProcessingContext;
using FromContext = NServiceBus.PhysicalMessageProcessingContext;

class DeserializeConnector : StageConnector<FromContext, ToContext>
{
    SerializationMapper serializationMapper;
    MessageMetadataRegistry messageMetadataRegistry;
    LogicalMessageFactory logicalMessageFactory;
    static ILog logger = LogManager.GetLogger<DeserializeConnector>();

    public DeserializeConnector(
        SerializationMapper serializationMapper,
        MessageMetadataRegistry messageMetadataRegistry,
        LogicalMessageFactory logicalMessageFactory)
    {
        this.serializationMapper = serializationMapper;
        this.messageMetadataRegistry = messageMetadataRegistry;
        this.logicalMessageFactory = logicalMessageFactory;
    }


    public async override Task Invoke(PhysicalMessageProcessingContext context, Func<LogicalMessageProcessingContext, Task> next)
    {
        var incomingMessage = context.Message;

        var messages = ExtractWithExceptionHandling(incomingMessage);

        foreach (var message in messages)
        {
            await next(new LogicalMessageProcessingContext(message, context.Message.Headers, context)).ConfigureAwait(false);
        }

    }

    List<LogicalMessage> ExtractWithExceptionHandling(IncomingMessage message)
    {
        try
        {
            return Extract(message);
        }
        catch (Exception exception)
        {
            throw new MessageDeserializationException(message.MessageId, exception);
        }
    }

    List<LogicalMessage> Extract(IncomingMessage physicalMessage)
    {
        if (physicalMessage.Body == null || physicalMessage.Body.Length == 0)
        {
            return new List<LogicalMessage>();
        }

        string messageTypeIdentifier;
        var messageMetadata = new List<MessageMetadata>();

        if (physicalMessage.Headers.TryGetValue(Headers.EnclosedMessageTypes, out messageTypeIdentifier))
        {
            foreach (var messageTypeString in messageTypeIdentifier.Split(';'))
            {
                var typeString = messageTypeString;

                MessageMetadata metadata = messageMetadataRegistry.GetMessageMetadata(typeString);

                if (metadata == null)
                {
                    continue;
                }

                messageMetadata.Add(metadata);
            }

            if (messageMetadata.Count == 0 && physicalMessage.GetMesssageIntent() != MessageIntentEnum.Publish)
            {
                logger.WarnFormat("Could not determine message type from message header '{0}'. MessageId: {1}", messageTypeIdentifier, physicalMessage.MessageId);
            }
        }

        using (var stream = new MemoryStream(physicalMessage.Body))
        {
            IMessageSerializer messageSerializer = serializationMapper.GetSerializer(physicalMessage.Headers);
            var messageTypes = messageMetadata.Select(metadata => metadata.MessageType).ToList();
            return messageSerializer.Deserialize(stream, messageTypes)
                .Select(x => logicalMessageFactory.Create(x.GetType(), x))
                .ToList();

        }
    }

}

#endregion