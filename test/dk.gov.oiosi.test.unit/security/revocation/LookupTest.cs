using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.test.unit.security.revocation
{
    public abstract class LookupTest
    {
        //public const string foces1RevokedCertificate = "Resources/Certificates/CVR30808460.Expire20131101.FOCES2(revoked).pfx";
        //public const string foces1ExpiredCertificate = "Resources/Certificates/CVR30808460.Expire20111016.FOCES1.pfx";
        //public const string foces1OkayCertificate = "Resources/Certificates/CVR30808460.Expire20150804.FOCES1.pfx";

        //public const string medarbejdercertifikatRevoked = "Resources/Certificates/CVR30808460.Expire20130307.Test MOCES1 (medarbejdercertificat 2)(Spærret).pfx";

        public const string foces2RevokedCertificate = "Resources/Certificates/CVR30808460.Expire20151025.TU GENEREL FOCES2 (Spærret) (Funktionscertifikat).pfx";
        public const string foces2ExpiredCertificate = "Resources/Certificates/CVR30808460.Expire20111105.TU GENEREL FOCES2 (Udløbet) (Funktionscertifikat).pfx";

        //public const string foces2OkayCertificate = "Resources/Certificates/CVR30808460.Expire20151026.TU GENEREL FOCES2 (Funktionscertifikat).pfx";
        public const string foces2OkayCertificate = "Resources/Certificates/CVR30808460.Expire20170324.TU GENEREL FOCES2 gyldig (Funktionscertifikat).pfx";

        public const string oces1RootCertificate = TestConstants.PATH_CERTIFICATE_TEST_ROOT_OCES1;
        public const string oces2RootCertificate = TestConstants.PATH_CERTIFICATE_TEST_ROOT_OCES2;
    }
}