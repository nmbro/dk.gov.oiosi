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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using dk.gov.oiosi.exception.MessageStore;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.exception
{
    /// <summary>
    /// The main exception of the exception handling structure. All exceptions
    /// that should use the exception handling structure must inherit this
    /// exception.
    /// </summary>
    /// <exception cref="DoubleStartOfKeywordException">See custom exception</exception>
    /// <exception cref="KeywordNotFoundException">See custom exception</exception>
    /// <exception cref="MessageToExceptionNotFoundException">See custom exception</exception>
    /// <exception cref="UnexpectedEndOfKeywordException">See custom exception</exception>
    [Serializable]
    public class MainException : System.Exception 
    {
        private ILogger logger;
        private static List<ResourceManager> resources = new List<ResourceManager>();
        private IExceptionMessageStore exceptionMessageStore = new ResourceFileExceptionMessageStore();
        private string message;

        /// <summary>
        /// Construct an exception. It assumes that the error message text is 
        /// stored in the main resource file of this module
        /// </summary>
        public MainException() 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(new Dictionary<string, string>(), null);
        }

        /// <summary>
        /// Constructs an exception with a resource file but with no keywords. The
        /// error message can be in either the external resource or the main resource
        /// of this module
        /// </summary>
        /// <param name="resourceManager">the resourcefile</param>
        public MainException(ResourceManager resourceManager) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(resourceManager, new Dictionary<string, string>(), null);
        }

        /// <summary>
        /// Constructs an exception with keywords, this assumes that the message
        /// text is stored in the main resource file of this module
        /// </summary>
        /// <param name="keywords">the keyword for the message</param>
        public MainException(Dictionary<string, string> keywords) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(keywords, null);
        }

        /// <summary>
        /// Constructs an exception with an innner exception. It assumes that the 
        /// error message text is stored in the main resource file
        /// </summary>
        /// <param name="innerException">the innerexception to throw</param>
        public MainException(Exception innerException)
            : base("", innerException)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(new Dictionary<string, string>(), innerException);
        }

        /// <summary>
        /// Constructs an exception with external error messages and keywords. The
        /// error message can be in either the external resource or the main resource
        /// of this module
        /// </summary>
        /// <param name="resourceManager">the resourcefile</param>
        /// <param name="keywords">the keyword for the message</param>
        public MainException(ResourceManager resourceManager, Dictionary<string, string> keywords) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(resourceManager, keywords, null);
        }

        /// <summary>
        /// Constructs an exception with external error messages and an inner exception. 
        /// The error message can be in either the external resource or the main 
        /// resource of the module
        /// </summary>
        /// <param name="resourceManager">the resourcefile</param>
        /// <param name="innerException">the innerexception to throw</param>
        public MainException(ResourceManager resourceManager, Exception innerException)
            : base("", innerException) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(resourceManager, new Dictionary<string, string>(), innerException);
        }

        /// <summary>
        /// Construct an exception with keywords and an innner exception. It assumes that
        /// the error message text is stored in the main resource file of this module
        /// </summary>
        /// <param name="keywords">the keyword for the message</param>
        /// <param name="innerException">the innerexception to throw</param>
        public MainException(Dictionary<string, string> keywords, Exception innerException)
            : base("", innerException) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(keywords, innerException);
        }

        /// <summary>
        /// Constructs an exception with external error messages, keywords and an inner 
        /// exception. The error message can be in either the external resource or the 
        /// main resource of this module
        /// </summary>
        /// <param name="resourceManager">the resourcefile</param>
        /// <param name="keywords">the keyword for the message</param>
        /// <param name="innerException">the innerexception of the thrown exception</param>
        public MainException(ResourceManager resourceManager, Dictionary<string, string> keywords, Exception innerException)
            : base("", innerException) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.SetMessage(resourceManager, keywords, innerException);
        }

        #region Standard Exception implementation
        
        /// <summary>
        /// This is used when you want to throw a custom message
        /// </summary>
        /// <param name="message">the message to throw</param>
        public MainException(string message) : base(message)
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }
        
        /// <summary>
        /// This is used when you want to throw the custom message and the innerexception of the thrown
        /// exception
        /// </summary>
        /// <param name="message">the message to throw</param>
        /// <param name="innerException">the innerexception of the thrown exception</param>
        public MainException(string message, Exception innerException) 
            : base(message, innerException) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serializationInfo">Serialization info</param>
        /// <param name="streaminContext">The streaming context</param>
        protected MainException(SerializationInfo serializationInfo, StreamingContext streaminContext) 
            : base(serializationInfo, streaminContext)
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }

        
        /// <summary>
        /// This sets a SerializationInfo with all the exception object data targeted for serialization 
        /// </summary>
        /// <param name="info">the object holds the serialized object data about 
        /// the exception being thrown</param>
        /// <param name="context">the object contains contextual information about
        /// the source or destination</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            base.GetObjectData(info, context); 
        }
        
        #endregion

        /// <summary>
        /// Property to get the error message
        /// </summary>
        public override string Message {
            get { return message; }
        }

        private void SetMessage(Dictionary<string, string> keywords, Exception originalException) {
            Type exceptionType = this.GetType();
            try
            {
                message = exceptionMessageStore.GetExceptionMessage(resources, exceptionType, keywords);
            }
            catch
            {
                if (originalException == null)
                {
                    this.logger.Error("No error description exist for the error type.");
                    this.message = "No translated error description exist for the error..";
                }
                else
                {
                    this.logger.Error("No error description exist for the error type: '" + exceptionType + "'.");
                    this.message = "No translated error description exist for the error: '" + exceptionType + "'.";
                }
                
                
            }            
        }

        private void SetMessage(ResourceManager resource, Dictionary<string, string> keywords, Exception originalException)
        {
            Type exceptionType = this.GetType();
            List<ResourceManager> collectiveResources = new List<ResourceManager>(resources);
            collectiveResources.Add(resource);
            try
            {
                message = exceptionMessageStore.GetExceptionMessage(collectiveResources, exceptionType, keywords);
            }
            catch
            {
                if (originalException == null)
                {
                    this.logger.Error("No error description exist for the error type.");
                    this.message = "No translated error description exist for the error.";
                }
                else
                {
                    this.logger.Error("No error description exist for the error type: '" + exceptionType + "'.");
                    this.message = "No translated error description exist for the error: '" + exceptionType + "'.";
                }
            }            
        }
    }
}