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
  *   Jacob Mogensen, mySupply ApS
  *
  */

namespace dk.gov.oiosi.configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Configuration of the Sending Option class. 
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class SendingOptionConfig
    {
        private bool schemaValidation = true;

        private bool schematronValidation = true;
                
        /// <summary>
        /// Default constructor used by XMLSerialization. It should not be used.
        /// </summary>
        public SendingOptionConfig()
        {
        }

        /// <summary>
        ///  Gets or sets the schema Validation
        /// </summary>
        [XmlElement("SchemaValidation")]
        public string SchemaValidation
        {
            get
            {
                return this.schemaValidation.ToString();
            }

            set
            {
                if (bool.TryParse(value, out this.schemaValidation))
                {
                    // tryParsed was succes
                }
                else
                {
                    // parsing failed, default to true
                    this.schemaValidation = true;
                }
            }
        }

        /// <summary>
        ///  Gets the indication whether or not to schema Validation
        /// </summary>
        public bool SchemaValidationBool
        {
            get
            {
                return this.schemaValidation;
            }
        }

        /// <summary>
        /// Gets or sets the schematron Validation
        /// </summary>
        [XmlElement("SchematronValidation")]
        public string SchematronValidation
        {
            get
            {
                return this.schematronValidation.ToString();
            }

            set
            {
                if (bool.TryParse(value, out this.schematronValidation))
                {
                    // tryParsed was succes
                }
                else
                {
                    // parsing failed, default to true
                    this.schematronValidation = true;
                }
            }
        }

        /// <summary>
        ///  Gets the indication whether or not to schematron Validation
        /// </summary>
        public bool SchematronValidationBool
        {
            get
            {
                return this.schematronValidation;
            }
        }
    }
}
