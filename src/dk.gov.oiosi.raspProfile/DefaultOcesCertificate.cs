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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Some default Oces certificate configuration values
    /// </summary>
    public class DefaultOcesCertificate {

        /// <summary>
        /// Set default, live values
        /// </summary>
        public void SetOcesCertificateConfig() {
            OcesX509CertificateConfig config = ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();
            string personalOcesCertificateOid = "1.2.208.169.1.1.1.1.1";
            string employeeOcesCertificateOid = "1.2.208.169.1.1.1.2.1";
            string organizationOcesCertificateOid = "1.2.208.169.1.1.1.3.1";
            string functionOcesCertificateOid = "1.2.208.169.1.1.1.4.1";

            string personalOcesCertificateSubjectKey = "PID";
            string employeeOcesCertificateSubjectKey = "RID";
            string organizationOcesCertificateSubjectKey = "UID";
            string functionOcesCertificateSubjectKey = "FID";

            config.PersonalCertificateOid.PolicyOidString = personalOcesCertificateOid;
            config.EmployeeCertificateOid.PolicyOidString = employeeOcesCertificateOid;
            config.OrganizationCertficateOid.PolicyOidString = organizationOcesCertificateOid;
            config.FunctionCertficateOid.PolicyOidString = functionOcesCertificateOid;

            config.PersonalCertificateSubjectKey.SubjectKeyString = personalOcesCertificateSubjectKey;
            config.EmployeeCertificateSubjectKey.SubjectKeyString = employeeOcesCertificateSubjectKey;
            config.OrganizationCertificateSubjectKey.SubjectKeyString = organizationOcesCertificateSubjectKey;
            config.FunctionCertificateSubjetKey.SubjectKeyString = functionOcesCertificateSubjectKey;
        }

        /// <summary>
        /// Set default, test values
        /// </summary>
        public void SetTestOcesCertificateConfig() {
            OcesX509CertificateConfig config = ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();
            string personalOcesCertificateOid = "1.2.208.169.1.1.1.1.1";
            string employeeOcesCertificateOid = "1.2.208.169.1.1.1.2.1";
            string organizationOcesCertificateOid = "1.2.208.169.1.1.1.3.1";
            string functionOcesCertificateOid = "1.2.208.169.1.1.1.4.1";

            string personalOcesCertificateSubjectKey = "PID";
            string employeeOcesCertificateSubjectKey = "RID";
            string organizationOcesCertificateSubjectKey = "UID";
            string functionOcesCertificateSubjectKey = "DID";

            config.PersonalCertificateOid.PolicyOidString = personalOcesCertificateOid;
            config.EmployeeCertificateOid.PolicyOidString = employeeOcesCertificateOid;
            config.OrganizationCertficateOid.PolicyOidString = organizationOcesCertificateOid;
            config.FunctionCertficateOid.PolicyOidString = functionOcesCertificateOid;

            config.PersonalCertificateSubjectKey.SubjectKeyString = personalOcesCertificateSubjectKey;
            config.EmployeeCertificateSubjectKey.SubjectKeyString = employeeOcesCertificateSubjectKey;
            config.OrganizationCertificateSubjectKey.SubjectKeyString = organizationOcesCertificateSubjectKey;
            config.FunctionCertificateSubjetKey.SubjectKeyString = functionOcesCertificateSubjectKey;
        }

        /// <summary>
        /// Use the default values
        /// </summary>
        public void SetIfNotExistsOcesCertificateConfig() {
            if (ConfigurationHandler.HasConfigurationSection<OcesX509CertificateConfig>())
                return;
            SetOcesCertificateConfig();
        }

        /// <summary>
        /// Use the test values
        /// </summary>
        public void SetIfNotExistsTestOcesCertificateConfig() {
            if (ConfigurationHandler.HasConfigurationSection<OcesX509CertificateConfig>())
                return;
            SetTestOcesCertificateConfig();
        }
    }
}