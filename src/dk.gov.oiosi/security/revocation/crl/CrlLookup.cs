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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;


namespace dk.gov.oiosi.security.revocation.crl
{
    public class CrlLookup : IRevocationLookup
    {
        private readonly static CrlCache cache = new CrlCache();

        #region IRevocationLookup Members

        public RevocationResponse CheckCertificate(X509Certificate2 certificate)
        {
            RevocationResponse response = new RevocationResponse();
		
    	    if (certificate != null)
    	    {
    		    // Retrieve URL distribution points
    		    List<Uri> URLs = GetURLs(certificate);
        		
    		    foreach (Uri url in URLs)
    		    {
    			    CRLInstance crl = cache.getCRL(url);
        			
    			    try 
    			    {
					    if(!crl.IsRevoked(certificate))
					    {
						    response.IsValid = true;
						    response.NextUpdate = crl.getNextUpdate();
						    return response;
					    }
					    else
					    {
						    throw new CertificateRevokedException(new CertificateSubject(certificate.Subject));
					    }
				    } catch (CheckCertificateRevokedUnexpectedException e) 
				    {
					    // There was an error in checking the certificate. Try the next url.
				    }
    		    }
            }

            // If any errors happen during CRL check, then abort the processing of the message.
            response.IsValid = false;
		    return response;
        }

        #endregion

        private List<Uri> GetURLs(X509Certificate2 cert) {
		    List<Uri> urls = new List<Uri>(); 
    		
	        foreach (System.Security.Cryptography.X509Certificates.X509Extension extension in cert.Extensions) {
                if (extension.Oid.Value == X509Extensions.CrlDistributionPoints.Id) {
                    CrlDistPoint crldp = CrlDistPoint.GetInstance(Asn1Object.FromByteArray(extension.RawData));

                    DistributionPoint[] distPoints = crldp.GetDistributionPoints();

                    foreach (DistributionPoint dp in crldp.GetDistributionPoints()) {
                        GeneralNames gns = (GeneralNames) dp.DistributionPointName.Name;

                        foreach (GeneralName name in gns.GetNames()) {
                            // Only retrieve URLs
                            if (name.TagNo == GeneralName.UniformResourceIdentifier) {
                                DerStringBase s = (DerStringBase) name.Name;
                                urls.Add(new Uri(s.GetString()));
                            }
                        }
                    }
                    
                    // There is only one CRL list so faster to break.
                    break;
                }
	        }
    		
		    return urls;
	    }
    }
}
