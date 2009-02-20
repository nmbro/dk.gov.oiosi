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
 *   Christian Uldall Pedersen (christian.u.pedersen@accenture.com)
 *   
 */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Org.BouncyCastle.X509;

namespace dk.gov.oiosi.security.revocation.crl
{
    /// <summary>
    /// Class used for storing CRLs retrieved from URL's in X509 certificates.
    /// </summary>
    class CrlCache
    {
        private readonly X509CrlParser crlParser;
	    private readonly Dictionary<Uri, CrlInstance> table;
    	
	    public CrlCache()
	    {
		    crlParser = new X509CrlParser();
		    table = new Dictionary<Uri, CrlInstance>();
	    }
    	
        /// <summary>
        /// Retrieves the CRL located at key.
        /// </summary>
        /// <param name="key">Get the CRL located at the URL "key"</param>
        /// <returns>An instance of a CRL</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
	    public CrlInstance getCRL(Uri key)
	    {
            CrlInstance instance;
		    if (!table.TryGetValue(key, out instance))
		    {
			    instance = new CrlInstance(crlParser, key);
                table.Add(key, instance);
		    }

            return instance;
	    }
    }
}
