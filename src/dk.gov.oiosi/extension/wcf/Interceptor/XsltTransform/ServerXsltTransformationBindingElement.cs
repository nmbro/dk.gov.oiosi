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
using System.Configuration;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schematron;
using dk.gov.oiosi.xml.xslt;

namespace dk.gov.oiosi.extension.wcf.Interceptor.XsltTransform {
    /// <summary>
    /// The pruning interceptor implementation.
    /// </summary>
    class ServerXsltTransformationBindingElement : CommonBindingElement {
        private ServerXsltTransformationBindingExtensionElement _configuration;
        private XsltUtility _xsltUtility;
        private DocumentTypeConfigSearcher _searcher;

        /// <summary>
        /// Constructor that takes a PruningBindingExtensionElement which contains 
        /// configuration information to the pruning interceptor.
        /// </summary>
        /// <param name="configuration"></param>
        public ServerXsltTransformationBindingElement(ServerXsltTransformationBindingExtensionElement configuration) {
            _configuration = configuration;
            _xsltUtility = new XsltUtility();
            _searcher = new DocumentTypeConfigSearcher();
        }

        #region BindingElement overrides
        /// <summary>
        /// Clones the binding element. By making a shallow copy.
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone() {
            return new ServerXsltTransformationBindingElement(_configuration);
        }

        #endregion

        #region IChannelInterceptor Members

        /// <summary>
        /// Returns whether the request should be intercepted for this interceptor.
        /// This is allways on for the pruning interceptor.
        /// </summary>
        public override bool DoesRequestIntercept {
            get { return true; }
        }

        /// <summary>
        /// Returns whether the response should be intercepted for this interceptor.
        /// This is allways off for the pruning interceptor.
        /// </summary>
        public override bool DoesResponseIntercept {
            get { return false; }
        }

        /// <summary>
        /// Returns whether a fault should be send when an exception is thrown or a
        /// property should be added to the message.
        /// </summary>
        public override bool DoesFaultOnRequestException {
            get { return _configuration.FaultOnTransformationException; }
        }

        /// <summary>
        /// Incepts the request and punes the input.
        /// </summary>
        /// <param name="message"></param>
        public override void InterceptRequest(InterceptorMessage message) {
            try {
                XmlDocument body = message.GetBody();
                CompiledXslt styleSheet = LoadStyleSheet(body);
                XmlDocument transformedBody = _xsltUtility.TransformXml(body, styleSheet);
                message.SetBody(transformedBody);
                if (_configuration.PropagateOriginalMessage) {
                    OriginalBody orgBody = new OriginalBody(body);
                    message.AddProperty(ServerXsltTransformationBindingExtensionElement.ORIGINALBODYPROPERTYNAME, orgBody);
                }
            } catch (Exception ex) {
                throw new XsltTransformFailedException(ex);
            }
        }

        /// <summary>
        /// The response interception is not used in the current version of the 
        /// library
        /// </summary>
        /// <param name="message"></param>
        public override void InterceptResponse(InterceptorMessage message) { }

        #endregion

        private CompiledXslt LoadStyleSheet(XmlDocument body) {
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(body);
            //string path = documentType.XsltTransformStylesheetPath;

            string stylesheetPath;
            string basePath = ConfigurationManager.AppSettings["ResourceBasePath"];
            if (string.IsNullOrEmpty(basePath))
            {
                stylesheetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, documentType.XsltTransformStylesheetPath);
            }
            else
            {
                stylesheetPath = Path.Combine(basePath, documentType.XsltTransformStylesheetPath);
            }

            CompiledXslt compiledXslt = new CompiledXslt(new FileInfo(stylesheetPath));

            return compiledXslt;
        }
    }
}
