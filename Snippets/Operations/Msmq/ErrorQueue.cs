﻿namespace Operations.Msmq
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Messaging;
    using System.Text;
    using System.Transactions;
    using System.Xml.Serialization;

    public static class ErrorQueue
    {

        static void Usage()
        {
            #region msmq-return-to-source-queue-usage

            ReturnMessageToSourceQueue(
                errorQueueMachine: Environment.MachineName,
                errorQueueName: "error",
                msmqMessageId: @"c390a6fb-4fb5-46da-927d-a156f75739eb\15386");

            #endregion
        }

        #region msmq-return-to-source-queue

        public static void ReturnMessageToSourceQueue(string errorQueueMachine, string errorQueueName, string msmqMessageId)
        {
            var errorQueue = new MessageQueue(errorQueueMachine + "\\private$\\" + errorQueueName);
            {
                var messageReadPropertyFilter = new MessagePropertyFilter
                {
                    Body = true,
                    TimeToBeReceived = true,
                    Recoverable = true,
                    Id = true,
                    ResponseQueue = true,
                    CorrelationId = true,
                    Extension = true,
                    AppSpecific = true,
                    LookupId = true,
                };
                errorQueue.MessageReadPropertyFilter = messageReadPropertyFilter;
                using (var scope = new TransactionScope())
                {
                    var message = errorQueue.ReceiveById(msmqMessageId, TimeSpan.FromSeconds(5), MessageQueueTransactionType.Automatic);
                    string fullPath = ReadFailedQueueHeader(message);
                    using (var failedQueue = new MessageQueue(fullPath))
                    {
                        failedQueue.Send(message, MessageQueueTransactionType.Automatic);
                    }
                    scope.Complete();
                }
            }
        }

        static string ReadFailedQueueHeader(Message message)
        {
            var headers = ExtractHeaders(message);
            string header = headers.Single(x => x.Key == "NServiceBus.FailedQ").Value;
            string queueName = header.Split('@')[0];
            string machineName = header.Split('@')[1];
            return machineName + "\\private$\\" + queueName;
        }

        public static List<HeaderInfo> ExtractHeaders(Message message)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HeaderInfo>));
            var extension = Encoding.UTF8.GetString(message.Extension);
            using (var stringReader = new StringReader(extension))
            {
                return (List<HeaderInfo>) serializer.Deserialize(stringReader);
            }
        }

        public class HeaderInfo
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        #endregion
    }

}