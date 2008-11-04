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
using System.Xml.Serialization;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Represents the configuration element to setup oid to the different certificates.
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class OcesX509CertificateConfig {
        private OcesCertificatePolicyOid _personalCertificateOid;
        private OcesCertificatePolicyOid _employeeCertificateOid;
        private OcesCertificatePolicyOid _organizationCertficateOid;
        private OcesCertificatePolicyOid _functionCertficateOid;
        private OcesCertificateSubjectKey _personalCertificateSubjectKey;
        private OcesCertificateSubjectKey _employeeCertificateSubjectKey;
        private OcesCertificateSubjectKey _organizationCertificateSubjectKey;
        private OcesCertificateSubjectKey _functionCertificateSubjectKey;

        /// <summary>
        /// Default constructor used by XMLSerialization. It should not be used.
        /// </summary>
        public OcesX509CertificateConfig() {
            _personalCertificateOid = new OcesCertificatePolicyOid();
            _employeeCertificateOid = new OcesCertificatePolicyOid();
            _organizationCertficateOid = new OcesCertificatePolicyOid();
            _functionCertficateOid = new OcesCertificatePolicyOid();
            _personalCertificateSubjectKey = new OcesCertificateSubjectKey();
            _employeeCertificateSubjectKey = new OcesCertificateSubjectKey();
            _organizationCertificateSubjectKey = new OcesCertificateSubjectKey();
            _functionCertificateSubjectKey = new OcesCertificateSubjectKey();
        }

        /// <summary>
        /// Constructor that takes the oid for each of the OCES certificate types.
        /// </summary>
        /// <param name="personalCertificateOid"></param>
        /// <param name="employeeCertificateOid"></param>
        /// <param name="organizationCertficateOid"></param>
        /// <param name="functionCertficateOid"></param>
        /// <param name="personalCertificateSubjectKey"></param>
        /// <param name="employeeCertificateSubjectKey"></param>
        /// <param name="organizationCertificateSubjectKey"></param>
        /// <param name="functionCertificateSubjectKey"></param>
        public OcesX509CertificateConfig(OcesCertificatePolicyOid personalCertificateOid,
                                         OcesCertificatePolicyOid employeeCertificateOid,
                                         OcesCertificatePolicyOid organizationCertficateOid,
                                         OcesCertificatePolicyOid functionCertficateOid,
                                         OcesCertificateSubjectKey personalCertificateSubjectKey,
                                         OcesCertificateSubjectKey employeeCertificateSubjectKey,
                                         OcesCertificateSubjectKey organizationCertificateSubjectKey,
                                         OcesCertificateSubjectKey functionCertificateSubjectKey) {
            if (personalCertificateOid == null) throw new NullArgumentException("personalCertificateOid");
            if (employeeCertificateOid == null) throw new NullArgumentException("employeeCertificateOid");
            if (organizationCertficateOid == null) throw new NullArgumentException("organizationCertficateOid");
            if (functionCertficateOid == null) throw new NullArgumentException("deviceCertficateOid");
            if (personalCertificateSubjectKey == null) throw new NullArgumentException("personalCertificateSubjectKey");
            if (employeeCertificateSubjectKey == null) throw new NullArgumentException("employeeCertificateSubjectKey");
            if (organizationCertificateSubjectKey == null) throw new NullArgumentException("organizationCertificateSubjectKey");
            if (functionCertificateSubjectKey == null) throw new NullArgumentException("functionCertificateSubjectKey");
            _personalCertificateOid = personalCertificateOid;
            _employeeCertificateOid = employeeCertificateOid;
            _organizationCertficateOid = organizationCertficateOid;
            _functionCertficateOid = functionCertficateOid;
            _personalCertificateSubjectKey = personalCertificateSubjectKey;
            _employeeCertificateSubjectKey = employeeCertificateSubjectKey;
            _organizationCertificateSubjectKey = organizationCertificateSubjectKey;
            _functionCertificateSubjectKey = functionCertificateSubjectKey;
        }

        /// <summary>
        /// Gets or sets the personal OCES certificate oid.
        /// </summary>
        public OcesCertificatePolicyOid PersonalCertificateOid {
            get { return _personalCertificateOid; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.PersonalCertificateOid");
                _personalCertificateOid = value; 
            }
        }

        /// <summary>
        /// Gets or sets the employee OCES certificate oid
        /// </summary>
        public OcesCertificatePolicyOid EmployeeCertificateOid {
            get { return _employeeCertificateOid; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.EmployeeCertificateOid");
                _employeeCertificateOid = value; 
            }
        }

        /// <summary>
        /// Gets or sets the organization OCES certificate oid
        /// </summary>
        public OcesCertificatePolicyOid OrganizationCertficateOid {
            get { return _organizationCertficateOid; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.OrganizationCertficateOid");
                _organizationCertficateOid = value; 
            }
        }

        /// <summary>
        /// Gets or sets the fucntion OCES certificate oid
        /// </summary>
        public OcesCertificatePolicyOid FunctionCertficateOid {
            get { return _functionCertficateOid; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.FunctionCertficateOid");
                _functionCertficateOid = value; 
            }
        }

        /// <summary>
        /// Gets or sets the personal certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey PersonalCertificateSubjectKey {
            get { return _personalCertificateSubjectKey; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.PersonalCertificateSubjectKey");
                _personalCertificateSubjectKey = value; 
            }
        }

        /// <summary>
        /// Gets or sets the employee certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey EmployeeCertificateSubjectKey {
            get { return _employeeCertificateSubjectKey; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.EmployeeCertificateSubjectKey");
                _employeeCertificateSubjectKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the organization certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey OrganizationCertificateSubjectKey {
            get { return _organizationCertificateSubjectKey; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.OrganizationCertificateSubjectKey");
                _organizationCertificateSubjectKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the functional certificate subject key
        /// </summary>
        public OcesCertificateSubjectKey FunctionCertificateSubjetKey {
            get { return _functionCertificateSubjectKey; }
            set {
                if (value == null) throw new NullArgumentException("OcesX509CertificateConfig.FunctionCertificateSubjetKey");
                _functionCertificateSubjectKey = value;
            }
        }
    }
}
