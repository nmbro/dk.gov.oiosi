using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.communication {

    /// <summary>
    /// TODO:Remove
    /// SMTP configuration properties
    /// </summary>
    public class SMTPConfiguration: TransportConfiguration {
        private string __smtpserver;

        /// <summary>
        /// The address of the SMTP server
        /// </summary>
        public string SMTPServer {
            get { return __smtpserver; }
            set { __smtpserver = value; }
        }

        private string _email;

        /// <summary>
        /// The EMail associated with the SMTP send process
        /// </summary>
        public string EMail {
            get { return _email; }
            set { _email = value; }
        }

        private string _accountname;
            
        /// <summary>
        /// The account name
        /// </summary>
        public string AccountName {
            get { return _accountname; }
            set { _accountname = value; }
        }

        private string _password;

        /// <summary>
        /// The account password
        /// </summary>
        public string Password {
            get { return _password; }
            set { _password = value; }
        }

	
    }
}
