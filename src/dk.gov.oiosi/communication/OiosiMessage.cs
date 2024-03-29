/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *   Jacob Mogensen, mySupply
  */

using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.common;
using System;

namespace dk.gov.oiosi.communication
{

    /// <summary>
    /// Represents an outbound message (xml + metadata)
    /// </summary>
    public class OiosiMessage
    {
        private const string DEFAULTREQUESTACTION = "*";
        private const string DEFAULTREPLYACTION = "*";

        private Dictionary<string, object> properties;
        private Dictionary<string, object> ubiquitousProperties;
        private Dictionary<XmlQualifiedName, MessageHeader> messageHeaders;

        private string requestAction;
        private string replyAction;

        private string messageAsString = string.Empty;
        private XmlDocument messageAsXml = null;

        private object lockObject = new object();

        #region Constructors

        private OiosiMessage()
        {
            this.properties = new Dictionary<string, object>();
            this.ubiquitousProperties = new Dictionary<string, object>();
            this.messageHeaders = new Dictionary<XmlQualifiedName, MessageHeader>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">The message</param>
        public OiosiMessage(string document)
            : this()
        {
            this.messageAsString = document;
            this.requestAction = DEFAULTREQUESTACTION;
            this.replyAction = DEFAULTREPLYACTION;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xml">the message</param>
        //[Obsolete("Add the document as string, as it is much faster", false)]
        public OiosiMessage(XmlDocument xml)
            : this()
        {
            this.messageAsXml = xml;

            this.RequestAction = DEFAULTREQUESTACTION;
            this.ReplyAction = DEFAULTREPLYACTION;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wcfMessage">the message</param>
        public OiosiMessage(Message wcfMessage)
            : this()
        {
            try
            {
                this.messageAsString = Utilities.GetMessageBodyAsString(wcfMessage, true);
                
                // Adding the headers
                for (int i = 0; i < wcfMessage.Headers.Count; i++)
                {
                    this.CopyMessageHeader(wcfMessage.Headers, i);
                }
            }
            catch (Exception e)
            {
                dk.gov.oiosi.logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Error, "Exception occurred in OiosiMessage: " + e);
            }
        }

        #endregion

        /// <summary>
        /// A list of cumstom headers
        /// </summary>
        public Dictionary<XmlQualifiedName, MessageHeader> MessageHeaders 
        {
            get
            {
                return this.messageHeaders;
            }
        }

        /// <summary>
        /// Custom properties
        /// </summary>
        public Dictionary<string, object> Properties
        { 
            get
            {
                return this.properties;
            }  
        }

        /// <summary>
        /// Custom properties
        /// </summary>
        public Dictionary<string, object> UbiquitousProperties
        {
            get
            {
                return this.ubiquitousProperties;
            }
        }

        /// <summary>
        /// Gets or sets the request action of the message
        /// </summary>
        public string RequestAction
        {
            get
            {
                return this.requestAction;
            }
            set
            {
                this.requestAction = value;
            }
        }

        /// <summary>
        /// Gets or sets the reply action for the message
        /// </summary>
        public string ReplyAction 
        {
            get
            {
                return this.replyAction;
            }
            set
            {
                this.replyAction = value;
            }
        }

        /// <summary>
        /// Property for the message
        /// 
        /// Note: The message as string, only exist on the receiver side, when the 
        /// OiosiMessage has be initialized though the constructor OiosiMessage(Message wcfMessage)
        /// </summary>
        public string MessageAsString
        {
            get
            {
                return this.messageAsString;
            }
        }

        /// <summary>
        /// Property for the message
        /// 
        /// Note: XmlDocument is not the best representaion, as it is slow to work with.
        /// On the receiver side, it is possible to get the message as a string instead
        /// 
        /// </summary>
        public XmlDocument MessageXml
        {
            get
            {
                lock (lockObject)
                {
                    if (this.messageAsXml == null && !string.IsNullOrEmpty(this.messageAsString))
                    {
                        this.messageAsXml = new XmlDocument();
                        this.messageAsXml.LoadXml(this.messageAsString);
                    }
                }

                return this.messageAsXml;
            }
        }

        /// <summary>
        /// Do we have a message body?
        /// </summary>
        public bool HasBody
        {
            get
            {
                bool hasBody;


                if (!string.IsNullOrEmpty(this.messageAsString))
                {
                    hasBody = true;
                }
                else if (this.MessageXml != null && this.MessageXml.DocumentElement != null)
                {
                    hasBody = true;
                }
                else
                {
                    hasBody = false;
                }

                return hasBody;
            }
        }

       /* public XPathDocument GetMessageXPathDocument()
        {
        * 
        * It will be faster to use XPathDocument, instead of xmlDocument
        * 
            XPathDocument xPathDocument;

            using (Stream stream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    // write document to memory stream 
                    sw.Write(this.messageAsString);
                    sw.Flush();

                    // document is now in a memory stream
                    // set stream index to 0.
                    stream.Seek(0, SeekOrigin.Begin);

                    xPathDocument = new XPathDocument(stream);
                }
            }

            return xPathDocument;
        }*/

        /// <summary>
        /// Returns an XmlReader that can read the message xml
        /// </summary>
        /// <returns>XmlReader that can read the message xml</returns>
        public XmlReader GetMessageXmlReader()
        {
            XmlDocument xmlDocument = this.MessageXml;
            XmlNodeReader xnr = new XmlNodeReader(xmlDocument.DocumentElement);
            return (XmlReader)xnr;
        }

        private void CopyMessageHeader(MessageHeaders messageHeaders, int index)
        {
            try
            {
                MessageHeaderInfo headerInfo = messageHeaders[index];
                XmlQualifiedName qName = new XmlQualifiedName(headerInfo.Name, headerInfo.Namespace);
                string headerValue = messageHeaders.GetHeader<string>(index);
                MessageHeader header = MessageHeader.CreateHeader(headerInfo.Name, headerInfo.Namespace, headerValue);
                MessageHeaders.Add(qName, header);
            }
            catch (Exception)
            {
                //eats away headers not of the right type
            }
        }
    }
}