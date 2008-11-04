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
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace dk.gov.oiosi.xml {
    /// <summary>
    /// Defines the RASP url resolver when looking for schemas. 
    /// Instead of looking for the schemas online it will attempt to find them locally.
    /// </summary>
    public class UrlToLocalFilelResolver : XmlUrlResolver {
        private DirectoryInfo _localSchemaDirectory;

        /// <summary>
        /// Constructor that takes the directory where the schema are located
        /// as parameter.
        /// </summary>
        public UrlToLocalFilelResolver(DirectoryInfo localSchemaDirectory) {
            _localSchemaDirectory = localSchemaDirectory;
        }

        /// <summary>
        /// Overrides the GetEntitiy method of the XmlUrlResolver to implement custom code
        /// for the RASP url resolver.
        /// </summary>
        /// <param name="absoluteUri"></param>
        /// <param name="role"></param>
        /// <param name="ofObjectToReturn"></param>
        /// <returns></returns>
        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn) {
            //The uri is to a file
            string uri = absoluteUri.ToString();
            uri = uri.Substring(uri.LastIndexOf("/") + 1);
            Uri newUri;
            string newPath = _localSchemaDirectory.FullName;
            if (!newPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                newPath += Path.DirectorySeparatorChar;
            newPath += uri;
            if (File.Exists(newPath)) {
                try {
                    newUri = new Uri(newPath); 
                }
                catch(Exception) {
                    newUri = absoluteUri;
                }
            } 
            else {
                newUri = absoluteUri;
            }
            return base.GetEntity(newUri, role, ofObjectToReturn);
        }
    }
}