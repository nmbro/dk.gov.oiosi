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

using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    /// Class that represents where to find a certificate in the certificate store.
    /// </summary>
    public class RootCertificateLocation : CertificateStoreIdentification
    {
        /// <summary>
        /// Default constructor for the XMLSerializer but it should not be used.
        /// </summary>
        public RootCertificateLocation() 
            : base()
        {
        }

        /// <summary>
        /// Constructor that takes the store location, store name and the serial number of 
        /// the certificate in the store as parameters.
        /// </summary>
        /// <param name="storeLocation"></param>
        /// <param name="storeName"></param>
        /// <param name="serialNumber"></param>
        public RootCertificateLocation(StoreLocation storeLocation, StoreName storeName, string serialNumber)
            : base(storeLocation, storeName, serialNumber)
        {
        }
    }
}
