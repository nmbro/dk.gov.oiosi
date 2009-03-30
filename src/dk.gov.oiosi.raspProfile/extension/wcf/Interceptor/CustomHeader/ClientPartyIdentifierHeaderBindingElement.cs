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
  *
  */
using System;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.common;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader {
    /// <summary>
    /// Interceptor to add custom headers to a message
    /// </summary>
    class ClientPartyIdentifierHeaderBindingElement : CommonBindingElement {
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
        EndpointKeyTypeCode _senderPartyIdentifierType = EndpointKeyTypeCode.other;
        EndpointKeyTypeCode _receiverPartyIdentifierType = EndpointKeyTypeCode.other;

        XmlQualifiedName _senderPartyIdentifierHeaderName;
        XmlQualifiedName _receiverPartyIdentifierHeaderName;
        XmlQualifiedName _senderPartyIdentifierTypeHeaderName;
        XmlQualifiedName _receiverPartyIdentifierTypeHeaderName;


        /// <summary>
        /// Constructor
        /// </summary>
        public ClientPartyIdentifierHeaderBindingElement(ClientPartyIdentifierHeaderBindingExtensionElement config)
        {
            _senderPartyIdentifierHeaderName = new XmlQualifiedName(config.SenderPartyIdentifierHeaderName, config.Namespace);
            _senderPartyIdentifierTypeHeaderName = new XmlQualifiedName(config.SenderPartyIdentifierTypeHeaderName, config.Namespace);
            _receiverPartyIdentifierHeaderName = new XmlQualifiedName(config.ReceiverPartyIdentifierHeaderName, config.Namespace);
            _receiverPartyIdentifierTypeHeaderName = new XmlQualifiedName(config.ReceiverPartyIdentifierTypeHeaderName, config.Namespace);

        }


        /// <summary>
        /// Constructor
        /// </summary>
        private ClientPartyIdentifierHeaderBindingElement()
        {
        }

        public override void InterceptRequest(InterceptorMessage interceptorMessage) {

            // Get the message, uncopied
            Message msg = interceptorMessage.GetMessage();

            // If a property is set use that to find the header values
            if (msg.Properties.ContainsKey(PartyIdentifierHeaderSettings.MessagePropertyKey)) {
                PartyIdentifierHeaderSettings settings = (PartyIdentifierHeaderSettings)msg.Properties[PartyIdentifierHeaderSettings.MessagePropertyKey];

                if (!string.IsNullOrEmpty(settings.SenderPartyHeaderValue))
                    _senderPartyIdentifier = settings.SenderPartyHeaderValue;
                if (!string.IsNullOrEmpty(settings.ReceiverPartyHeaderValue))
                    _receiverPartyIdentifier = settings.ReceiverPartyHeaderValue;
                
                _senderPartyIdentifierType = settings.SenderPartyKeyType;
                _receiverPartyIdentifierType = settings.ReceiverPartyKeyType;
            }

            // Add the headers
            msg.Headers.Add(MessageHeader.CreateHeader(
                _senderPartyIdentifierHeaderName.Name,
                _senderPartyIdentifierHeaderName.Namespace,
                _senderPartyIdentifier));
            msg.Headers.Add(MessageHeader.CreateHeader(
                _senderPartyIdentifierTypeHeaderName.Name,
                _senderPartyIdentifierTypeHeaderName.Namespace,
                _senderPartyIdentifierType));

            msg.Headers.Add(MessageHeader.CreateHeader(
                _receiverPartyIdentifierHeaderName.Name,
                _receiverPartyIdentifierHeaderName.Namespace,
                _receiverPartyIdentifier));
            msg.Headers.Add(MessageHeader.CreateHeader(
                _receiverPartyIdentifierTypeHeaderName.Name,
                _receiverPartyIdentifierTypeHeaderName.Namespace,
                _receiverPartyIdentifierType));

            // TODO: Why add headers when msg is never used for anything?
        }

        public override void InterceptResponse(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            throw new NotImplementedException();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context) {
            IChannelListener<TChannel> listener = (IChannelListener<TChannel>)base.BuildChannelListener<TChannel>(context);
            return listener;
        }

        public override bool DoesRequestIntercept {
            get { return true; }
        }

        public override bool DoesResponseIntercept {
            get { return false; }
        }

        public override bool DoesFaultOnRequestException {
            get { return false; }
        }

        public override BindingElement Clone() {
            ClientPartyIdentifierHeaderBindingElement clone = new ClientPartyIdentifierHeaderBindingElement();
            clone._receiverPartyIdentifier = _receiverPartyIdentifier;
            clone._receiverPartyIdentifierHeaderName = _receiverPartyIdentifierHeaderName;
            clone._receiverPartyIdentifierType = _receiverPartyIdentifierType;
            clone._receiverPartyIdentifierTypeHeaderName = _receiverPartyIdentifierTypeHeaderName;

            clone._senderPartyIdentifier = _senderPartyIdentifier;
            clone._senderPartyIdentifierHeaderName = _senderPartyIdentifierHeaderName;
            clone._senderPartyIdentifierType = _senderPartyIdentifierType;
            clone._senderPartyIdentifierTypeHeaderName = _senderPartyIdentifierTypeHeaderName;

            return clone;
        }
    }
}
