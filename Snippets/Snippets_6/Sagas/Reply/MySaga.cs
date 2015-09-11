﻿namespace Snippets6.Sagas.Reply
{
    using NServiceBus;

    #region saga-with-reply

    public class MySaga : Saga<MySagaData>,
        IAmStartedByMessages<StartMessage>
    {

        public void Handle(StartMessage message)
        {
            Data.SomeID = message.SomeID;
            ReplyToOriginator(new AlmostDoneMessage
            {
                SomeID = Data.SomeID
            });
        }

        #endregion

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
        }
    }

}