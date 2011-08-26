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

using System.Xml.Serialization;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;
using System.Collections.Generic;

namespace dk.gov.oiosi.security {
    /// <summary>
    /// Configuration on what root certificate to use and where to find it.
    /// </summary>
    [XmlRoot("RootCertificateCollectionConfig", Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class RootCertificateCollectionConfig 
    {
        private List<RootCertificateLocation> rootCertificateLocationList;
        //private CertificateStoreIdentification _rootCertificateLocation;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public RootCertificateCollectionConfig()
        {
            //_rootCertificateLocation = new CertificateStoreIdentification();
            this.rootCertificateLocationList = new List<RootCertificateLocation>();
        }

      /*  /// <summary>
        /// Gets and set the root certificate location.
        /// </summary>
        public List<CertificateStoreIdentification> getAsList
        {
            get { return this.rootCertificateLocationList; }
            set { this.rootCertificateLocationList = value; }
        }*/

        /*
         * virker
         * 
        /// <summary>
        /// A list OIOUBL Profiles, and the mapping between unique profile name and the 
        /// corresponding tModel GUID
        /// </summary>
        [XmlArray("RootCertificateLocationCollection")]
        public RootCertificateLocation[] ProfileMappingCollection
        {
            get { return this.rootCertificateLocationList.ToArray(); }
            set { this.rootCertificateLocationList.AddRange(value); }
        }*/

        /// <summary>
        /// A list OIOUBL Profiles, and the mapping between unique profile name and the 
        /// corresponding tModel GUID
        /// </summary>
        [XmlArray("RootCertificateLocationCollection")]
        public RootCertificateLocation[] ProfileMappingCollection
        {
            get { return this.rootCertificateLocationList.ToArray(); }
            set { this.rootCertificateLocationList.AddRange(value); }
        }

        public List<RootCertificateLocation> GetAsList()
        {
            return this.rootCertificateLocationList;
        }
    }
}
