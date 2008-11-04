/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compl,iance with the License. You may obtain
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
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi.ranges {


    /// <summary>
    /// Configuration of gateway endpoint key ranges, i.e. a range of keys that a
    /// specific gateway handles
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class GatewayRange {
        private string _rangeStart = "";
        private string _rangeEnd = "";
        private GatewayRegistrationParameters _registrationParameters = null;


        /// <summary>
        /// Default constructor
        /// </summary>
        public GatewayRange() {}

        /// <summary>
        /// Checks if an EAN number is within the range
        /// </summary>
        /// <param name="eanNumber"></param>
        /// <returns>True if the EAN number is within the range</returns>
        public bool IsInRange(string eanNumber) {
            long ean = long.Parse(eanNumber);
            if (ean >= RangeStartInt && ean <= RangeEndInt) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets first number of the range, inclusive
        /// </summary>
        public string RangeStart {
            get { return _rangeStart; }
            set { _rangeStart = value; }
        }

        /// <summary>
        /// Gets or sets last number of the range, inclusive
        /// </summary>
        public string RangeEnd {
            get { return _rangeEnd; }
            set { _rangeEnd = value; }
        }

        /// <summary>
        /// First number of the range, inclusive, as a long integer.
        /// In case of conversion errors, an exception is thrown. 
        /// In case of an empty setting in config, 0 is returned.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public long RangeStartInt {
            get {
                if (String.IsNullOrEmpty(_rangeStart)) return 0;
                else return long.Parse(_rangeStart);
            }
        }

        /// <summary>
        /// Last number of the range, inclusive, as a long integer.
        /// In case of conversion errors, an exception is thrown. 
        /// In case of an empty setting in config, 0 is returned.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public long RangeEndInt {
            get {
                if (String.IsNullOrEmpty(_rangeEnd)) return 0;
                else return long.Parse(_rangeEnd);
            }
        }

        /// <summary>
        /// The parameters used to look up the gateway registration in UDDI, 
        /// when combined with the parameters given by the user/client requesting
        /// the UDDI lookup
        /// </summary>
        public GatewayRegistrationParameters GatewayRegistrationParameters {
            get { return _registrationParameters; }
            set { _registrationParameters = value; }
        }
    }
}
