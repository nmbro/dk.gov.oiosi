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
  *
  */

using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.communication.listener {

    /// <summary>
    /// Represents a listener request
    /// </summary>
    public class ListenerRequest {
        private const string NOMESSAGEIDENTIFIERFOUND = "Der var ingen besked identifikation";
        private Dictionary<Type, object> _properties = new Dictionary<Type, object>();
        
        /// <summary>
        /// Returns the request message from the remote party
        /// </summary>
        public OiosiMessage RequestMessage {
            get { return _requestMessage; }
        }
        private readonly OiosiMessage _requestMessage;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestMessage">The incoming message</param>
        public ListenerRequest(OiosiMessage requestMessage) {
            _requestMessage = requestMessage;
        }

        /// <summary>
        /// Returns any custom property of the given type if it exists.
        /// If not, the method throws an exception.
        /// </summary>
        /// <typeparam name="PropertyType">The type of the property object to look for</typeparam>
        /// <returns>Returns any custom property of the given type if it exists.
        /// If not, the method throws an exception.</returns>
        public PropertyType GetProperty<PropertyType>() where PropertyType : class {
            Type type = typeof(PropertyType);
            PropertyType property = (PropertyType)_properties[type];
            return property;
        }

        /// <summary>
        /// Tries to get a property of a given type. If it exists it returns 
        /// true and if not it returns false. The property parameter is set
        /// to the property wanted.
        /// </summary>
        /// <typeparam name="PropertyType">The property type wanted</typeparam>
        /// <param name="property">The property reference if it exists</param>
        /// <returns></returns>
        public bool TryGetProperty<PropertyType>(out PropertyType property) where PropertyType : class {
            Type type = typeof(PropertyType);
            object propertyObject = null;
            bool success = _properties.TryGetValue(type, out propertyObject);
            property = (PropertyType)propertyObject;
            return success;
        }

        /// <summary>
        /// Adds a property to the collection
        /// </summary>
        public void AddProperty(object property) {
            _properties.Add(property.GetType(), property);
        }

        /// <summary>
        /// Gets the message identification from the profile.
        /// </summary>
        /// <returns></returns>
        public string GetMessageIdentifier() {
            try {
                XmlQualifiedName key = new XmlQualifiedName("MessageIdentifier", dk.gov.oiosi.common.Definitions.DefaultOiosiNamespace2007);

                MessageHeader header = null;
                //Handles messages without the header by returning a string telling 
                //that no message identification could be found
                if (!RequestMessage.MessageHeaders.TryGetValue(key, out header))
                    return NOMESSAGEIDENTIFIERFOUND;
                string messageHeader = header.ToString();
                XmlDocument document = new XmlDocument();
                document.LoadXml(messageHeader);
                string messageIdentifierValue = document.DocumentElement.FirstChild.Value;
                return messageIdentifierValue;
            } 
            catch (Exception ex) {
                throw new Exception("Failed to get message identification", ex);
            }
        }
    }
}