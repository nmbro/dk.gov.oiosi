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
using System.Text.RegularExpressions;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security {
    
    /// <summary>
    /// Represents the Subject Number of an OCES certificate.
    /// Example format:
    /// 'SERIALNUMBER=CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test, O=TDC TOTALLØSNINGER A/S // CVR:25767535, C=DK'
    /// Note that windows certificate viewer shows these 
    /// </summary>
    public class CertificateSubject {
        private const string serialNumberKeyword = "serialNumber=";
        private readonly string _subjectString;
        private string _dnsSearchBase;
        private string _dnsSearchFilter;
        private string _serialNumberValue;
        private string _o;
        private string _c;
        private string _cn;

        /// <summary>
        /// Returns the entire subject string of an OCES x509 certificate
        /// </summary>
        public string SubjectString {
            get { return _subjectString; }
        }

        /// <summary>
        /// Gets the DNS search base part of the subject string
        /// </summary>
        public string DnsSearchBase {
            get { return _dnsSearchBase; }
        }

        /// <summary>
        /// Gets the DNS search filter part of the subject string,
        /// e.g. 'SerialNumber=CVR:19343634-RID:1164102113956'
        /// </summary>
        public string DnsSearchFilter {
            get { return _dnsSearchFilter; }
        }

        /// <summary>
        /// Gets the value of the serial number part of the subject serial number, 
        /// e.g. 'CVR:19343634-RID:1164102113956'.
        /// </summary>
        public string SerialNumberValue {
            get { return _serialNumberValue; }
        }

        /// <summary>
        /// Gets the organization
        /// </summary>
        public string O {
            get { return _o; }
        }

        /// <summary>
        /// Gets the organization value
        /// </summary>
        public string OValue {
            get { return _o.Substring(_o.IndexOf('=') + 1); }
        }

        /// <summary>
        /// Gets the country
        /// </summary>
        public string C {
            get { return _c; }
        }

        /// <summary>
        /// Gets the cn
        /// </summary>
        public string CN {
            get { return _cn; }
        }

        /// <summary>
        /// Gets the entire serial number part of the subject serial number,
        /// e.g. 'SerialNumber=CVR:19343634-RID:1164102113956'. Currently equals the DNS search filter
        /// </summary>
        public string SerialNumber {
            get { return DnsSearchFilter; }
        }

        /// <summary>
        /// Constructor. Checks the format of the certificate subject
        /// </summary>
        /// <param name="subjectString">The subject of the certificate</param>
        public CertificateSubject(string subjectString) {
            if (subjectString == null){
                throw new NullArgumentException("SubjectSerialNumber");
            }
            if (subjectString.Trim().Length < 1) {
                throw new EmptyStringException("SubjectSerialNumber");
            }

            _subjectString = subjectString.Trim();

            // 1. Get DNS base:
            GetBase();
           
            // 2. Get DNS filter:
            GetFilter();
        }

        /// <summary>
        /// Fetches the base string used in the ldap search. The string is gained from the subject string.
        /// If there is no pattern in the subject string that tells what o= and c= an exception will be thrown.
        /// </summary>
        private void GetBase() {
            const string oRegExpPattern = "(o|O)(\\s)*=([^+,])*";
            Regex o = new Regex(oRegExpPattern);
            const string cRegExpPattern = "(c|C)(\\s)*=([^+,])*";
            Regex c = new Regex(cRegExpPattern);
            const string cnRegExpPattern = "(c|C)(n|N)(\\s)*=([^+,])*";
            Regex cn = new Regex(cnRegExpPattern);
            if ((!o.IsMatch(_subjectString)) || (!c.IsMatch(_subjectString)) || (!cn.IsMatch(_subjectString)))
                throw new dk.gov.oiosi.security.ldap.PatternsDoesNotMatchException(_subjectString, new string[] { oRegExpPattern, cRegExpPattern });
            string[] subjectParts = _subjectString.Split(',');

            Match oMatch = o.Match(_subjectString);
            _o = GetAssignment(oMatch.Value).Trim();
            
            Match cMatch = c.Match(_subjectString);
            _c = GetAssignment(cMatch.Value).Trim();

            Match cnMatch = cn.Match(_subjectString);
            _cn = GetAssignment(cnMatch.Value).Trim();

            _dnsSearchBase = oMatch.Value + ", " + cMatch.Value;
        }

        /// <summary>
        /// Fetches the filter string used in the ldap search. The string is gained from the subject string.
        /// 
        /// If there is no pattern in the subject string that tells what serrialNumber= and exception will be thrown.
        /// </summary>
        /// <returns>Fetches the filter string used in the ldap search.</returns>
        private void GetFilter() {
            const string snRegExpPattern = "(((s|S)(e|E)(r|R)(i|I)(a|A)(l|L)(n|N)(u|U)(m|M)(b|B)(e|E)(r|R))|(OID.2.5.4.5))(\\s)*=(\\s)*([^+,])*";
            Regex sn = new Regex(snRegExpPattern);
            if (!sn.IsMatch(_subjectString))
                return;
            Match match = sn.Match(_subjectString);
            _serialNumberValue = GetAssignment(match.Value).Trim();
            string serialNumber = serialNumberKeyword + _serialNumberValue;
            _dnsSearchFilter = serialNumber.Trim();
        }

        private string GetAssignment(string text) {
            string[] assignmentParts = text.Split('=');
            return assignmentParts[1];
        }

        /// <summary>
        /// Returns the hashcode of the certificate subject string
        /// </summary>
        /// <returns>The computed hashcode</returns>
        public override int GetHashCode()
        {
            return _subjectString.GetHashCode();
        }

        /// <summary>
        /// Compares two instances of a certificate subject.
        /// The subject string is compared.
        /// </summary>
        /// <param name="obj">The object to compare against "this"</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj) {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;
            CertificateSubject other = (CertificateSubject)obj;

            return other._subjectString.Equals(_subjectString);
        }
    }
}
