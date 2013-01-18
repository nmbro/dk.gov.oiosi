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

namespace dk.gov.oiosi.extension.wcf.Interceptor.UbiquitousProperties {

    /// <summary>
    /// Interceptor to insert a list of properties to all messages being sent (including Reliable messaging conversations)
    /// <remarks>Should be put right beneath the reliable messaging stack element</remarks>
    /// </summary>
    public class UbiquitousPropertiesBindingElement: CommonBindingElement
    {

        private Dictionary<string, object> properties = new Dictionary<string,object>();

        /// <summary>
        /// Constructor
        /// </summary>
        public UbiquitousPropertiesBindingElement(Dictionary<string,object> properties)
        {
            this.properties = properties;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UbiquitousPropertiesBindingElement()
        {}

        /// <summary>
        /// Sets the ubiquitous properties
        /// </summary>
        /// <param name="properties">The properties that are to be added on all messages</param>
        public void SetProperties(Dictionary<string, object> properties) 
        {
            foreach (KeyValuePair<string, object> p in properties)
            {
                this.properties.Add(p.Key, p.Value);
            }
        }

        /// <summary>
        /// Intercepts the request call, and adds properties
        /// </summary>
        /// <param name="interceptorMessage">The SOAP message</param>
        public override void InterceptRequest(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) 
        {
            if (this.properties != null) 
                {
                    Message msg = interceptorMessage.GetMessage();
                    foreach (KeyValuePair<string, object> p in this.properties)
                    {
                        msg.Properties.Add(p.Key, p.Value);
                    }
                }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="interceptorMessage">Not Implemented</param>
        public override void InterceptResponse(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do we intercept requests?
        /// </summary>
        public override bool DoesRequestIntercept
        {
            get
            {
                return true; 
            }
        }

        /// <summary>
        /// Do we intercept reponses?
        /// </summary>
        public override bool DoesResponseIntercept 
        {
            get
            { 
                return false;
            }
        }

        /// <summary>
        /// Do we send faults on exceptions?
        /// </summary>
        public override bool DoesFaultOnRequestException 
        {
            get 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Clone override
        /// </summary>
        /// <returns></returns>
        public override System.ServiceModel.Channels.BindingElement Clone() 
        {
            if (this.properties == null)
            {
                return new UbiquitousPropertiesBindingElement();
            }
            else
            {
                return new UbiquitousPropertiesBindingElement(this.properties);
            }
        }
    }
}
