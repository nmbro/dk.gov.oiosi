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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System.Collections.Specialized;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Contains the information needed to create a mail attachment
    /// compliant with the "Smtp/MIME Base64 Transport Binding for SOAP 1.2"
    /// protocol
    /// </summary>
    public class MailSoap12TransportBindingAttachment
    {
        #region Properties
        /// <summary>
        /// The x-service-path custom MIME header. Contains the path to the service to be called.
        /// </summary>
        public string XServicePath
        {
            get { return _xServicePath; }
        }
        private string _xServicePath;

        /// <summary>
        /// The content-transfer-encoding MIME header. Always "base64".
        /// </summary>
        public string ContentTransferEncoding
        {
            get { return "base64"; }
        }

        /// <summary>
        /// The content-type MIME header. Always "application/soap+xml; charset=UTF-8; action=[SOAP action]".
        /// </summary>
        public string ContentType
        {
            get { return "application/soap+xml; charset=UTF-8; action=\"" + _action + "\""; }
        }
        private string _action;

        /// <summary>
        /// The content-description MIME header. Contains the path to the service to be called.
        /// </summary>
        public string ContentDescription
        {
            get { return _contentDescription; }
        }
        private string _contentDescription;

        /// <summary>
        /// The actual content of the MIME attachment as a byte array. Encoded according to the "Smtp/MIME Base64 Transport Binding for SOAP 1.2" protocol.
        /// </summary>
        public byte[] Data
        {
            get {

                MemoryStream stream = new MemoryStream();
                
                // Get a buffered copy
                MessageBuffer bufferCopy = _wcfMessage.CreateBufferedCopy(int.MaxValue);
                Message copy = bufferCopy.CreateMessage();
                _wcfMessage = bufferCopy.CreateMessage();
                bufferCopy.Close();


                MessageEncoder encoder = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, Encoding.UTF8).CreateMessageEncoderFactory().CreateSessionEncoder();
                encoder.WriteMessage(copy, stream);
                byte[] data = stream.ToArray();
                stream.Close();

                return data;
            }
        }

        /// <summary>
        /// The SOAP as a WCF Message object
        /// </summary>
        public Message WcfMessage {
            get {
                return _wcfMessage;
            }
        }
        private Message _wcfMessage;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">A WCF Message that will be used to extract the necessary fields from</param>
        public MailSoap12TransportBindingAttachment(Message msg)
        {
            //0. Null check
            if (msg == null)
                throw new dk.gov.oiosi.exception.NullArgumentException("msg");

            // 1. Check that there is an action header
            if (msg == null || msg.Headers.Action == null)
            {
                throw new MailBindingFieldMissingException("Action");
            }
            else
            {
                _action = msg.Headers.Action;
            }

            // 2. Check if there is a to header
            if (msg.Headers.To != null)
            {
                // Is there a path directly attached to the mail address?
                if (msg.Headers.To.LocalPath.Trim('/').Length > 0)
                {
                    _xServicePath = _contentDescription = msg.Headers.To.LocalPath;
                }
                // If not, was a path given as a query?
                else if (msg.Headers.To.Query.Length > 0)
                {
                    NameValueCollection nvc = HttpUtility.ParseQueryString(msg.Headers.To.Query.ToLower());
                    string servicePath = nvc.Get("x-service-path");
                    if (servicePath == null)
                    {
                        _contentDescription = "\"\"";
                    }
                    else
                    {
                        _contentDescription = "\"" + servicePath.TrimEnd('/') + "\"";
                    }
                }
                // No path was given so Content-Description should be: Content-Description=""
                else
                {
                    _contentDescription = "\"\"";
                }
            }

            _wcfMessage = msg;
        }

    }
}