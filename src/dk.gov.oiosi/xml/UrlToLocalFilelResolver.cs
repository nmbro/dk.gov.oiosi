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
using System.IO;
using System.Xml;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.xml
{
    /// <summary>
    /// Defines the RASP url resolver when looking for schemas. 
    /// Instead of looking for the schemas online it will attempt to find them locally.
    /// </summary>
    public class UrlToLocalFilelResolver : XmlUrlResolver
    {
        private ILogger logger;
        private string basePathForResources;

        /// <summary>
        /// Constructor that takes the directory where the schema are located
        /// as parameter.
        /// </summary>
        public UrlToLocalFilelResolver(string basePathForResources)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.basePathForResources = basePathForResources;
        }

        /// <summary>
        /// Constructor that takes the directory where the schema are located
        /// as parameter.
        /// </summary>
        public UrlToLocalFilelResolver(DirectoryInfo basePathForResources)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.basePathForResources = basePathForResources.FullName;
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            return base.GetEntity(absoluteUri, role, ofObjectToReturn);
        }


        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            Uri uri = null;

            if (baseUri != null && !string.IsNullOrEmpty(baseUri.OriginalString))
            {
                FileInfo baseFileInfo = new FileInfo(baseUri.LocalPath);
                if (baseFileInfo.Exists)
                {
                    uri = base.ResolveUri(baseUri, relativeUri);
                }
            }

            if (uri == null)
            {
                string fullPath = Path.Combine(this.basePathForResources, relativeUri);
                FileInfo fileInfo = new FileInfo(fullPath);

                if (fileInfo.Exists)
                {
                    Uri newUri = new Uri(fileInfo.FullName);
                    uri = base.ResolveUri(newUri, relativeUri);
                }
                else
                {
                    string baseUriString = string.Empty;
                    if (baseUri != null)
                    {
                        baseUriString = baseUri.LocalPath;
                    }

                    this.logger.Warn("The resource identified by the relative uri '" + relativeUri + "' and the baseUri '" + baseUriString + "' and base path '" + this.basePathForResources + "' could not be located. The resource was not found.");
                }
            }

            return uri;
        }
    }
}