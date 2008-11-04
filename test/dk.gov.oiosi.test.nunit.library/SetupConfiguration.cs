using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.raspProfile;

namespace dk.gov.oiosi.test.nunit.library {
    public class SetupConfiguration {
        public SetupConfiguration() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
        }
    }
}
