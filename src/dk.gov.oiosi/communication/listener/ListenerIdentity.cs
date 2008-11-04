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
using dk.gov.oiosi.security.oces;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.transport;

namespace dk.gov.oiosi.communication.listener {

    /// <summary>
    /// Represents the identity of a listener
    /// </summary>
    public class ListenerIdentity {

        /// <summary>
        /// What type of messages (http/mailto) are we listening for
        /// </summary>
        protected ProtocolType pProtocolType = ProtocolType.email;

        /// <summary>
        /// The listener certificate
        /// </summary>
        protected OcesX509Certificate pListenerCertificate;

        /// <summary>
        /// The transport element used for the listener (can be used to override default transport settings)
        /// </summary>
        protected ITransport pTransport;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ServiceType">The type of the service</param>
        /// <param name="transportBinding">binding transport</param>
        /// <param name="listenerCertificate">certificate of the listener</param>
        public ListenerIdentity(Type ServiceType, ITransport transportBinding, OcesX509Certificate listenerCertificate) {
            this.pTransport = transportBinding;
            this.pListenerCertificate = listenerCertificate;
            this.pServiceType = ServiceType;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportBinding">binding transport</param>
        /// <param name="listenerCertificate">certificate of the listener</param>
        public ListenerIdentity(ITransport transportBinding, OcesX509Certificate listenerCertificate) {
           this.pTransport = transportBinding;
           this.pListenerCertificate = listenerCertificate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listenerCertificate">certificate of the listener</param>
        public ListenerIdentity(OcesX509Certificate listenerCertificate) {
            this.pListenerCertificate = listenerCertificate;
            
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ServiceType">The type of the service</param>
        /// <param name="listenerCertificate">certificate of the listener</param>
        public ListenerIdentity(Type ServiceType, OcesX509Certificate listenerCertificate) {
            this.pListenerCertificate = listenerCertificate;
            this.pServiceType = ServiceType;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceType">The type of the service</param>
        public ListenerIdentity(Type serviceType) {
            this.pServiceType = serviceType;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListenerIdentity() {
        }

        /// <summary>
        /// Property for transport
        /// </summary>
        public ITransport Transport {
            get {
                return pTransport;
            }
            set {
                pTransport = value;
            }
        }

        /// <summary>
        /// Property for listener certificate
        /// </summary>
        public OcesX509Certificate ListenerCertificate {
            get {
                return pListenerCertificate;
            }
            set {
                pListenerCertificate = value;
            }
        }
        
        /// <summary>
        /// Property for the protocol
        /// </summary>
        public ProtocolType Protocol {
            get {
                return pProtocolType;
            }
            set {
                pProtocolType = value;
            }
        }

        /// <summary>
        /// The type of the service, i.e. the service interface it implements
        /// </summary>
        public Type ServiceType {
            get { return pServiceType; }
            set { pServiceType = value; }
        }
        private Type pServiceType;
    }
}