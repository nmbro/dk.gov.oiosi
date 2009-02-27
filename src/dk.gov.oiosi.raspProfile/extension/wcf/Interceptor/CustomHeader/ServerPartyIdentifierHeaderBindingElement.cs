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
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.common;
using dk.gov.oiosi.uddi;

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
        string _senderPartyIdentifierType = EndpointKeyTypeCode.other.ToString();
        string _receiverPartyIdentifierType = EndpointKeyTypeCode.other.ToString();

        XmlQualifiedName _senderPartyIdentifierHeaderName;
        XmlQualifiedName _receiverPartyIdentifierHeaderName;
        XmlQualifiedName _senderPartyIdentifierTypeHeaderName;
        XmlQualifiedName _receiverPartyIdentifierTypeHeaderName;


        /// <summary>
        /// Constructor
        /// </summary>
        public ServerPartyIdentifierHeaderBindingElement(ServerPartyIdentifierHeaderBindingExtensionElement config)
        {
            _senderPartyIdentifierHeaderName = new XmlQualifiedName(config.SenderPartyIdentifierHeaderName, config.Namespace);
            _senderPartyIdentifierTypeHeaderName = new XmlQualifiedName(config.SenderPartyIdentifierTypeHeaderName, config.Namespace);
            _receiverPartyIdentifierHeaderName = new XmlQualifiedName(config.ReceiverPartyIdentifierHeaderName, config.Namespace);
            _receiverPartyIdentifierTypeHeaderName = new XmlQualifiedName(config.ReceiverPartyIdentifierTypeHeaderName, config.Namespace);

        }

        private ServerPartyIdentifierHeaderBindingElement(){ }

        public override void InterceptRequest(InterceptorMessage interceptorMessage)
        {
            // Get the message, uncopied
            Message msg = interceptorMessage.GetMessage();

            // Extract headers and switch sender for receiver
            _senderPartyIdentifier = ExtractHeaderValue(msg, _receiverPartyIdentifierHeaderName.Name, _receiverPartyIdentifierHeaderName.Namespace, DefaultReceiverPartyIdentifier);
            _senderPartyIdentifierType = ExtractHeaderValue(msg, _receiverPartyIdentifierTypeHeaderName.Name, _receiverPartyIdentifierTypeHeaderName.Namespace, EndpointKeyTypeCode.other.ToString());
            _receiverPartyIdentifier = ExtractHeaderValue(msg, _senderPartyIdentifierHeaderName.Name, _senderPartyIdentifierHeaderName.Namespace, DefaultSenderPartyIdentifier);
            _receiverPartyIdentifierType = ExtractHeaderValue(msg, _senderPartyIdentifierTypeHeaderName.Name, _senderPartyIdentifierTypeHeaderName.Namespace, EndpointKeyTypeCode.other.ToString());

        }

        private string ExtractHeaderValue(Message msg, string name, string ns, string defaultValue){
            string header = msg.Headers.GetHeader<string>(name,ns);
            if (header == null || header == "")
                return defaultValue;
            else
                return header;
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

        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            IChannelListener<TChannel> listener = (IChannelListener<TChannel>)base.BuildChannelListener<TChannel>(context);
            return listener;
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

        public override BindingElement Clone()
        {
            ServerPartyIdentifierHeaderBindingElement clone = new ServerPartyIdentifierHeaderBindingElement();
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
