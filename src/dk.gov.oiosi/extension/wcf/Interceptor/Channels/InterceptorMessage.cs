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
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *   Jacob Mogensen, mySupply ApS
  *   Jens Madsen, Comcare
  */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.logging;
using System.IdentityModel.Tokens;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels
{

    /// <summary>
    /// Represents a interceptor message
    /// </summary>
    public class InterceptorMessage
    {
        private MessageBuffer bufferCopy;
        private Dictionary<string, object> newProperties;
        private MessageProperties properties;
        private bool isFault;
        private Message originalMessage;

        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">a message</param>
        public InterceptorMessage(Message message)
        {
            this.originalMessage = message;
            this.newProperties = new Dictionary<string, object>();
            this.properties = message.Properties;
            this.isFault = message.IsFault;
        }

        /// <summary>
        /// Gets whether the message is a fault
        /// </summary>
        public bool IsFault
        {
            get 
            {
                return this.isFault; 
            }
        }

        /// <summary>
        /// Gets the properties of the message.
        /// </summary>
        public MessageProperties Properties
        {
            get 
            {
                return this.properties; 
            }
        }

        /// <summary>
        /// Gets the certificate with which the message has been encrypted
        /// </summary>
        public X509Certificate2 Certificate
        {
            get 
            {
                X509Certificate2 x509Certificate2 = null;
                SecurityToken securityToken = this.originalMessage.Properties.Security.InitiatorToken.SecurityToken;
                if (securityToken is X509SecurityToken)
                {
                    x509Certificate2 = ((X509SecurityToken)securityToken).Certificate;
                }
                else
                {
                    throw new NotSupportedException("SecurityToken must be of type X509Certificate2");
                }

                return x509Certificate2;
            }
        }


        /// <summary>
        /// Returns a copy of the message.
        /// </summary>
        /// <returns>the message</returns>
        public Message GetCopy()
        {
            // http://msdn.microsoft.com/en-us/library/ms734675.aspx
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get copy of message");
            if (this.bufferCopy == null)
            {
                bufferCopy = originalMessage.CreateBufferedCopy(int.MaxValue);
            }

            Message message = bufferCopy.CreateMessage();
            foreach (KeyValuePair<string, object> pair in newProperties)
            {
                string key = pair.Key;
                object value = pair.Value;
                message.Properties.Add(key, value);
            }

            return message;
        }

        /// <summary>
        /// Returns the message if it has not already been copied. If it has a copy of the message will be returne.
        /// </summary>
        /// <remarks>In case an interceptor has been added UNDER the security layer a copy of the Message object should never be done - seeing how this leads to the To header not being signed.</remarks>
        /// <returns>The message</returns>
        public Message GetMessage()
        {
            Message message;
            // This is because a Message object can only be read once.
            if (this.originalMessage.State == MessageState.Created)
            {
                return this.originalMessage;
            }
            else
            {                
                message = this.GetCopy();
            }

            return message;
        }

        /// <summary>
        /// Returns the wcf message headers.
        /// </summary>
        /// <returns></returns>
        public MessageHeaders GetHeaders()
        {
            MessageHeaders messageHeaders = null;
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get headers");
            if (this.bufferCopy == null)
            {
                messageHeaders = new MessageHeaders(originalMessage.Headers);
            }
            else
            {
                Message message = bufferCopy.CreateMessage();
                messageHeaders = message.Headers;
            }

            return messageHeaders;
        }

        /// <summary>
        /// Returns the body of a message
        /// </summary>
        /// <returns>a xmldocument with the body element only</returns>
        public XmlDocument GetBody()
        {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get body");
            if (this.bufferCopy == null)
            {
                this.bufferCopy = this.originalMessage.CreateBufferedCopy(int.MaxValue);
            }

            Message message = this.bufferCopy.CreateMessage();
            XmlDocument xmlDocument = Utilities.GetMessageBodyAsXmlDocument(message);

            return xmlDocument;
        }

        /// <summary>
        /// Returns the body of a message
        /// </summary>
        /// <returns>a xmldocument with the body element only</returns>
        public string GetBodyAsString()
        {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get body");
            if (this.bufferCopy == null)
            {
                this.bufferCopy = this.originalMessage.CreateBufferedCopy(int.MaxValue);
            }

            Message message = this.bufferCopy.CreateMessage();
            string body = Utilities.GetMessageBodyAsString(message);

            return body;
        }

        /// <summary>
        /// Sets a new body on the message
        /// </summary>
        /// <param name="body"></param>
        public void SetBody(XmlDocument body)
        {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage set body");
            if (this.bufferCopy == null)
            {
                this.bufferCopy = this.originalMessage.CreateBufferedCopy(int.MaxValue);
            }

            XmlNodeReader xnr = new XmlNodeReader(body.DocumentElement);
            Message copy = this.bufferCopy.CreateMessage();
            Message updatedMessage = Message.CreateMessage(copy.Version, copy.Headers.Action, xnr);
            updatedMessage.Headers.Clear();
            updatedMessage.Headers.CopyHeadersFrom(copy.Headers);

            // ?? update buffer again
            this.bufferCopy = updatedMessage.CreateBufferedCopy(int.MaxValue);
        }


        /// <summary>
        /// Adds a custom property the message.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddProperty(string key, object value)
        {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage adds property");
            this.properties.Add(key, value);
            this.newProperties.Add(key, value);
        }

        /// <summary>
        /// Trys to get the certificate subject from the message.
        /// </summary>
        /// <param name="certificateSubject"></param>
        /// <returns></returns>
        public bool TryGetCertificateSubject(out string certificateSubject)
        {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage try get certificate subject");
            bool success;
            try
            {
                string identityName = this.properties.Security.ServiceSecurityContext.PrimaryIdentity.Name;
                int index = identityName.LastIndexOf(';');
                certificateSubject = identityName.Substring(0, index);
                success = true;
            }
            catch (Exception)
            {
                certificateSubject = string.Empty;
                success = false;
            }

            return success;
        }
    }
}