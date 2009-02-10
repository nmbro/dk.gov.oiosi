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
using System.Xml;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using dk.gov.oiosi.common;


namespace dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader
{
    /// <summary>
    /// Server party identifier header binding extension element
    /// </summary>
    public class ServerPartyIdentifierHeaderBindingExtensionElement : BindingElementExtensionElement
    {

        /// <summary>
        /// Gets or sets the sender party identifier header name
        /// </summary>
        [ConfigurationProperty("senderPartyIdentifierHeaderName", IsRequired=false, DefaultValue="SenderPartyIdentifier")]
        public string SenderPartyIdentifierHeaderName
        {
            get { return (string)base["senderPartyIdentifierHeaderName"]; }
            set { base["senderPartyIdentifierHeaderName"] = value; }
        }

        /// <summary>
        /// Gets or sets the receiver party identifier header name
        /// </summary>
        [ConfigurationProperty("receiverPartyIdentifierHeaderName", IsRequired = false, DefaultValue = "ReceiverPartyIdentifier")]
        public string ReceiverPartyIdentifierHeaderName
        {
            get { return (string)base["receiverPartyIdentifierHeaderName"]; }
            set { base["receiverPartyIdentifierHeaderName"] = value; }
        }

        /// <summary>
        /// The sender party identifier header name
        /// </summary>
        [ConfigurationProperty("senderPartyIdentifierTypeHeaderName", IsRequired = false, DefaultValue = "SenderPartyIdentifierType")]
        public string SenderPartyIdentifierTypeHeaderName
        {
            get { return (string)base["senderPartyIdentifierTypeHeaderName"]; }
            set { base["senderPartyIdentifierTypeHeaderName"] = value; }
        }

        /// <summary>
        /// The receiver party identitier header name
        /// </summary>
        [ConfigurationProperty("receiverPartyIdentifierTypeHeaderName", IsRequired = false, DefaultValue = "ReceiverPartyIdentifierType")]
        public string ReceiverPartyIdentifierTypeHeaderName
        {
            get { return (string)base["receiverPartyIdentifierTypeHeaderName"]; }
            set { base["receiverPartyIdentifierTypeHeaderName"] = value; }
        }

        /// <summary>
        /// Gets or sets the namespace
        /// </summary>
        [ConfigurationProperty("namespace", IsRequired = false, DefaultValue = Definitions.DefaultOiosiNamespace2007)]
        public string Namespace
        {
            get { return (string)base["namespace"]; }
            set { base["namespace"] = value; }
        }

        /// <summary>
        /// Gets the binding element type
        /// </summary>
        public override Type BindingElementType
        {
            get { return typeof(ClientPartyIdentifierHeaderBindingElement); }
        }

        /// <summary>
        /// Creates the binding element
        /// </summary>
        /// <returns>The binding element</returns>
        protected override BindingElement CreateBindingElement()
        {
            return new ServerPartyIdentifierHeaderBindingElement(this);
        }
    }
}
