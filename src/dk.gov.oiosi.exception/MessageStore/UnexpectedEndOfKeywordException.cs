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
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace dk.gov.oiosi.exception.MessageStore
{
    /// <summary>
    /// This custom exception is thrown, when there was an unexpected end of a keyword
    /// </summary>
    [Serializable]
    public class UnexpectedEndOfKeywordException : Exception
    {
        /// <summary>
        /// This is the default constructor
        /// </summary>
        public UnexpectedEndOfKeywordException() : base() { }
        
        /// <summary>
        /// This constructor is used when you want to pass a custom message to the calling method
        /// </summary>
        /// <param name="message">the message to forward</param>
        public UnexpectedEndOfKeywordException(string message) : base(message) { }
        
        /// <summary>
        /// This constructor is used when you want to pass a custom message and the innerexception 
        /// to the calling method
        /// </summary>
        /// <param name="message">the message to forward</param>
        /// <param name="innerException">the innerexception of the thrown exception</param>
        public UnexpectedEndOfKeywordException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// This constructor is used when you want to pass serialized data to the calling method
        /// </summary>
        /// <param name="serializationInfo">the object holds the serialized object data about 
        /// the exception being thrown</param>
        /// <param name="streaminContext">the object contains contextual information about
        /// the source or destination</param>
        protected UnexpectedEndOfKeywordException(SerializationInfo serializationInfo, StreamingContext streaminContext) : base(serializationInfo, streaminContext) { }
        
        /// <summary>
        /// This sets a SerializationInfo with all the exception object data targeted for serialization
        /// </summary>
        /// <param name="info">the object holds the serialized object data about 
        /// the exception being thrown</param>
        /// <param name="context">the object contains contextual information about
        /// the source or destination</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
