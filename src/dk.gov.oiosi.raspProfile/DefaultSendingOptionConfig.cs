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
  *   Jacob Mogensen, mySupply ApS
  *
  */

namespace dk.gov.oiosi.raspProfile
{
    using dk.gov.oiosi.configuration;

    /// <summary>
    /// A default LDAP connection configuration
    /// </summary>
    public class DefaultSendingOptionConfig
    {
        /// <summary>
        /// Use the dafult, live factory
        /// </summary>
        public virtual void SetIfNotExistsSendingOptionConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<SendingOptionConfig>())
            {
                // all okay
            }
            else
            {
                this.SetSendingOptionConfig();
            }
        }

        /// <summary>
        /// Fill configuration section with default live values
        /// </summary>
        public virtual void SetSendingOptionConfig()
        {
            SendingOptionConfig SendingOptionConfig = ConfigurationHandler.GetConfigurationSection<SendingOptionConfig>();
            SendingOptionConfig.SchemaValidation = bool.TrueString;
            SendingOptionConfig.SchematronValidation = bool.TrueString;
        }
    }
}