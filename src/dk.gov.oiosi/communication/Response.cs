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
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;

namespace dk.gov.oiosi.communication {

    /// <summary>
    /// Response gotten from an OIOSI http or email endpoint.
    /// </summary>
    public class Response : dk.gov.oiosi.communication.IResponse {

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">the message</param>
        public Response(Message msg) {
            // Get the xml body
            XmlDocument messageXml = ExtractXmlFromWcfMessage(msg);

            _responseMessage = new OiosiMessage(messageXml);

            // Who was the response from?
            if (msg.Headers.From != null)
                _responseUri = msg.Headers.From.Uri;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The actual response message
        /// </summary>
        public OiosiMessage ResponseMessage {
            get { return _responseMessage; }
        }
        OiosiMessage _responseMessage;

        /// <summary>
        /// Method for getting a specific property.
        /// Not implemented for the async profile
        /// </summary>
        /// <typeparam name="PropertyType">the property type</typeparam>
        /// <returns>the property type</returns>
        public PropertyType GetProperty<PropertyType> () where PropertyType : class {
            return (PropertyType)_properties[typeof(PropertyType)];
        }


        /// <summary>
        /// Adds a property to the collection
        /// </summary>
        public void AddProperty(object property) {
            _properties.Add(property.GetType(), property);
        }
        private Dictionary<Type, object> _properties = new Dictionary<Type, object>();

        /// <summary>
        /// The remote endpoint from which the response was gotten
        /// </summary>
        public Uri ResponseUri { 
            get { return _responseUri; } 
        }
        private Uri _responseUri;

        #endregion


        /// <summary>
        /// Extracts the XML body from a WCF Message
        /// </summary>
        private XmlDocument ExtractXmlFromWcfMessage(Message msg) {
            XmlDocument doc = new XmlDocument();

            if (msg == null || msg.IsEmpty) 
                return null;
             
            doc.Load(msg.GetReaderAtBodyContents());
            return doc;
        }
    }
}