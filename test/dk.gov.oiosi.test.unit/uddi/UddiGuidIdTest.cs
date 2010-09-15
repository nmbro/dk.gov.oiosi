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
using System.Linq;
using System.Text;
using NUnit.Framework;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.test.unit.uddi
{
    [TestFixture]
    public class UddiGuidIdTest {

        [Test]
        public void checkUddiGuidValidWithoutUddi() {
            Assert.IsFalse(UddiGuidId.IsValidGuidId("uddi:4ba19475-2b75-452c-969c-fdeb5f07739e", false));
            Assert.IsTrue(UddiGuidId.IsValidGuidId("4ba19475-2b75-452c-969c-fdeb5f07739e", false));
        }
        
        [Test]
        public void checkUddiGuidValidWithUddi() {
            var notValid = UddiGuidId.IsValidGuidId("4ba19475-2b75-452c-969c-fdeb5f07739e", true);
            var valid = UddiGuidId.IsValidGuidId("uddi:4ba19475-2b75-452c-969c-fdeb5f07739e", true);
            Assert.IsFalse(notValid);
            Assert.IsTrue(valid);
        }
        
        [Test]
        public void checkUddiGuidValidWithNemHandel() {
            var notValid = UddiGuidId.IsValidGuidId("4ba19475-2b75-452c-969c-fdeb5f07739e", true);
            var valid = UddiGuidId.IsValidGuidId("uddi:nemhandel.dk:4ba19475-2b75-452c-969c-fdeb5f07739e", true);
            Assert.IsFalse(notValid);
            Assert.IsTrue(valid);
        }
    }
}
