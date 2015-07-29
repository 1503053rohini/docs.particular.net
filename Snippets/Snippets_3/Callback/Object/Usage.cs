﻿namespace Snippets3.Callback.Object
{
    using System;
    using NServiceBus;

    class Usage
    {

        void Simple()
        {
            IBus bus = null;
            #region ObjectCallback

            Message message = new Message();
            bus.Send(message)
                .Register(ar =>
                {
                    CompletionResult localResult = (CompletionResult)ar.AsyncState;
                    ResponseMessage response = (ResponseMessage)localResult.Messages[0];
                    Console.WriteLine("Callback received with response:" + response.Property);
                }, null);

            #endregion
        }

    }
}