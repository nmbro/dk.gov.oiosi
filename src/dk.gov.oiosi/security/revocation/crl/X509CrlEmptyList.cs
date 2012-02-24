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
  */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.bouncycastle.x509;

namespace dk.gov.oiosi.security.revocation.crl
{
    /// <summary>
    /// This is a empty shallow implementation of a X509Crl instance.
    /// It is used, when the real X509Crl could not be retrived (server offline).
    /// If the server is offline ~ 5 minuter, and in that time, 100 documents is received, the list is tried to retrived 100
    /// synchroned - takes long time.
    /// 
    /// So instrad this hollow class can be used
    /// </summary>
    public class X509CrlEmptyList : Org.BouncyCastle.X509.X509Crl
    {
        private Org.BouncyCastle.Utilities.Date.DateTimeObject nextUpdate;

        public X509CrlEmptyList()
            : base(null)
        {
            nextUpdate = new Org.BouncyCastle.Utilities.Date.DateTimeObject(DateTime.Now.AddMinutes(5));
        }

        public override bool IsRevoked(Org.BouncyCastle.X509.X509Certificate cert)
        {
            return false;
        }

        public override Org.BouncyCastle.Utilities.Date.DateTimeObject NextUpdate
        {
            get
            {
                return this.nextUpdate;
            }
        }
    }
}
