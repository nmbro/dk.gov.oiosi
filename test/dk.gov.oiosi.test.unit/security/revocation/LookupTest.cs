using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.test.unit.security.revocation
{
    public abstract class LookupTest
    {
        //public const string medarbejdercertifikatRevoked = "Resources/Certificates/CVR30808460.Expire20130307.Test MOCES1 (medarbejdercertificat 2)(Spærret).pfx";
        public const string foces2ExpiredCertificate = "Resources/Certificates/CVR30808460.Expire20111102.TU GENEREL FOCES2 (Udløbet) (Funktionscertifikat).pfx";

        public const string foces2RevokedCertificate = "Resources/Certificates/CVR30808460.Expire20200313.TU GENEREL FOCES spaerret (Funktionscertifikat).pfx";
        public const string foces2OkayCertificate = "Resources/Certificates/CVR30808460.Expire20200130.TU GENEREL FOCES gyldig (Funktionscertifikat).pfx";

        public const string oces1RootCertificate = TestConstants.PATH_CERTIFICATE_TEST_ROOT_OCES1;
        public const string oces2RootCertificate = TestConstants.PATH_CERTIFICATE_TEST_ROOT_OCES2;
    }
}