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
using System.Text;
using System.IO;

namespace dk.gov.oiosi.exception.Keyword {
    /// <summary>
    /// Get keywords from fileinfo.
    /// </summary>
    public class KeywordsFromFileInfo {

        /// <summary>
        /// Returns a set of keywords got from a FileInfo object
        /// </summary>
        /// <param name="fileInfo">The FileInfo object</param>
        /// <returns>Returns a set of keywords got from a FileInfo object</returns>
        public static Dictionary<string, string> GetKeywords(FileInfo fileInfo) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, fileInfo);
            return keywords;
        }

        /// <summary>
        /// Inserts a set of keywords got from a FileInfo object into the 'keywords' collection
        /// </summary>
        /// <param name="keywords">The keyword collection</param>
        /// <param name="fileInfo">The FileInfo object</param>
        public static void GetKeywords(Dictionary<string, string> keywords, FileInfo fileInfo) {
            GetKeywords(keywords, "", fileInfo);
        }

        /// <summary>
        /// Returns a set of keywords got from a FileInfo object, adding a prefix to to the keyword key
        /// </summary>
        /// <param name="prefix">The prefix to add to the keyword key</param>
        /// <param name="fileInfo">The FileInfo object</param>
        /// <returns>Returns a set of keywords got from a FileInfo object</returns>
        public static Dictionary<string, string> GetKeywords(string prefix, FileInfo fileInfo) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, prefix, fileInfo);
            return keywords;
        }

        /// <summary>
        /// Inserts a set of keywords got from a FileInfo object into the 'keywords' collection, adding
        /// the supplied prefix to the keyword key name
        /// </summary>
        /// <param name="keywords">The keyword collection</param>
        /// <param name="prefix">The prefix to add to the keyword key</param>
        /// <param name="fileInfo">The FileInfo object</param>
        public static void GetKeywords(Dictionary<string, string> keywords, string prefix, FileInfo fileInfo) {
            keywords.Add(prefix + "filename", fileInfo.Name);
            keywords.Add(prefix + "filefullname", fileInfo.FullName);
        }
    }
}
