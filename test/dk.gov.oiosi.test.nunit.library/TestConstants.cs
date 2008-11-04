using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.test.nunit.library {
    public class TestConstants {
        public const string PATH_APPLICATIONRESPONSE_XML = "Resources/Documents/Examples/OIOUBL_ApplicationResponse_v2p1.xml";
        public const string PATH_CREDITNOTE_XML = "Resources/Documents/Examples/OIOUBL_CreditNote_v2p1.xml";
        public const string PATH_INVOICE_XML = "Resources/Documents/Examples/OIOUBL_Invoice_v2p1.xml";
        public const string PATH_INVOICE07_XML = "Resources/Documents/Examples/OIOXML_Invoice_v0.7.xml";
        public const string PATH_INVOICEWRONGELEMENT_XML = "Resources/Documents/Examples Invalid/OIOUBL_Invoice_v2p1_WrongElement.xml";
        public const string PATH_INVOICEWRONGNAMESPACE_XML = "Resources/Documents/Examples Invalid/OIOUBL_Invoice_v2p1_WrongNamespace.xml";
        public const string PATH_INVOICENOID_XML = "Resources/Documents/Examples Invalid/OIOUBL_Invoice_v2p1_NoId.xml";
        public const string PATH_INVOICEN7INVALIDEANNUMBER_XML = "Resources/Documents/Examples Invalid/OIOXML_Invoice_Invalid_EanNumber_v0.7.xml";
        public const string PATH_ORDER_XML = "Resources/Documents/Examples/OIOUBL_Order_v2p1.xml";
        public const string PATH_ORDERRESPONSESIMPLE_XML = "Resources/Documents/Examples/OIOUBL_OrderResponseSimple_v2p1.xml";
        public const string PATH_REMINDER_XML = "Resources/Documents/Examples/OIOUBL_Reminder_v2p1.xml";
        public const string PATH_UNKNOWNTYPE_XML = "Resources/Documents/Examples Invalid/UnknownType.xml";

        public const string PATH_SCHEMAS201 = "Resources/Schemas/OIOUBL v2.01/";
        public const string PATH_SCHEMAS07 = "Resources/Schemas/OIOXML v0.7/";

        public const string PATH_CERTIFICATE_ORGANISATION = "Resources/Certifcates/Organisation.cer";
        public const string PATH_CERTIFICATE_EMPLOYEE = "Resources/Certifcates/Employee.cer";
        public const string PATH_CERTIFICATE_DEVICE = "Resources/Certifcates/Device.cer";
        public const string PATH_CERTIFICATE_ROOT = "Resources/Certifcates/Root.cer";

        public const string ITST_CERTIFICATE_SUBJECT = "CN=NemHandel Test 2 + SERIALNUMBER=CVR:26769388-DID:00000002, O=IT- og Telestyrelsen // CVR:26769388, C=DK";
    }
}
