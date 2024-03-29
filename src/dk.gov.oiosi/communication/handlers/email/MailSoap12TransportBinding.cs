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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Contains the information needed to create a mail
    /// compliant with the "Smtp/MIME Base64 Transport Binding for SOAP 1.2"
    /// protocol
    /// </summary>
    public class MailSoap12TransportBinding {
        private string _subject = "Base64 encoded SOAP 1.2";
        private string _body = "This message was auto generated " + DateTime.Now.ToString() + " in accordance with the OIO SOAP over SMTP protocol(http://www.oio.dk/files/OIO_SOAP_SMTP_Binding.pdf).\n\nThe attachment contains XML/SOAP and should under no circumstances be altered.";
        private string _from;
        private string _to;
        private string _replyTo;
        private string _messageId;

        #region Properties

        /// <summary>
        /// The mail subject line
        /// </summary>
        public string Subject {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// Main mail body (always returns an empty string, which is what the "Smtp/MIME Base64 Transport Binding for SOAP 1.2" requires)
        /// </summary>
        public string Body {
            get { return _body; }
            set { _body = value; }
        }

        /// <summary>
        /// The sender
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// The receiver
        /// </summary>
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        /// <summary>
        /// URI to reply to
        /// </summary>
        public string ReplyTo
        {
            get { return _replyTo; }
            set { _replyTo = value; }
        }

        /// <summary>
        /// The Message-Id of the mail
        /// </summary>
        public string MessageId {
            get { return _messageId; }
        }


        /// <summary>
        /// In-Reply-To header. Should refer to the Message-Id of the mail to which this is a reply.
        /// </summary>
        public string InReplyTo {
            get { return _inReplyTo; }
            set { _inReplyTo = value; }
        }
        private string _inReplyTo;
        

        /// <summary>
        /// The attachment to be added to the mail
        /// </summary>
        public MailSoap12TransportBindingAttachment Attachment
        {
            get { return _attachment; }
        }
        private MailSoap12TransportBindingAttachment _attachment;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">A WCF Message that will be used to extract the necessary fields from</param>
        [Obsolete("Please use the MailSoap12TransportBinding(Message msg, string fromAddress) constuctor instead due to gmail compability")]
        public MailSoap12TransportBinding(Message msg) 
        {
            // Set the from and to headers
            if (msg.Headers.From != null)
            {
                _from = TrimMailAddress(msg.Headers.From.Uri);
            }

            if (msg.Headers.To != null)
            {
                _to = TrimMailAddress(msg.Headers.To);
            }

            // In case there happens to be a replyTo header, use it
            if (msg.Headers.ReplyTo != null)
                _replyTo = msg.Headers.ReplyTo.ToString();
   
            // Create a new ID for the mail and uses the from address as ID due to the specification
            _messageId = Guid.NewGuid().ToString() + "@127.0.0.1";

            // Create the attachment
            _attachment = new MailSoap12TransportBindingAttachment(msg);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">A WCF Message that will be used to extract the necessary fields from</param>
        /// <param name="fromAddress">The from address that has to be used to generate the id used in the email transport</param>
        public MailSoap12TransportBinding(Message msg, string fromAddress) {
            _from = fromAddress;

            // to headers
            if (msg.Headers.To != null) {
                _to = TrimMailAddress(msg.Headers.To);
            }

            // In case there happens to be a replyTo header, use it
            if (msg.Headers.ReplyTo != null)
                _replyTo = msg.Headers.ReplyTo.ToString();

            // Create a new ID for the mail and uses the from address as ID due to the specification
            _messageId = Guid.NewGuid().ToString() + "_" + _from;

            // Create the attachment
            _attachment = new MailSoap12TransportBindingAttachment(msg);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">A WCF Message that will be used to extract the necessary fields from</param>
        /// <param name="from">Sender</param>
        /// <param name="to">Receiver</param>
        /// <param name="replyTo">To whom one should reply</param>
        /// <param name="messageId">The Message-Id of the mail</param>
        public MailSoap12TransportBinding(Message msg, string from, string to, string replyTo, string messageId) { 
            _from = from;
            _to = to;
            _replyTo = replyTo;
            _messageId = messageId;
            _attachment = new MailSoap12TransportBindingAttachment(msg);
        }


        /// <summary>
        /// Trims an URI (mailto:a@b.com/c/d) down to a simple mail address: a@b.com
        /// </summary>
        public static string TrimMailAddress(Uri address)
        {

            // Make sure the address uses the right protocol
            if (String.Compare(address.Scheme, "mailto", true) != 0)
            {
                
                throw new MailBindingAddressNotCompliantException(address.ToString());
            }

            // Make sure we can read the actual address
            if (address.UserInfo == null || address.Host == null)
            {
                throw new MailBindingAddressNotCompliantException(address.ToString());
            }
            else
            {
                return address.UserInfo + "@" + address.Host;
            }
        }



        /// <summary>
        /// Trims an URI (mailto:a@b.com/c/d) down to a simple mail address: a@b.com
        /// </summary>
        public static string TrimMailAddress(string address) {
            if (address.StartsWith("mailto:"))
                address = address.Substring("mailto:".Length);
            if (address.IndexOf('?') >= 0)
                address = address.Substring(0, address.IndexOf('?') + 1);
            else if (address.IndexOf('/') >= 0)
                address = address.Substring(0, address.IndexOf('/') + 1);
            

            return address;
        }
    }
}