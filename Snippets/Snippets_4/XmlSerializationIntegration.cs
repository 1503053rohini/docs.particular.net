﻿using System.Xml.Linq;
using NServiceBus;

namespace Snippets4
{
    public class XmlSerializationIntegration
    {
        public void RawXml()
        {
            #region ConfigureRawXmlSerialization

            Configure.Serialization.Xml(c => c.DontWrapRawXml()).DontWrapSingleMessages();

            #endregion
        }

        #region MessageWithXDocument

        public class MessageWithXDocument : IMessage
        {
            public XDocument nutrition { get; set; } // name and casing must match the rootnode
        }
        /*
        Document payload
        <?xml version='1.0' encoding='UTF-8'?>
        <nutrition>
            <daily-values>
                <total-fat units='g'>65</total-fat>
                <saturated-fat units='g'>20</saturated-fat>
            </daily-values>
        </nutrition>
         */
        #endregion

        #region MessageWithXElement

        public class MessageWithXElement : IMessage
        {
            public XElement nutrition { get; set; } // name and casing must match the rootnode
        }
        /*
        Element payload
        <nutrition>
            <daily-values>
                <total-fat units='g'>65</total-fat>
                <saturated-fat units='g'>20</saturated-fat>
            </daily-values>
        </nutrition>    
        */
        #endregion
    }
}