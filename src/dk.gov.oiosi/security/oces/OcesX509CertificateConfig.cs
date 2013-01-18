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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Represents the configuration element to setup oid to the different certificates.
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class OcesX509CertificateConfig {
        private OcesCertificateSubjectKey _personalCertificateSubjectKey;
        private OcesCertificateSubjectKey _employeeCertificateSubjectKey;
        private OcesCertificateSubjectKey _organizationCertificateSubjectKey;
        private OcesCertificateSubjectKey _functionCertificateSubjectKey;

        /// <summary>
        /// Default constructor used by XMLSerialization. It should not be used.
        /// </summary>
        public OcesX509CertificateConfig() {
            _personalCertificateSubjectKey = new OcesCertificateSubjectKey();
            _employeeCertificateSubjectKey = new OcesCertificateSubjectKey();
            _organizationCertificateSubjectKey = new OcesCertificateSubjectKey();
            _functionCertificateSubjectKey = new OcesCertificateSubjectKey();
        }

        /// <summary>
        /// Constructor that takes the subject key for each of the OCES certificate types.
        /// </summary>
        /// <param name="personalCertificateSubjectKey"></param>
        /// <param name="employeeCertificateSubjectKey"></param>
        /// <param name="organizationCertificateSubjectKey"></param>
        /// <param name="functionCertificateSubjectKey"></param>
        public OcesX509CertificateConfig(OcesCertificateSubjectKey personalCertificateSubjectKey,
                                         OcesCertificateSubjectKey employeeCertificateSubjectKey,
                                         OcesCertificateSubjectKey organizationCertificateSubjectKey,
                                         OcesCertificateSubjectKey functionCertificateSubjectKey) {
            if (personalCertificateSubjectKey == null) throw new NullArgumentException("personalCertificateSubjectKey");
            if (employeeCertificateSubjectKey == null) throw new NullArgumentException("employeeCertificateSubjectKey");
            if (organizationCertificateSubjectKey == null) throw new NullArgumentException("organizationCertificateSubjectKey");
            if (functionCertificateSubjectKey == null) throw new NullArgumentException("functionCertificateSubjectKey");
            _personalCertificateSubjectKey = personalCertificateSubjectKey;
            _employeeCertificateSubjectKey = employeeCertificateSubjectKey;
            _organizationCertificateSubjectKey = organizationCertificateSubjectKey;
            _functionCertificateSubjectKey = functionCertificateSubjectKey;
        }

        /// <summary>
        /// Gets or sets the personal certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey PersonalCertificateSubjectKey 
        {
            get { return this._personalCertificateSubjectKey; }
            set 
            {
                if (value == null)
                {
                    throw new NullArgumentException("OcesX509CertificateConfig.PersonalCertificateSubjectKey");
                }
                else
                {
                    this._personalCertificateSubjectKey = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the employee certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey EmployeeCertificateSubjectKey 
        {
            get { return this._employeeCertificateSubjectKey; }
            set {
                if (value == null)
                {
                    throw new NullArgumentException("OcesX509CertificateConfig.EmployeeCertificateSubjectKey");
                }
                else
                {
                    this._employeeCertificateSubjectKey = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the organization certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey OrganizationCertificateSubjectKey 
        {
            get { return this._organizationCertificateSubjectKey; }
            set {
                if (value == null)
                {
                    throw new NullArgumentException("OcesX509CertificateConfig.OrganizationCertificateSubjectKey");
                }
                else
                {
                    this._organizationCertificateSubjectKey = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the functional certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey FunctionCertificateSubjetKey 
        {
            get { return _functionCertificateSubjectKey; }
            set {
                if (value == null)
                {
                    throw new NullArgumentException("OcesX509CertificateConfig.FunctionCertificateSubjetKey");
                }
                else
                {
                    this._functionCertificateSubjectKey = value;
                }
            }
        }
    }
}
