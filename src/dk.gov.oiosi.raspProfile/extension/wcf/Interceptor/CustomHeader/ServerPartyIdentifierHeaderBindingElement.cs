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
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader
{
    /// <summary>
    /// Interceptor to add custom headers to a message
    /// </summary>
    class ServerPartyIdentifierHeaderBindingElement: CommonBindingElement
    {
        /// <summary>
        /// The default value of the Sender Party ID header
        /// </summary>
        public const string DefaultSenderPartyIdentifier = Definitions.DefaultOiosiNamespace2007 + "anonymous";

        /// <summary>
        /// The default value of the Receiver Party ID header
        /// </summary>
        public const string DefaultReceiverPartyIdentifier = Definitions.DefaultOiosiNamespace2007 + "anonymous";

        string _senderPartyIdentifier = DefaultSenderPartyIdentifier;
        string _receiverPartyIdentifier = DefaultReceiverPartyIdentifier;
        XmlQualifiedName _senderPartyIdentifierHeaderName;
        XmlQualifiedName _receiverPartyIdentifierHeaderName;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="senderPartyIdentifierHeaderName">The header name that </param>
        /// <param name="receiverPartyIdentifierHeaderName"></param>
        public ServerPartyIdentifierHeaderBindingElement(XmlQualifiedName senderPartyIdentifierHeaderName, XmlQualifiedName receiverPartyIdentifierHeaderName)
        {
            _senderPartyIdentifierHeaderName = senderPartyIdentifierHeaderName;
            _receiverPartyIdentifierHeaderName = receiverPartyIdentifierHeaderName;

        }

        public override void InterceptRequest(InterceptorMessage interceptorMessage)
        {
            // Get the message, uncopied
            Message msg = interceptorMessage.GetMessage();

                // Save the Sender Party ID
                string header = msg.Headers.GetHeader<string>(_senderPartyIdentifierHeaderName.Name, _senderPartyIdentifierHeaderName.Namespace);

                if (header == null || header == "")
                    _receiverPartyIdentifier = DefaultReceiverPartyIdentifier;
                else
                    _receiverPartyIdentifier = header;

            // Save the Receiver Party ID
            header = msg.Headers.GetHeader<string>(_receiverPartyIdentifierHeaderName.Name, _receiverPartyIdentifierHeaderName.Namespace);
            if (header == null || header == "")
                _senderPartyIdentifier = DefaultSenderPartyIdentifier;
            else
                _senderPartyIdentifier = header;
        }

        public override void InterceptResponse(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage)
        {
            
            // Get the message, uncopied
            Message msg = interceptorMessage.GetMessage();

            // Add the headers
            msg.Headers.Add(MessageHeader.CreateHeader(
                _senderPartyIdentifierHeaderName.Name,
                _senderPartyIdentifierHeaderName.Namespace,
                _senderPartyIdentifier));

            msg.Headers.Add(MessageHeader.CreateHeader(
                _receiverPartyIdentifierHeaderName.Name,
                _receiverPartyIdentifierHeaderName.Namespace,
                _receiverPartyIdentifier));

        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            IChannelListener<TChannel> listener = (IChannelListener<TChannel>)base.BuildChannelListener<TChannel>(context);
            return listener;
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            return base.CanBuildChannelFactory<TChannel>(context);
        }

        public override bool DoesRequestIntercept
        {
            get { return true; }
        }
        
        public override bool DoesResponseIntercept
        {
            get { return true; }
        }

        public override bool DoesFaultOnRequestException
        {
            get { return false; }
        }

        public override System.ServiceModel.Channels.BindingElement Clone()
        {
            ServerPartyIdentifierHeaderBindingElement clone = new ServerPartyIdentifierHeaderBindingElement(_senderPartyIdentifierHeaderName, _receiverPartyIdentifierHeaderName);
            return clone;
        }
    }
}
