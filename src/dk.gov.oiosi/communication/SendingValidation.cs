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
  *   Jacob Mogensen, mySupply
  * 
  */

namespace dk.gov.oiosi.communication
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using dk.gov.oiosi.configuration;
    using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema;
    using System.Xml;
    using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron;
using dk.gov.oiosi.logging;

    public class SendingValidation
    {
        private ILogger logger;

        public SendingValidation()
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }

        public bool Validate(OiosiMessage oiosiMessage)
        {
            bool result = true;
            SendingOptionConfig sendingOptionConfig = ConfigurationHandler.GetConfigurationSection<SendingOptionConfig>();
            this.logger.Trace("Strart SendingValidation");

            if (sendingOptionConfig.SchemaValidationBool)
            {
                this.logger.Trace("Strart schema");
                SchemaValidatorWithLookup schemaValidatorWithLookup = new SchemaValidatorWithLookup();
                //string document = oiosiMessage.MessageXml
                XmlDocument document = oiosiMessage.MessageXml;
                schemaValidatorWithLookup.Validate(document);
                result = true;                
            }

            if (result && sendingOptionConfig.SchematronValidationBool)
            {
                this.logger.Trace("Strart schematron");
                SchematronValidatorWithLookup schematronValidatorWithLookup = new SchematronValidatorWithLookup();
                //string document = oiosiMessage.MessageString;
                XmlDocument document = oiosiMessage.MessageXml;
                schematronValidatorWithLookup.Validate(document);
                result = true;
            }

            this.logger.Trace("Finish SendingValidation");
            return result;
        }
    }
}
