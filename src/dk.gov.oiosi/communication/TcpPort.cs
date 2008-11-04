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

namespace dk.gov.oiosi.communication {
    /// <summary>
    /// Represents a legal communication port number
    /// </summary>
    public struct TcpPort {
        private int _number;

        /// <summary>
        /// Constructor that takes an int as port number.
        /// It must be in the range from 0 to 65536.
        /// </summary>
        /// <param name="number"></param>
        public TcpPort(int number) {
            _number = 0;
            SetValue(number);
        }

        /// <summary>
        /// Gets and sets the port number.
        /// It must be in the range from 0 to 65536.
        /// </summary>
        public int Number {
            get { return _number; }
            set { SetValue(value); }
        }

        private void SetValue(int number) {
            if (number < 0)
                throw new InvalidTcpPortException(number);
            if (number > 65536)
                throw new InvalidTcpPortException(number);
            _number = number;
        }

        /// <summary>
        /// Overrides the base ToString and returns the portnumber as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return _number.ToString();
        }

        /// <summary>
        /// Converts TcpPort to an int
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static implicit operator int(TcpPort port) {
            return port.Number;
        }

        /// <summary>
        /// Converts int to TcpPort
        /// </summary>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        public static explicit operator TcpPort(int portNumber) {
            TcpPort port = new TcpPort(portNumber);
            return port;
        }
    }
}
