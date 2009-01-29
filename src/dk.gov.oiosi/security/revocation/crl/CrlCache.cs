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
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Org.BouncyCastle.X509;

namespace dk.gov.oiosi.security.revocation.crl
{
    class CrlCache
    {
        private readonly X509CrlParser crlParser;
	    private readonly Dictionary<Uri, CRLInstance> table;
    	
	    public CrlCache()
	    {
		    crlParser = new X509CrlParser();
		    table = new Dictionary<Uri, CRLInstance>();
	    }
    	
        [MethodImpl(MethodImplOptions.Synchronized)]
	    public CRLInstance getCRL(Uri key)
	    {
            CRLInstance instance;
		    if (!table.TryGetValue(key, out instance))
		    {
			    instance = new CRLInstance(crlParser, key);
                table.Add(key, instance);
		    }

            return instance;
	    }
    }

    class CRLInstance
    {
        private readonly X509CrlParser crlParser;
        private X509Crl data;
	    private readonly Uri url;

	    private readonly ReaderWriterLock rwl = new ReaderWriterLock();
        private readonly X509CertificateParser cp = new X509CertificateParser();

        public CRLInstance(X509CrlParser crlParser, Uri url)
	    {
            this.crlParser = crlParser;
		    this.data = null;
		    this.url = url;
	    }
    	   
	    public bool IsRevoked(X509Certificate2 cert)
	    {
	        rwl.AcquireReaderLock(0);
		    if (!cacheValid())
		    {
			    // upgrade lock manually
		        LockCookie cookie = rwl.UpgradeToWriterLock(0);   // must unlock first to obtain writelock
			    if (!cacheValid()) // recheck
			    { 
				    try 
				    {
					    upgradeData();
				    } catch (CheckCertificateRevokedUnexpectedException e) {
					    rwl.ReleaseLock();
					    throw e;
				    }
			    }

                rwl.DowngradeFromWriterLock(ref cookie);
		    }
            
		    bool isRevoked = data.IsRevoked(cp.ReadCertificate(cert.RawData));
            rwl.ReleaseLock(); 
		    return isRevoked;
	    }
    	   
	    private void upgradeData()
	    {
		    try 
		    {
		        WebRequest request = WebRequest.Create(url);
		        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
    			
			    // Only download .crl file if it is present on the server.
                if (response.StatusCode == HttpStatusCode.OK)
			    {
                    Stream stream = response.GetResponseStream();
    				
				    // Downloads the .crl file into an X509CRL object.
                    data = crlParser.ReadCrl(stream);

				    stream.Close();
    				
				    return; // Everything went well.
			    }

                throw new CheckCertificateRevokedUnexpectedException(new Exception("CRL could not be downloaded: " + response.StatusDescription));
		    } catch (IOException e) {
			    // Could not download new crl
			    throw new CheckCertificateRevokedUnexpectedException(e);
		    }
	    }
    	   
	    private bool cacheValid()
	    {
		    return data != null && data.NextUpdate.Value > DateTime.Now;
	    }

	    public DateTime getNextUpdate() 
	    {
		    rwl.AcquireReaderLock(0);
		    DateTime date = data != null ? data.NextUpdate.Value : new DateTime(0);
		    rwl.ReleaseReaderLock();
		    return date;
	    }
    }
}
