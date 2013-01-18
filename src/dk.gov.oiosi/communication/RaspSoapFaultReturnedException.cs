using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using dk.gov.oiosi.exception;
using System.ServiceModel.Channels;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.communication {
    /// <summary>
    /// Thrown when a SOAP fault was returned
    /// </summary>
    public class RaspSoapFaultReturnedException : RaspCommunicationException {
        private string _errorMessage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fault">The returned SOAP fault</param>
        public RaspSoapFaultReturnedException(Message fault) : base() 
        {
            _errorMessage = GetFaultAsString(fault);    
        }

        /// <summary>
        /// Returns the error message from the SOAP fault
        /// </summary>
        public override string Message {
            get {
                return _errorMessage;
            }
        }

        /// <summary>
        /// Saves the SOAP fault as a string
        /// </summary>
        private static string GetFaultAsString(Message fault){
            return "A SOAP fault was returned\n\n" + Utilities.GetMessageBodyAsXmlDocument(fault).OuterXml;
        }
    }
}
