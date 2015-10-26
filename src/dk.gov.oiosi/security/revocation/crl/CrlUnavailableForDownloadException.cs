using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace dk.gov.oiosi.security.revocation.crl
{
    public class CrlUnavailableForDownloadException: RevocationException
    {
        public CrlUnavailableForDownloadException(string cerUrl)
            : base(MakeDir(cerUrl))
        { }

        private static Dictionary<string,string> MakeDir(string cerUrl)
        {
            Dictionary<string,string> dir = new  Dictionary<string,string>();
            dir.Add("url", cerUrl);

            return dir;
        }
    }
}
