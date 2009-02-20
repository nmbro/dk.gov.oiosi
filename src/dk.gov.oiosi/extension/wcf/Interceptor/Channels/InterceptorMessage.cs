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
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {

    /// <summary>
    /// Represents a interceptor message
    /// </summary>
    public class InterceptorMessage {
        private MessageBuffer _bufferCopy;
        private Dictionary<string, object> _newProperties;
        private MessageProperties _properties;
        private bool _isFault;
        private Message _originalMessage;

        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">a message</param>
        public InterceptorMessage(Message message) {
            _originalMessage = message;
            _newProperties = new Dictionary<string, object>();
            _properties = message.Properties;
            _isFault = message.IsFault;
        }

        /// <summary>
        /// Gets whether the message is a fault
        /// </summary>
        public bool IsFault {
            get { return _isFault; }
        }

        /// <summary>
        /// Gets the properties of the message.
        /// </summary>
        public MessageProperties Properties {
            get { return _properties; }
        }

        /// <summary>
        /// Gets the certificate with which the message has been encrypted
        /// </summary>
        public X509Certificate2 Certificate {
            get { return ((System.IdentityModel.Tokens.X509SecurityToken)_originalMessage.Properties.Security.InitiatorToken.SecurityToken).Certificate; }
        }


        /// <summary>
        /// Returns a copy of the message.
        /// </summary>
        /// <returns>the message</returns>
        public Message GetCopy() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get copy of message");
            if(_bufferCopy == null)_bufferCopy = _originalMessage.CreateBufferedCopy(int.MaxValue);
            Message message = _bufferCopy.CreateMessage();
            foreach(KeyValuePair<string, object> pair in _newProperties) {
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
        public Message GetMessage() {
            if (_originalMessage.State == MessageState.Created)
                return _originalMessage;
            else
                return GetCopy();
        }

        /// <summary>
        /// Returns the wcf message headers.
        /// </summary>
        /// <returns></returns>
        public MessageHeaders GetHeaders() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get headers");
            if (_bufferCopy == null) 
                return new MessageHeaders(_originalMessage.Headers);
            else {
                Message message = _bufferCopy.CreateMessage();
                return message.Headers;
            }
        }

        /// <summary>
        /// Returns the body of a message
        /// </summary>
        /// <returns>a xmldocument with the body element only</returns>
        public XmlDocument GetBody() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage get body");
            if (_bufferCopy == null) _bufferCopy = _originalMessage.CreateBufferedCopy(int.MaxValue);
            Message message = _bufferCopy.CreateMessage();
            return Utilities.GetMessageBodyAsXmlDocument(message);
        }

        /// <summary>
        /// Sets a new body on the message
        /// </summary>
        /// <param name="body"></param>
        public void SetBody(XmlDocument body) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage set body");
            if (_bufferCopy == null) 
                _bufferCopy = _originalMessage.CreateBufferedCopy(int.MaxValue);
            XmlNodeReader xnr = new XmlNodeReader(body.DocumentElement);
            Message copy = _bufferCopy.CreateMessage();
            Message updatedMessage = Message.CreateMessage(copy.Version, copy.Headers.Action, xnr);
            updatedMessage.Headers.Clear();
            updatedMessage.Headers.CopyHeadersFrom(copy.Headers);
            _bufferCopy = updatedMessage.CreateBufferedCopy(int.MaxValue);
        }

        /// <summary>
        /// Adds a custom property the message.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddProperty(string key, object value) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage adds property");
            _properties.Add(key, value);
            _newProperties.Add(key, value);
        }

        /// <summary>
        /// Trys to get the certificate subject from the message.
        /// </summary>
        /// <param name="certificateSubject"></param>
        /// <returns></returns>
        public bool TryGetCertificateSubject(out string certificateSubject) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorMessage try get certificate subject");
            certificateSubject = null;
            try {
                string identityName = _properties.Security.ServiceSecurityContext.PrimaryIdentity.Name;
                int index = identityName.LastIndexOf(';');
                certificateSubject = identityName.Substring(0, index);
                return true;
            } 
            catch (Exception) {
                return false;
            }
        }
    }
}