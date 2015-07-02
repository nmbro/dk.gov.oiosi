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
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.raspProfile
{
    /// <summary>
    /// Some default Oces certificate configuration values
    /// </summary>
    public class DefaultOcesCertificate
    {
        /// <summary>
        /// Set default, live values
        /// </summary>
        public virtual void SetOcesCertificateConfig()
        {
            OcesX509CertificateConfig config = ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();

            string personalOcesCertificateSubjectKey = "PID";
            string employeeOcesCertificateSubjectKey = "RID";
            string organizationOcesCertificateSubjectKey = "UID";
            string functionOcesCertificateSubjectKey = "FID";

            config.PersonalCertificateSubjectKey.SubjectKeyString = personalOcesCertificateSubjectKey;
            config.EmployeeCertificateSubjectKey.SubjectKeyString = employeeOcesCertificateSubjectKey;
            config.OrganizationCertificateSubjectKey.SubjectKeyString = organizationOcesCertificateSubjectKey;
            config.FunctionCertificateSubjetKey.SubjectKeyString = functionOcesCertificateSubjectKey;
        }

        /// <summary>
        /// Set default, test values
        /// </summary>
        public virtual void SetTestOcesCertificateConfig()
        {
            OcesX509CertificateConfig config = ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();

            string personalOcesCertificateSubjectKey = "PID";
            string employeeOcesCertificateSubjectKey = "RID";
            string organizationOcesCertificateSubjectKey = "UID";
            string functionOcesCertificateSubjectKey = "FID";

            config.PersonalCertificateSubjectKey.SubjectKeyString = personalOcesCertificateSubjectKey;
            config.EmployeeCertificateSubjectKey.SubjectKeyString = employeeOcesCertificateSubjectKey;
            config.OrganizationCertificateSubjectKey.SubjectKeyString = organizationOcesCertificateSubjectKey;
            config.FunctionCertificateSubjetKey.SubjectKeyString = functionOcesCertificateSubjectKey;
        }

        /// <summary>
        /// Use the default values
        /// </summary>
        public virtual void SetIfNotExistsOcesCertificateConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<OcesX509CertificateConfig>())
                return;
            SetOcesCertificateConfig();
        }

        /// <summary>
        /// Use the test values
        /// </summary>
        public virtual void SetIfNotExistsTestOcesCertificateConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<OcesX509CertificateConfig>())
                return;
            SetTestOcesCertificateConfig();
        }
    }
}