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
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.security.lookup {

    /// <summary>
    /// Utility class for loading certificates from store
    /// </summary>
    public class CertificateLoader 
    {
        /// <summary>
        /// Gets a certificate from a certificate store from certificate subject serial number
        /// </summary>
        /// <param name="subjectSerialNumber">The subject serial number</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="storeName">The store name</param>
        /// <returns></returns>
        public X509Certificate2 GetCertificateFromStoreWithSSN(string subjectSerialNumber, StoreLocation storeLocation, StoreName storeName)
        {
            X509Certificate2 x509Certificate2 = this.GetCertificateFromStore(subjectSerialNumber, storeLocation, storeName, X509FindType.FindBySubjectName);
            return x509Certificate2;
        }

        /// <summary>
        /// Gets a certificate from the certificate store from the  given certificate store 
        /// information.
        /// </summary>
        /// <param name="certificateStoreIdentification"></param>
        /// <returns></returns>
        public X509Certificate2 GetCertificateFromCertificateStoreInformation(ICertificateStoreIdentification certificateStoreIdentification) 
        {
            X509Certificate2 x509Certificate2 = this.GetCertificateFromStoreWithSerialNumber(certificateStoreIdentification.SerialNumber, certificateStoreIdentification.StoreLocation, certificateStoreIdentification.StoreName);
            return x509Certificate2;
        }

        /// <summary>
        /// Gets a certificate from store with a serial key
        /// </summary>
        /// <param name="serialNumber">The subject serial number</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="storeName">The store name</param>
        /// <returns></returns>
        public X509Certificate2 GetCertificateFromStoreWithSerialNumber(string serialNumber, StoreLocation storeLocation, StoreName storeName)
        {
            X509Certificate2 x509Certificate2 = this.GetCertificateFromStore(serialNumber, storeLocation, storeName, X509FindType.FindBySerialNumber);
            return x509Certificate2;
        }

        /// <summary>
        /// Checks if a certificate from store with a serial key exists
        /// </summary>
        /// <param name="serialNumber">The subject serial number</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="storeName">The store name</param>
        /// <returns>bool</returns>
        public bool CertificateFromStoreWithSerialNumberExists(string serialNumber, StoreLocation storeLocation, StoreName storeName) 
        {
            bool succes;
            try 
            {
                this.GetCertificateFromStore(serialNumber, storeLocation, storeName, X509FindType.FindBySerialNumber);
                succes = true;
            } 
            catch (CertificateLoaderCertificateNotFoundException) 
            {
                succes = false;
            }
            catch (Exception exp) 
            {
                throw exp;
            }

            return succes;
        }

        /// <summary>
        /// Gets a certificate from store with a serialkey identifier
        /// </summary>
        /// <param name="subjectKeyIdentifier">The subject serial number</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="storeName">The store name</param>
        /// <returns></returns>
        public X509Certificate2 GetCertificateFromStoreWithSubjectKeyIdentifier(string subjectKeyIdentifier, StoreLocation storeLocation, StoreName storeName)
        {
            X509Certificate2 x509Certificate2 = this.GetCertificateFromStore(subjectKeyIdentifier, storeLocation, storeName, X509FindType.FindBySubjectKeyIdentifier);
            return x509Certificate2;
        }

        /// <summary>
        /// Gets a Root Certificate from store identified by
        /// </summary>
        /// <param name="rootCertificateLocation">The location information</param>
        /// <returns>The root certificate</returns>
        public X509Certificate2 GetCertificateFromStore(RootCertificateLocation rootCertificateLocation)
        {
            X509Certificate2 x509Certificate2 = this.GetCertificateFromStore(rootCertificateLocation.SerialNumber, rootCertificateLocation.StoreLocation, rootCertificateLocation.StoreName, X509FindType.FindBySerialNumber);
            return x509Certificate2;
        }

        /// <summary>
        /// Fetchs a certificate from store
        /// </summary>
        /// <param name="searchString">the search string</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="storeName">The store name</param>
        /// <param name="findType">find type</param>
        /// <returns>An x5092 certificate or null</returns>
        public X509Certificate2 GetCertificateFromStore(string searchString, StoreLocation storeLocation, StoreName storeName, X509FindType findType)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            X509Certificate2Collection result;

            try 
            {
                store.Open(OpenFlags.ReadOnly);
                result = store.Certificates.Find(findType, searchString, false);
            }
            finally
            {
                if (store != null)
                {
                    store.Close();
                }
            }

            if (result.Count < 1)
            {
                throw new CertificateLoaderCertificateNotFoundException(store, findType, searchString);
            }
            if (result.Count > 1)
            {
                throw new CertificateLoaderMultipleCertificatesFoundException(store, findType, searchString);
            }

            return result[0];
        }

        /// <summary>
        /// Fetchs a certificate from store
        /// </summary>
        /// <param name="storeName">The store name</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="issuerName">The name of the issuer</param>
        /// <returns>Returns a x5092 certificate object or null</returns>
        public X509Certificate2Collection GetCertificatesFromStore(StoreName storeName, StoreLocation storeLocation, string issuerName) 
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            // se på MultipleRootX509CertificateValidator.isValid(certifica, rootCertificate);
            // TODO - JLM  - hvis der er tre nivauer - får vi så det ønskede certificat, eller det mellemste niveau?

            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByIssuerName, issuerName, false);
            store.Close();
            return certificates;
        }
    }
}